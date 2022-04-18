using Finance.Models;
using Microsoft.EntityFrameworkCore;

namespace Finance.DataAccess
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Revenue> Revenues { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<RevenueCategory> RevenueCategories { get; set; }
        public DbSet<ExpenseCategory> ExpenseCategories { get; set; }
    }
}
