using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankApp.Data.DTOs;
using BankApp.Domain.Models;

namespace BankApp.Data.Interfaces
{
    public interface IAccountTypeRepo
    {
        public AccountType GetAccountTypeById(int id);
        public List<AccountTypeDto> GetAllAccountTypes();
    }
}
