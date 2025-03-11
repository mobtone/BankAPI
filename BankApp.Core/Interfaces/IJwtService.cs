using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Core.Interfaces
{
    public interface IJwtService
    {
        string GenerateTokenForAdmin(int adminId, string email);


        string GenerateTokenForCustomer(int customerId, string email);


        string GenerateToken(string id, string email, string role);
    }
}
