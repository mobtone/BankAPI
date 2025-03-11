using AutoMapper.Configuration.Annotations;
using BankApp.Core.Interfaces;
using BankApp.Data.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace BankApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        //Behöriga admins läggs till manuellt i databasen för att de vid inloggning
        //ska kunna matcha sin företagsmail mot DB och då kunna hämta in sin säkerhetsnyckel
        //via en http-get metod (simulerar ett email eller sms) och därefter logga in

        private readonly ICustomerService _service;
        private readonly IAdminService _adminService;
        private readonly IJwtService _jwtService;

        public AdminController(ICustomerService service, IAdminService adminService, IJwtService jwtService)
        {
            _service = service;
            _adminService = adminService;
            _jwtService = jwtService;
        }


        [Tags("Admin")]
        [Authorize(Roles = "Admin")]
        [HttpPost("RegisterCustomer")]
        public IActionResult RegisterCustomer([FromBody] CreateCustomerDto customerDto)
        {
            try
            {
                var result = _service.RegisterCustomerWithAccount(customerDto);

                return Ok(new
                {
                    Message = "Customer and account registered successfully.",
                    CustomerId = result.CustomerId,
                    AccountId = result.AccountId,
                    SecurityKey = result.SecurityKey
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }
    }
}