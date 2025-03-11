using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankApp.Core.Interfaces;

namespace BankApp.Core.Services
{
    public class SecurityKeyGenerator : ISecurityKeyGenerator
    {
        public static string GenerateSecurityKey()
        {
            const int keyLength = 32;
            var random = new Random();
            var key = new StringBuilder();

            for (int i = 0; i < keyLength; i++)
            {
                var character = (char)random.Next(33, 126); // Generera tecken från ASCII-tabellen
                key.Append(character);
            }

            return key.ToString();
        }
    }
}
