using MediatR;

namespace MajaMayo.API.Models.Survey.Command.HealthAssesment
{
    public class CompleteSurveyCommand : IRequest<bool>
    {
        public int haid { get; set; }
    }
}
