using MajaMayo.API.DTOs;
using MajaMayo.API.Models;

namespace MajaMayo.API.Repository
{
    public interface IDGCommandRepository
    {
        Task<string> InsertDeltaGeneraliApprovedUsers(ICollection<DGApprovedUserResponse> dGApproveds);

        Task<bool> HandleDGRequests(int healthAssessmentId);
        Task<byte[]> HandleSurveyPDF(byte[] html, int healthAssessmentId);


    }
}
