using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BankApp.Data.DTOs;
using BankApp.Domain.Models;

namespace BankApp.Data.Interfaces
{
    public interface IAdminRepo
    {
        public Admin GetAdminByEmail (int adminID, string email);

        void UpdateSecurityKey(string email, string securityKey);

        public Admin GetAdminById(int adminId);
        void UpdateAdminPassword(int adminId, string hashedNewPassword);
        Admin GetAdminByEmailAndPassword(string email, string password);
        Admin GetAdminByIdAndEmail(int adminId, string email);
        Admin GetAdminByEmailAndSecurityKey(string email, string securityKey);
        Admin GetAdminByIdEmailAndPassword(int adminId, string email, string passwordHash);
        string GetStoredSecurityKey(string email, string password);
       // Admin GetAdminByIdEmailAndSecurityKey(int adminId, string email, string securityKey);

    }
}
