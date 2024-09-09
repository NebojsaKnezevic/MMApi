using MajaMayo.API.Repository;
using MediatR;

namespace MajaMayo.API.Models.Survey.Command.Answer
{
    public class SubmitAnswersHandler : IRequestHandler<SubmitAnswersCommand, bool>
    {
        private readonly ICommandRepository _repository;

        public SubmitAnswersHandler(ICommandRepository repository)
        {
            _repository = repository;
        }
        public async Task<bool> Handle(SubmitAnswersCommand request, CancellationToken cancellationToken)
        {
            var result = await _repository.SubmitAnswers(request);
            return result;
        }
    }
}
