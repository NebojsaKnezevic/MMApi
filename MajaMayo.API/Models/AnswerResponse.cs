namespace MajaMayo.API.Models
{
    public class AnswerResponse
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public string Text { get; set; }
        public int HealthAssesmentId { get; set; }
        public int AnswerId { get; set; }
    }
}
