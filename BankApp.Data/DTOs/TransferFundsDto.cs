using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Data.DTOs
{
    public class TransferFundsDto
    {
        [Required]
        public int FromAccountId { get; set; } // Avsändarens konto

        [Required]
        public int ToAccountId { get; set; } // Mottagarens konto

        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Amount { get; set; } // Belopp att överföra
    }
}
