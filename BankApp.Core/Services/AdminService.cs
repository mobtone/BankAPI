using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BankApp.Core.Interfaces;
using BankApp.Data.DTOs;
using BankApp.Data.Interfaces;
using BankApp.Data.Repositories.Repos;
using BankApp.Domain.Models;

namespace BankApp.Core.Services
{
    //Detta är Dto-klassen för Admin och serviceklassen för Admin som 
    //hanterar alla metoder som en Admin kan utföra i applikationen,

    //denna klass använder repository-klassen för att hämta in domain-data
    //och omvandlar datan till dto-klassen som visas utåt för användaren

    /*En Admin ska kunna:
    - Lägga upp nya användarkonton åt en Customer
    - När ett nytt användarkonto läggs upp får Customer automatiskt ett personkonto kopplat till detta
    - Lägga upp lån för en Customer och
    - när kunden tar ett lån ska pengarna sättas in på kundens konto
     */

    public class AdminService : IAdminService
    { 
        private readonly IAdminRepo _repo;
        private readonly IEncryptionHelper _encryptionHelper;
        private readonly IJwtService _jwtService;

        public AdminService(IAdminRepo repo, IEncryptionHelper helper, IJwtService jwtService)
        {
            _repo = repo;
            _encryptionHelper = helper;
            _jwtService = jwtService;
        }

        //servicemetod för att ta emot lösenordet som admin väljer, hasha det och skicka 
        //vidare till admin-repometoden för att spara det till databasen
        public void UpdateAdminPassword(int adminId, string email, string currentPassword, string newPassword)
        {
            //verifierar admin med nuvarande standardlösenord
            var admin = _repo.GetAdminByIdEmailAndPassword(adminId, email, currentPassword);

            if (admin == null)
            {
                throw new UnauthorizedAccessException("Invalid ID, Email or Current password");
            }

            //uppdaterar lösenordet i databasen via repository-metoden
            _repo.UpdateAdminPassword(adminId, newPassword);

            //generera en ny säkerhetsnyckel för att kunna logga in i bankappen
            var newSecurityKey = SecurityKeyGenerator.GenerateSecurityKey();

            //uppdaterrar säkerhetsnyckeln i databasen
            _repo.UpdateSecurityKey(email, newSecurityKey);
        }



        public string GetSecurityKey(string email, string password)
        {
            //verifierar admin med email och lösenord
            var admin = _repo.GetAdminByEmailAndPassword(email, password);

            if (admin == null)
            {
                throw new UnauthorizedAccessException("Invalid email or password");
            }

            //kollar om en säkerhetsnyckel redan finns i db
            var storedKey = _repo.GetStoredSecurityKey(email, password);

            if (!string.IsNullOrEmpty(storedKey))
            {
                //om säkerhetsnyckeln hittas så returneras den
                return storedKey;
            }

            //om säkerhetsnyckeln inte finns så genereras här en ny
            var newSecurityKey = SecurityKeyGenerator.GenerateSecurityKey();

            //här uppdateras säkerhetsnyckeln i databasen
            _repo.UpdateSecurityKey(email, newSecurityKey);

            return newSecurityKey;
        }

        public AdminLoginDto AdminLogin(string email, string securityKey)
        {
            var admin = _repo.GetAdminByEmailAndSecurityKey(email, securityKey);
            if (admin == null)
            {
                return null;
            }

            return new AdminLoginDto
                {
                AdminId = admin.AdminId,
                Email = email,
                SecurityKey = securityKey,
               };
        }
    }
}