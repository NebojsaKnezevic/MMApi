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
            };
        }
    }
}
