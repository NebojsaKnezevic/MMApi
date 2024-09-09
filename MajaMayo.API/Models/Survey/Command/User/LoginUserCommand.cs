using MediatR;

namespace MajaMayo.API.Models.Survey.Command.User
{
    public record LoginUserCommand(string Email, string Pwd) : IRequest<UserResponse>;
}
