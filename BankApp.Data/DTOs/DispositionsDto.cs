using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Data.DTOs
{
    public class DispositionsDto
    {
        //Denna klass representerar kopplingen mellan customer och accounts (kopplingstabell)
        //eftersom att jag inte behöver ändra någonting i dispositions i programmet så räcker det
        //med en dto-klass för att kunna läsa in datan och transportera datan mellan olika lager i apit
            public int DispositionId { get; set; }
            public int CustomerId { get; set; }
            public int AccountId { get; set; }
            public string Type { get; set; } //exempelvis "OWNER"
    }
}
