﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Data.DTOs
{
    public class CustomerLoginDto
    {
        public int CustomerId { get; set; }
        public string Email { get; set; }
        public string SecurityKey { get; set; }
    }
}
