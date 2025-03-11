using System;
using System.Runtime.InteropServices.JavaScript;

namespace BankApp.Domain.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string Gender { get; set; }
        public string Givenname { get; set; }
        public string Surname { get; set; }
        public string Streetaddress { get; set; }
        public string City { get; set; }
        public string Zipcode { get; set; }
        public string Country { get; set; }
        public string CountryCode { get; set; }
      //  public DateTime? Birthday { get; set; } //? innebär att det kan vara nullable
      //  public string Telephonecountrycode { get; set; }
       // public string Telephonenumber { get; set; }
        public string Emailaddress { get; set; }

        //Egenskaper som krävs för att kunna registrera en kund (som admin)
        public Account Account { get; set; }


    }
}
