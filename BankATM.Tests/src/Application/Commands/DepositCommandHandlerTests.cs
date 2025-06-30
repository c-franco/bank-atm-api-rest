﻿using BankATM.Application.Commands;
using BankATM.Application.Handlers;
using BankATM.Domain.Constants;
using BankATM.Domain.Entities;
using BankATM.Domain.Interfaces.Repositories;
using FluentAssertions;
using Moq;

namespace BankATM.Tests.Application.Commands
{
    public class DepositCommandHandlerTests
    {
        private readonly Mock<IBankRepository> _repositoryMock;
        private readonly DepositCommandHandler _handler;

        public DepositCommandHandlerTests()
        {
            _repositoryMock = new Mock<IBankRepository>();
            _handler = new DepositCommandHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldDeposit_WhenAccountExistsAndAmountIsValid()
        {
            var account = new BankAccount
            {
                AccountNumber = "ES1920956893611111113923",
                Balance = 1000
            };

            _repositoryMock.Setup(r => r.GetByAccountNumber("ES1920956893611111113923")).ReturnsAsync(account);

            var command = new DepositCommand(new() { AccountNumber = "ES1920956893611111113923", Amount = 500 });

            await _handler.Handle(command, CancellationToken.None);

            account.Balance.Should().Be(1500);
            _repositoryMock.Verify(r => r.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenAccountDoesNotExist()
        {
            _repositoryMock.Setup(r => r.GetByAccountNumber("ES192095689361113923")).ReturnsAsync((BankAccount)null);

            var command = new DepositCommand(new() { AccountNumber = "ES192095689361113923", Amount = 500 });

            var act = async () => await _handler.Handle(command, CancellationToken.None);

            await act.Should().ThrowAsync<Exception>().WithMessage(GlobalErrors.AccountNotFound);
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenAmountExceedsLimit()
        {
            var account = new BankAccount
            {
                AccountNumber = "ES1920956893611111113923",
                Balance = 1000
            };

            _repositoryMock.Setup(r => r.GetByAccountNumber("ES1920956893611111113923")).ReturnsAsync(account);

            var command = new DepositCommand(new() { AccountNumber = "ES1920956893611111113923", Amount = 5000 });

            var act = async () => await _handler.Handle(command, CancellationToken.None);

            await act.Should().ThrowAsync<InvalidOperationException>().WithMessage(GlobalErrors.DepositLimitExceeded);

            _repositoryMock.Verify(r => r.SaveAsync(), Times.Never);
        }
    }
}
