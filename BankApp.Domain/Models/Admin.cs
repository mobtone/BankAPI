using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace BankApp.Domain.Models
{
    public class Admin
    { 
        public int AdminId { get; set; }
        public string ResponsibleEmployeeMail { get; set; }
        public string AdminPassword { get; set; }
        public string SecurityKeyHash { get; set; }

    }
}
