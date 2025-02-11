using MajaMayo.API.Helpers;
using MajaMayo.API.Models;
using MajaMayo.API.Repository;
using MediatR;

namespace MajaMayo.API.Features.Survey.Command.User
{
    public class CookieLoginUserHandler : IRequestHandler<CookieLoginUserCommand, UserResponse>
    {
        private readonly ICommandRepository _commandRepository;
        private readonly HttpContext _httpContext;

        public CookieLoginUserHandler(ICommandRepository commandRepository, IHttpContextAccessor httpContextAccessor)
        {
            _commandRepository = commandRepository;
            _httpContext = httpContextAccessor.HttpContext;
        }
        public async Task<UserResponse> Handle(CookieLoginUserCommand request, CancellationToken cancellationToken)
        {
            var jwtToken = _httpContext.Request.Cookies[JWTHelper.SecretTokenName];
            if (jwtToken is null)
            {
                throw new UnauthorizedAccessException("JWT token is missing.");
            }

            var jwtInfo = JWTHelper.DeconstructJWT(jwtToken);

            var result = await _commandRepository.CookieLoginUser(jwtInfo.Id, jwtInfo.Email);

            if (result is null)
            {
                throw new UnauthorizedAccessException("Invalid cookie data.");
            }

            return result;
        }
    }
}
