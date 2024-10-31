using MajaMayo.API.DTOs;
using MajaMayo.API.Models;

namespace MajaMayo.API.Mapper
{
    public static class SurveyMapper
    {
        public static UserResponseDTO ToDTO(this UserResponse model) 
        {
            return new UserResponseDTO
            {
                Id = model.Id,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                DateOfBirth = model.DateOfBirth,
                Gender = model.Gender,
                Height = model.Height,
                JMBG = model.JMBG,
                PassportNumber = model.PassportNumber,
                Password = model.Password,
                PhoneNumber = model.PhoneNumber,
                PolicyNumber = model.PolicyNumber,
                RegisteredOn = model.RegisteredOn,
                Weight = model.Weight,
            };
        }
    }
}
