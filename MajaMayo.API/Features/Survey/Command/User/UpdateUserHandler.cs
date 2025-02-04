using MajaMayo.API.Repository;
using MediatR;

namespace MajaMayo.API.Features.Survey.Command.User
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, bool>
    {
        private readonly ICommandRepository _repository;

        public UpdateUserHandler(ICommandRepository repository)
        {
            _repository = repository;
        }
        public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var result = await _repository.UpdateUserData(request);
            return result;
        }
    }
}
