namespace PaperSquare.API.Feature.Auth.Dto
{
    public record LoginInsertRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class LoginInsertRequestOptions
    {
        public const int PasswordLength = 8;
    }
}
