using CatApllication.Data;
using CatApllication.Data.Models;
using CatApllication.Models;
using Microsoft.AspNetCore.Mvc;

namespace CatApllication.Controllers
{
    public class CatController : Controller
    {
        private readonly ApplicationDbContext db;
        public CatController(ApplicationDbContext db)
        {
            this.db = db;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Add()//Get
        {
            return View();
        }
        [HttpPost]
       
        public IActionResult Add(CatViewModel model)
        {
            Cat cat = new Cat
            {
               Name = model.Name,
               Age = model.Age,
               Breed = model.Breed,
               ImageURL = model.ImageURL
            };
            db.Cats.Add(cat);
            db.SaveChanges();
            return Redirect("/Home/Index");
        }
        public IActionResult All()    //Read all records 
        {
            List<CatViewModel> model = db.Cats.Select(x => new CatViewModel 
            {
                Id = x.Id,
                Name=x.Name,
                Age=x.Age,
                Breed=x.Breed,
                ImageURL=x.ImageURL 
            }
            ).ToList();
            return View(model);
        }
        public IActionResult Details(int id)
        {
            var cat = db.Cats.FirstOrDefault(x => id == x.Id);
            CatViewModel model = new CatViewModel
            {
                Name = cat.Name,
                Age=cat.Age,
                Breed =cat.Breed,
                ImageURL=cat.ImageURL
            };

            return View(model);
        }
        public IActionResult Edit(int id)//Get

        {
            var cat = db.Cats.FirstOrDefault(x => id == x.Id);
            CatViewModel model = new CatViewModel
            {
                Id=cat.Id,
                Name = cat.Name,
                Age = cat.Age,
                Breed = cat.Breed,
                ImageURL = cat.ImageURL
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(CatViewModel model)//Update
        {
            var cat = db.Cats.FirstOrDefault(x => x.Id == model.Id);
            cat.Name = model.Name;
            cat.Age = model.Age;
            cat.Breed = model.Breed;
            cat.ImageURL = model.ImageURL;
            db.SaveChanges();
            
            
            return Redirect("/Cat/All");
        }
        public IActionResult Delete(int id) 
        {
            Cat cat = db.Cats.FirstOrDefault(x => id == x.Id);
            db.Cats.Remove(cat);
            db.SaveChanges();
            return Redirect("/Cat/All");
        }

    }
}
