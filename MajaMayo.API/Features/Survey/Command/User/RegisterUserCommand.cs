using MediatR;

namespace MajaMayo.API.Features.Survey.Command.User
{
    public record RegisterUserCommand(string Email, string Pwd) : IRequest<bool>;
}
