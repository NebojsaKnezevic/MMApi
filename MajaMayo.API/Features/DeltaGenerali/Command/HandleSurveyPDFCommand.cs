using MediatR;

namespace MajaMayo.API.Features.DeltaGenerali.Command
{
    public class HandleSurveyPDFCommand : IRequest<byte[]>
    {
        public int HealthAssessmentId { get; set; }
        public byte[] Html { get; set; }
        public HandleSurveyPDFCommand(int HealthAssessmentId, byte[] Html)
        {
            this.HealthAssessmentId = HealthAssessmentId;
            this.Html = Html;
        }
    }
}
