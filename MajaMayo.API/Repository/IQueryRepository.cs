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

        Task<ICollection<HealthAssesmentResponse>> GetHealthAssesment(int userId, int healthAssesmentId = 0);

        Task<FamilyHistoryModel> GetFamilyHistory(int id);

        Task<ICollection<HealthExaminationResponse>> GetHealthExaminations();

        Task<ICollection<HealthAssessmentScoresResponse>> GetHealthAssessmentScores(int healthAssessmentId);

    }
}
