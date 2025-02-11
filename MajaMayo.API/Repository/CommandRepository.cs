
using ACT.Security.Service;
using Dapper;
using Google.Apis.Auth;
using MailKit.Net.Smtp;
using MailKit.Security;
using MajaMayo.API.ConfigModel;
using MajaMayo.API.DTOs;
using MajaMayo.API.Helpers;
using MajaMayo.API.Models;
using MajaMayo.API.Features.Survey.Command.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MimeKit;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;

using System.Security.Claims;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.ComponentModel;
using MajaMayo.API.Constants;
using MajaMayo.API.Errors;
using System.Data.Common;
using static Google.Apis.Auth.GoogleJsonWebSignature;

namespace MajaMayo.API.Repository
{
    public class CommandRepository : ICommandRepository
    {
        private readonly IDbConnection _connection;
       
        private readonly IHttpContextAccessor _httpContext;
        //private readonly UserManager<UserResponse> _userManager;
        
        //private readonly UserManager<UserResponse> _userManager;
        private readonly SecuritySettings _securitySettings;
        private readonly EmailSettings _emailSettings;

        public CommandRepository(
            IDbConnection connection,
            IOptions<SecuritySettings> securitySettings,
            IOptions<EmailSettings> emailSettings,
            IHttpContextAccessor httpContext)
        {
            _connection = connection;
            _emailSettings = emailSettings.Value;
            _securitySettings = securitySettings.Value;

            _httpContext = httpContext;
            //_userManager = userManager;
        }
        public async Task<bool> RegisterUser(string Email, string Pwd)
        {
            byte[] passwordHash;
            byte[] passwordSalt;
            PasswordHash.CreatePasswordHash(Pwd, out passwordHash, out passwordSalt);
            var pars = new { Email = Email, PwdHash = passwordHash, PwdSalt = passwordSalt };
            var result = await _connection.ExecuteScalarAsync<bool>("spRegisterUser", pars, commandType: CommandType.StoredProcedure);

            return result;
        }

        public async Task<bool> IsAllowed(string email)
        {
            var parsdg = new DynamicParameters();
            parsdg.Add("@Email", email);
            var dgApprovedUser = await _connection.QueryAsync<DGApprovedUserResponse>("spGetDGApprovedUserByEmail", parsdg, commandType: CommandType.StoredProcedure);
      
            return dgApprovedUser.Count() == 0;

        }

        public void SendEmail(EmailDTO email)
        {
            var mail = new MimeMessage();
            mail.From.Add(new MailboxAddress(_emailSettings.FromName, _emailSettings.FromEmail));
            mail.To.Add(new MailboxAddress("", email.To));
            mail.Subject = email.Subject;

            var builder = new BodyBuilder { HtmlBody = email.Body };
            mail.Body = builder.ToMessageBody();

            using (var smtp = new SmtpClient())
            {
                smtp.Connect(_emailSettings.SmtpServer, _emailSettings.Port, SecureSocketOptions.StartTls);
                smtp.Authenticate(_emailSettings.Username, _emailSettings.Password);
                smtp.Send(mail);
                smtp.Disconnect(true);
            }
        }

        public async Task<UserResponse> LoginUser(string Email, string Pwd)
        {
            var pars = new { Email = Email };
            var result = await _connection.QuerySingleAsync<UserResponse>("spLoginUser", pars, commandType: CommandType.StoredProcedure);

            return result;
        }
            
        

        public async Task<UserResponse> GoogleLogin(Payload payload)
        {
            DynamicParameters pars = new DynamicParameters();
            pars.Add("@Email", payload.Email, DbType.String);
            pars.Add("@FirstName", payload.GivenName, DbType.String);
            pars.Add("@LastName", payload.FamilyName, DbType.String);
            pars.Add("@VerifiedEmail", payload.EmailVerified, DbType.Boolean);

            var result = await _connection.QueryFirstOrDefaultAsync<UserResponse>("spGoogleLoginUser", pars, commandType: CommandType.StoredProcedure);

            return result;
        }

      

      

