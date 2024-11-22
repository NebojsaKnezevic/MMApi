using MajaMayo.API.Repository;
using MediatR;

namespace MajaMayo.API.Models.Survey.Command.HealthAssesment
{
    public class CommandSurveyHandler : IRequestHandler<CompleteSurveyCommand, bool>
    {
        private readonly ICommandRepository _repository;

        public CommandSurveyHandler(ICommandRepository repository)
        {
            _repository = repository;
        }
 

        public async Task<bool> Handle(CompleteSurveyCommand request, CancellationToken cancellationToken)
        {
            var result = await _repository.CompleteSurvey(request.haid);
            return result;
        }
    }
}
