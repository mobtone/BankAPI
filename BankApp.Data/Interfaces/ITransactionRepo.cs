using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankApp.Data.DTOs;
using BankApp.Domain.Models;

namespace BankApp.Data.Interfaces
{
    public interface ITransactionRepo
    {
        public List<TransactionDto> GetTransactionByAccountId(int accountId);

        public void TransferFunds(int fromAccountId, int toAccountId, decimal amount);

    }
}
