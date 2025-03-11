using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Data.DTOs
{
    public class LoanDto
    {
        [Required]
        public int CustomerId { get; set; }

        [Required]
        public int AccountId { get; set; }

        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal LoanAmount { get; set; }

        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Payments { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        public DateTime Date { get; set; }
        //[Required]
      //  public int RepaymentAccountId { get; set; }
    }
}
