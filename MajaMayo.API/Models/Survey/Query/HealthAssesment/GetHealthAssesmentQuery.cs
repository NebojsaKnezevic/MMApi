using MediatR;

namespace MajaMayo.API.Models.Survey.Query.HealthAssesment
{
    public class GetHealthAssesmentQuery : IRequest<HealthAssesmentResponse>
    {
        public int UserId { get; set; }
        public int HealthAssesmentId { get; set; }
    }
}
