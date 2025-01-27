using MediatR;

namespace MajaMayo.API.Models.DeltaGenerali.Command
{
    public class InsertDeltaGeneraliApprovedUsersCommand : IRequest<string>
    {
        public List<DGApprovedUserResponse> Users { get; set; }

        public InsertDeltaGeneraliApprovedUsersCommand(List<DGApprovedUserResponse> users)
        {
            Users = users;
        }
    }
}
