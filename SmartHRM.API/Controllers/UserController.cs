using HUG.CRUD.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartHRM.Repository;
using SmartHRM.Services;
using SmartHRM.Services.Models;

namespace SmartHRM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AccountService _accountService;
        public UserController(AccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet("VerifyToken")]
        [Authorize]
        public IActionResult VerifyToken()
        {
            return Ok("Valid Token");
        }

        [HttpPost("Login")]
        [ProducesResponseType(200, Type = typeof(Account))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Validate(Account account)
        {
            var user = await _accountService.ValidateAccount(account);
            if (user.Status != 200)
            {
                ModelState.AddModelError("", user.StatusMessage);
                return StatusCode(user.Status, ModelState);
            }

            //Cấp token
            return Ok(user);
        }

        [HttpPost("RenewToken")]
        public async Task<IActionResult> RenewToken(TokenModel model)
        {
            var renewToken = await _accountService.RenewToken(model);
            if (renewToken.Status != 200)
            {
                ModelState.AddModelError("", renewToken.StatusMessage);
                return StatusCode(renewToken.Status, ModelState);
            }

            //Cấp token
            return Ok(renewToken);
        }

    }
}
