using MajaMayo.API.Models;
using MediatR;

namespace MajaMayo.API.Features.Survey.Command.User
{
    public class UpdateUserCommand : UserResponse, IRequest<bool>
    {

    }
}
