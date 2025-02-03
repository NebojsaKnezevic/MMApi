using MajaMayo.API.DTOs;
using MediatR;

namespace MajaMayo.API.Features.Survey.Command.Email
{
    public class SendEmailCommand : EmailDTO, IRequest
    {
    }
}
