namespace MajaMayo.API.Models
{
    public class QuestionResponse
    {
        public int Id { get; set; }
        public int OrderNo { get; set; }
        public string Text { get; set; }
        public string AdditionalComment { get; set; }
        public int? QuestionGroupId { get; set; }
        //public int? ApplicableAgeLow { get; set; }
        //public int? ApplicableAgeHigh { get; set; }
        //public bool ApplicableMale { get; set; }
        //public bool ApplicableFemale { get; set; }
        //public bool IsActive { get; set; }
        //public DateTime CreatedOn { get; set; }
        public int? Image { get; set; }
        public List<AnswerResponse>? Answers { get; set; }
        public bool IsMultipleSelect { get; set; }
        public int MaxSelect { get; set; }
        public int MinSelect { get; set; }
        public int ParentId { get; set; }
        public string InputType { get; set; }
        public bool IsSavedInDB { get; set; } = true;

        public QuestionResponse()
        {
            Answers = new List<AnswerResponse>();
        }
    }
}
