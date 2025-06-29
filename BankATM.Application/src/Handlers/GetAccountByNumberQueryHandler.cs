using BankATM.Application.DTO;
using BankATM.Application.Queries;
using BankATM.Domain.Constants;
using BankATM.Domain.Interfaces.Repositories;
using MediatR;

namespace BankATM.Application.Handlers
{
    public class GetAccountByNumberQueryHandler : IRequestHandler<GetAccountByNumberQuery, BankAccountResponseDTO>
    {
        private readonly IBankRepository _repository;

        public GetAccountByNumberQueryHandler(IBankRepository repository)
        {
            _repository = repository;
        }

        public async Task<BankAccountResponseDTO> Handle(GetAccountByNumberQuery request, CancellationToken cancellationToken)
        {
            var account = await _repository.GetByAccountNumber(request.AccountNumber);

            if (account == null)
                throw new Exception(GlobalErrors.AccountNotFound);

            return new BankAccountResponseDTO
            {
                AccountNumber = account.AccountNumber,
                Balance = account.Balance,
                Entity = account.Entity,
            };
        }
    }
}
