using MajaMayo.API.Models;

namespace MajaMayo.API.Repository
{
    public interface IQueryRepository
    {
        Task<ICollection<QuestionResponse>> GetQuestions();
        Task<ICollection<QuestionGroupResponse>> GetQuestionsQueryOld(string email);
        Task<ICollection<CommonResponseObject>> GetQuestionsQuery(string email);
    }
}
