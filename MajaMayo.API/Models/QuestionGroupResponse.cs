namespace MajaMayo.API.Models
{
    public class QuestionGroupResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //public int ApplicableAgeLow { get; set; }
        //public int ApplicableAgeHigh { get; set; }
        //public bool ApplicableMale { get; set; }
        //public bool ApplicableFemale { get; set; }
        //public bool IsActive { get; set; }
        public int InLevel { get; set; }
        public int ParentId { get; set; }
        //public DateTime CreatedOn { get; set; }
        public List<QuestionResponse>? Questions { get; set; }

        public List<QuestionGroupResponse>? Subgroups { get; set; }
        public string Url { get; set; }
 

        public QuestionGroupResponse()
        {
            Questions = new List<QuestionResponse>();
            Subgroups = new List<QuestionGroupResponse>();
        }
    }
}
