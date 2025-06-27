using BankATM.Common.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BankATM.Service.Interface;
using BankATM.Common.Response;

namespace BankATM.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        #region Constructor

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        #endregion

        #region Post

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Auth(LoginRequestDTO request)
        {
            var token = await _authService.AuthenticateAsync(request);

            if (token == null)
                return Unauthorized();

            return Ok(ApiResponse<string>.Ok(token));
        }

        #endregion
    }
}
