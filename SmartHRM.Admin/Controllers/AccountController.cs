using Microsoft.AspNetCore.Mvc;
using SmartHRM.Admin.Attributes;

namespace SmartHRM.Admin.Controllers
{
    [CustomAuthorize("Admin")]
    public class AccountController : Controller
    {

        [Route("Account")]
        [Route("Account/AccountList")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("Account/AccountList/Trash")]
        public IActionResult AccountListTrash()
        {
            return View();
        }

        [Route("Account/RoleList")]
        public IActionResult RoleList()
        {
            return View();
        }

        [Route("Account/RoleList/Trash")]
        public IActionResult RoleListTrash()
        {
            return View();
        }

    }
}
