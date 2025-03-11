using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankApp.Data.DTOs;

namespace BankApp.Core.Interfaces
{
    public interface IAuthorizationService
    {
        public (int CustomerId, int AccountId, string SecurityKey) RegisterCustomer(CreateCustomerDto customerDto);
        public string GetSecurityKeyByEmail(string email);
    }
}
