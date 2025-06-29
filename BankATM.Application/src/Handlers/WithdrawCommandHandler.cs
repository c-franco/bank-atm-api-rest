using BankATM.Application.Commands;
using BankATM.Domain.Constants;
using BankATM.Domain.Interfaces.Repositories;
using MediatR;

namespace BankATM.Application.Handlers
{
    public class WithdrawCommandHandler : IRequestHandler<WithdrawCommand, Unit>
    {
        private readonly IBankRepository _repository;

        public WithdrawCommandHandler(IBankRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(WithdrawCommand request, CancellationToken cancellationToken)
        {
            var account = await _repository.GetByAccountNumber(request.AccountNumber);

            if (account == null)
                throw new Exception(GlobalErrors.AccountNotFound);

            account.Withdraw(request.Amount);

            await _repository.SaveAsync();

            return Unit.Value;
        }
    }
}
