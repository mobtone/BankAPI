using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Core.Interfaces
{
    public interface IEncryptionHelper
    {
        string HashValue(string value);
        //public  string Encrypt(string plainText);
        //public  string Decrypt(string encryptedText);
    }
}
