using MajaMayo.API.Models;
using MediatR;

namespace MajaMayo.API.Features.Survey.Query.Answer
{
    public class GetAnswersQuery : IRequest<ICollection<AnswerResponse>>
    {
        public int UserId { get; set; }
        public int HealthAssesmentId { get; set; }
    }
}
