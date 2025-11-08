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
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        /// <summary>
        /// Constructor
        /// </summary>
        public ProductService(IProductRepository productRepository) 
        { 
            _productRepository = productRepository; 
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<Product> CreateAsync(Product product)
        {
            await _productRepository.AddAsync(product);
            return product;
        }
        /// <summary>
        /// Función para eliminar por Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product != null)
            {
                _productRepository.Delete(product);
            }
        }
        /// <summary>
        /// Función que retorna todos los productos
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _productRepository.GetAllAsync();
        }
        /// <summary>
        /// Obtiene por Id el Producto
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _productRepository.GetByIdAsync(id);
        }
        /// <summary>
        /// Actualiza el Producto por Id
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public async Task UpdateAsync(Product product)
        {
            _productRepository.Update(product);
            await Task.CompletedTask; // Como Update es sincrónico aquí solo devuelvo Task.CompletedTask
        }
    }
}
