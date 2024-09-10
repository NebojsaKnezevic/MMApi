namespace MajaMayo.API.Models
{
    public class AnswerResponse
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public int OrderNo { get; set; }
        public string Text { get; set; }
        public bool IsActive { get; set; }
        public bool IsSelected { get; set; }
        public bool IsAnswered { get; set; }
    }
}
