using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PaperSquare.API.Feature.Auth.Dto
{
    public record LoginInsertRequest
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }

    public class LoginInsertRequestOptions
    {
        public const int PasswordLength = 8;
    }
}
