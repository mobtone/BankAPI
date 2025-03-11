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
    public class AccountTypeRepo : IAccountTypeRepo
    {
        private readonly IBankAppContext _context;

        public AccountTypeRepo(IBankAppContext context)
        {
            _context = context;
        }

        public AccountType GetAccountTypeById(int id)
        {
            throw new NotImplementedException();
        }

        public List<AccountTypeDto> GetAllAccountTypes()
        {
            var procedure = "GetAccountTypes";
            using var connection = _context.GetConnection();

            return connection.Query<AccountTypeDto>(procedure,
                commandType: CommandType.StoredProcedure).ToList();
        }
    }
}
