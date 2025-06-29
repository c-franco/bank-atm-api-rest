using MediatR;

namespace BankATM.Application.Commands
{
    public record DepositCommand(string AccountNumber, decimal Amount) : IRequest<Unit>;
}
