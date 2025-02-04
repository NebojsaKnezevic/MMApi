using MediatR;

namespace MajaMayo.API.Features.DeltaGenerali.Command
{
    public class HandleDGRequestsCommand : IRequest<bool>
    {
        public int HealthAssessmentId { get; set; }

        public HandleDGRequestsCommand(int healthAssessmentId)
        {
            HealthAssessmentId = healthAssessmentId;
        }
    }
}
