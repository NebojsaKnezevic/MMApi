using MajaMayo.API.Models;
using MajaMayo.API.Repository;
using MediatR;

namespace MajaMayo.API.Features.Survey.Command.User
{
    public class CookieLoginUserHandler : IRequestHandler<CookieLoginUserCommand, UserResponse>
    {
        private readonly ICommandRepository _commandRepository;

        public CookieLoginUserHandler(ICommandRepository commandRepository)
        {
            _commandRepository = commandRepository;
        }
        public async Task<UserResponse> Handle(CookieLoginUserCommand request, CancellationToken cancellationToken)
        {
            var result = await _commandRepository.CookieLoginUser();
            return result;
        }
    }
}
