using MajaMayo.API.DTOs;
using MajaMayo.API.Models;

namespace MajaMayo.API.Repository
{
    public interface ICommandRepository
    {
        Task<bool> RegisterUser(string Email, string Pwd);
        Task<bool> EmailVerification(string encryptedData);
        void SendEmail(EmailDTO email);
        Task<UserResponse> LoginUser(string Email, string Pwd);
        Task<bool> LogoutUser();
        Task<UserResponse> CookieLoginUser();

        Task<HealthAssesmentResponse> CreateNewHealthAssesment(int userId);
        Task<bool> SubmitAnswers(SubmitAnswersModel model);

        Task<bool> UpdateUserData(UserResponse user);
        Task<bool> InsertUpdateFamilyHistory(FamilyHistoryModel model);

        Task<bool> CompleteSurvey(int healthHssesmentId);

        Task<UserResponse> GoogleLogin(GoogleLoginResponse googleResponse);
    }
}
