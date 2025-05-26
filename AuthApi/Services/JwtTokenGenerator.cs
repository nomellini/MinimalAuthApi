using AuthApi.Domain.Identity;
using AuthApi.Domain.Models;
using AuthApi.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthApi.Services
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {

        private readonly JwtOptions _jwtOptions;

        public JwtTokenGenerator(IOptions<JwtOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
        }

        public string GenerateToken(ApplicationUser applicationUser, IEnumerable<string> roles)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_jwtOptions.Secret);

            var claimList = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, applicationUser.Id),
                new Claim(JwtRegisteredClaimNames.Name, applicationUser.FullName!),
                new Claim(JwtRegisteredClaimNames.Email, applicationUser.Email!),
            };

            claimList.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = _jwtOptions.Audience,
                Issuer = _jwtOptions.Issuer,
                Subject = new ClaimsIdentity(claimList),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);

        }
    }
}

// dotnet user-secrets set "JwtOptions:Secret" "ssh-rsa AAAAB3NzaC1yc2EAAAADAQABAAABAQDPQKKR4XJ3yfrPAyvxX56WeaI0gGfqvOYmcBtyv9aAzwcOj8meuIWhUCpfDL+OP6Cy1/izAJuKrPdOrD1lZF+1FO/zxuvPXZLylioWQfcLncqVzVEp2H/NgmWq3inLgPC0XJA5yTc+nbOaWJX3w0SGcTwggLfcRybZVoEMYGMm18wO5MZxjleBfc0CHUfg/4GQ5qK8poqTHjw/6/oNjrQsjoXyzJa2fRU2E0mkXDm/RTbIOu0Gbm7E6pb2GWKkkDsIHR36gx42YI9m1+6XG9RFO7AwVCoZOV"
