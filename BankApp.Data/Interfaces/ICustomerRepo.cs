using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Tasks;
using BankApp.Data.DataModels;
using BankApp.Data.DTOs;
using BankApp.Domain.Models;

namespace BankApp.Data.Interfaces
{
    public interface ICustomerRepo
    {
        public Customer GetCustomerById(int id);

        //create CreateNewAccount //öppna ett nytt bankkonto
        //public void CreateNewAccount(Account account);


        //List<TransactionDto> GetAccountTransactions(int accountId);


        //kunden har mottagarens kontonr - saldot ska då uppdateras med nya summan)

        //delete subtract amount from account after transaction
        RegisteredCustomerResponseDto CreateCustomerAndAccount(CreateCustomerDto createCustomerDto);

        void AddCustomerLoan(int customerId, int accountId, decimal loanAmount, decimal payments,
            string status, DateTime date);
        string GetCustomerSecurityKey(int customerId, string email);
       // CustomerDto GetCustomerWithAccountsAndTransactions(int customerId);
        CustomerDto ValidateCustomerLogin(string email, string securityKey);

        //void PayCustomerLoan(int loanId, int customerAccountId, int bankAccountId, decimal paymentAmount);
    }
}
