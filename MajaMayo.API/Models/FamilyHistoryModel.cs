namespace MajaMayo.API.Models
{
    public class FamilyHistoryModel
    {
        public int Id { get; set; }
        public bool? Cardiovascular { get; set; } = false;
        public bool? Diabetes { get; set; } = false; 
        public bool? Cancer { get; set; } = false;
        public string? CancerType { get; set; } = string.Empty;
        public bool? HighBloodPressure { get; set; } = false;
        public bool? Other { get; set; } = false;
        public string? OtherConditions { get; set; } = string.Empty ;
        public bool? MentalIllness { get; set; } = false;
    }
}
