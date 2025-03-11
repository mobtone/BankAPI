using BankApp.Core.Interfaces;
using BankApp.Core.Services;
using BankApp.Data.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace BankApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {

        //Denna controller ansvarar för inloggning, 
        //och Autentisering av Customers och Admins

        private readonly ICustomerService _customerService;
        private readonly IAdminService _adminService;
        private readonly IJwtService _jwtService;

        public AuthorizationController(IJwtService jwtService, ICustomerService customerService, 
                                        IAdminService adminService)
        {
            _jwtService = jwtService;
            _customerService = customerService;
            _adminService = adminService;
        }

        [AllowAnonymous]
        [Tags("Login")] //detta är bara en hjälp-endpoint för att kunna hämta kundens securitykey enkelt vid test
        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginDto dto)
        {
            try
            {
                //här kontrolleras om användaren är en admin
                var admin = _adminService.AdminLogin(dto.Email, dto.SecurityKey);
                if (admin != null)
                {
                    //här genereras JWT-token för admin
                    var token = _jwtService.GenerateTokenForAdmin(admin.AdminId, admin.Email);

                    return Ok(new
                    {
                        Token = token,
                        Role = "Admin",
                        AdminId = admin.AdminId,
                    });
                }

                //här kontrolleras om användaren är en customer
                var customer = _customerService.CustomerLogin(dto.Email, dto.SecurityKey);
                if (customer != null)
                {
                    //här genereras JWT-token för kunden
                    var customerToken = _jwtService.GenerateTokenForCustomer(customer.CustomerId, customer.Email);

                    return Ok(new
                    {
                        Token = customerToken,
                        Role = "Customer"
                    });
                }

                //om ingen match hittas i databasen- returneras Unauthorized
                return Unauthorized(new { Error = "Invalid email or security key." });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { Error = ex.Message });
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
    }
}
