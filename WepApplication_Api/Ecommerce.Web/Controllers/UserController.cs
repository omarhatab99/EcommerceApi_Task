using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Web.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
