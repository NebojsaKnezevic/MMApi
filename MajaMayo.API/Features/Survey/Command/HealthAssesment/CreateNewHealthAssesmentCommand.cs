using MajaMayo.API.Models;
using MediatR;

namespace MajaMayo.API.Features.Survey.Command.HealthAssesment
{
    public class CreateNewHealthAssesmentCommand : IRequest<HealthAssesmentResponse>
    {
        public int UserId { get; set; }
    }
}
