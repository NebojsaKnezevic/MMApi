namespace MajaMayo.API.Models
{
    public class LogEntry
    {
        public string LogLevel { get; set; }
        public string Message { get; set; }
        public string? Exception { get; set; }
        public int? EventId { get; set; }
        public string? Source { get; set; }
        public string? RequestPath { get; set; }
        public string? UserId { get; set; }
    }

}
