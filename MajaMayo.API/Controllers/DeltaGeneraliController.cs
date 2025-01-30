using MajaMayo.API.Models;
using MajaMayo.API.Models.DeltaGenerali.Command;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace MajaMayo.API.Controllers
{
    [ApiController]
    [Route("DeltaGenerali")]
    public class DeltaGeneraliController : ApiController
    {
        private readonly ISender _sender;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DeltaGeneraliController(ISender sender, IHttpContextAccessor httpContextAccessor) : base(sender)
        {
            _sender = sender;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost("RegisterDGUsers")]
        public async Task<IActionResult> RegisterDGUsers([FromBody] List<DGApprovedUserResponse> dGApproveds, [FromHeader] string API_KEY)
        {
            var res = await _sender.Send(new InsertDeltaGeneraliApprovedUsersCommand(dGApproveds));
            return Ok(res);

        }
    }
}
