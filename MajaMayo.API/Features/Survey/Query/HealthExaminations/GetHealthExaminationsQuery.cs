using MajaMayo.API.Models;
using MediatR;

namespace MajaMayo.API.Features.Survey.Query.HealthExaminations
{
    public class GetHealthExaminationsQuery : IRequest<ICollection<HealthExaminationResponse>>
    {

    }
}
