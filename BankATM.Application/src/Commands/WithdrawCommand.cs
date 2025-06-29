using BankATM.Application.DTO;
using MediatR;

namespace BankATM.Application.Commands
{
    public record WithdrawCommand(WithdrawRequestDTO Request) : IRequest<Unit>;
}
