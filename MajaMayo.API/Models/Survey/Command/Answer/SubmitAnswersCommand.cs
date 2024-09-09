using MediatR;

namespace MajaMayo.API.Models.Survey.Command.Answer
{
    public class SubmitAnswersCommand : SubmitAnswersModel, IRequest<bool>
    {

    }
}
