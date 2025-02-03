using MajaMayo.API.DTOs;
using MajaMayo.API.Repository;
using MediatR;

namespace MajaMayo.API.Features.Survey.Command.Email
{
    public class SendEmailHandler : IRequestHandler<SendEmailCommand>
    {
        private readonly ICommandRepository _repository;

        public SendEmailHandler(ICommandRepository repository)
        {
            _repository = repository;
        }
        public async Task Handle(SendEmailCommand request, CancellationToken cancellationToken)
        {
            var x = new EmailDTO
            {
                Body = request.Body,
                Subject = request.Subject,
                To = request.To,
            };
            _repository.SendEmail(x);
        }
    }
}
