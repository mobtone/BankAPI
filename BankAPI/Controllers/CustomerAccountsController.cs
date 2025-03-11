using BankApp.Core.Interfaces;
using BankApp.Data.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;
using System.Security.Claims;


namespace BankApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerAccountsController : ControllerBase
    {
        //Denna controller hanterar alla api-anrop relaterade till Customers, ink. 
        //Registrering & hantering av kundinformation - denna klass ska använda Dtos för att ta emot och skicka data


        //dependency injection av accountService med hjälpmetoder för att visa kundens konton
        private readonly IAccountService _accountService;


        public CustomerAccountsController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [Tags("Accounts")]
        [Authorize(Roles = "Customer")]
        [HttpGet("AccountOverview")] 
        public IActionResult GetCustomerAccounts()
        {
            try
            {
                //hämtar customerId från tokenen
                var customerIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                if (customerIdClaim == null)
                    return Unauthorized(new { Error = "Invalid token: Customer ID is missing." });

                int customerId = int.Parse(customerIdClaim.Value);

                //här anropas servicemetoden för att hämta in kundens konton
                var accounts = _accountService.GetCustomerAccounts(customerId);

                return Ok(accounts);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { Error = ex.Message });
            }

            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
    }
}
