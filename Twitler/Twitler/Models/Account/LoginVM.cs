using System.ComponentModel.DataAnnotations;

namespace Twitler.Models.Account
{
    public class LoginVm
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}