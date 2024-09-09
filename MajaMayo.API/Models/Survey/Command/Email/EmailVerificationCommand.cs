using MediatR;

namespace MajaMayo.API.Models.Survey.Command.Email
{
    public class EmailVerificationCommand : IRequest<bool>
    {
        public string Email { get; set; }
    }
}
