using MajaMayo.API.Models;
using MajaMayo.API.Repository;
using MediatR;

namespace MajaMayo.API.Features.Survey.Query.HealthExaminations
{
    public class GetHealthExaminationsHandler : IRequestHandler<GetHealthExaminationsQuery, ICollection<HealthExaminationResponse>>
    {
        private readonly IQueryRepository _repository;

        public GetHealthExaminationsHandler(IQueryRepository repository)
        {
            _repository = repository;
        }
        public async Task<ICollection<HealthExaminationResponse>> Handle(GetHealthExaminationsQuery request, CancellationToken cancellationToken)
        {
            var ressult = await _repository.GetHealthExaminations();
            return ressult.ToList();
        }
    }
}
