using Finance.DataAccess;
using Finance.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Finance.Controllers
{
    public class ExpenseCategoryController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public ExpenseCategoryController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var expenseCategoryes = await _appDbContext.ExpenseCategories.ToListAsync();

            return View(expenseCategoryes);
        }

        [HttpGet]
        public IActionResult Create() => View(new ExpenseCategory());

        [HttpPost]
        public async Task<IActionResult> Create(ExpenseCategory model)
        {
            if (ModelState.IsValid)
            {
                await _appDbContext.ExpenseCategories.AddAsync(model);
                await _appDbContext.SaveChangesAsync();

                TempData["Success"] = "Category has been added!";

                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await _appDbContext.ExpenseCategories.FindAsync(id);
            await _appDbContext.SaveChangesAsync();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ExpenseCategory model)
        {
            if (ModelState.IsValid)
            {
                _appDbContext.ExpenseCategories.Update(model);
                await _appDbContext.SaveChangesAsync();

                TempData["Success"] = "Category has been updated!";

                return RedirectToAction("Index");
            }
            return View(model);
        }

        public async Task<IActionResult> Delete (int id)
        {
            var model = await _appDbContext.ExpenseCategories.FindAsync(id);

            if (model == null)
            {
                return NotFound();
            }
            else
            {
                _appDbContext.ExpenseCategories.Remove(model);
                await _appDbContext.SaveChangesAsync();

                TempData["Success"] = "Category has been deleted!";
            }

            return RedirectToAction("Index");
        }
    }
}
