using MediatR;

namespace MajaMayo.API.Models.Survey.Command.User
{
    public record RegisterUserCommand(string Email, string Pwd) : IRequest<bool>;
}
