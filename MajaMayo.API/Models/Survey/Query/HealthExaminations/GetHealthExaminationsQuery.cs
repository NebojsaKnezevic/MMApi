using MediatR;

namespace MajaMayo.API.Models.Survey.Query.HealthExaminations
{
    public class GetHealthExaminationsQuery : IRequest<ICollection<HealthExaminationResponse>>
    {
        
    }
}
