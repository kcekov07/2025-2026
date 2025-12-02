using EcoLoop.Data;
using EcoLoop.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcoLoop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminStoresController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminStoresController(ApplicationDbContext context)
        {
            _context = context;
        }

        // /Admin/AdminStores
        public async Task<IActionResult> Index()
        {
            var stores = await _context.Stores
                .OrderByDescending(s => s.CreatedAt)
                .ToListAsync();

            return View(stores);
        }

        // /Admin/AdminStores/Approve/5
        public async Task<IActionResult> Approve(int id)
        {
            var store = await _context.Stores.FindAsync(id);
            if (store == null) return NotFound();

            store.IsApproved = true;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // /Admin/AdminStores/Reject/5  -> махаме магазина
        public async Task<IActionResult> Reject(int id)
        {
            var store = await _context.Stores.FindAsync(id);
            if (store == null) return NotFound();

            _context.Stores.Remove(store);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: /Admin/AdminStores/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var store = await _context.Stores.FindAsync(id);
            if (store == null) return NotFound();

            return View(store);
        }

        // POST: /Admin/AdminStores/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Store model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var store = await _context.Stores.FindAsync(id);
            if (store == null) return NotFound();

            // обновяваме само основните полета
            store.Name = model.Name;
            store.Category = model.Category;
            store.Description = model.Description;
            store.Address = model.Address;
            store.Latitude = model.Latitude;
            store.Longitude = model.Longitude;
            store.AcceptsOwnPackaging = model.AcceptsOwnPackaging;
            store.Delivery = model.Delivery;
            store.IsProducer = model.IsProducer;
            store.OpeningHours = model.OpeningHours;
            store.IsApproved = model.IsApproved;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // /Admin/AdminStores/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var store = await _context.Stores.FindAsync(id);
            if (store == null) return NotFound();

            _context.Stores.Remove(store);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
