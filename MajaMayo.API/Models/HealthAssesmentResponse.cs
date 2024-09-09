namespace MajaMayo.API.Models
{
    public class HealthAssesmentResponse
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime StartedOn { get; set; }
        public DateTime CompletedOn { get; set; }
    }
}
