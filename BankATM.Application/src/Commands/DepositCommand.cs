using BankATM.Application.DTO;
using MediatR;

namespace BankATM.Application.Commands
{
    public record DepositCommand(DepositRequestDTO Request) : IRequest<Unit>;
}
