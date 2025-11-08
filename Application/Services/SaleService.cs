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
    public class SaleService : ISaleService
    {
        /// <summary>
        /// Repositorio 
        /// </summary>
        private readonly ISaleRepository _saleRepository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="saleRepository"></param>
        public SaleService(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }

        /// <summary>
        /// Función para crear la venta
        /// </summary>
        /// <param name="sale"></param>
        /// <returns></returns>
        public async Task<Sale> CreateAsync(Sale sale)
        {
            await _saleRepository.AddAsync(sale);
            return sale;
        }

        /// <summary>
        /// Función para eliminar por Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteAsync(int id)
        {
            var sale = await _saleRepository.GetByIdAsync(id);
            if (sale != null)
            {
                _saleRepository.Delete(sale);
            }
        }

        /// <summary>
        /// Función que retorna todas las ventas
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Sale>> GetAllAsync()
        {
            return await _saleRepository.GetAllAsync();
        }

        /// <summary>
        /// Obtiene por Id la Venta
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Sale?> GetByIdAsync(int id)
        {
            return await _saleRepository.GetByIdAsync(id);
        }

        /// <summary>
        /// Obtiene las ventas filtradas por rango de fechas
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Sale>> GetSalesByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _saleRepository.GetSalesByDateRangeAsync(startDate, endDate);
        }

        /// <summary>
        /// Actualiza la Venta por Id
        /// </summary>
        /// <param name="sale"></param>
        /// <returns></returns>
        public async Task UpdateAsync(Sale sale)
        {
            _saleRepository.Update(sale);
            await Task.CompletedTask; // Como Update es sincrónico aquí solo devuelvo Task.CompletedTask
        }
    }
}
