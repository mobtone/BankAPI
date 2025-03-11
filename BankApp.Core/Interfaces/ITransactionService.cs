using BankApp.Data.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Core.Interfaces
{
    public interface ITransactionService
    {
        List<TransactionDto> GetTransactionsByAccountId(int accountId);

        void TransferFunds(int fromAccountId, int toAccountId, decimal amount);
    }
}
