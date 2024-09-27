using MediatR;

namespace MajaMayo.API.Models.Survey.Command.FamilyHistory
{
    public class InsertUpdateFamilyHistoryCommand : FamilyHistoryModel, IRequest<bool>
    {
    }
}
