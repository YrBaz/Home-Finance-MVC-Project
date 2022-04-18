using System;

namespace Finance.Models
{
    public class Revenue
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }
        public int RevenueCategoryId { get; set; }
        public DateTime CreatedDate { get; set; }
        public virtual RevenueCategory RevenueCategory { get; set; }

        public Revenue()
        {
            CreatedDate = DateTime.Now;
        }
    }
}
