using MajaMayo.API.Repository;
using MediatR;
using System.Windows.Input;

namespace MajaMayo.API.Features.Survey.Command.Email
{
    public class EmailVerificationHandler : IRequestHandler<EmailVerificationCommand, bool>
    {
        private readonly ICommandRepository _command;

        public EmailVerificationHandler(ICommandRepository command)
        {
            _command = command;
        }

        public Task<bool> Handle(EmailVerificationCommand request, CancellationToken cancellationToken)
        {
            var result = _command.EmailVerification(request.Email);
            return result;
        }
    }
}
