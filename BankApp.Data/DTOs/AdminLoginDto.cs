using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Data.DTOs
{
    public class AdminLoginDto
    {
        public int AdminId { get; set; }
        public string Email { get; set; }
        public string SecurityKey { get; set; }
    }
}