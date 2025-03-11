using BankApp.Core.Interfaces;
using BankApp.Data.DTOs;
using BankApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BankApp.Data.Interfaces;
using BankApp.Data.Repositories.Repos;
using BankApp.Data.DataModels;
using Microsoft.AspNetCore.Http;

namespace BankApp.Core.Services
{
    public class CustomerService : ICustomerService
    {
        //Detta är serviceklassen för customer som 
        //hanterar alla metoder som en Customer kan utföra i applikationen,
        //denna klass använder repository-klassen för att hämta in domain-data
        //och omvandlar datan till dto-klassen som visas utåt för användaren

        /*En Customer ska kunna:
       - Se alla sina konton och aktuellt saldo på dessa (typ av konto + saldo)
       - Gå in på varje konto separat och se transaktioner som genomförts
       - Öppna nya bankkonton till sig själv
       - Göra överföringar mellan egna konton och till andra konton med mottagarens kontonr
       - Om en överföring görs så ska pengar dras från kundens konto och läggas in på mottagarens konto
         */

        private readonly ICustomerRepo _repo;
        private readonly IMapper _mapper;
        private readonly IJwtService _jwtService;

        public CustomerService(ICustomerRepo repo, IMapper mapper, IJwtService jwtService)
        {
            _repo = repo;
            _mapper = mapper;
            _jwtService = jwtService;
        }

        public RegisteredCustomerResponseDto RegisterCustomerWithAccount(CreateCustomerDto customerDto)
        {
            return _repo.CreateCustomerAndAccount(customerDto);
        }

        public string GetCustomerSecurityKey(int customerId, string email)
        {
            //kontrollerar om användaren har rätt att hämta sin säkerhetsnyckel genom att matcha användarid och email till behörig kund
            var securityKey = _repo.GetCustomerSecurityKey(customerId, email);

            if (securityKey == null)
            {
                throw new UnauthorizedAccessException("Security key can not be retrieved");
            }
            return securityKey;
        }

        public CustomerLoginDto CustomerLogin(string email, string securityKey)
        {
            //här valideras kunden med hjälp av repometoden
            var customer = _repo.ValidateCustomerLogin(email, securityKey);

            if (customer == null)
            {
                return null;
            }

            return new CustomerLoginDto
            {
                CustomerId = customer.CustomerId,
                Email = email,
                SecurityKey = securityKey
            };
        }

        public void AddLoanForCustomer(int customerId, int accountId, decimal loanAmount, decimal payments, string status, DateTime date)
        {
            //här kontrolleras att lånebeloppet inte är mindre än 0
            if (loanAmount <= 0)
                throw new ArgumentException("Loan amount must be greater than zero");


            if (string.IsNullOrWhiteSpace(status))
                throw new ArgumentException("Status cannot be empty");

            //här anropas repometoden för att lägga upp lånet
            _repo.AddCustomerLoan(customerId, accountId, loanAmount, payments, status, date);
        }
    }
}
//HttpContext.User.Claims- används för att hämta claims från den autentiserade användarens Jwt-token,
//om token saknar claimet customerId så returneras ett undantag med fel-statuskod unauthorized




//public CustomerDto GetCustomerWithAccountsAndTransactions(HttpContext httpContext)
////httpcontext används för att hämta claims från den autentiserade användaren 
//{
//    var customerIdClaim = httpContext.User.Claims.FirstOrDefault(c =>
//        c.Type == ClaimTypes.NameIdentifier);

//    if (customerIdClaim == null)
//    {
//        throw new UnauthorizedAccessException("Invalid token: CustomerId is missing");
//    }

//    int customerId = int.Parse(customerIdClaim.Value);

//    //här anropas repoometoden för att hämta kundens data om konton och transaktioner
//    return _repo.GetCustomerWithAccountsAndTransactions(customerId);
//}



//public void PayLoan(int loanId, int customerAccountId, int bankAccountId, decimal paymentAmount)
//{
//    //här kontrolleras att beloppet är mer än 0
//    if (paymentAmount <= 0)
//        throw new ArgumentException("Payment amount must be greater than zero");

//    //anropa repometod för att genomföra betalningen på lånet
//    _repo.PayCustomerLoan(loanId, customerAccountId, bankAccountId, paymentAmount);
//}