using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankApp.Data.DTOs;
using BankApp.Domain.Models;

namespace BankApp.Data.Interfaces
{
    public interface IAccountRepo
    {
        //public Account GetAccountById(int id);
        //public List<Account> GetAccountsByCustomerId(int customerId);

        public NewAccountDto CreateNewAccountForCustomer(int customerId, int accountTypeId);

        List<AccountDto> GetCustomerAccounts(int customerId);
    }
}
