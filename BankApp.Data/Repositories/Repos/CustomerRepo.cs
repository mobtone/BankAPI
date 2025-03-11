using System.ComponentModel;
using System.Data;
using System.Diagnostics.Metrics;
using System.Net;
using System.Reflection.Emit;
using System.Reflection;
using System.Runtime.InteropServices.JavaScript;
using BankApp.Data.DataModels;
using BankApp.Data.DTOs;
using BankApp.Data.Interfaces;
using BankApp.Domain.Models;
using Dapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Castle.Core.Resource;
using Microsoft.Identity.Client;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BankApp.Data.Repositories.Repos
{
    public class CustomerRepo : ICustomerRepo
    {
        private readonly IBankAppContext _context;

        public CustomerRepo(IBankAppContext context)
        {
            _context = context;
        }

        public RegisteredCustomerResponseDto CreateCustomerAndAccount(CreateCustomerDto createCustomerDto)
        { 
            //parametrar som skickas till stored proceduren
            var parameters = new DynamicParameters();
            parameters.Add("@Firstname", createCustomerDto.Firstname);
            parameters.Add("@Lastname", createCustomerDto.Lastname);
            parameters.Add("@Gender", createCustomerDto.Gender);
            parameters.Add("@Address", createCustomerDto.Address);
            parameters.Add("@City", createCustomerDto.City);
            parameters.Add("@Zipcode", createCustomerDto.Zipcode);
            parameters.Add("@Country", createCustomerDto.Country);
            parameters.Add("@Email", createCustomerDto.Email);

            //outputparameter för säkerhetsnyckeln
            parameters.Add("@GeneratedPassword", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);

            using var connection = _context.GetConnection();

            //här anropas proceduren och returnerar customerid och accountid
            var result = connection.QueryFirstOrDefault<(int CustomerId, int AccountId)>(
                "CreateCustomerAndAccount",
                parameters,
                commandType: CommandType.StoredProcedure);

            //hämta den genererade säkerhetsnyckeln från outputparameter
            var securityKey = parameters.Get<string>("@GeneratedPassword");

            return new RegisteredCustomerResponseDto
            {
                CustomerId = result.CustomerId,
                AccountId = result.AccountId,
                SecurityKey = securityKey
            };
        }
        public Customer GetCustomerById(int id)
        {
            throw new NotImplementedException();
        }

        public void AddCustomerLoan(int customerId, int accountId, decimal loanAmount, decimal payments, 
            string status, DateTime date)
        {
            var procedure = "AddLoanForCustomer";

            var parameters = new DynamicParameters();
            parameters.Add("@CustomerId", customerId);
            parameters.Add("@AccountId", accountId);
            parameters.Add("@LoanAmount", loanAmount);
            parameters.Add("@Payments", payments);
            parameters.Add("@Status", status);
            parameters.Add("@Date", date);

            using var connection = _context.GetConnection();

            connection.Execute(procedure, parameters,
                commandType: CommandType.StoredProcedure);
        }

        public string GetCustomerSecurityKey(int customerId, string email)
        {
            using var connection = _context.GetConnection();

            var query = @"SELECT SecurityKey 
                  FROM Customers 
                  WHERE Id = @CustomerId AND Emailaddress = @Email";

            return connection.QueryFirstOrDefault<string>(query, new { CustomerId = customerId, Email = email });
        }

        public CustomerDto ValidateCustomerLogin(string email, string securityKey)
        {
            var query = "ValidateCustomerLogin";
            using var connection = _context.GetConnection();

            //anropar sp med parametrar från dtoklassen
            var customer = connection.QuerySingleOrDefault<CustomerDto>(
                query, new
                {
                    Email = email,
                    SecurityKey = securityKey
                }, commandType: CommandType.StoredProcedure);

            if (customer == null)
                throw new UnauthorizedAccessException("Invalid email or security key");

            return customer;
        }
    }
}


//public void CreateNewAccount(Account account)
//    {
//        throw new NotImplementedException();
//    }

//public List<AccountDto> GetCustomerAccounts(int customerId)
//{
//    var procedure = "GetCustomerAccounts";

//    using var connection = _context.GetConnection();

//    return connection.Query<AccountDto>(
//        procedure, new { CustomerId = customerId },
//        commandType: CommandType.StoredProcedure).ToList();
//}
//public List<TransactionDto> GetAccountTransactions(int accountId)
//{
//    var procedure = "GetAccountTransactions";

//    using var connection = _context.GetConnection();

//    return connection.Query<TransactionDto>(
//        procedure, new { AccountId = accountId }, 
//        commandType: CommandType.StoredProcedure).ToList();
//}


//public void PayCustomerLoan(int loanId, int customerAccountId, int bankAccountId, decimal paymentAmount)
//{
//    var procedure = "PayLoan";

//    var parameters = new DynamicParameters();
//    parameters.Add("@LoanId", loanId);
//    parameters.Add("@CustomerAccountId", customerAccountId);
//    parameters.Add("@BankAccountId", bankAccountId);
//    parameters.Add("@PaymentAmount", paymentAmount);

//    using var connection = _context.GetConnection();
//    connection.Execute(procedure, parameters,
//        commandType: CommandType.StoredProcedure);
//}