﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Domain.Models
{
    public class Account
    {
        public int AccountId { get; set; }
        public int AccountTypesId { get; set; }
        public decimal Balance { get; set; }
        public AccountType AccountType { get; set; }
    }
}
