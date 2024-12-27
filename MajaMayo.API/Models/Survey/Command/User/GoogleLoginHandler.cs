using MajaMayo.API.Repository;
using MediatR;

namespace MajaMayo.API.Models.Survey.Command.User
{
    public class GoogleLoginHandler : IRequestHandler<GoogleLoginCommand, UserResponse>
    {
        private readonly ICommandRepository _repository;

        public GoogleLoginHandler(ICommandRepository repository)
        {
            _repository = repository;
        }
        public async Task<UserResponse> Handle(GoogleLoginCommand request, CancellationToken cancellationToken)
        {
            var result = await _repository.GoogleLogin(request);
            return result;
        }
    }
}
