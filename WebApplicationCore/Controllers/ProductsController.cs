using API_RestFull_Prueba.Models.User;
using Microsoft.AspNetCore.Mvc;
using SportShop.Model.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API_RestFull_Prueba.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly UsersDAL _repository;
        public List<UpdateUserDTO> ListUsers { get; set; }
        public ProductsController(UsersDAL repository1)
        {
            _repository = repository1 ?? throw new ArgumentNullException(nameof(repository1));
        }

        /// <summary>
        /// Trae la Lista de Usuarios
        /// </summary>
        /// <returns></returns>
        [HttpGet("getProducts")]
        public async Task<IActionResult> GetProductsAll()
        {
            try
            {
                var result = await _repository.GetProducts();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        /// <summary>
        /// Crear o actualiza el producto
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost("CreateProduct", Name = "CreateProduct")]
        public async Task Post([FromBody] Products products)
        {
            await _repository.StoreOrUpdateProducts(products);
        }

        /// <summary>
        /// Elimina venta
        /// </summary>
        /// <param name="pidSales"></param>
        /// <returns></returns>
        [HttpDelete("DeleteProduct/{pidProduct}", Name = "DeleteProduct")]
        public async Task<IActionResult> Delete(int pidProduct)
        {
            try
            {
                int result = await _repository.DeleteProduct(pidProduct);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
