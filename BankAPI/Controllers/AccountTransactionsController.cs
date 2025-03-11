using BankApp.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BankApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountTransactionsController : ControllerBase
    {
        private readonly ITransactionService _service;

        public AccountTransactionsController(ITransactionService service)
        {
            _service = service;
        }

        [Tags("Accounts")]
        [Authorize(Roles = "Customer")]
        [HttpGet("Account/{accountId}Transactions")]
        public IActionResult GetTransactions(int accountId)
        {
            try
            {
                //hämtar customerid från token
                var customerClaim = HttpContext.User.Claims.FirstOrDefault(c => 
                    c.Type == ClaimTypes.NameIdentifier);
                if (customerClaim == null)
                    return Unauthorized(new { Error = "Invalid token: Customer ID is missing." });

                //här anropas servicemetoden för att hämta transaktioner för ett konto
                var transactions = _service.GetTransactionsByAccountId(accountId);

                return Ok(transactions);
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
