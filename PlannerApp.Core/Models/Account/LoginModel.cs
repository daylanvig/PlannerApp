using System.ComponentModel.DataAnnotations;

namespace PlannerApp.Shared.Models.Account
{
    public class LoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
