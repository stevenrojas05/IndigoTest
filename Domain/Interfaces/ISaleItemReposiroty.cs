using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ISaleItemReposiroty:IGenericRepository<SaleItem>
    {
        /// <summary>
        /// Obtiene los items de venta por Id de venta
        /// </summary>
        /// <param name="saleId"></param>
        /// <returns></returns>
        Task<IEnumerable<SaleItem>> GetBySaleIdAsync(int saleId);
    }
}
