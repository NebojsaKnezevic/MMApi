using MajaMayo.API.Models;
using MediatR;

namespace MajaMayo.API.Features.Survey.Query.HealthAssesment
{
    public class GetHealthAssesmentQuery : IRequest<ICollection<HealthAssesmentResponse>>
    {
        public int UserId { get; set; }
        public int HealthAssesmentId { get; set; }
    }
}
