
using MajaMayo.API.Models.Survey.Query.GetQuestions;
using MajaMayo.API.Repository;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MajaMayo.API.Models.Survey.Query.GetAnswers;
using MajaMayo.API.Models.Survey.Query.GetQuestionGroups;
using System.ComponentModel.DataAnnotations;
using MajaMayo.API.Models.Survey.Command.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MajaMayo.API.Mapper;
using MajaMayo.API.DTOs;
using MajaMayo.API.Models.Survey.Command.HealthAssesment;
using MajaMayo.API.Models.Survey.Command.Answer;
using MajaMayo.API.Models.Survey.Query.HealthAssesment;
using Microsoft.AspNetCore.Identity;
using MajaMayo.API.Models.Survey.Command.Email;
using Microsoft.VisualBasic;
using ACT.Security.Service;
using MajaMayo.API.Models;
using MajaMayo.API.Models.Survey.Command.FamilyHistory;
//using System.Web.Http;
using MajaMayo.API.Models.Survey.Query.FamilyHistory;

namespace MajaMayo.API.Controllers
{
    [ApiController]
    [Route("Survey")]
    public class SurveyController : ApiController
    {
        private readonly ISender _sender;
        private readonly IConfiguration _configuration;


        public SurveyController(ISender sender, IConfiguration configuration) : base(sender)
        {
            _sender = sender;
            _configuration = configuration;

        }

        //[Authorize(Roles = "Admin")]
        [HttpGet("Query/GetQuestions")]
        public async Task<IActionResult> GetQuestions()
        {
            var res = await _sender.Send(new GetQuestionsQuery());
            return Ok(res);
        }

        //[HttpGet("GetQuestionsQuery")]
        //public async Task<IActionResult> GetQuestionsQuery([FromQuery]string email)
        //{
        //    var res = await _sender.Send(new GetQuestionsByEmailQuery(email));
        //    return Ok(res);
        //}
        //[Authorize(Roles = "Admin")]
        [HttpGet("Query/GetQiestionGroups")]
        public async Task<IActionResult> GetQuestionGroups() 
        {
            var result = await _sender.Send(new GetQuestionGroupsQuery());
            return Ok(result);
        }
        //[Authorize(Roles = "Admin")]
        [HttpGet("Query/GetAnswers")]
        public async Task<IActionResult> GetAnswers([FromQuery]GetAnswersQuery query) 
        {
            var result = await _sender.Send(query);
            return Ok(result);
        }

        [HttpGet("Query/GetHealthAssesment")]
        public async Task<IActionResult> GetHealthAssesment([FromQuery] int userId, [FromQuery] int healthAssesmentId)
        {
            var request = new GetHealthAssesmentQuery()
            {
                UserId = userId,
                HealthAssesmentId = healthAssesmentId
            };
            var result = await _sender.Send(request);
            return Ok(result);
        }

        [HttpPost("Command/RegisterUser")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserCommand registerUserCommand) 
        {
            var result = await _sender.Send(registerUserCommand);
            return Ok(result);
        }

        [HttpPost("Command/LoginUser")]
        public async Task<IActionResult> LoginUser([FromBody] LoginUserCommand loginUserQuery)
        {
            var result = await _sender.Send(loginUserQuery);
            return Ok(result);
        }

        [HttpPost("Command/CookieLoginUser")]
        public async Task<IActionResult> CookieLoginUser() 
        {
            var result = await _sender.Send(new CookieLoginUserCommand());
            return Ok(result);
        }

         [HttpPost("Command/CreateNewHealthAssesment/{userId:int}")]
        public async Task<IActionResult> CreateNewHealthAssesment([FromRoute]int userId)
        {
            var request = new CreateNewHealthAssesmentCommand();
            request.UserId = userId;
            var result = await _sender.Send(request);
            return Ok(result);
        }

        [HttpPost("Command/SubmitAnswers")]
        public async Task<IActionResult> SubmitAnswers([FromBody] SubmitAnswersCommand submit)
        { 
            var request = await _sender.Send(submit);
            return Ok(request);
        }

        [HttpPost("Command/LogoutUser")]
        public async Task<IActionResult> LogoutUser()
        {
            var request = await _sender.Send(new LogoutUserCommand());
            return Ok(request); 
        }

        [HttpPost("Command/SendEmail")]
        public IActionResult SendEmail([FromBody] SendEmailCommand sendEmailCommand)
        {
            try
            {
                _sender.Send(sendEmailCommand);
            }
            catch (Exception e)
            {
                Console.WriteLine( e);
            }
           
            return Ok(new{ Msg = "OK"});
        }

        [HttpGet("Command/VerifyEmail")]
        public async Task<IActionResult> VerifyEmail([FromQuery] string data)
        {
            /*var testX = *//*System.Net.WebUtility.UrlDecode(data.ToString());*/
            //var testX = Security.Encrypt(_configuration.GetSection("SecurityKey").Value!, email);
            var result = await _sender.Send(new EmailVerificationCommand() { Email = data });
            return Ok("<h1>Congrats! You registered successfully!</h1>");
        }

        [HttpPut("Command/UpdateUserData")]
        public async Task<IActionResult> UpdateUserData([FromBody] UpdateUserCommand user) 
        {
            var result = await _sender.Send(user);
            return Ok(result);
        }

        [HttpPut("Command/InsertUpdateFamilyHistory")]
        public async Task<IActionResult> InsertUpdateFamilyHistory([FromBody] InsertUpdateFamilyHistoryCommand insertUpdate) 
        {
            var result = await _sender.Send(insertUpdate);
            return Ok(result);
        }

        [HttpGet("Query/GetFamilyHistory/{id:int}")]
        public async Task<IActionResult> GetFamilyHistory( int id)
        {
            var par = new GetFamilyHistoryQuery();
            par.Id = id;
            var result = await _sender.Send(par);
            return Ok(result);
        }

    }
}
