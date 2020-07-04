namespace PlannerApp.Shared.Models.Account
{
    public class LoginResult
    {
        public bool IsSuccessful { get; set; }
        public string Error { get; set; }
        public string Token { get; set; }
    }
}
