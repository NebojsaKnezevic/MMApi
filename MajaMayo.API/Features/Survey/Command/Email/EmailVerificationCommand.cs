using MediatR;

namespace MajaMayo.API.Features.Survey.Command.Email
{
    public class EmailVerificationCommand : IRequest<bool>
    {
        public string Email { get; set; }
    }
}
