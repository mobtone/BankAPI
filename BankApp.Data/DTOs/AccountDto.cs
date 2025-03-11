using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankApp.Domain.Models;

namespace BankApp.Data.DTOs
{
    public class AccountDto
    { 
        public int AccountId { get; set; }
        public string AccountType { get; set; } //typ av konto (sparkonto, personkonto osv)
        public decimal Balance { get; set; } //saldo på kontot

       // public string FormattedBalance { get; set; } //formaterar saldot och visar accounttype och saldo tillsammans
       //public List<TransactionDto> Transactions { get; set; } = new List<TransactionDto>(); //transaktioner för kontor

    }
}
