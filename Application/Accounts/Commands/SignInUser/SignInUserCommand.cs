using Application.Interfaces.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Accounts.Commands.SignInUser
{
    public class SignInUserCommand : ISignInUserCommand
    {
        private readonly IUserService userService;
        private readonly IConfiguration configuration;

        public SignInUserCommand(IUserService userService, IConfiguration configuration)
        {
            this.userService = userService;
            this.configuration = configuration;
        }

        public async Task<SignInUserResult> Execute(SignInUserModel signInUserModel)
        {
            var user = await userService.SignIn(signInUserModel.Email, signInUserModel.Password);
            if (user == null)
            {
                return new SignInUserResult { IsSuccessful = false, Error = "Invalid Login Details" };
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim("tenantID", user.TenantID)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSecurityKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiry = DateTime.Now.AddDays(Convert.ToInt32(configuration["JwtExpiryInDays"]));

            var token = new JwtSecurityToken(
                configuration["JwtIssuer"],
                configuration["JwtAudience"],
                claims,
                expires: expiry,
                signingCredentials: creds
            );
            return new SignInUserResult { IsSuccessful = true, Token = new JwtSecurityTokenHandler().WriteToken(token) };
        }
    }
}
