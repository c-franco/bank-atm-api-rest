using BankATM.Application.Commands;
using BankATM.Application.DTO;
using BankATM.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankATM.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> Auth([FromBody] LoginRequestDTO request)
        {
            var token = await _mediator.Send(new AuthenticateCommand(request));

            return Ok(ApiResponse<string>.Ok(token));
        }
    }
}
