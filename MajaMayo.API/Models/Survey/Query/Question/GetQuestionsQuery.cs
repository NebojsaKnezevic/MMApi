using MediatR;

namespace MajaMayo.API.Models.Survey.Query.GetQuestions
{
    public class GetQuestionsQuery : IRequest<ICollection<QuestionResponse>>
    {

    }
}
