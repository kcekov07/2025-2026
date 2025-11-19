using EcoLoop.Data;
using EcoLoop.Data.Models;
using EcoLoop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcoLoop.Controllers
{
    public class StoreController : Controller
    {
        private readonly ApplicationDbContext context;

        public StoreController(ApplicationDbContext _context)
        {
            context = _context;
        }

        // GET: /Store/All
        public async Task<IActionResult> All()
        {
            var stores = await context.Stores
                .Select(s => new StoreViewModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    Category = s.Category,
                    Address = s.Address,
                    ImageUrl = s.ImageUrl
                })
                .ToListAsync();

            return View(stores);
        }

        // GET: /Store/Add
        public IActionResult Add()
        {
            return View();
        }

        // POST: /Store/Add
        [HttpPost]
        public async Task<IActionResult> Add(StoreAddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var store = new Store
            {
                Name = model.Name,
                Category = model.Category,
                Address = model.Address,
                Description = model.Description,
                AcceptsOwnPackaging = model.AcceptsOwnPackaging,
                ImageUrl = model.ImageUrl
            };

            context.Stores.Add(store);
            await context.SaveChangesAsync();

            return RedirectToAction("All");
        }

        // GET: /Store/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var store = await context.Stores.FindAsync(id);

            if (store == null) return NotFound();

            return View(store);
        }

        // GET: /Store/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var store = await context.Stores.FindAsync(id);
            if (store == null) return NotFound();

            var model = new StoreEditViewModel
            {
                Id = store.Id,
                Name = store.Name,
                Category = store.Category,
                Address = store.Address,
                Description = store.Description,
                AcceptsOwnPackaging = store.AcceptsOwnPackaging,
                ImageUrl = store.ImageUrl
            };

            return View(model);
        }

        // POST: /Store/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(StoreEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var store = await context.Stores.FindAsync(model.Id);
            if (store == null) return NotFound();

            store.Name = model.Name;
            store.Category = model.Category;
            store.Address = model.Address;
            store.Description = model.Description;
            store.AcceptsOwnPackaging = model.AcceptsOwnPackaging;
            store.ImageUrl = model.ImageUrl;

            await context.SaveChangesAsync();

            return RedirectToAction("All");
        }
    }
}
