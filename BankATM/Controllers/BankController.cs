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
        public async Task<ActionResult<List<BankAccountResponseDTO>>> GetAll()
        {
            var accounts = await _service.GetAllAccounts();

            return Ok(ApiResponse<List<BankAccountResponseDTO>>.Ok(accounts));
        }

        #endregion

        #region Post

        [Authorize]
        [HttpPost("GetByAccountNumber")]
        public async Task<ActionResult<BankAccountResponseDTO>> GetByAccountNumber(AccountRequestDTO request)
        {
            BankAccountResponseDTO account = await _service.GetByAccountNumber(request.AccountNumber);

            return Ok(ApiResponse<BankAccountResponseDTO>.Ok(account));
        }

        [Authorize]
        [HttpPost("Deposit")]
        public async Task<ActionResult<string>> Deposit(DepositRequestDTO request)
        {
            await _service.Deposit(request);

            return Ok(ApiResponse<string>.Ok(GlobalMessages.SuccessfulDeposit));
        }

        [Authorize]
        [HttpPost("Withdraw")]
        public async Task<ActionResult<string>> Withdraw(WithdrawRequestDTO request)
        {
            await _service.Withdraw(request);

            return Ok(ApiResponse<string>.Ok(GlobalMessages.SuccessfulWithdrawal));
        }

        #endregion
    }
}
