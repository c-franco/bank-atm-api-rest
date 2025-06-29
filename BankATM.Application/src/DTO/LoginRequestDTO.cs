using BankATM.Domain.Constants;
using System.ComponentModel.DataAnnotations;

namespace BankATM.Application.DTO
{
    public class LoginRequestDTO
    {
        [Required(ErrorMessage = GlobalErrors.UsernameRequired)]
        [StringLength(50, MinimumLength = 3, ErrorMessage = GlobalErrors.UsernameLength)]
        public string Username { get; set; }

        [Required(ErrorMessage = GlobalErrors.PasswordRequired)]
        [StringLength(50, MinimumLength = 6, ErrorMessage = GlobalErrors.PasswordLength)]
        public string Password { get; set; }
    }
}
