using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class SaleItemService : ISaleItemService
    {
        /// <summary>
        /// Repositorio 
        /// </summary>
        private readonly ISaleItemReposiroty _saleItemRepository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="saleItemRepository"></param>
        public SaleItemService(ISaleItemReposiroty saleItemRepository)
        {
            _saleItemRepository = saleItemRepository;
        }

        /// <summary>
        /// Función para crear el item de venta
        /// </summary>
        /// <param name="saleItem"></param>
        /// <returns></returns>
        public async Task<SaleItem> CreateAsync(SaleItem saleItem)
        {
            await _saleItemRepository.AddAsync(saleItem);
            return saleItem;
        }

        /// <summary>
        /// Función para eliminar por Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteAsync(int id)
        {
            var saleItem = await _saleItemRepository.GetByIdAsync(id);
            if (saleItem != null)
            {
                _saleItemRepository.Delete(saleItem);
            }
        }

        /// <summary>
        /// Obtiene los items de venta por Id de venta
        /// </summary>
        /// <param name="saleId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<SaleItem>> GetBySaleIdAsync(int saleId)
        {
            return await _saleItemRepository.GetBySaleIdAsync(saleId);
        }

        /// <summary>
        /// Actualiza el Item de Venta por Id
        /// </summary>
        /// <param name="saleItem"></param>
        /// <returns></returns>
        public async Task UpdateAsync(SaleItem saleItem)
        {
            _saleItemRepository.Update(saleItem);
            await Task.CompletedTask; // Como Update es sincrónico aquí solo devuelvo Task.CompletedTask
        }
    }
}
