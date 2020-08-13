using System.Collections.Generic;

namespace Application.Accounts.Commands.RegisterNewUser
{
    public class RegisterResult
    {
        public bool IsSuccessful { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
