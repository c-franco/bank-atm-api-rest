using BankATM.Application.Handlers;
using BankATM.Application.Queries;
using BankATM.Domain.Constants;
using BankATM.Domain.Entities;
using BankATM.Domain.Interfaces.Repositories;
using FluentAssertions;
using Moq;

namespace BankATM.Tests.Application.Queries
{
    public class GetAccountByNumberQueryHandlerTests
    {
        private readonly Mock<IBankRepository> _repositoryMock;
        private readonly GetAccountByNumberQueryHandler _handler;

        public GetAccountByNumberQueryHandlerTests()
        {
            _repositoryMock = new Mock<IBankRepository>();
            _handler = new GetAccountByNumberQueryHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnAccount_WhenAccountExists()
        {
            var account = new BankAccount
            {
                AccountNumber = "ES1920956893611111113923",
                Balance = 1500.54m,
                Entity = "TestBank"
            };

            _repositoryMock.Setup(r => r.GetByAccountNumber("ES1920956893611111113923")).ReturnsAsync(account);

            var query = new GetAccountByNumberQuery(new() { AccountNumber = "ES1920956893611111113923" });

            var result = await _handler.Handle(query, CancellationToken.None);

            result.Should().NotBeNull();
            result.AccountNumber.Should().Be("ES1920956893611111113923");
            result.Balance.Should().Be(1500.54m);
            result.Entity.Should().Be("TestBank");
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenAccountDoesNotExist()
        {
            _repositoryMock.Setup(r => r.GetByAccountNumber("ES192095689363923")).ReturnsAsync((BankAccount)null);

            var query = new GetAccountByNumberQuery(new() { AccountNumber = "ES192095689363923" });

            var act = async () => await _handler.Handle(query, CancellationToken.None);

            await act.Should().ThrowAsync<Exception>().WithMessage(GlobalErrors.AccountNotFound);
        }
    }
}
