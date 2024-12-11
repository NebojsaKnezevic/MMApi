namespace MajaMayo.API.Models
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Gender { get; set; }
        //public string? Password { get; set; }
        public DateTime RegisteredOn { get; set; }
        public string? PolicyNumber { get; set; }
        public string? JMBG { get; set; }
        public string? PassportNumber { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public byte[]? PasswordHash { get; set; }
        public byte[]? PasswordSalt { get; set; }
        public string? Role { get; set; }
        //public int BMI { get; set; }
    }
}
