using MajaMayo.API.ConfigModel;
using MajaMayo.API.Models;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MajaMayo.API.Helpers
{
    public static class JWTHelper
    {
        public static readonly CookieOptions CurrentOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.None,
            Expires = DateTime.UtcNow.AddHours(12) //Koliko sati traje cookie
        };

        public static readonly CookieOptions LogoutOptions = new CookieOptions()
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.None,
            Expires = DateTime.UtcNow.AddDays(-1)
        };

        public static readonly string SecretTokenName = "ACTTokenAuth";
        public static readonly string securityKey = Environment.GetEnvironmentVariable("SECURITY_KEY") ?? "F2D8A2D1E9B74C6F92A1D3E5B8C4F7A9D6E3B1C5A7E2D4F8B9C1A3E6D7F2B4C\r\n";
        public static string GenerateJWTToken(UserResponse user)
        {
            var claims = new List<Claim> {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            //new Claim(ClaimTypes.Name, user.FirstName),
            //new Claim(ClaimTypes.Surname, user.LastName),
            new Claim(ClaimTypes.Email, user.Email),
            //new Claim(ClaimTypes.Gender, user.Gender),
            new Claim(ClaimTypes.GivenName, user.FirstName + user.LastName),
            new Claim(ClaimTypes.Role, user.Role)
            };

            var jwtToken = new JwtSecurityToken(
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddHours(12),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(
                       Encoding.UTF8.GetBytes(securityKey)
                        ),
                    SecurityAlgorithms.HmacSha256Signature)
                );
            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }

        public static UserResponse DeconstructJWT(string jwtToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(jwtToken);

            //var expTime = jwtSecurityToken.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Expiration)?.Value;
            var id = jwtSecurityToken.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value;
            //var firstName = jwtSecurityToken.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Name)?.Value;
            //var lastName = jwtSecurityToken.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Surname)?.Value;
            var email = jwtSecurityToken.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Email)?.Value;
            //var gender = jwtSecurityToken.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Gender)?.Value;
            var givenName = jwtSecurityToken.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.GivenName)?.Value;

            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(email))
            {
                throw new UnauthorizedAccessException("Invalid JWT token.");
            }
            
            return new UserResponse
            {
                Id = int.Parse(id),
                //FirstName = firstName,
                //LastName = lastName,
                Email = email,
                //Gender = gender
            };
        }

        public static CookieOptions GetCookieOptions()
        { 
            return new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddHours(12)
            };
        }
    }
}
