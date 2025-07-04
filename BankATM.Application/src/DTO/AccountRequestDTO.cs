﻿using BankATM.Domain.Constants;
using System.ComponentModel.DataAnnotations;

namespace BankATM.Application.DTO
{
    public class AccountRequestDTO
    {
        [Required(ErrorMessage = GlobalErrors.AccountNumberRequired)]
        [RegularExpression(@"^[A-Z]{2}[0-9A-Z]{13,30}$", ErrorMessage = GlobalErrors.AccountNumberInvalidFormat)]
        public string AccountNumber { get; set; }
    }
}
