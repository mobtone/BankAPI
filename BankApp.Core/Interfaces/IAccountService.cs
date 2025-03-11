using BankApp.Data.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Core.Interfaces
{
    public interface IAccountService
    {
        NewAccountDto CreateNewAccountForCustomer(int customerId, int accountTypeId);

        List<AccountDto> GetCustomerAccounts(int customerId);

    }
}
