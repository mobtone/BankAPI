using System.Security.Claims;
using BankApp.Core.Interfaces;
using BankApp.Data.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanController : ControllerBase
    {
        private readonly ICustomerService _service;

        public LoanController(ICustomerService service)
        {
            _service = service;
        }

        [Tags("Admin")]
        [Authorize(Roles = "Admin")]
        [HttpPost("AddCustomerLoan")]
        public IActionResult AddLoan([FromBody] LoanDto dto)
        {
            try
            {
                //hämtar adminId från token
                var adminIdClaim = HttpContext.User.Claims.FirstOrDefault(c =>
                    c.Type == ClaimTypes.NameIdentifier);

                if (adminIdClaim == null) 
                    return Unauthorized(new { Error = "Invalid token: AdminI ID is missing" });
                
                int adminId = int.Parse(adminIdClaim.Value);

                //här anropas´servicemetoden för att lägga upp lånet för kunden
                _service.AddLoanForCustomer(dto.CustomerId, dto.AccountId, dto.LoanAmount, dto.Payments, dto.Status, dto.Date);

                return Ok(new { Message = "Loan successfully added to customer" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
        
    }
}
