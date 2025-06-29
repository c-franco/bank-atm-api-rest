using BankATM.Application.DTO;
using BankATM.Application.Queries;
using BankATM.Domain.Interfaces.Repositories;
using MediatR;

namespace BankATM.Application.Handlers
{
    public class GetAllAccountsQueryHandler : IRequestHandler<GetAllAccountsQuery, List<BankAccountResponseDTO>>
    {
        private readonly IBankRepository _repository;

        public GetAllAccountsQueryHandler(IBankRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<BankAccountResponseDTO>> Handle(GetAllAccountsQuery request, CancellationToken cancellationToken)
        {
            var accounts = await _repository.GetAll();

            return accounts.Select(a => new BankAccountResponseDTO
            {
                AccountNumber = a.AccountNumber,
                Entity = a.Entity,
                Balance = a.Balance
            }).ToList();
        }
    }
}
