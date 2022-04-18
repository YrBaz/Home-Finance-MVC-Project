using Finance.DataAccess;
using Finance.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Finance.Controllers
{
    public class RevenueController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public RevenueController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var revenues = await _appDbContext.Revenues.Include(r => r.RevenueCategory).ToListAsync();

            return View(revenues);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewData["RevenueCategoryId"] = new SelectList(_appDbContext.RevenueCategories, "Id", "Name");
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Revenue model)
        {
            if (ModelState.IsValid)
            {
                await _appDbContext.Revenues.AddAsync(model);
                await _appDbContext.SaveChangesAsync();

                TempData["Success"] = "Income has been added!";

                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            ViewData["RevenueCategoryId"] = new SelectList(_appDbContext.RevenueCategories, "Id", "Name");

            var model = await _appDbContext.Revenues.FindAsync(id);
            await _appDbContext.SaveChangesAsync();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Revenue model)
        {
            if (ModelState.IsValid)
            {
                _appDbContext.Revenues.Update(model);
                await _appDbContext.SaveChangesAsync();

                TempData["Success"] = "Income has been updated!";

                return RedirectToAction("Index");
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var model = await _appDbContext.Revenues.FindAsync(id);

            if(model == null)
            {
                return NotFound();
            }
            else
            {
                _appDbContext.Revenues.Remove(model);
                await _appDbContext.SaveChangesAsync();

                TempData["Success"] = "Income has been deleted!";
            }

            return RedirectToAction("Index");
        }
    }
}
