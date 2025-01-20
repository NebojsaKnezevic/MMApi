using MediatR;

namespace MajaMayo.API.Models.Survey.Query.HealthAssessmentScore
{
    public class GetHealthAssessmentScoresQuery :  IRequest<ICollection<HealthAssessmentScoresResponse>>
    {
        public int HealthAssessmentId { get; set; }
    }
}
