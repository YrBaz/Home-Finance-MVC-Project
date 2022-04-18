using System.Collections.Generic;

namespace Finance.Models
{
    public class ExpenseCategory
    {
        public ExpenseCategory()
        {
            Expenses = new List<Expense>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        ICollection<Expense> Expenses { get; set; }
    }
}
