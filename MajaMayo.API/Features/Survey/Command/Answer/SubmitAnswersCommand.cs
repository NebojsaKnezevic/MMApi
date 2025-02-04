using MajaMayo.API.Models;
using MediatR;

namespace MajaMayo.API.Features.Survey.Command.Answer
{
    public class SubmitAnswersCommand : SubmitAnswersModel, IRequest<bool>
    {

    }
}
