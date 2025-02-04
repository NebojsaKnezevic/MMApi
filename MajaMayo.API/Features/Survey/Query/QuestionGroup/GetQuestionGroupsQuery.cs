using MajaMayo.API.Models;
using MediatR;

namespace MajaMayo.API.Features.Survey.Query.QuestionGroup
{
    public class GetQuestionGroupsQuery : IRequest<ICollection<QuestionGroupResponse>>
    {
    }
}
