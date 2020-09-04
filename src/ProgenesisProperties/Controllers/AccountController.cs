using Microsoft.AspNetCore.Mvc;

namespace ProgenesisProperties.Controllers
{

    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult SignUp ()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login ()
        {
            return View ();
        }
    }

}