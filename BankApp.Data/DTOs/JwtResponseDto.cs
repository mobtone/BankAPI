using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Data.DTOs
{
    public class JwtResponseDto
    {
        public string Token { get; set; }
        public string Role { get; set; }
    }
}
