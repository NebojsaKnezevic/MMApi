using MajaMayo.API.Models;


namespace MajaMayo.API.Repository
{
    public interface IQueryRepository
    {
        Task<ICollection<QuestionResponse>> GetQuestions();
        //Task<ICollection<QuestionGroupResponse>> GetQuestionsQueryOld(string email);
        //Task<ICollection<CommonResponseObject>> GetQuestionsQuery(string email);

        Task<ICollection<QuestionGroupResponse>> GetQuestionGroups();
 
        Task<ICollection<AnswerResponse>> GetAnswers(int userId, int healthAssesmentId);

        Task<HealthAssesmentResponse> GetHealthAssesment(int userId, int healthAssesmentId = 0);



    }
}
