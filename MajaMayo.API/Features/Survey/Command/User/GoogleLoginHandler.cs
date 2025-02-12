using Google.Apis.Auth;
using MajaMayo.API.Helpers;
using MajaMayo.API.Models;
using MajaMayo.API.Repository;
using MediatR;

namespace MajaMayo.API.Features.Survey.Command.User
{
    public class GoogleLoginHandler : IRequestHandler<GoogleLoginCommand, UserResponse>
    {
        private readonly ICommandRepository _repository;
        private readonly HttpContext _httpContext;

        public GoogleLoginHandler(ICommandRepository repository, IHttpContextAccessor httpContextAccessor)
        {
            _repository = repository;
            _httpContext = httpContextAccessor.HttpContext;
        }
        public async Task<UserResponse> Handle(GoogleLoginCommand request, CancellationToken cancellationToken)
        {
            var payload = await GoogleJsonWebSignature.ValidateAsync(request.Credential);
            if (payload is null)
            {
                throw new Exception("Missing google credentials.");
            }
            var result = await _repository.GoogleLogin(payload);

            var tokenString = JWTHelper.GenerateJWTToken(result);

            _httpContext.Response.Cookies.Append(JWTHelper.SecretTokenName, tokenString, JWTHelper.CurrentOptions);
            return result;
        }
    }
}
