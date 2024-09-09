using MediatR;

namespace MajaMayo.API.Models.Survey.Query.GetQuestionGroups
{
    public class GetQuestionGroupsQuery : IRequest<ICollection<QuestionGroupResponse>>
    {
    }
}
