using BankATM.Application.DTO;
using MediatR;

namespace BankATM.Application.Commands
{
    public record AuthenticateCommand(LoginRequestDTO Request) : IRequest<string>;
}
