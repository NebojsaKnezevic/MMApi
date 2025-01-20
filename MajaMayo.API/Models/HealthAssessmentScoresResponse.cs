namespace MajaMayo.API.Models
{
    public class HealthAssessmentScoresResponse
    {
        public int Id { get; set; }
        public int HealthAssessmentId { get; set; }
        public string HealthAssessment { get; set; }
        public int Score { get; set; }
    }
}
