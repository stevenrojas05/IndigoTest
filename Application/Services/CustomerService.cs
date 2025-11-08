using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CustomerService : ICustomerService
    {
        /// <summary>
        /// Repositorio 
        /// </summary>
        private readonly ICustomerRepository _customerRepository;
        
        /// <summary>
        /// Configuration para JWT
        /// </summary>
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="customerRepository"></param>
        /// <param name="configuration"></param>
        public CustomerService(ICustomerRepository customerRepository, IConfiguration configuration)
        {
            _customerRepository = customerRepository;
            _configuration = configuration;
        }

        /// <summary>
        /// Función para crear el cliente
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public async Task<Customer> CreateAsync(Customer customer)
        {
            // Hashear el password antes de guardar
            customer.PasswordHash = BCrypt.Net.BCrypt.HashPassword(customer.PasswordHash);
            customer.CreatedAt = DateTime.Now;
            
            await _customerRepository.AddAsync(customer);
            return customer;
        }

        /// <summary>
        /// Función para eliminar por Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteAsync(int id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);
            if (customer != null)
            {
                _customerRepository.Delete(customer);
            }
        }

        /// <summary>
        /// Función que retorna todos los clientes
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await _customerRepository.GetAllAsync();
        }

        /// <summary>
        /// Obtiene por Id el Cliente
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Customer?> GetByIdAsync(int id)
        {
            return await _customerRepository.GetByIdAsync(id);
        }

        /// <summary>
        /// Actualiza el Cliente por Id
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public async Task UpdateAsync(Customer customer)
        {
            _customerRepository.Update(customer);
            await Task.CompletedTask; // Como Update es sincrónico aquí solo devuelvo Task.CompletedTask
        }

        /// <summary>
        /// Método de autenticación de usuarios
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            // Buscar el customer por UserName
            var customer = await _customerRepository.GetByUserNameAsync(request.UserName);
            
            if (customer == null)
            {
                throw new UnauthorizedAccessException("Credenciales inválidas");
            }

            // Verificar el password con BCrypt
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(request.Password, customer.PasswordHash);
            
            if (!isPasswordValid)
            {
                throw new UnauthorizedAccessException("Credenciales inválidas");
            }

            // Generar el token JWT
            var token = GenerateJwtToken(customer);

            // Retornar la respuesta con el token
            return new LoginResponse
            {
                Token = token,
                CustomerId = customer.CustomerId,
                UserName = customer.UserName,
                Email = customer.Email
            };
        }

        /// <summary>
        /// Genera el token JWT para el usuario autenticado
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        private string GenerateJwtToken(Customer customer)
        {
            var jwtKey = _configuration["Jwt:Key"];
            var jwtIssuer = _configuration["Jwt:Issuer"];
            var jwtAudience = _configuration["Jwt:Audience"];

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, customer.UserName),
                new Claim(JwtRegisteredClaimNames.Email, customer.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("CustomerId", customer.CustomerId.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: jwtIssuer,
                audience: jwtAudience,
                claims: claims,
                expires: DateTime.Now.AddHours(24),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
