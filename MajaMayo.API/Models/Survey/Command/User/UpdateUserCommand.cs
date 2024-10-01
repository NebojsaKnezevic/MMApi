using MediatR;

namespace MajaMayo.API.Models.Survey.Command.User
{
    public class UpdateUserCommand : UserResponse, IRequest<bool>
    {

    }
}
