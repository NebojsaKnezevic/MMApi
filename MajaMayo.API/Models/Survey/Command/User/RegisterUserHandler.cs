using MajaMayo.API.Repository;
using MediatR;
using System.Data;

namespace MajaMayo.API.Models.Survey.Command.User
{
    public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, bool>
    {
        private readonly ICommandRepository _commandRepository;

        public RegisterUserHandler(ICommandRepository commandRepository)
        {
            _commandRepository = commandRepository;
        }
        public async Task<bool> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var result = await _commandRepository.RegisterUser(request.Email, request.Pwd);
            return result;  
        }
    }
}
