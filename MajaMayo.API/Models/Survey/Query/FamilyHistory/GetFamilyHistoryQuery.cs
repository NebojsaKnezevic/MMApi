using MediatR;

namespace MajaMayo.API.Models.Survey.Query.FamilyHistory
{
    public class GetFamilyHistoryQuery : IRequest<FamilyHistoryModel>
    {
        public int Id { get; set; }
    }
}
