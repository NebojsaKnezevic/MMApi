using MediatR;

namespace MajaMayo.API.Models.Survey.Query.GetAnswers
{
    public class GetAnswersQuery : IRequest<ICollection<AnswerResponse>>
    {
        public int UserId { get; set; }
        public int HealthAssesmentId { get; set; }
    }
}
