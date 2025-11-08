using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Sale
    {
        public int SaleId { get; set; }
        public DateTime SaleDate { get; set; }
        public decimal Total { get; set; }
        public IList<SaleItem> Items { get; set; } = new List<SaleItem>();
    }
}
