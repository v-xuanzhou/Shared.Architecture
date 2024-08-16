using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace UserApi.Msic.Auth
{
    public class JwtHelper
    {
        private readonly IConfiguration _configuration;

        public JwtHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string CreateToken()
        {
            var Claims = new[]
            {
                new Claim(ClaimTypes.Name, "u_admin"), 
                new Claim(ClaimTypes.Role, "r_admin"),
                new Claim(JwtRegisteredClaimNames.Jti, "admin"),
                new Claim("Username", "Admin"),
                new Claim("Name", "超级管理员")
            };

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));

            var algorithm = SecurityAlgorithms.HmacSha256;

            var signingCredentials = new SigningCredentials(secretKey, algorithm);

            var jwtSecurityToken = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],     
                _configuration["Jwt:Audience"],   
                Claims,                         
                DateTime.Now,                    
                DateTime.Now.AddSeconds(30),    
                signingCredentials               
            );

            var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            return token;
        }
    }
}
