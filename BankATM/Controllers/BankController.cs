using BankATM.Common.Constants;
using BankATM.Common.DTO;
using BankATM.Common.Response;
using BankATM.Domain;
using BankATM.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankATM.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BankController : Controller
    {
        private readonly IBankService _service;

        #region Constructor

        public BankController(IBankService service)
        {
            _service = service;
        }

        #endregion

        #region Get

        [Authorize]
        [HttpGet("All")]
        public async Task<IActionResult> GetAll()
        {
            var accounts = await _service.GetAllAccounts();

            return Ok(ApiResponse<List<BankAccount>>.Ok(accounts));
        }

        [Authorize]
        [HttpGet("{accountNumber}")]
        public async Task<IActionResult> GetByAccountNumber(string accountNumber)
        {
            BankAccount account = await _service.GetByAccountNumber(accountNumber);

            return Ok(ApiResponse<BankAccount>.Ok(account));
        }

        #endregion

        #region Post

        [Authorize]
        [HttpPost("Deposit")]
        public async Task<IActionResult> Deposit(DepositRequestDTO request)
        {
            await _service.Deposit(request);

            return Ok(ApiResponse<string>.Ok(GlobalMessages.SuccessfulDeposit));
        }

        [Authorize]
        [HttpPost("Withdraw")]
        public async Task<ActionResult> Withdraw(WithdrawRequestDTO request)
        {
            await _service.Withdraw(request);

            return Ok(ApiResponse<string>.Ok(GlobalMessages.SuccessfulWithdrawal));
        }

        #endregion
    }
}
