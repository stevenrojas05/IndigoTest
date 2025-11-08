using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ISaleItemService
    {
        Task<IEnumerable<SaleItem>> GetBySaleIdAsync(int saleId);
        Task<SaleItem> CreateAsync(SaleItem saleItem);
        Task UpdateAsync(SaleItem saleItem);
        Task DeleteAsync(int id);
    }
}
