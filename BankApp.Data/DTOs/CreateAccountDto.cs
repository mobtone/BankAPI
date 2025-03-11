using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Data.DTOs
{
    public class CreateAccountDto
    {
        //Detta är egenskaperna som krävs för att kunna skapa ett nytt account
        //som blir kopplat till en Customer efter skapandet

        public CreateCustomerDto Customer { get; set; }
        public string AccountTypeName { get; set; }
    }
}
