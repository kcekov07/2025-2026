using FirstWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace FirstWebApp.Controllers
{
    public class ArticleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public DataViewModel ById(string Name)
        {
            var model = new DataViewModel();
            model.Now = DateTime.Now;
            model.ProcessorCount=Environment.ProcessorCount;
            model.Name = Name;
            return model;
        }
        public IActionResult Demo()
        {
            var model= new DataViewModel();
            model.Now = DateTime.Now;
            model.ProcessorCount=Environment.ProcessorCount;
            model.Name = "Ivan Ivanov";
            return View(model);
        }
        public IActionResult Student()
        {
            var model= new StudentViewModel();
            model.Id = 12;
            model.Name = "Kalin Ivanov";
            model.Age = 18;
            return View(model);
        }
    }
}
