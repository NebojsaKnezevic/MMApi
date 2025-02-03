using MajaMayo.API.Models;
using MajaMayo.API.Repository;
using MediatR;

namespace MajaMayo.API.Features.Survey.Query.Question
{
    public class GetQuestionsQueryHandler : IRequestHandler<GetQuestionsQuery, ICollection<QuestionResponse>>
    {
        private readonly IQueryRepository _queryRepository;

        public GetQuestionsQueryHandler(IQueryRepository queryRepository)
        {
            _queryRepository = queryRepository;

        }
        public async Task<ICollection<QuestionResponse>> Handle(GetQuestionsQuery request, CancellationToken cancellationToken)
        {
            var result = await _queryRepository.GetQuestions();
            return result;
        }
    }
}
