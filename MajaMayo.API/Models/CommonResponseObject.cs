namespace MajaMayo.API.Models
{
    public class CommonResponseObject
    {

        public int Id { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Question { get; set; }
        public List<Answer> Answers { get; set; }
        public List<int> UserAnswered { get; set; }

        public CommonResponseObject()
        {
            Answers = new List<Answer>();
            UserAnswered = new List<int>();
        }

        public enum StepType
        {
            Question, Group,
        }

        public class Answer
        {
            public int Id { get; set; }
            public string? Val { get; set; }
        }

   
    }
}
