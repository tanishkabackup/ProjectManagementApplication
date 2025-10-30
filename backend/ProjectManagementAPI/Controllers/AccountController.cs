using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Application.Dtos.Request;
using ProjectManagement.Application.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace ProjectManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly AccountService _accountService;
        public AccountController(AccountService accountService)
        {
            _accountService = accountService;
        }

        [SwaggerOperation(Summary = "Register User")]
        [ApiExplorerSettings(GroupName = "v1")]
        [HttpPost]
        [Route("RegisterUser")]
        public async Task<IActionResult> RegisterUser(RegisterUserRequest registerUserRequest)
        {
            var response = await _accountService.RegisterUserAsync(registerUserRequest);
            return Ok(response);
        }
    }
}
