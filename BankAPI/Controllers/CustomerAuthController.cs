using BankApp.Core.Interfaces;
using BankApp.Core.Services;
using BankApp.Data.DTOs;
using BankApp.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;
using IAuthorizationService = BankApp.Core.Interfaces.IAuthorizationService;

namespace BankApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerAuthController : ControllerBase
    {
        private readonly IAuthorizationService _authService;


        public CustomerAuthController(IAuthorizationService authService)
        {
            _authService = authService;
        }

        [Tags("Customer")] //detta är bara en hjälp-endpoint för att kunna hämta kundens securitykey enkelt vid test
        [HttpGet("SecurityKey")]
        public IActionResult GetSecurityKey([FromQuery] string email)
        {
            try
            {
                var securityKey = _authService.GetSecurityKeyByEmail(email);
                return Ok(new { SecurityKey = securityKey });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }


        //[Tags("Login")]
        //[HttpPost("CustomerLogin")]
        //public IActionResult CustomerLogin([FromBody] CustomerLoginDto dto)
        //{
        //    try
        //    {
        //        //här genereras en token om inloggningen lyckas
        //        var token = _service.CustomerLogin(dto.Email, dto.SecurityKey);

        //        return Ok(new
        //        {
        //            Token = token,
        //            Role = "Customer"

        //        });
        //    }
        //    catch (UnauthorizedAccessException ex)
        //    {
        //        return Unauthorized(new { Error = ex.Message });
        //    }
        //}
    }
}
