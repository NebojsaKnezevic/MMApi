using MajaMayo.API.Repository;
using Microsoft.AspNetCore.Mvc;

namespace MajaMayo.API.Controllers
{
    [ApiController]
    [Route("Query")]
    public class AssesmentController : ControllerBase
    {
        private readonly IQueryRepository _queryRepository;

        public AssesmentController(IQueryRepository queryRepository)
        {
            _queryRepository = queryRepository;
        }

        [HttpGet("GetQuestions")]
        public async Task<IActionResult> GetQuestions()
        { 
            return Ok(await _queryRepository.GetQuestions());
        }

        [HttpGet("GetQuestionsQuery")]
        public async Task<IActionResult> GetQuestionsQuery([FromQuery]string email)
        {
            return Ok(await _queryRepository.GetQuestionsQuery(email));
        }
    }
}
