using Microsoft.AspNetCore.Mvc;

namespace VisiblePT.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
