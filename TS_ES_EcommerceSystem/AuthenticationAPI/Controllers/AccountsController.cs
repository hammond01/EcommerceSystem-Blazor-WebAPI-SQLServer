using AuthenticationAPI.Models;
using AuthenticationAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountServices repo;

        public AccountsController(IAccountServices _repo)
        {
            repo = _repo;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(Register register)
        {
            var result = await repo.Register(register);
            if (result.Succeeded)
            {
                return Ok(result.Succeeded);
            }
            return BadRequest();
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(Login login)
        {
            var result = await repo.LoginAsync(login);
            return Ok(result);
        }

        [HttpPost("create-employee")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> CreateEmloyee(RegisterEmloyee registerEmloyee)
        {
            var result = await repo.RegisterEmployee(registerEmloyee);
            return Ok(result);
        }

        [HttpGet("GetRoles")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetRoles()
        {
            var result = await repo.GetRoles();
            return Ok(result);
        }
    }
}
