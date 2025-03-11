using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Data.DTOs
{
    public class CreateCustomerAccountDto
    {
        [Required] 
        public int AccountTypeId { get; set; } //id för kontotypen som kunden väljer
    }
}
