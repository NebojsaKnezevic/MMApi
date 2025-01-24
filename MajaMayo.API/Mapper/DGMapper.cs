using MajaMayo.API.DTOs;
using MajaMayo.API.Models;
using System.Data;
using System.Runtime.CompilerServices;

namespace MajaMayo.API.Mapper
{
    public static class DGMapper
    {
        public static ICollection<DGApprovedUserResponseDTO> ToDTO(this ICollection<DGApprovedUserResponse> dGApproveds)
        {
            ICollection<DGApprovedUserResponseDTO> result = new List<DGApprovedUserResponseDTO>();
            foreach (var element in dGApproveds)
            {
                result.Add
                    (
                    new DGApprovedUserResponseDTO()
                         { 
                            FirstName = element.FirstName,
                            LastName = element.LastName,
                            Email = element.Email,
                            JMBG = element.JMBG,
                            DateOfBirth = GetDateOfBirthFromJMBG(element.JMBG),
                            Gender = GetGenderFromJMBG(element.JMBG),
                            GeneraliUID = element.GeneraliUID,
                         }
                    );
            }

            return result;
        }
        public static DataTable ToDataTable(this ICollection<DGApprovedUserResponseDTO> dGApproveds)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("Email", typeof(string));
            dt.Columns.Add("FirstName", typeof(string));
            dt.Columns.Add("LastName", typeof(string));
            dt.Columns.Add("JMBG", typeof(string));
            dt.Columns.Add("DateOfBirth", typeof(string));
            dt.Columns.Add("Gender", typeof(string));
            dt.Columns.Add("GeneraliUID", typeof(string));

            foreach (var user in dGApproveds)
            {
                dt.Rows.Add(
                    user.Email,
                    user.FirstName,
                    user.LastName,
                    user.JMBG,
                    user.DateOfBirth,
                    user.Gender,
                    user.GeneraliUID
                );
            }

            return dt;
        }


        private static string GetDateOfBirthFromJMBG(string jmbg)
        {
            if (string.IsNullOrEmpty(jmbg) || jmbg.Length != 13)
                throw new ArgumentException("Invalid JMBG format. Must be 13 characters long.");

            try
            {
                string day = jmbg.Substring(0, 2);       // "13"
                string month = jmbg.Substring(2, 2);     // "05"
                string yearSuffix = jmbg.Substring(4, 2); // "99"

                // Determine the century (1900s or 2000s)
                int year = int.Parse(yearSuffix) > DateTime.Now.Year % 100 ? 1900 + int.Parse(yearSuffix) : 2000 + int.Parse(yearSuffix);

                // Construct the date
                return new DateTime(year, int.Parse(month), int.Parse(day)).ToShortDateString();
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Invalid date format in JMBG.", ex);
            }
        }


        private static string GetGenderFromJMBG(string jmbg)
        {
            if (string.IsNullOrEmpty(jmbg) || jmbg.Length != 13)
                throw new ArgumentException("Invalid JMBG format.");

            int genderDigit = int.Parse(jmbg[10].ToString());
            return genderDigit % 2 == 0 ? "Female" : "Male";
        }
    }
}
