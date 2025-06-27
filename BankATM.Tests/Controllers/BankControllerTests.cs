using BankATM.Common.Constants;
using BankATM.Common.DTO;
using BankATM.Common.Response;
using BankATM.Controllers;
using BankATM.Domain;
using BankATM.Service.Interface;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Moq;

namespace BankATM.Tests.Controllers
{
    public class BankControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly Mock<IBankService> _serviceMock;
        private readonly BankController _controller;

        public BankControllerTests()
        {
            _serviceMock = new Mock<IBankService>();
            _controller = new BankController(_serviceMock.Object);
        }

        [Fact]
        public async Task GetAll_ShouldReturnAllAccounts()
        {
            var accounts = new List<BankAccount> {
                new BankAccount { AccountNumber = "ES6031901591125842183313", Balance = 100.51m },
                new BankAccount { AccountNumber = "ES4720958157318193827371", Balance = 4303.27m}};

            _serviceMock.Setup(x => x.GetAllAccounts()).ReturnsAsync(accounts);

            var result = await _controller.GetAll();

            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();

            var response = okResult.Value as ApiResponse<List<BankAccount>>;
            response.Data.Should().BeEquivalentTo(accounts);
        }

        [Fact]
        public async Task GetByAccountNumber_ShouldReturnAccount()
        {
            string accNum = "ES6031901591125842183313";
            var account = new BankAccount { AccountNumber = accNum, Balance = 100.51m };
            _serviceMock.Setup(x => x.GetByAccountNumber(accNum)).ReturnsAsync(account);

            var result = await _controller.GetByAccountNumber(accNum);

            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();

            var response = okResult.Value as ApiResponse<BankAccount>;
            response.Data.AccountNumber.Should().Be(accNum);
        }

        [Fact]
        public async Task Deposit_ShouldSucceed_WhenAmountIsValid()
        {
            var request = new DepositRequestDTO { AccountNumber = "ES6031901591125842183313", Amount = 1000 };
            _serviceMock.Setup(x => x.Deposit(request)).Returns(Task.CompletedTask);

            var result = await _controller.Deposit(request);

            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();

            var response = okResult.Value as ApiResponse<string>;
            response.Data.Should().Be(GlobalMessages.SuccessfulDeposit);
        }

        [Fact]
        public async Task Withdraw_ShouldSucceed_WhenAmountIsValid()
        {
            var request = new WithdrawRequestDTO { AccountNumber = "ES6031901591125842183313", Amount = 500 };
            _serviceMock.Setup(x => x.Withdraw(request)).Returns(Task.CompletedTask);

            var result = await _controller.Withdraw(request);

            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();

            var response = okResult.Value as ApiResponse<string>;
            response.Data.Should().Be(GlobalMessages.SuccessfulWithdrawal);
        }

        [Theory]
        [InlineData(3500, GlobalErrors.DepositLimitExceeded)]
        public void Deposit_ShouldThrow_WhenAmountExceedsLimit(decimal amount, string expectedError)
        {
            var account = new BankAccount();
            var action = () => account.Deposit(amount);

            action.Should().Throw<InvalidOperationException>().WithMessage(expectedError);
        }

        [Theory]
        [InlineData(1100, 2000, GlobalErrors.WithdrawLimitExceeded)]
        [InlineData(750, 500, GlobalErrors.InsufficientFunds)]
        public void Withdraw_ShouldThrow_WhenInvalid(decimal amount, decimal balance, string expectedError)
        {
            var account = new BankAccount { Balance = balance };
            var action = () => account.Withdraw(amount);

            action.Should().Throw<InvalidOperationException>().WithMessage(expectedError);
        }
    }
}
