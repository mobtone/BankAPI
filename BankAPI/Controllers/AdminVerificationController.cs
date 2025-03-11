using BankApp.Core.Interfaces;
using BankApp.Data.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminVerificationController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminVerificationController(IAdminService adminService)
        {
            _adminService = adminService;
        }
        [Tags("Admin")]

        [HttpPost("UpdateAdminPassword")]
        public IActionResult UpdatePassword([FromBody] UpdateAdminPasswordDto dto)
        {
            try
            {
                _adminService.UpdateAdminPassword(dto.AdminId, dto.Email, dto.CurrentPassword, dto.NewPassword);
                return Ok("Admin password updated successfully.\n" +
                          "Use your password to get Security Key for admin-login");
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }
        [Tags("Admin")]

        [HttpPost("GetAdminSecrurityKey")]
        public IActionResult GetSecurityKey([FromBody] GetAdminSecurityKeyDto keyDto)
        {
            try
            {
                var securityKey = _adminService.GetSecurityKey(keyDto.Email, keyDto.Password);
                return Ok(new { SecurityKey = securityKey });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }
    }
}