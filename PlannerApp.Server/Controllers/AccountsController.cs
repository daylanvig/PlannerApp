using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PlannerApp.Server.Models.Identity;
using PlannerApp.Shared.Models.Account;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PlannerApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<PlannerAppUser> userManager;
        private readonly SignInManager<PlannerAppUser> signInManager;
        private readonly IConfiguration configuration;

        public AccountsController(UserManager<PlannerAppUser> userManager, SignInManager<PlannerAppUser> signInManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterModel registerModel)
        {
            var result = await userManager.CreateAsync(new PlannerAppUser
            {
                Email = registerModel.Email,
                UserName = registerModel.Email,
                TenantID = Guid.NewGuid().ToString()
            }, registerModel.Password);

            if (!result.Succeeded)
            {
                return BadRequest(new RegisterResult { IsSuccessful = false, Errors = result.Errors.Select(e => e.Description) });
            }

            return Ok(new RegisterResult { IsSuccessful = true });
        }

        [HttpPut("Login")]
        public async Task<IActionResult> LogIn([FromBody] LoginModel loginModel)
        {
            var result = await signInManager.PasswordSignInAsync(loginModel.Email, loginModel.Password, true, true);
            if (!result.Succeeded)
            {
                return BadRequest(new LoginResult { IsSuccessful = false, Error = "Invalid Login Details" });
            }
            var user = await userManager.FindByEmailAsync(loginModel.Email);
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, loginModel.Email),
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

            return Ok(new LoginResult { IsSuccessful = true, Token = new JwtSecurityTokenHandler().WriteToken(token)});
        }
    }
}
