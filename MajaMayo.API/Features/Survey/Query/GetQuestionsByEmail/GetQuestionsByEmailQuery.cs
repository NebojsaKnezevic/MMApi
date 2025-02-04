using MajaMayo.API.Models;
using MediatR;

namespace MajaMayo.API.Features.Survey.Query.GetQuestionsByEmail
{
    public class GetQuestionsByEmailQuery : IRequest<ICollection<CommonResponseObject>>
    {
        public string Email { get; set; }

        public GetQuestionsByEmailQuery(string email)
        {
            Email = email;
        }
    }
}
