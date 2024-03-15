using Microsoft.AspNetCore.Mvc;

namespace simpleWebAppUsingIdentity.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
