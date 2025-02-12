using MajaMayo.API.Helpers;
using MajaMayo.API.Models;
using MajaMayo.API.Repository;
using MediatR;

namespace MajaMayo.API.Features.Survey.Command.User
{
    public class LoginUserHandler : IRequestHandler<LoginUserCommand, UserResponse>
    {
        private readonly ICommandRepository _commandRepository;
        private readonly HttpContext _context;

        public LoginUserHandler(ICommandRepository commandRepository, IHttpContextAccessor httpContextAccessor)
        {
            _commandRepository = commandRepository;
            _context = httpContextAccessor.HttpContext;
        }
        public async Task<UserResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {

            if (request.Email == "" || request.Pwd == "") 
            {
                throw new UnauthorizedAccessException("Invalid email or password.");
            }

            if (request.Email is null || request.Pwd is null)
            {
                throw new UnauthorizedAccessException("Invalid email or password.");
            }

            var result = await _commandRepository.LoginUser(request.Email, request.Pwd);
            if (result is null)
            {
                throw new UnauthorizedAccessException("Invalid email or password.");
            }
            

            var pwdCorrect = PasswordHash.VerifyPasswordHash(request.Pwd, result.PasswordHash, result.PasswordSalt);

            if (!pwdCorrect) throw new UnauthorizedAccessException("Invalid email or password.");

            var tokenString = JWTHelper.GenerateJWTToken(result);
            _context.Response.Cookies.Append(JWTHelper.SecretTokenName, tokenString, JWTHelper.CurrentOptions);

            return result;
        }
    }
}
