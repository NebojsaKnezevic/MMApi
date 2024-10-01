using MajaMayo.API.Repository;
using MediatR;

namespace MajaMayo.API.Models.Survey.Query.FamilyHistory
{
    public class GetFamilyHistoryHandler : IRequestHandler<GetFamilyHistoryQuery, FamilyHistoryModel>
    {
        private readonly IQueryRepository _repository;

        public GetFamilyHistoryHandler(IQueryRepository repository)
        {
            _repository = repository;
        }
        public async Task<FamilyHistoryModel> Handle(GetFamilyHistoryQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetFamilyHistory(request.Id);
            return result;
        }
    }
}
