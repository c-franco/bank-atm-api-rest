using BankATM.Application.Commands;
using BankATM.Domain.Constants;
using BankATM.Domain.Entities;
using BankATM.Domain.Interfaces.Repositories;
using MediatR;

namespace BankATM.Application.Handlers
{
    public class DepositCommandHandler : IRequestHandler<DepositCommand, Unit>
    {
        private readonly IBankRepository _repository;

        public DepositCommandHandler(IBankRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(DepositCommand request, CancellationToken cancellationToken)
        {
            BankAccount account = await _repository.GetByAccountNumber(request.AccountNumber);

            if (account == null)
                throw new Exception(GlobalErrors.AccountNotFound);

            account.Deposit(request.Amount);

            await _repository.SaveAsync();

            return Unit.Value;
        }
    }
}
