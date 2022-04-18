using System.Collections.Generic;

namespace Finance.Models
{
    public class RevenueCategory
    {
        public RevenueCategory()
        {
            Revenues = new List<Revenue>();
        }

        public int Id { get; set; }
        public string  Name { get; set; }
        ICollection<Revenue> Revenues { get; set; }
    }
}
