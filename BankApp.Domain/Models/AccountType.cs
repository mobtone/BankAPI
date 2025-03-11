using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Domain.Models
{
    public class AccountType
    {
        public int AccountTypesId { get; set; } //PrimaryKey som kopplas med Account
        public string TypeName { get; set; }

    }
}
