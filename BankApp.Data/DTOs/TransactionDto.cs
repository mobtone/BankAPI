using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankApp.Domain.Models;
using System.ComponentModel.DataAnnotations;


namespace BankApp.Data.DTOs
{
    public class TransactionDto
    {
        public int TransactionId { get; set; }
        public int AccountId { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; } //typ av transaktion, credit eller debit
        public string Operation { get; set; } //beskrivning av operationen som utförs
        public decimal Amount { get; set; } //belopp för transaktionen
        public decimal Balance { get; set; } //saldo efter transaktionen

        public int? RelatedAccount { get; set; } //om det är en avsändare/mottagare för en transaktion ? används för att det inte behöver finnas någon
    }
}
