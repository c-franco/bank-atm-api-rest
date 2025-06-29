using BankATM.Application.Commands;
using BankATM.Application.DTO;
using BankATM.Application.Queries;
using BankATM.Domain.Constants;
using BankATM.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankATM.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BankController : Controller
    {
        private readonly IMediator _mediator;

        #region Constructor

        public BankController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #endregion

        #region Get

        [Authorize]
        [HttpGet("All")]
        public async Task<ActionResult<List<BankAccountResponseDTO>>> GetAll()
        {
            var result = await _mediator.Send(new GetAllAccountsQuery());

            return Ok(ApiResponse<List<BankAccountResponseDTO>>.Ok(result));
        }

        #endregion

        #region Post

        [Authorize]
        [HttpPost("GetByAccountNumber")]
        public async Task<ActionResult<BankAccountResponseDTO>> GetByAccountNumber(AccountRequestDTO request)
        {
            var result = await _mediator.Send(new GetAccountByNumberQuery(request));

            return Ok(ApiResponse<BankAccountResponseDTO>.Ok(result));
        }

        [Authorize]
        [HttpPost("Deposit")]
        public async Task<ActionResult<string>> Deposit(DepositRequestDTO request)
        {
            await _mediator.Send(new DepositCommand(request));

            return Ok(ApiResponse<string>.Ok(GlobalMessages.SuccessfulDeposit));
        }

        [Authorize]
        [HttpPost("Withdraw")]
        public async Task<ActionResult<string>> Withdraw(WithdrawRequestDTO request)
        {
            await _mediator.Send(new WithdrawCommand(request));

            return Ok(ApiResponse<string>.Ok(GlobalMessages.SuccessfulWithdrawal));
        }

        #endregion
    }
}
