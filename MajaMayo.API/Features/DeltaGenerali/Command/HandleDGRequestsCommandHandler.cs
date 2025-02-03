using MajaMayo.API.Repository;
using MediatR;

namespace MajaMayo.API.Features.DeltaGenerali.Command
{
    public class HandleDGRequestsCommandHandler : IRequestHandler<HandleDGRequestsCommand, bool>
    {
        private readonly IDGCommandRepository _repository;

        public HandleDGRequestsCommandHandler(IDGCommandRepository repository)
        {
            _repository = repository;
        }
        public async Task<bool> Handle(HandleDGRequestsCommand request, CancellationToken cancellationToken)
        {
            var result = await _repository.HandleDGRequests(request.HealthAssessmentId);
            return result;
        }
    }
}
