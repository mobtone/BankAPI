using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BankApp.Data.Interfaces;
using BankApp.Domain.Models;
using BankApp.Data.DTOs;

namespace BankApp.Core.Interfaces
{
    public interface IAdminService
    {
        //public void UpdateSecurityKey(int adminId, string securityKey);

        public string GetSecurityKey(string email, string password);

        AdminLoginDto AdminLogin(string email, string securityKey);

        void UpdateAdminPassword(int adminId, string email, string currentPassword, string newPassword);
    }
}
