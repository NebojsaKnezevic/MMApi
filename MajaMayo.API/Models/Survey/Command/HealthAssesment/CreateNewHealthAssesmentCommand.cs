using MediatR;

namespace MajaMayo.API.Models.Survey.Command.HealthAssesment
{
    public class CreateNewHealthAssesmentCommand : IRequest<HealthAssesmentResponse>
    {
        public int UserId { get; set; }
    }
}
