namespace Application.Accounts.Commands.SignInUser
{
    public class SignInUserResult
    {
        public bool IsSuccessful { get; set; }
        public string Error { get; set; }
        public string Token { get; set; }
    }
}
