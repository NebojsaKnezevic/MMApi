using MajaMayo.API.Models;
using MediatR;

namespace MajaMayo.API.Features.Survey.Query.HealthAssessmentScore
{
    public class GetHealthAssessmentScoresQuery : IRequest<ICollection<HealthAssessmentScoresResponse>>
    {
        public int HealthAssessmentId { get; set; }
    }
}
