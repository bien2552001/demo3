

using DOANV1.Entities.DTO.UserDto.LoginDto;
using DOANV1.Entities.Model.User;
using DOANV1.Interface.IUser;
using Interface.IService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.Intrinsics.X86;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DOANV1.Repository.User.Login
{
    
    public class Login_JWT : ILogin_JWT
    {
        private readonly UserManager<User_Model> _userManager;

        private readonly IConfiguration _configuration;
        private readonly ILoggerService _logger;
        private User_Model _userModel;


        public Login_JWT(UserManager<User_Model> userManager, IConfiguration configuration, ILoggerService logger)
        {
            _userManager = userManager;
            _configuration = configuration;
            _logger = logger;
        }



        //LOGIN 

        public async Task<bool> ValidateUser(LoginRequestDto userForAuth)
        {
            _userModel = await _userManager.FindByEmailAsync(userForAuth.Email);
            _logger.LogInfo("========>>>>>>> ValidateUser  successful");
            return (_userModel != null && await _userManager.CheckPasswordAsync(_userModel, userForAuth.Password));
        }


        public async Task<string> CreateToken()
        {
            var signingCredentials = GetSigningCredentials();

            var claims = await GetClaims();

            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);

            _logger.LogInfo("========>>>>>>> CreateToken  successful");

            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }


        private SigningCredentials GetSigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("SECRET"));

            var secret = new SymmetricSecurityKey(key);

            _logger.LogInfo("========>>>>>>> GetSigningCredentials  successful");

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }


        private async Task<List<Claim>> GetClaims()
        {
            var claims = new List<Claim>
            {
            new Claim(ClaimTypes.Name, _userModel.UserName)
            };

            var roles = await _userManager.GetRolesAsync(_userModel);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            _logger.LogInfo("========>>>>>>> GetClaims  successful");
            return claims;
        }


        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");

            var tokenOptions = new JwtSecurityToken
            (
                issuer: jwtSettings.GetSection("validIssuer").Value,

                audience: jwtSettings.GetSection("validAudience").Value,

                claims: claims,
     
                expires: DateTime.Now.AddMinutes(1),

                signingCredentials: signingCredentials
            );
            _logger.LogInfo("========>>>>>>> GenerateTokenOptions  successful");

            return tokenOptions;
        }
    }
}
