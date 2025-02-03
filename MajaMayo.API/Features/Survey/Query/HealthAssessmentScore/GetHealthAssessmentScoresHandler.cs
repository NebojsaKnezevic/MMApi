using MajaMayo.API.Models;
using MajaMayo.API.Repository;
using MediatR;

namespace MajaMayo.API.Features.Survey.Query.HealthAssessmentScore
{
    public class GetHealthAssessmentScoresHandler : IRequestHandler<GetHealthAssessmentScoresQuery, ICollection<HealthAssessmentScoresResponse>>
    {
        private readonly IQueryRepository _repository;

        public GetHealthAssessmentScoresHandler(IQueryRepository repository)
        {
            _repository = repository;
        }
        public async Task<ICollection<HealthAssessmentScoresResponse>> Handle(GetHealthAssessmentScoresQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetHealthAssessmentScores(request.HealthAssessmentId);
            return result;
        }
    }
}