        public async Task<UserResponse> CookieLoginUser(int id, string email)
        {
            DynamicParameters pars = new DynamicParameters();
            pars.Add("@Id", id, DbType.Int32, ParameterDirection.Input);
            pars.Add("@Email", email, DbType.String, ParameterDirection.Input);

            var result = await _connection.QuerySingleAsync<UserResponse>("spCookieLoginVerification", pars, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<HealthAssesmentResponse> CreateNewHealthAssesment(int userId)
        {
            DynamicParameters pars = new DynamicParameters();
            pars.Add("@UserId", userId, DbType.Int32, ParameterDirection.Input);
            var result = await _connection.QuerySingleAsync<HealthAssesmentResponse>("spCreateNewAssessment", pars, commandType:CommandType.StoredProcedure);
            return result;
        }

        public async Task<bool> SubmitAnswers(SubmitAnswersModel model)
        {
            var TVD = new DataTable();

            TVD.Columns.Add("AnswerId", typeof(int));
            TVD.Columns.Add("IsSelected", typeof(bool));
            TVD.Columns.Add("Text", typeof(string));

            foreach (var answer in model.Answers)
            {
                TVD.Rows.Add(answer.AnswerId, answer.IsSelected, answer.Text);
            }

            var pars = new DynamicParameters();
            pars.Add("@QuestionId", model.QuestionId, DbType.Int32);
            pars.Add("@HealthAssesmentId", model.HealthAssesmentId, DbType.Int32);
            pars.Add("@Answers", TVD.AsTableValuedParameter("dbo.AnswerSelectionType"));
            //pars.Add("@AdditinalComment", model.AdditionalComment, DbType.String);

            var response = await _connection.ExecuteScalarAsync<bool>("dbo.spSubmitAnswers", pars, commandType: CommandType.StoredProcedure);

            return response;
        }

        public Task<bool> LogoutUser()
        {
            return Task.FromResult(true);
        }

        public async Task<bool> EmailVerification(string encryptedData)
        {
            //var decoded = System.Net.WebUtility.UrlDecode(encryptedData);
            var decryptEmail = Security.Decrypt(_securitySettings.SecurityKey, encryptedData);
            var pars = new DynamicParameters();
            pars.Add("@Email", decryptEmail, DbType.String);
            var isVerified = await _connection.ExecuteScalarAsync<bool>("dbo.spVerifyEmail", pars, commandType: CommandType.StoredProcedure);
            return isVerified;
        }

        public async Task<bool> UpdateUserData(UserResponse user)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", user.Id, DbType.Int32);
            parameters.Add("@FirstName", user.FirstName, DbType.String);
            parameters.Add("@LastName", user.LastName, DbType.String);
            //parameters.Add("@Email", user.Email);
            //parameters.Add("@PhoneNumber", user.PhoneNumber);
            parameters.Add("@DateOfBirth", user.DateOfBirth, DbType.String);
            parameters.Add("@Gender", user.Gender, DbType.String);
            //parameters.Add("@PolicyNumber", user.PolicyNumber);
            //parameters.Add("@JMBG", user.JMBG);
            //parameters.Add("@PassportNumber", user.PassportNumber);
            parameters.Add("@Height", user.Height, DbType.Int32);
            parameters.Add("@Weight", user.Weight, DbType.Int32);
            parameters.Add("@HealthAssesmentId", user.HealthAssessmentId, DbType.Int32);

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

        public async Task<bool> CompleteSurvey(int healthHssesmentId)
        {
            var pars = new DynamicParameters();

            pars.Add("@HealthAssesmentId", healthHssesmentId, DbType.Int32);

            var result = await _connection.ExecuteAsync("dbo.spCompleteSurvey", pars, commandType: CommandType.StoredProcedure);
            return result > 0;
        }

        public async Task<bool> LogError(LogEntry logEntry)
        {
            var pars = new DynamicParameters();
            pars.Add("@LogLevel", logEntry.LogLevel);
            pars.Add("@Message", logEntry.Message, DbType.String);
            pars.Add("@Exception", logEntry.Exception);
            pars.Add("@EventId", logEntry.EventId);
            pars.Add("@Source", logEntry.Source);
            pars.Add("@RequestPath", logEntry.RequestPath);
            pars.Add("@UserId", logEntry.UserId);

            var result = await _connection.ExecuteAsync("dbo.spInsertLog", pars, commandType: CommandType.StoredProcedure);
            return result > 0; 
        }
        
    }
}
