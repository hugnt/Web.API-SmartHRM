using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartHRM.Repository;
using SmartHRM.Services;

namespace SmartHRM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AccountService _accountService;
        public AccountController(AccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("Verify")]
        [ProducesResponseType(200, Type = typeof(Account))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Validate(Account account)
        {
            var user = await _accountService.ValidateUsernameAndPassword(account);
            if (user.Status != 200)
            {
                ModelState.AddModelError("", user.StatusMessage);
                return StatusCode(user.Status, ModelState);
            }

            return Ok(user);
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Account>))]
        public IActionResult GetAccounts()
        {
            var accounts = _accountService.GetAccounts();
            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(accounts);
        }

        [HttpGet("Role/{roleId}")]
        [ProducesResponseType(200, Type = typeof(Role))]
        [ProducesResponseType(400)]
        public IActionResult GetRole(int roleId)
        {
            var role = _accountService.GetRoleById(roleId);
            if (!ModelState.IsValid) return BadRequest();
            if (role == null) return NotFound();
            return Ok(role);
        }

        [HttpGet("Role")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult GetRoles()
        {
            var roles = _accountService.GetRoles();
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(roles);
        }

        [HttpGet("{accountId}")]
        [ProducesResponseType(200, Type = typeof(Account))]
        [ProducesResponseType(400)]
        public IActionResult GetAccount(int accountId)
        {
            var account = _accountService.GetAccount(accountId);
            if (!ModelState.IsValid) return BadRequest();
            if (account == null) return NotFound();
            return Ok(account);
        }

        [HttpGet("GetById/{accountId}")]
        [ProducesResponseType(200, Type = typeof(Account))]
        [ProducesResponseType(400)]
        public IActionResult GetAccount2(int accountId)
        {
            var account = _accountService.GetAccountById2(accountId);
            if (!ModelState.IsValid) return BadRequest();
            if (account == null) return NotFound();
            return Ok(account);
        }

        [HttpGet("AccountInfor/{accountId}")]
        [ProducesResponseType(200, Type = typeof(Account))]
        [ProducesResponseType(400)]
        public IActionResult GetAccountInfor(int accountId)
        {
            var account = _accountService.GetAccountInfoById(accountId);
            if (!ModelState.IsValid) return BadRequest();
            if (account == null) return NotFound();
            return Ok(account);
        }


        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult CreateAccount([FromBody] Account accountCreate)
        {
            if (accountCreate == null) return BadRequest(ModelState);

            var res = _accountService.CreateAccount(accountCreate);

            if (res.Status != 201)
            {
                ModelState.AddModelError("", res.StatusMessage);
                return StatusCode(res.Status, ModelState);
            }

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return StatusCode(res.Status, res.StatusMessage);
        }

        [HttpPut("{accountId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateAccountAsync(int accountId, [FromForm] string oldPassword, [FromForm] Account updatedAccount)
        {
            if (updatedAccount == null) return BadRequest(ModelState);
            if (accountId != updatedAccount.Id) return BadRequest(ModelState);

            var validateAccount = new Account()
            {
                Username = updatedAccount.Username,
                Password = oldPassword,
            };

            var user = await _accountService.ValidateUsernameAndPassword(validateAccount);
            if (user.Status != 200)
            {
                ModelState.AddModelError("", user.StatusMessage);
                return StatusCode(user.Status, ModelState);
            }

            var res = _accountService.UpdateAccount(accountId, updatedAccount);
            if (res.Status != 204)
            {
                ModelState.AddModelError("", res.StatusMessage);
                return StatusCode(res.Status, ModelState);
            }

          
            if (!ModelState.IsValid) return BadRequest(ModelState);

            return NoContent();
        }

        [HttpPut("Cheat/{accountId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateAccountCheat(int accountId, [FromBody] Account updatedAccount)
        {
            if (updatedAccount == null) return BadRequest(ModelState);
            if (accountId != updatedAccount.Id) return BadRequest(ModelState);

            var res = _accountService.UpdateAccountByAdmin(accountId, updatedAccount);
            if (res.Status != 204)
            {
                ModelState.AddModelError("", res.StatusMessage);
                return StatusCode(res.Status, ModelState);
            }


            if (!ModelState.IsValid) return BadRequest(ModelState);

            return NoContent();
        }


        [HttpDelete("{accountId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteAccount(int accountId)
        {
            var res = _accountService.DeleteAccount(accountId);
            if (res.Status != 204)
            {
                ModelState.AddModelError("", res.StatusMessage);
                return StatusCode(res.Status, ModelState);
            }

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return NoContent();
        }


        [HttpPut("DeletedStatus/{AccountId}/{status}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateDeleteStatus(int AccountId, bool status)
        {

            var res = _accountService.UpdateDeleteStatus(AccountId, status);
            if (res.Status != 204)
            {
                ModelState.AddModelError("", res.StatusMessage);
                return StatusCode(res.Status, ModelState);

            }
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return NoContent();
        }



    }
}
