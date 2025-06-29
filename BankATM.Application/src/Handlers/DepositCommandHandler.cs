using BankATM.Application.Commands;
using BankATM.Domain.Constants;
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

        public async Task<Unit> Handle(DepositCommand command, CancellationToken cancellationToken)
        {
            var account = await _repository.GetByAccountNumber(command.Request.AccountNumber);

            if (account == null)
                throw new Exception(GlobalErrors.AccountNotFound);

            account.Deposit(command.Request.Amount);

            await _repository.SaveAsync();

            return Unit.Value;
        }
    }
}
