using Finance.DataAccess;
using Finance.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Finance.Controllers
{
    public class RevenueCategoryController : Controller
    {
        private readonly AppDbContext _appDbContext;
        public RevenueCategoryController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var revenueCategoryes = await _appDbContext.RevenueCategories.ToListAsync();
            return View(revenueCategoryes);
        }

        [HttpGet]
        public IActionResult Create() => View(new RevenueCategory());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RevenueCategory model)
        {
            if (ModelState.IsValid)
            {
                await _appDbContext.RevenueCategories.AddAsync(model);
                await _appDbContext.SaveChangesAsync();

                TempData["Success"] = "Category has been added!";

                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await _appDbContext.RevenueCategories.FindAsync(id);
            await _appDbContext.SaveChangesAsync();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(RevenueCategory model)
        {
            if (ModelState.IsValid)
            {
                _appDbContext.RevenueCategories.Update(model);
                await _appDbContext.SaveChangesAsync();

                TempData["Success"] = "Category has been updated!";

                return RedirectToAction("Index");
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var model = await _appDbContext.RevenueCategories.FindAsync(id);

            if(model == null)
            {
                return NotFound();
            }
            else
            {
                _appDbContext.RevenueCategories.Remove(model);
                await _appDbContext.SaveChangesAsync();

                TempData["Success"] = "Category has been deleted!";
            }

            return RedirectToAction("Index");
        }
    }
}
