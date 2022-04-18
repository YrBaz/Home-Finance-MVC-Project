using System;

namespace Finance.Models
{
    public class Expense
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }
        public int ExpenseCategoryId { get; set; }
        public DateTime CreatedDate { get; set; }
        public virtual ExpenseCategory ExpenseCategory { get; set; }

        public Expense()
        {
            CreatedDate = DateTime.Now;
        }
    }
}
