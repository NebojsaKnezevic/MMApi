using MajaMayo.API.Features.DeltaGenerali.Command;
using MajaMayo.API.Repository;
using MediatR;

namespace MajaMayo.API.Features.Survey.Command.HealthAssesment
{
    public class CommandSurveyHandler : IRequestHandler<CompleteSurveyCommand, bool>
    {
        private readonly ICommandRepository _repository;
        private readonly IMediator _mediator;

        public CommandSurveyHandler(ICommandRepository repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }


        public async Task<bool> Handle(CompleteSurveyCommand request, CancellationToken cancellationToken)
        {
            var result = await _repository.CompleteSurvey(request.haid);
            if (!result)
            {
                throw new InvalidOperationException("Failed Complete Survey.");
            }
            if (result)
            {
                await _mediator.Send(new HandleDGRequestsCommand(request.haid));
            }
            
            return result;
        }
    }
}
