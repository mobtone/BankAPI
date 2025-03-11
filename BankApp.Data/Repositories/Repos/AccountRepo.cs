using BankApp.Data.Interfaces;
using BankApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankApp.Data.DTOs;
using Dapper;

namespace BankApp.Data.Repositories.Repos
{
    public class AccountRepo : IAccountRepo
    {
        private readonly IBankAppContext _context;
        public AccountRepo(IBankAppContext context)
        {
            _context = context;
        }

        //public Account GetAccountById(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //public List<Account> GetAccountsByCustomerId(int customerId)
        //{
        //    throw new NotImplementedException();
        //}

        public NewAccountDto CreateNewAccountForCustomer(int customerId, int accountTypeId)
        {
            var procedure = "CreateNewAccountForCustomer";

            var parameters = new DynamicParameters();
            parameters.Add("@CustomerId", customerId);
            parameters.Add("@AccountTypeId", accountTypeId);

            using var connection = _context.GetConnection();

            return connection.QuerySingleOrDefault<NewAccountDto>(
                procedure, parameters, 
                commandType: CommandType.StoredProcedure);

        }
        public List<AccountDto> GetCustomerAccounts(int customerId)
        {
            var procedure = "GetCustomerAccounts";

            using var connection = _context.GetConnection();

            return connection.Query<AccountDto>(
                procedure, new { CustomerId = customerId },
                commandType: CommandType.StoredProcedure).ToList();
        }

    }
}
