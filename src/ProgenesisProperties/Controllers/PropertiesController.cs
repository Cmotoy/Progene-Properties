using Microsoft.AspNetCore.Mvc;

namespace ProgenesisProperties.Controllers
{
    public class PropertiesController : Controller
    {
        public IActionResult Index ()
        {
            return View ();
        }
    }
}