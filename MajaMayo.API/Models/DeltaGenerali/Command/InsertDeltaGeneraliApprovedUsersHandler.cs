using MajaMayo.API.Repository;
using MediatR;

namespace MajaMayo.API.Models.DeltaGenerali.Command
{
    public class InsertDeltaGeneraliApprovedUsersHandler : IRequestHandler<InsertDeltaGeneraliApprovedUsersCommand, string>
    {
        private readonly IDGCommandRepository _repository;

        public InsertDeltaGeneraliApprovedUsersHandler(IDGCommandRepository repository)
        {
            _repository = repository;
        }
        public async Task<string> Handle(InsertDeltaGeneraliApprovedUsersCommand request, CancellationToken cancellationToken)
        {
            var result = await _repository.InsertDeltaGeneraliApprovedUsers(request.Users);
            return result;
        }
    }
}
