using BankATM.Application.DTO;
using MediatR;

namespace BankATM.Application.Queries
{
    public record GetAccountByNumberQuery(string AccountNumber) : IRequest<BankAccountResponseDTO>;
}
