using MajaMayo.API.DTOs;
using MediatR;

namespace MajaMayo.API.Models.Survey.Command.Email
{
    public class SendEmailCommand : EmailDTO, IRequest
    {
    }
}
