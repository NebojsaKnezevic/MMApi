using MajaMayo.API.Repository;
using MediatR;

namespace MajaMayo.API.Features.Survey.Command.User
{
    public class LogoutUserHandler : IRequestHandler<LogoutUserCommand, bool>
    {
        private readonly ICommandRepository _repository;

        public LogoutUserHandler(ICommandRepository repository)
        {
            _repository = repository;
        }
        public Task<bool> Handle(LogoutUserCommand request, CancellationToken cancellationToken)
        {
            var result = _repository.LogoutUser();
            return result;
        }
    }
}
