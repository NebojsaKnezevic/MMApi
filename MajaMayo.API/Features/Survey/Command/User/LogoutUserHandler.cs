using MajaMayo.API.Helpers;
using MajaMayo.API.Repository;
using MediatR;

namespace MajaMayo.API.Features.Survey.Command.User
{
    public class LogoutUserHandler : IRequestHandler<LogoutUserCommand, bool>
    {
        private readonly ICommandRepository _repository;
        private readonly HttpContext _context;

        public LogoutUserHandler(ICommandRepository commandRepository, IHttpContextAccessor httpContextAccessor)
        {
            _repository = commandRepository;
            _context = httpContextAccessor.HttpContext;
        }
        public Task<bool> Handle(LogoutUserCommand request, CancellationToken cancellationToken)
        {
            var result = _repository.LogoutUser();
            _context.Response.Cookies.Append(JWTHelper.SecretTokenName, "", JWTHelper.LogoutOptions);
            return result;
        }
    }
}
