using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankApp.Core.Interfaces;
using BankApp.Data.DTOs;
using BankApp.Data.Interfaces;
using BankApp.Domain.Models;

namespace BankApp.Core.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepo _repo;

        public TransactionService(ITransactionRepo repo)
        {
            _repo = repo;
        }

        public List<TransactionDto> GetTransactionsByAccountId(int accountId)
        {
            return _repo.GetTransactionByAccountId(accountId);
        }


        public void TransferFunds(int fromAccountId, int toAccountId, decimal amount)
        {
            if (amount <= 0)
            
                throw new ArgumentException("Transfer amount cannot be less than 0");
            if (fromAccountId == toAccountId)
                throw new ArgumentException("Source and destination accounts cannot be the same");

            _repo.TransferFunds(fromAccountId, toAccountId, amount);
        }
      
    }
}
