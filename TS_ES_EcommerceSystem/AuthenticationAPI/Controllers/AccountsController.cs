using AuthenticationAPI.Models;
using AuthenticationAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.ResponseModel;

namespace AuthenticationAPI.Controllers
{
    /// <summary>
    /// Provides endpoints for managing user accounts.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountServices repo;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountsController"/> class.
        /// </summary>
        /// <param name="_repo">The repository.</param>
        public AccountsController(IAccountServices _repo)
        {
            repo = _repo;
        }

        /// <summary>
        /// Registers a new user account.
        /// </summary>
        /// <param name="register">The user account information.</param>
        /// <returns>A <see cref="IActionResult"/> indicating whether the operation succeeded.</returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register(Register register)
        {
            /// <summary>
            /// Registers a new user account.
            /// </summary>
            /// <param name="register">The user account information.</param>
            /// <returns>A <see cref="IActionResult"/> indicating whether the operation succeeded.</returns>
            var result = await repo.Register(register);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description);
                return Ok(new RegisterResponse { Successful = false, Errors = errors });
            }
            return Ok(new RegisterResponse { Successful = true });
        }

        /// <summary>
        /// Logs in an existing user account.
        /// </summary>
        /// <param name="login">The user account information.</param>
        /// <returns>The user's authentication token.</returns>
        [HttpPost("Login")]
        public async Task<IActionResult> Login(Login login)
        {
            /// <summary>
            /// Logs in an existing user account.
            /// </summary>
            /// <param name="login">The user account information.</param>
            /// <returns>The user's authentication token.</returns>
            var result = await repo.LoginAsync(login);
            return Ok(result);
        }

        /// <summary>
        /// Creates a new employee account.
        /// </summary>
        /// <param name="registerEmployee">The employee account information.</param>
        /// <returns>A <see cref="IActionResult"/> indicating whether the operation succeeded.</returns>
        [HttpPost("create-employee")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> CreateEmployee(RegisterEmployee registerEmployee)
        {
            /// <summary>
            /// Creates a new employee account.
            /// </summary>
            /// <param name="registerEmployee">The employee account information.</param>
            /// <returns>A <see cref="IActionResult"/> indicating whether the operation succeeded.</returns>
            var result = await repo.RegisterEmployee(registerEmployee);
            return Ok(result);
        }

        /// <summary>
        /// Gets a list of available user roles.
        /// </summary>
        /// <returns>A list of available user roles.</returns>
        [HttpGet("GetRoles")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetRoles()
        {
            /// <summary>
            /// Gets a list of available user roles.
            /// </summary>
            /// <returns>A list of available user roles.</returns>
            var result = await repo.GetRoles();
            return Ok(result);
        }
    }
}