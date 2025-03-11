using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankApp.Data.DTOs;
using BankApp.Data.Interfaces;
using BankApp.Domain.Models;
using Dapper;

namespace BankApp.Data.Repositories.Repos
{
    public class TransactionRepo : ITransactionRepo
    {
        private readonly IBankAppContext _context;
        public TransactionRepo(IBankAppContext context)
        {
            _context = context;
        }

        public List<TransactionDto> GetTransactionByAccountId(int accountId)
        {
            var procedure = "GetAccountTransactions";

            using var connection = _context.GetConnection();

            return connection.Query<TransactionDto>(
                procedure, new { AccountId = accountId},
                commandType: CommandType.StoredProcedure).ToList();
        }

        public void TransferFunds(int fromAccountId, int toAccountId, decimal amount)
        {
            var procedure = "TransferFunds";

            var parameters = new DynamicParameters();
            parameters.Add("@FromAccountId", fromAccountId);
            parameters.Add("@ToAccountId", toAccountId);
            parameters.Add("@Amount", amount);

            using var connection = _context.GetConnection();
            connection.Execute(procedure,
                parameters, commandType:
                CommandType.StoredProcedure);
        }
    }
}
