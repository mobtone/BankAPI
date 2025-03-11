using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankApp.Data.DTOs;
using BankApp.Domain.Models;
using Microsoft.AspNetCore.Http;

namespace BankApp.Core.Interfaces
{
    public interface ICustomerService
    {
        RegisteredCustomerResponseDto RegisterCustomerWithAccount(CreateCustomerDto customerDto);
        //För att kunna se alla konton och aktuellt saldo
        CustomerLoginDto CustomerLogin(string email, string securityKey);
        void AddLoanForCustomer(int customerId, int accountId, decimal loanAmount, decimal payments, string status, DateTime date);

       // void PayLoan(int loanId, int customerAccountId, int bankAccountId, decimal paymentAmount);
    }
}
