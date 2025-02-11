using MajaMayo.API.Models;
using MajaMayo.API.Repository;
using MediatR;

namespace MajaMayo.API.Features.Survey.Command.HealthAssesment
{
    public class CreateNewHealthAssesmentHandler : IRequestHandler<CreateNewHealthAssesmentCommand, HealthAssesmentResponse>
    {
        private readonly ICommandRepository _repository;
        public CreateNewHealthAssesmentHandler(ICommandRepository repository)
        {
            _repository = repository;
        }
        public async Task<HealthAssesmentResponse> Handle(CreateNewHealthAssesmentCommand request, CancellationToken cancellationToken)
        {
            var result = await _repository.CreateNewHealthAssesment(request.UserId);
            if (result is null)
            {
                throw new InvalidOperationException("Failed to create new health assessment due to internal server error.");
            }
            return result;
        }
    }
}
