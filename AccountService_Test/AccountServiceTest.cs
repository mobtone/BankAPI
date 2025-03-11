using BankApp.Core.Interfaces;
using BankApp.Core.Services;
using BankApp.Data.DTOs;
using BankApp.Data.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace AccountService_Test
{ //test f�r att skapa nytt konto f�r en kund och 
    //kontrollera att ett nytt konto kopplas till customer
    public class AccountServiceTest
    {
        private readonly IAccountService _accountService;
        private readonly Mock<IAccountRepo> _accountRepoMock;

        public AccountServiceTest()
        {
            //h�r mockas accountrepo
            _accountRepoMock = new Mock<IAccountRepo>();

            //registrera tj�nster och mockade di's
            var serviceProvider = new ServiceCollection()
                .AddScoped<IAccountService, AccountService>() //h�r registreras accountService
                .AddScoped(_ => _accountRepoMock.Object) //h�r mockas repot
                .BuildServiceProvider();

            _accountService = serviceProvider.GetRequiredService<IAccountService>();
        }


        [Fact]
        public void CreateNewCustomerAccount_ShouldCreateAndLinkAccountToCustomer()
        {
            //arrange
            var customerId = 1001;
            var accountTypeId = 2;

            var newAccount = new NewAccountDto { AccountId = 12345 };

            //h�r mockas repometoder
            _accountRepoMock.Setup(r =>
                    r.CreateNewAccountForCustomer(customerId, accountTypeId))
                .Returns(newAccount);

            //act
            var result = _accountService.CreateNewAccountForCustomer(customerId, accountTypeId);

            //assert
            //h�r kontrolleras att resultatet inte �r null och att kundens kontonr blir 12345
            Assert.NotNull(result);
            Assert.Equal(12345, result.AccountId);

            _accountRepoMock.Verify(r =>
                r.CreateNewAccountForCustomer(customerId, accountTypeId),
                Times.Once);


        }
    }
}