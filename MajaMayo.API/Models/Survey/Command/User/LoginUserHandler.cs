using MajaMayo.API.Repository;
using MediatR;

namespace MajaMayo.API.Models.Survey.Command.User
{
    public class LoginUserHandler : IRequestHandler<LoginUserCommand, UserResponse>
    {
        private readonly ICommandRepository _commandRepository;

        public LoginUserHandler(ICommandRepository commandRepository)
        {
            _commandRepository = commandRepository;
        }
        public async Task<UserResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var result = await _commandRepository.LoginUser(request.Email, request.Pwd);
            return result;
        }
    }
}
