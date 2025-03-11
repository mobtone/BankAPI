using BankApp.Core.Interfaces;
using BankApp.Core.Services;
using BankApp.Data.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace TransactionService_Test
{

    //test som kontrollerar att pengar dras från avsändarkonto och 
    //läggs till på mottagarkontot
    public class TransactionServiceTest
    {
        private readonly ITransactionService _transactionService;
        private readonly Mock<ITransactionRepo> _transactionRepoMock;

        public TransactionServiceTest()
        {
            //här mockas transactionrepo
            _transactionRepoMock = new Mock<ITransactionRepo>();

            //här registreras tjänster och mockade beroenden
            var serviceProvider = new ServiceCollection()
                .AddScoped<ITransactionService, TransactionService>() //registrerar TransactionService
                .AddScoped(_ => _transactionRepoMock.Object) //mockar repository
                .BuildServiceProvider();

            _transactionService = serviceProvider.GetRequiredService<ITransactionService>();
        }



        [Fact]
        public void TransferFunds_ShouldTransferAmountBetweenAccounts()
        {
            //arrange
            var fromAccountId = 1;
            var toAccountId = 2;
            var transferAmount = 200m;

            //act
            _transactionService.TransferFunds(fromAccountId, toAccountId, transferAmount);

            //assert
            _transactionRepoMock.Verify(r=>
                r.TransferFunds(fromAccountId, toAccountId, transferAmount),
                Times.Once);
        }
    }
}