﻿using MajaMayo.API.Repository;
using MediatR;

namespace MajaMayo.API.Models.Survey.Query.GetAnswers
{
    public class GetAnswersHandler : IRequestHandler<GetAnswersQuery, ICollection<AnswerResponse>>
    {
        private readonly IQueryRepository _queryRepository;

        public GetAnswersHandler(IQueryRepository queryRepository)
        {
            _queryRepository = queryRepository;
        }
        public async Task<ICollection<AnswerResponse>> Handle(GetAnswersQuery request, CancellationToken cancellationToken)
        {
            var result = await _queryRepository.GetAnswers(request.UserId, request.HealthAssesmentId);
            return result;
        }
    }
}
