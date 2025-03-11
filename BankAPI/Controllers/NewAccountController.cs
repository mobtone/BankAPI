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
    public class NewAccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public NewAccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [Tags("Accounts")]
        [Authorize(Roles = "Customer")]
        [HttpPut("CreateNewAccount")]
        public IActionResult CreateNewAccount([FromBody] CreateCustomerAccountDto dto)
        {
            try
            {
                //här hämtas customerid från token
                var customerClaim = HttpContext.User.Claims.FirstOrDefault(c =>
                    c.Type == ClaimTypes.NameIdentifier);
                if (customerClaim == null)

                    return Unauthorized(new { Error = "Invalid token: Customer ID is missing" });

                int customerId = int.Parse(customerClaim.Value);

                //här anropas servicemetoden för att skapa det nya kontot för kunden
                var newAccount = _accountService.CreateNewAccountForCustomer(customerId, dto.AccountTypeId);

                return Ok(new { Message = "New account created successfully", newAccount });
            }

            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
    }
}