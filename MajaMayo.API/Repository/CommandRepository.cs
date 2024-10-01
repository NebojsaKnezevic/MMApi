
using ACT.Security.Service;
using Dapper;
using MailKit.Net.Smtp;
using MailKit.Security;
using MajaMayo.API.DTOs;
using MajaMayo.API.Models;
using MajaMayo.API.Models.Survey.Command.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MimeKit;
using System.Data;
using System.IdentityModel.Tokens.Jwt;

using System.Security.Claims;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MajaMayo.API.Repository
{
    public class CommandRepository : ICommandRepository
    {
        private readonly IDbConnection _connection;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContext;
        //private readonly UserManager<UserResponse> _userManager;
        private readonly string SecretTokenName = "ACTTokenAuth";
        //private readonly UserManager<UserResponse> _userManager;

        public CommandRepository(
            IDbConnection connection, 
            IConfiguration configuration, 
            IHttpContextAccessor httpContext)
        {
            _connection = connection;
            _configuration = configuration;
            _httpContext = httpContext;
            //_userManager = userManager;
        }
        public async Task<bool> RegisterUser(string Email, string Pwd)
        {
            Pwd = Security.Encrypt(_configuration.GetSection("SecurityKey").Value!, Pwd);
            var pars = new { Email = Email, Pwd = Pwd };
            var result = await _connection.ExecuteScalarAsync<bool>("spRegisterUser", pars, commandType: CommandType.StoredProcedure);
            if (!result) throw new Exception("Email already exists!");

            // Encrypt Email
            var emailVerification = Security.Encrypt(_configuration.GetSection("SecurityKey").Value!, Email);

            // URL Encode the encrypted email
            //var encodedEmail = System.Net.WebUtility.UrlEncode(emailVerification);

            // Create verification link
            string verificationLink = $"http://localhost:5141/Survey/Command/VerifyEmail?data={emailVerification}";

           

            // Create HTML body
            string emailBody = $@"
            <html>
            <body>
                <h2>Email Verification</h2>
                <p>Thank you for registering with us. Please click the link below to verify your email address:</p>
                <p><a href='{verificationLink}'>Verify Email</a></p>
                <p>If you did not register, please ignore this email.</p>
            </body>
            </html>";

            // Send the email
            SendEmail(new EmailDTO
            {
                To = Email,
                Subject = "Email Verification",
                Body = emailBody
            });


            return result;
        }

        public void SendEmail(EmailDTO email)
        {
            var mail = new MimeMessage();
            mail.From.Add(new MailboxAddress(_configuration["EmailSettings:FromName"], _configuration["EmailSettings:FromEmail"]));
            mail.To.Add(new MailboxAddress("", email.To));
            mail.Subject = email.Subject;

            var builder = new BodyBuilder { HtmlBody = email.Body };
            mail.Body = builder.ToMessageBody();

            using (var smtp = new SmtpClient())
            {
                smtp.Connect(_configuration["EmailSettings:SmtpServer"], 587, SecureSocketOptions.StartTls);
                smtp.Authenticate(_configuration["EmailSettings:Username"], _configuration["EmailSettings:Password"]);
                smtp.Send(mail);
                smtp.Disconnect(true);
            }
        }

        public async Task<UserResponse> LoginUser(string Email, string Pwd)
        {
            try
            {
                Pwd = Security.Encrypt(_configuration.GetSection("SecurityKey").Value!, Pwd);
                var pars = new { Email = Email, Pwd = Pwd };
                var result = await _connection.QuerySingleAsync<UserResponse>("spLoginUser", pars, commandType: CommandType.StoredProcedure);
                var tokenString = GenerateJWTToken(result);

                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTime.UtcNow.AddHours(12)
                };

                _httpContext.HttpContext.Response.Cookies.Append(SecretTokenName, tokenString, cookieOptions);

                return result;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Email not found"))
                {
                    throw new Exception("The provided email address is not registered.");
                }
                else if (ex.Message.Contains("Incorrect password"))
                {
                    throw new Exception("The provided password is incorrect.");
                }
                else
                {
                    throw;
                }
            }
            
        }

        private string GenerateJWTToken(UserResponse user)
        {
            var claims = new List<Claim> {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.FirstName),
            new Claim(ClaimTypes.Surname, user.LastName),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Gender, user.Gender),
            new Claim(ClaimTypes.GivenName, user.FirstName + user.LastName),
            new Claim(ClaimTypes.Role, "Admin")
            };

            var jwtToken = new JwtSecurityToken(
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddHours(12),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(
                       Encoding.UTF8.GetBytes(_configuration.GetSection("SecurityKey").Value!)
                        ),
                    SecurityAlgorithms.HmacSha256Signature)
                );
            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }

      

        public async Task<UserResponse> CookieLoginUser()
        {
            var jwtToken = _httpContext.HttpContext.Request.Cookies[SecretTokenName];
            UserResponse userDataFromCookie;

            if (string.IsNullOrEmpty(jwtToken))
            {
                throw new UnauthorizedAccessException("JWT token is missing.");
            }

            DynamicParameters pars = new DynamicParameters();

            try
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtSecurityToken = handler.ReadJwtToken(jwtToken);

                //var expTime = jwtSecurityToken.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Expiration)?.Value;
                var id = jwtSecurityToken.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value;
                var firstName = jwtSecurityToken.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Name)?.Value;
                var lastName = jwtSecurityToken.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Surname)?.Value;
                var email = jwtSecurityToken.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Email)?.Value;
                var gender = jwtSecurityToken.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Gender)?.Value;
                var givenName = jwtSecurityToken.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.GivenName)?.Value;

                if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(email))
                {
                    throw new UnauthorizedAccessException("Invalid JWT token.");
                }
                userDataFromCookie = new UserResponse
                {
                    Id = int.Parse(id),
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    Gender = gender
                };
                pars.Add("@Id", id, DbType.Int32, ParameterDirection.Input);
                pars.Add("@FirstName", firstName, DbType.String, ParameterDirection.Input);
                pars.Add("@LastName", lastName, DbType.String, ParameterDirection.Input);
                pars.Add("@Email", email, DbType.String, ParameterDirection.Input);
                pars.Add("@Gender", gender, DbType.String, ParameterDirection.Input);

            }
            catch (Exception ex)
            {
                throw new UnauthorizedAccessException("Error processing JWT token.", ex);
            }

            //Send data to db to verify

            var result = await _connection.ExecuteScalarAsync<bool>("spCookieLoginVerification", pars, commandType: CommandType.StoredProcedure);

            if (result == true) return userDataFromCookie;
            else throw new UnauthorizedAccessException("Something went wrong with cookie verification, please login again.");

            //var userDataFromDB = await _connection.


        }

        public async Task<HealthAssesmentResponse> CreateNewHealthAssesment(int userId)
        {
            DynamicParameters pars = new DynamicParameters();
            pars.Add("@UserId", userId, DbType.Int32, ParameterDirection.Input);
            var result = await _connection.QuerySingleAsync<HealthAssesmentResponse>("spCreateNewAssesment", pars, commandType:CommandType.StoredProcedure);
            return result;
        }

        public async Task<bool> SubmitAnswers(SubmitAnswersModel model)
        {
            var TVD = new DataTable();

            TVD.Columns.Add("AnswerId", typeof(int));
            TVD.Columns.Add("IsSelected", typeof(bool));

            foreach (var answer in model.Answers)
            {
                TVD.Rows.Add(answer.AnswerId, answer.IsSelected);
            }

            var pars = new DynamicParameters();
            pars.Add("@QuestionId", model.QuestionId, DbType.Int32);
            pars.Add("@HealthAssesmentId", model.HealthAssesmentId, DbType.Int32);
            pars.Add("@Answers", TVD.AsTableValuedParameter("dbo.AnswerSelectionType"));

            var response = await _connection.ExecuteScalarAsync<bool>("dbo.spSubmitAnswers", pars, commandType: CommandType.StoredProcedure);

            return response;
        }

        public Task<bool> LogoutUser()
        {
            var cookieOptions = new CookieOptions() 
            { 
                Expires = DateTime.UtcNow.AddDays(-1)
                ,HttpOnly = true
                ,Secure = true
                ,SameSite = SameSiteMode.Strict
            };

            _httpContext.HttpContext.Response.Cookies.Append(SecretTokenName, "", cookieOptions);
            return Task.FromResult(true);
        }

        public async Task<bool> EmailVerification(string encryptedData)
        {
            //var decoded = System.Net.WebUtility.UrlDecode(encryptedData);
            var decryptEmail = Security.Decrypt(_configuration.GetSection("SecurityKey").Value!, encryptedData);
            var pars = new DynamicParameters();
            pars.Add("@Email", decryptEmail, DbType.String);
            var isVerified = await _connection.ExecuteScalarAsync<bool>("dbo.spVerifyEmail", pars, commandType: CommandType.StoredProcedure);
            return isVerified;
        }

        public async Task<bool> UpdateUserData(UserResponse user)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", user.Id);
            parameters.Add("@FirstName", user.FirstName);
            parameters.Add("@LastName", user.LastName);
            //parameters.Add("@Email", user.Email);
            parameters.Add("@PhoneNumber", user.PhoneNumber);
            parameters.Add("@DateOfBirth", user.DateOfBirth);
            parameters.Add("@Gender", user.Gender);
            parameters.Add("@PolicyNumber", user.PolicyNumber);
            parameters.Add("@JMBG", user.JMBG);
            parameters.Add("@PassportNumber", user.PassportNumber);

            var result = await _connection.ExecuteAsync("dbo.spFormUser", parameters, commandType: CommandType.StoredProcedure);

            return result > 0; 
        }

        public async Task<bool> InsertUpdateFamilyHistory(FamilyHistoryModel model)
        {
            var pars = new DynamicParameters();

            pars.Add("@Id", model.Id, DbType.Int32);
            pars.Add("@Cardiovascular", model.Cardiovascular, DbType.Boolean);
            pars.Add("@Diabetes", model.Diabetes, DbType.Boolean);
            pars.Add("@Cancer", model.Cancer, DbType.Boolean);
            pars.Add("@CancerType", model.CancerType, DbType.String);
            pars.Add("@HighBloodPressure", model.HighBloodPressure, DbType.Boolean);
            pars.Add("@Other", model.Other, DbType.String);
            pars.Add("@OtherConditions", model.OtherConditions, DbType.String);
            pars.Add("@MentalIllness", model.MentalIllness, DbType.Boolean);

            var result = await _connection.ExecuteAsync("dbo.spInsertUpdateFamilyHistory", pars, commandType: CommandType.StoredProcedure);

            return result > 0;
        }
    }
}
