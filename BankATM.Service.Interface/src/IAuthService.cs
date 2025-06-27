using BankATM.Common.DTO;

namespace BankATM.Service.Interface
{
    public interface IAuthService
    {
        Task<string> AuthenticateAsync(LoginRequestDTO loginRequest);
    }
}
