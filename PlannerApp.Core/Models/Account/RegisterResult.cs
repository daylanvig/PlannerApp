using System.Collections.Generic;

namespace PlannerApp.Shared.Models.Account
{
    public class RegisterResult
    {
        public bool IsSuccessful { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
