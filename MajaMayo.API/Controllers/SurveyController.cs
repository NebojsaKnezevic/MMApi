﻿
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MajaMayo.API.Mapper;
using MajaMayo.API.Features.Survey.Command.HealthAssesment;
using MajaMayo.API.Features.Survey.Query.HealthAssesment;
using MajaMayo.API.Models;
using MajaMayo.API.Features.Survey.Query.HealthExaminations;
using MajaMayo.API.Features.Survey.Query.HealthAssessmentScore;
using MajaMayo.API.Features.Survey.Command.Answer;
using MajaMayo.API.Features.Survey.Command.Email;
using MajaMayo.API.Features.Survey.Command.User;
using MajaMayo.API.Features.Survey.Query.Answer;
using MajaMayo.API.Features.Survey.Query.Question;
using MajaMayo.API.Features.Survey.Query.QuestionGroup;
using Microsoft.AspNetCore.RateLimiting;

namespace MajaMayo.API.Controllers
{
    [ApiController]
    [Route("Survey")]
    public class SurveyController : ApiController
    {
        private readonly ISender _sender;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SurveyController(ISender sender, IHttpContextAccessor httpContextAccessor) : base(sender)
        {
            _sender = sender;
            _httpContextAccessor = httpContextAccessor;
        }

        //[Authorize(Roles = "admin,user")]
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
        //[Authorize(Roles = "admin,user")]
        [HttpGet("Query/GetQiestionGroups")]
        public async Task<IActionResult> GetQuestionGroups() 
        {
            var result = await _sender.Send(new GetQuestionGroupsQuery());
            return Ok(result);
        }
        //[Authorize(Roles = "admin,user")]
        [HttpGet("Query/GetAnswers")]
        public async Task<IActionResult> GetAnswers([FromQuery]GetAnswersQuery query) 
        {
            var result = await _sender.Send(query);
            return Ok(result);
        }
        //[Authorize(Roles = "admin,user")]
        [HttpGet("Query/GetHealthAssesment")]
        public async Task<IActionResult> GetHealthAssesment([FromQuery] int userId, [FromQuery] int healthAssesmentId)
        {
            var x = _httpContextAccessor.HttpContext.Response.Cookies;
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
        [EnableRateLimiting("strict")]
        public async Task<IActionResult> LoginUser([FromBody] LoginUserCommand loginUserQuery)
        {
            var result = await _sender.Send(loginUserQuery);
            return Ok(result);
        }

        [HttpPost("Command/CookieLoginUser")]
        public async Task<IActionResult> CookieLoginUser() 
        {
            var result = await _sender.Send(new CookieLoginUserCommand());
            return Ok(result.ToDTO());
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
        //[Authorize(Roles = "admin,user")]
        //[HttpPut("Command/InsertUpdateFamilyHistory")]
        //public async Task<IActionResult> InsertUpdateFamilyHistory([FromBody] InsertUpdateFamilyHistoryCommand insertUpdate) 
        //{
        //    var result = await _sender.Send(insertUpdate);
        //    return Ok(result);
        //}
        //[Authorize(Roles = "admin,user")]
        //[HttpGet("Query/GetFamilyHistory/{id:int}")]
        //public async Task<IActionResult> GetFamilyHistory( int id)
        //{
        //    var par = new GetFamilyHistoryQuery();
        //    par.Id = id;
        //    var result = await _sender.Send(par);
        //    return Ok(result);
        //}

        [HttpPut("Command/CompleteSurvey/{id:int}")]
        public async Task<IActionResult> CompleteSurvey(int id) 
        {
            var result = await _sender.Send(new CompleteSurveyCommand() { haid = id });
            //if (result == true)
            //{
            //    // Redirect to a different controller action
            //    var dgResult = await _sender.Send(new HandleDGRequestsCommand(id));
            //    return Ok(dgResult);
            //}
            return Ok(result);
        }
        [HttpGet("Query/GetHealthExaminations")]
        public async Task<IActionResult> GetHealthExaminations()
        {
            var result = await _sender.Send(new GetHealthExaminationsQuery());
            return Ok(result.ToList());
        }


        [HttpPost("Command/GoogleLogin")]
        public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginResponse googleLoginResponse) 
        {
            var result = await _sender.Send(new GoogleLoginCommand() { ClientId = googleLoginResponse.ClientId, Credential = googleLoginResponse.Credential});
            return Ok(result);
        }

        [HttpGet("Query/GetHealthAssessmentScores/{healthAssessmentId:int}")]
        public async Task<IActionResult> GetHealthAssessmentScores(int healthAssessmentId) 
        {
            var result = await _sender.Send(new GetHealthAssessmentScoresQuery() { HealthAssessmentId = healthAssessmentId });
            return Ok(result);  

        }
    }
}
