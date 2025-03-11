using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Data.DTOs
{
    public class CustomerDto
    {
            public int CustomerId { get; set; }
            public string Firstname { get; set; }
            public string Lastname { get; set; }
            public string Email { get; set; }

            public List<AccountDto> Accounts { get; set; }
                = new List<AccountDto>();


    }
}
