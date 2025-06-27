using BankATM.Common.Constants;
using System.ComponentModel.DataAnnotations;

namespace BankATM.Common.DTO
{
    public class AccountRequestDTO
    {
        [Required(ErrorMessage = GlobalErrors.AccountNumberRequired)]
        [RegularExpression(@"^[A-Z]{2}[0-9A-Z]{13,30}$", ErrorMessage = GlobalErrors.AccountNumberInvalidFormat)]
        public string AccountNumber { get; set; }
    }
}
