using BankATM.Application.DTO;
using MediatR;

namespace BankATM.Application.Queries
{
    public record GetAllAccountsQuery() : IRequest<List<BankAccountResponseDTO>>;
}
