namespace MajaMayo.API.DTOs
{
    public class UserResponseDTO
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public string? Password { get; set; }
        public DateTime RegisteredOn { get; set; }
        public string? PolicyNumber { get; set; }
        public string? JMBG { get; set; }
        public string? PassportNumber { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }

    }
}
