using BankApp.Core.Interfaces;
using BankApp.Core.Services;
using BankApp.Data.DTOs;
using BankApp.Data.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace AccountTypeService_Test
{
    //test för att hämta alla kontotyper från databasen 
    public class AccountTypeServiceTest
    {
        private readonly IAccountTypeService _accountTypeService;
        private readonly Mock<IAccountTypeRepo> _accountTypeRepoMock;

        public AccountTypeServiceTest()
        {
            //repository mockas/imiteras
            _accountTypeRepoMock = new Mock<IAccountTypeRepo>();

            //här registreras tjänster och mockade beroenden
            var serviceProvider = new ServiceCollection()
                .AddScoped<IAccountTypeService, AccountTypeService>() //här registreras accounttypeservice
                .AddScoped(_ => _accountTypeRepoMock.Object) //mockar repot
                .BuildServiceProvider();

            _accountTypeService = serviceProvider.GetRequiredService<IAccountTypeService>();
        }


        [Fact]
        public void GetAllAccountTypes_ShouldReturnListOfAllAccountTypes()
        {
            //arrange
            var accountTypes = new List<AccountTypeDto>
            {
                new AccountTypeDto
                {
                    AccountTypeId = 1,
                    TypeName = "Savings", Description = "Savings account"
                },
                new AccountTypeDto
                {
                    AccountTypeId = 2,
                    TypeName = "Checking", Description = "Checking account"
                }
            };

            //här mockas repometoden för att returnera kontotyperna
            _accountTypeRepoMock.Setup(r =>
                r.GetAllAccountTypes()).Returns(accountTypes);

            //act
            var result = _accountTypeService.GetAllAccountTypes();

            //assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }
    }
}
