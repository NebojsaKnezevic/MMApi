namespace MajaMayo.API.Models
{
    public class SubmitAnswersModel
    {
        public int QuestionId { get; set; }
        public int HealthAssesmentId { get; set; }
        public List<AnswerSelectionType> Answers { get; set; }
    }
}
