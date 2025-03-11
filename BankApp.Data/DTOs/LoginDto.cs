using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Data.DTOs
{
    public class LoginDto
    {
        [Required]
        [EmailAddress(ErrorMessage = "A valid e-mail must contain a '@'")]
        public string Email { get; set; }
        [Required]
        public string SecurityKey { get; set; }
    }
}
