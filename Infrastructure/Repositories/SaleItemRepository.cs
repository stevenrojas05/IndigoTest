using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class SaleItemRepository : GenericRepository<SaleItem>, ISaleItemReposiroty
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context"></param>
        public SaleItemRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtiene los items de venta por Id de venta
        /// </summary>
        /// <param name="saleId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<SaleItem>> GetBySaleIdAsync(int saleId)
        {
            return await _context.SaleItems
                .Where(si => si.SaleId == saleId)
                .ToListAsync();
        }
    }
}



