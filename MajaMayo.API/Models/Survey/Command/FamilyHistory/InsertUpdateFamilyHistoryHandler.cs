using MajaMayo.API.Repository;
using MediatR;

namespace MajaMayo.API.Models.Survey.Command.FamilyHistory
{
    public class InsertUpdateFamilyHistoryHandler : IRequestHandler<InsertUpdateFamilyHistoryCommand, bool>
    {
        private readonly ICommandRepository _repository;
        public InsertUpdateFamilyHistoryHandler(ICommandRepository repository)
        {
            _repository = repository;
        }
        public async Task<bool> Handle(InsertUpdateFamilyHistoryCommand request, CancellationToken cancellationToken)
        {
            var results = await _repository.InsertUpdateFamilyHistory(request);
            return results;
        }
    }
}
