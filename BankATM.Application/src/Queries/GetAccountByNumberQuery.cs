using BankATM.Application.DTO;
using MediatR;

namespace BankATM.Application.Queries
{
    public record GetAccountByNumberQuery(AccountRequestDTO Request) : IRequest<BankAccountResponseDTO>;
}
