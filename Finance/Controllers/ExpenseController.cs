using Finance.DataAccess;
using Finance.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Finance.Controllers
{
    public class ExpenseController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public ExpenseController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var expenses = await _appDbContext.Expenses.Include(e => e.ExpenseCategory).ToListAsync();

            return View(expenses);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewData["ExpenseCategoryId"] = new SelectList(_appDbContext.ExpenseCategories, "Id", "Name");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Expense model)
        {
            if (ModelState.IsValid)
            {
                await _appDbContext.Expenses.AddAsync(model);
                await _appDbContext.SaveChangesAsync();

                TempData["Success"] = "Expense has been added!";

                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            ViewData["ExpenseCategoryId"] = new SelectList(_appDbContext.ExpenseCategories, "Id", "Name");

            var model = await _appDbContext.Expenses.FindAsync(id);
            await _appDbContext.SaveChangesAsync();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Expense model)
        {
            if (ModelState.IsValid)
            {
                _appDbContext.Expenses.Update(model);
                await _appDbContext.SaveChangesAsync();

                TempData["Success"] = "Expense has been updated!";

                return RedirectToAction("Index");
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var model = await _appDbContext.Expenses.FindAsync(id);

            if (model == null)
            {
                return NotFound();
            }
            else
            {
                _appDbContext.Expenses.Remove(model);
                await _appDbContext.SaveChangesAsync();

                TempData["Success"] = "Expense has been deleted!";
            }

            return RedirectToAction("Index");
        }
    }
}
