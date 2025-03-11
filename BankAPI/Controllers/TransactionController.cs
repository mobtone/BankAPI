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
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _service;

        public TransactionController(ITransactionService service)
        {
            _service = service;
        }

        [Tags("Accounts")]
        [Authorize(Roles = "Customer")]
        [HttpPost("TransferFunds")]
        public IActionResult TransferFunds([FromBody] TransferFundsDto dto)
        {
            try
            {
                var customerClaim = HttpContext.User.Claims.FirstOrDefault(c =>
                    c.Type == ClaimTypes.NameIdentifier);

                if (customerClaim == null)
                    return Unauthorized(new { Error = "Invalid token: Customer ID is missing" });

                int customerId = int.Parse(customerClaim.Value);

                _service.TransferFunds(dto.FromAccountId, dto.ToAccountId, dto.Amount);

                return Ok((new { Message = "Transfer succesfully completed" }));
            }

            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
    }
}
