using MajaMayo.API.DTOs;
using MajaMayo.API.Models;
using System.IdentityModel.Tokens.Jwt;
using static Google.Apis.Auth.GoogleJsonWebSignature;

namespace MajaMayo.API.Repository
{
    public interface ICommandRepository
    {
        Task<bool> RegisterUser(string Email, string Pwd);
        Task<bool> IsAllowed(string email);
        Task<bool> EmailVerification(string encryptedData);
        void SendEmail(EmailDTO email);
        Task<UserResponse> LoginUser(string Email, string Pwd);
        Task<bool> LogoutUser();
        Task<UserResponse> CookieLoginUser(int id, string email);

        Task<HealthAssesmentResponse> CreateNewHealthAssesment(int userId);
        Task<bool> SubmitAnswers(SubmitAnswersModel model);

        Task<bool> UpdateUserData(UserResponse user);
        Task<bool> InsertUpdateFamilyHistory(FamilyHistoryModel model);

        Task<bool> CompleteSurvey(int healthHssesmentId);

        Task<UserResponse> GoogleLogin(Payload payload);

        Task<bool> LogError(LogEntry logEntry);
    }
}
