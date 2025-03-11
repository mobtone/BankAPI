using BankApp.Data.Interfaces;
using BankApp.Domain.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;


namespace BankApp.Data.DataModels
{
    public class BankAppDbContext : IBankAppContext
    {
        private readonly string _connectionString;

        //här används IConfiguration för att kunna hämta in connectionstring från appsettings.json
        public BankAppDbContext(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("BankAppData");
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }

        }
    }

