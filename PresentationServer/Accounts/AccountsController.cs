using Application.Accounts.Commands.RegisterNewUser;
using Application.Accounts.Commands.SignInUser;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using System.Threading.Tasks;


namespace PlannerApp.PresentationServer.Accounts
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IRegisterNewUserCommand registerNewUserCommand;
        private readonly ISignInUserCommand signInUserCommand;

        public AccountsController(IRegisterNewUserCommand registerNewUserCommand, ISignInUserCommand signInUserCommand)
        {
            this.registerNewUserCommand = registerNewUserCommand;
            this.signInUserCommand = signInUserCommand;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterModel registerModel)
        {
            var result = await registerNewUserCommand.Execute(registerModel);
            if (result.IsSuccessful)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("Login")]
        public async Task<IActionResult> LogIn([FromBody] SignInUserModel loginModel)
        {
            var result = await signInUserCommand.Execute(loginModel);
            if (result.IsSuccessful)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
