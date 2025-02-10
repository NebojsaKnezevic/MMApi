using MajaMayo.API.Models;
using MajaMayo.API.Features.DeltaGenerali.Command;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Configuration;

namespace MajaMayo.API.Controllers
{
    [ApiController]
    [Route("DeltaGenerali")]
    public class DeltaGeneraliController : ApiController
    {
        private readonly ISender _sender;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;

        public DeltaGeneraliController(ISender sender, IHttpContextAccessor httpContextAccessor, IConfiguration configuration) : base(sender)
        {
            _sender = sender;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }

        [HttpPost("RegisterDGUsers")]
        public async Task<IActionResult> RegisterDGUsers([FromBody] List<DGApprovedUserResponse> dGApproveds, [FromHeader]string ApiKey)
        {

            var res = await _sender.Send(new InsertDeltaGeneraliApprovedUsersCommand(dGApproveds));
            return Ok(res);

        }

        [HttpPost("HandleDGRequests/{id:int}")]
        public async Task<IActionResult> HandleDGRequests(int id)
        {
            var res = await _sender.Send(new HandleDGRequestsCommand(id));
            return Ok(res);
        }

        [HttpPost("HandleSurveyPDF/{id:int}")]
        public async Task<IActionResult> HandleSurveyPDF([FromBody] byte[] html, [FromRoute] int id)
        {
            var result = await _sender.Send(new HandleSurveyPDFCommand(id, html));
            return File(result, "application/pdf", "report.pdf");
        }

        [HttpGet("Test")]
        public IActionResult Test()
        {
            return Ok("TEST");
        }
    }
}
