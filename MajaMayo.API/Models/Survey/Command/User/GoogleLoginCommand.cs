using MediatR;

namespace MajaMayo.API.Models.Survey.Command.User
{
    public class GoogleLoginCommand : GoogleLoginResponse, IRequest<UserResponse>
    {
    }
}
