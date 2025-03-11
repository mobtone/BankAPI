using BankApp.Core.Interfaces;
using BankApp.Data.DTOs;
using BankApp.Data.Interfaces;
using BankApp.Domain.Models;
using Microsoft.Identity.Client;

namespace BankApp.Core.Services
{
    public class AccountTypeService : IAccountTypeService
    {
        private readonly IAccountTypeRepo _repo;
        public AccountTypeService(IAccountTypeRepo repo)
        {
            _repo = repo;
        }
        public AccountType GetAccountTypeById(int id)
        {
            throw new NotImplementedException();
        }

        public List<AccountTypeDto> GetAllAccountTypes()
        {
            return _repo.GetAllAccountTypes();
        }
    }
}
