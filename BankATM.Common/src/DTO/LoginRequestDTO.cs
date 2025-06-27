using BankATM.Common.Constants;
using System.ComponentModel.DataAnnotations;

namespace BankATM.Common.DTO
{
    public class LoginRequestDTO
    {
        [Required(ErrorMessage = GlobalErrors.UsernameRequired)]
        public string Username { get; set; }

        [Required(ErrorMessage = GlobalErrors.PasswordRequired)]
        public string Password { get; set; }
    }
}
