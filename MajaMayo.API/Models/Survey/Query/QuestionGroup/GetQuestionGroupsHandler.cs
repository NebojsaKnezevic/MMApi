using MajaMayo.API.Repository;
using MediatR;

namespace MajaMayo.API.Models.Survey.Query.GetQuestionGroups
{
    public class GetQuestionGroupsHandler : IRequestHandler<GetQuestionGroupsQuery, ICollection<QuestionGroupResponse>>
    {
        private readonly IQueryRepository _queryRepository;

        public GetQuestionGroupsHandler(IQueryRepository queryRepository)
        {
            _queryRepository = queryRepository;

        }

        public async Task<ICollection<QuestionGroupResponse>> Handle(GetQuestionGroupsQuery request, CancellationToken cancellationToken)
        {
            var result = await _queryRepository.GetQuestionGroups();
            return result;
        }
    }
}
