using MajaMayo.API.Models;
using MediatR;

namespace MajaMayo.API.Features.Survey.Query.FamilyHistory
{
    public class GetFamilyHistoryQuery : IRequest<FamilyHistoryModel>
    {
        public int Id { get; set; }
    }
}
