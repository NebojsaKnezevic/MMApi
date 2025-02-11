using MajaMayo.API.Repository;
using MediatR;
using System.Data;

namespace MajaMayo.API.Features.Survey.Command.User
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
            if (await _commandRepository.IsAllowed(request.Email))
            {
                throw new UnauthorizedAccessException("The email provided is not on the Delta Generali approved user list. Please contact them for further assistance.");
            }

            var result = await _commandRepository.RegisterUser(request.Email, request.Pwd);
            if (!result) throw new Exception("Email is already in use!");

            return result;
        }
    }
}
