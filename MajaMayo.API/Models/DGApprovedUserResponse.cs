namespace MajaMayo.API.Models
{
    public class DGApprovedUserResponse
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string JMBG { get; set; }
        public string GeneraliUID { get; set; }

        //public DateTime DateOfBirth
        //{
        //    get
        //    {
        //        return GetDateOfBirthFromJMBG(JMBG);
        //    }
        //}

        //public string Gender
        //{
        //    get
        //    {
        //        return GetGenderFromJMBG(JMBG);
        //    }
        //}

        //private DateTime GetDateOfBirthFromJMBG(string jmbg)
        //{
        //    if (string.IsNullOrEmpty(jmbg) || jmbg.Length != 13)
        //        throw new ArgumentException("Invalid JMBG format.");

        //    string dateOfBirthString = jmbg.Substring(0, 7); // DDMMGG
        //    int yearPrefix = (int.Parse(dateOfBirthString.Substring(4, 2)) > DateTime.Now.Year % 100) ? 1900 : 2000;
        //    string fullYear = (yearPrefix + dateOfBirthString.Substring(4, 2)).ToString();
        //    string dateOfBirthFormatted = dateOfBirthString.Substring(0, 4) + fullYear.Substring(2) + dateOfBirthString.Substring(6);
        //    return DateTime.ParseExact(dateOfBirthFormatted, "ddMMyyyy", null);
        //}

        //private string GetGenderFromJMBG(string jmbg)
        //{
        //    if (string.IsNullOrEmpty(jmbg) || jmbg.Length != 13)
        //        throw new ArgumentException("Invalid JMBG format.");

        //    int genderDigit = int.Parse(jmbg[10].ToString());
        //    return genderDigit % 2 == 0 ? "Female" : "Male";
        //}
    }
}
