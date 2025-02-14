﻿using MajaMayo.API.Models;
using MajaMayo.API.Repository;
using MediatR;

namespace MajaMayo.API.Features.Survey.Query.HealthAssesment
{
    public class GetHealthAssesmentHandler : IRequestHandler<GetHealthAssesmentQuery, ICollection<HealthAssesmentResponse>>
    {
        private readonly IQueryRepository _repository;

        public GetHealthAssesmentHandler(IQueryRepository repository)
        {
            _repository = repository;
        }
        public async Task<ICollection<HealthAssesmentResponse>> Handle(GetHealthAssesmentQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetHealthAssesment(request.UserId, request.HealthAssesmentId);
            return result;
        }
    }
}
