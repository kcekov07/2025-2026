using Bookfavourites.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bookfavourites.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(CategoryViewModel model)
        {
            return Redirect("/Home/Index");
        }
    }
}
