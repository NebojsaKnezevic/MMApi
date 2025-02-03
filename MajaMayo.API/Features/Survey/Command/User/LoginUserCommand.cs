using MajaMayo.API.Models;
using MediatR;

namespace MajaMayo.API.Features.Survey.Command.User
{
    public record LoginUserCommand(string Email, string Pwd) : IRequest<UserResponse>;
}
