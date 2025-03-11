using BankApp.Data.Interfaces;
using BankApp.Domain.Models;
using System;
using System.Data;
using Dapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;


namespace BankApp.Data.Repositories.Repos
{
    public class AdminRepo : IAdminRepo
    {
        //alla metoder som hör till en admin som pratar med databasen

        private readonly IBankAppContext _context;

        public AdminRepo(IBankAppContext context)
        {
            _context = context;
        }


        public void UpdateAdminPassword(int adminId, string newPassword)
        {
            var query = "UpdateAdminPassword";

            _context.GetConnection().Execute(
                query,
                new { AdminId = adminId, NewPassword = newPassword },
                commandType: CommandType.StoredProcedure);
        }

        public Admin GetAdminByEmailAndPassword(string email, string password)
        {
            using (var connection = _context.GetConnection())
            {
                var query = "SELECT * FROM Admins WHERE ResponsibleEmployeeMail = @Email AND AdminPassword = @Password";
                return _context.GetConnection().QueryFirstOrDefault<Admin>(
                    query,
                    new { Email = email, Password = password });
            }
           
        }

        public Admin GetAdminById(int adminId)
        {
            using (var connection = _context.GetConnection())
            {
                var query = "SELECT * FROM Admins WHERE AdminId = @AdminId";
                return connection.QueryFirstOrDefault<Admin>(query, new { AdminId = adminId });
            }
        }

        public Admin GetAdminByEmail(int adminID, string email)
        {
            var query = "SELECT * FROM Admins WHERE ResponsibleEmployeeMail = @Email";
            return _context.GetConnection().QueryFirstOrDefault<Admin>(query, new { Email = email });
        }

        public Admin GetAdminByIdAndEmail(int adminId, string email)
        {
            var query = "GetAdminByIdAndEmail";
            var admin = _context.GetConnection().QueryFirstOrDefault<Admin>(
                query, new { AdminId = adminId, Email = email },
                commandType: CommandType.StoredProcedure);

            return admin;
        }

        public Admin GetAdminByEmailAndSecurityKey(string email, string securityKey)
        {
            var query = "GetAdminByEmailAndSecurityKey";

            var admin = _context.GetConnection().QueryFirstOrDefault<Admin>(
                query,
                new { Email = email, SecurityKey = securityKey },
                commandType: CommandType.StoredProcedure);

            return admin;
        }

        public Admin GetAdminByIdEmailAndPassword(int adminId, string email, string currentPassword)
        {
            var query = "SELECT * FROM Admins WHERE AdminId = @AdminId AND ResponsibleEmployeeMail = @Email AND AdminPassword = @CurrentPassword";
            return _context.GetConnection().QueryFirstOrDefault<Admin>(
                query, new { AdminId = adminId, Email = email, CurrentPassword = currentPassword });
        }

        public string GetStoredSecurityKey(string email, string password)
        {
            var query = "SELECT SecurityKeyHash FROM Admins WHERE ResponsibleEmployeeMail = @Email AND AdminPassword = @Password";
            return _context.GetConnection().QueryFirstOrDefault<string>(
                query,
                new { Email = email, Password = password });
        }

        public void UpdateSecurityKey(string email, string securityKey)
        {
            var query = "UPDATE Admins SET SecurityKeyHash = @SecurityKey WHERE ResponsibleEmployeeMail = @Email";

            _context.GetConnection().Execute(query, new { Email = email, SecurityKey = securityKey });
        }

    }
}
