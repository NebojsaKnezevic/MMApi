using MajaMayo.API.Repository;
using MediatR;

namespace MajaMayo.API.Features.DeltaGenerali.Command
{
    public class HandleSurveyPDFHandler : IRequestHandler<HandleSurveyPDFCommand, byte[]>
    {
        private readonly IDGCommandRepository _repository;

        public HandleSurveyPDFHandler(IDGCommandRepository repository)
        {
            _repository = repository;
        }
        public async Task<byte[]> Handle(HandleSurveyPDFCommand request, CancellationToken cancellationToken)
        {
            var result = await _repository.HandleSurveyPDF(request.Html, request.HealthAssessmentId);
            return result;
        }
    }
}
