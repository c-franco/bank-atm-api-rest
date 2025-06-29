using MediatR;

namespace BankATM.Application.Commands
{
    public record WithdrawCommand(string AccountNumber, decimal Amount) : IRequest<Unit>;
}
