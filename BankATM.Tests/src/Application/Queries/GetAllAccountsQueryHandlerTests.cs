using BankATM.Application.DTO;
using BankATM.Application.Handlers;
using BankATM.Application.Queries;
using BankATM.Domain.Entities;
using BankATM.Domain.Interfaces.Repositories;
using FluentAssertions;
using Moq;

namespace BankATM.Tests.Application.Queries
{
    public class GetAllAccountsQueryHandlerTests
    {
        private readonly Mock<IBankRepository> _repositoryMock;
        private readonly GetAllAccountsQueryHandler _handler;

        public GetAllAccountsQueryHandlerTests()
        {
            _repositoryMock = new Mock<IBankRepository>();
            _handler = new GetAllAccountsQueryHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnAllAccounts()
        {
            var accounts = new List<BankAccount>
            {
                new BankAccount { AccountNumber = "ES1920956893611111113923", Entity = "BankA", Balance = 1140.94m },
                new BankAccount { AccountNumber = "ES6420386343135175761749", Entity = "BankB", Balance = 2055.23m }
            };

            _repositoryMock.Setup(r => r.GetAll()).ReturnsAsync(accounts);

            var query = new GetAllAccountsQuery();

            var result = await _handler.Handle(query, CancellationToken.None);

            result.Should().NotBeNull();
            result.Should().HaveCount(2);

            result.Should().ContainEquivalentOf(new BankAccountResponseDTO
            {
                AccountNumber = "ES1920956893611111113923",
                Entity = "BankA",
                Balance = 1140.94m
            });

            result.Should().ContainEquivalentOf(new BankAccountResponseDTO
            {
                AccountNumber = "ES6420386343135175761749",
                Entity = "BankB",
                Balance = 2055.23m
            });
        }

        [Fact]
        public async Task Handle_ShouldReturnEmptyList_WhenNoAccountsExist()
        {
            _repositoryMock.Setup(r => r.GetAll()).ReturnsAsync(new List<BankAccount>());

            var query = new GetAllAccountsQuery();

            var result = await _handler.Handle(query, CancellationToken.None);

            result.Should().NotBeNull();
            result.Should().BeEmpty();
        }
    }
}
