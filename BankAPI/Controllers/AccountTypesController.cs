using BankApp.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountTypesController : ControllerBase
    {
        private readonly IAccountTypeService _accountTypes;

        public AccountTypesController(IAccountTypeService accountTypes)
        {
            _accountTypes = accountTypes;
        }

        [Tags("Accounts")]
        [Authorize(Roles = "Customer")]
        [HttpGet("GetAccountTypes")]
        public IActionResult GetAccountTypes()
        {
            try
            {
                var accountTypes = _accountTypes.GetAllAccountTypes();
                return Ok(accountTypes);
            }
            catch (Exception ex)
            {
                return BadRequest((new { Error = ex.Message }));
            }
        }
    }
}
