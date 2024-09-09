using MajaMayo.API.Repository;
using MediatR;

namespace MajaMayo.API.Models.Survey.Query.GetQuestionsByEmail
{
    //public class GetQuestionsByEmailHandler : IRequestHandler<GetQuestionsByEmailQuery, ICollection<CommonResponseObject>>
    //{
    //    private readonly IQueryRepository _queryRepository;

    //    public GetQuestionsByEmailHandler(IQueryRepository queryRepository)
    //    {
    //        _queryRepository = queryRepository;

    //    }

    //    public async Task<ICollection<CommonResponseObject>> Handle(GetQuestionsByEmailQuery request, CancellationToken cancellationToken)
    //    {
    //        var res = await _queryRepository.GetQuestionsQuery(request.Email);
    //        return res;
    //    }
    //}
}
