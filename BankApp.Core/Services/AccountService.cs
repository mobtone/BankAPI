using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankApp.Core.Interfaces;
using BankApp.Data.DTOs;
using BankApp.Data.Interfaces;

namespace BankApp.Core.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepo _repo;

        public AccountService(IAccountRepo repo)
        {
            _repo = repo;
        }

        public NewAccountDto CreateNewAccountForCustomer(int customerId, int accountTypeId)
        {
            if (accountTypeId <= 0)
                throw new UnauthorizedAccessException("Invalid account type");

            return _repo.CreateNewAccountForCustomer(customerId, accountTypeId);
        }

        public List<AccountDto> GetCustomerAccounts(int customerId)
        {
            return _repo.GetCustomerAccounts(customerId);
        }
    }
}
