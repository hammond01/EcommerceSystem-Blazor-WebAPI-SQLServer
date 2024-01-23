using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Server.Helper.JWTModel;
using Server.Repositories.Interfaces;

namespace Server.Controllers
{
    public class AuthenticationsController(IAccountRoleServices _repo) : ConBase
    {
        [HttpGet("get-roles")]
        public async Task<IActionResult> GetCategories()
        {
            try
            {
                var res = await _repo.GetRoles();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("get-role/{name}")]
        public async Task<IActionResult> GetRoleByName(string name)
        {
            try
            {
                var res = await _repo.GetRoleByName(name);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(Users users)
        {
            try
            {
                var res = await _repo.Register(users);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("login")]
        public async Task<IActionResult> Register(LoginModel login)
        {
            try
            {
                var res = await _repo.LoginUser(login.UserName, login.Password);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
