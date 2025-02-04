using MajaMayo.API.Models;
using MediatR;

namespace MajaMayo.API.Features.Survey.Query.Question
{
    public class GetQuestionsQuery : IRequest<ICollection<QuestionResponse>>
    {

    }
}
