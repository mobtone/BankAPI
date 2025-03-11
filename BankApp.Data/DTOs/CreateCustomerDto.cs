using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Tasks;
using BankApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.MicrosoftExtensions;
using Microsoft.VisualBasic;

namespace BankApp.Data.DTOs
{
    public class CreateCustomerDto
    {
        [Required(ErrorMessage = "Firstname is required")]
        [StringLength(100, ErrorMessage = "Firstname is too long")]
        public string Firstname { get; set; }

        [Required(ErrorMessage = "Lastname is required")]
        [StringLength(100, ErrorMessage = "Lastname is too long")]
        public string Lastname { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        [StringLength(6, ErrorMessage = "Gender-specification is too long")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [StringLength(100, ErrorMessage = "Address is too long")]
        public string Address { get; set; }

        [Required(ErrorMessage = "City is required")]
        [StringLength(100, ErrorMessage = "City is too long")]
        public string City { get; set; }

        [Required(ErrorMessage = "Zipcode is required")]
        [StringLength(15, ErrorMessage = "Zipcode is too long")]
        public string Zipcode { get; set; }

        [Required(ErrorMessage = "Country is required")]
        [StringLength(100, ErrorMessage = "")]
        public string Country { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "A valid e-mail must contain a '@'")]
        public string Email { get; set; }

       // public DateTime? DateOfBirth { get; set; }
    }
}
