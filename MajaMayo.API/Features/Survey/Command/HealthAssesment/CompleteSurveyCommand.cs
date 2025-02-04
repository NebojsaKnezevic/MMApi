using MediatR;

namespace MajaMayo.API.Features.Survey.Command.HealthAssesment
{
    public class CompleteSurveyCommand : IRequest<bool>
    {
        public int haid { get; set; }
    }
}
