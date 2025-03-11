using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using BankApp.Core.Interfaces;
using Microsoft.IdentityModel.Tokens;
using BankApp.Data.DataModels;
using Microsoft.Extensions.Configuration;
using BankApp.Data.DTOs;
using Dapper;
using System.Data;
using System.Net.NetworkInformation;
using BankApp.Data.Interfaces;
using BankApp.Domain.Models;

namespace BankApp.Core.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IBankAppContext _dbContext;
        //private readonly ICustomerRepo _repo;
        //private readonly AuthorizationService _service;

        public AuthorizationService(IBankAppContext dbContext/*, ICustomerRepo repo, AuthorizationService service*/)
        {
            _dbContext = dbContext;
            //_repo = repo;
            //_service = service;

        }

        public (int CustomerId, int AccountId, string SecurityKey) RegisterCustomer(CreateCustomerDto customerDto)
        {
            using var connection = _dbContext.GetConnection();

            var parameters = new DynamicParameters();
            parameters.Add("@Firstname", customerDto.Firstname);
            parameters.Add("@Lastname", customerDto.Lastname);
            parameters.Add("@Gender", customerDto.Gender);
            parameters.Add("@Address", customerDto.Address);
            parameters.Add("@City", customerDto.City);
            parameters.Add("@Zipcode", customerDto.Zipcode);
            parameters.Add("@Country", customerDto.Country);
            parameters.Add("@Email", customerDto.Email);

           // parameters.Add("@DateOfBirth", customerDto.DateOfBirth);

            parameters.Add("@GeneratedPassword", dbType: DbType.String, direction: ParameterDirection.Output,
                size: 255);

            var result = connection.QueryFirstOrDefault<(int, int)>("CreateCustomerAndAccount",
                parameters, commandType: CommandType.StoredProcedure);

            var generatedPassword = parameters.Get<string>("@GeneratedPassword");

            // return (result.Item1, result.Item2, generatedPassword);
            var customer = (result.Item1, result.Item2, generatedPassword);
            return customer;
        }

    public string GetSecurityKeyByEmail(string email)
        {
            using var connection = _dbContext.GetConnection();

            var query = "Select SecurityKey From Customers WHERE Emailaddress = @Email";
            var securityKey = connection.QueryFirstOrDefault<string>(query, new { Email = email });

            return securityKey ?? throw new KeyNotFoundException("E-mail could not be found");
        }


    }
}
