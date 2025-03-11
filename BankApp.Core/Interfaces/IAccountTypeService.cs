using BankApp.Data.DTOs;
using BankApp.Data.Interfaces;
using BankApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Core.Interfaces
{
    public interface IAccountTypeService
    {
        public AccountType GetAccountTypeById(int id);

        public List<AccountTypeDto> GetAllAccountTypes();
    }
}
