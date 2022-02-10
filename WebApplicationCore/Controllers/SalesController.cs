using API_RestFull_Prueba.Models.User;
using Microsoft.AspNetCore.Mvc;
using SportShop.Model.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApplicationCore.Controllers
{
    namespace API_RestFull_Prueba.Controllers
    {
        [Route("api/[controller]")]
        [ApiController]
        public class SalesController : ControllerBase
        {
            private readonly UsersDAL _repository;
            public List<UpdateUserDTO> ListUsers { get; set; }

            public List<Sales> Sales { get; set; } 
            public SalesController(UsersDAL repository1)
            {
                _repository = repository1 ?? throw new ArgumentNullException(nameof(repository1));
            }

            /// <summary>
            /// Trae la Lista de Perfiles
            /// </summary>
            /// <returns></returns>
            [HttpGet("getSales")]
            public async Task<IActionResult> GetSalesAll()
            {
                try
                {
                    Sales = await _repository.getSales();
                    return Ok(Sales);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, ex);
                }
            }

            /// <summary>
            /// Trae la Lista de Perfiles
            /// </summary>
            /// <returns></returns>
            [HttpGet("getSalesGroup")]
            public async Task<IActionResult> GetSalesGroup()
            {
                try
                {
                    Sales = await _repository.getSalesGroup();
                    return Ok(Sales);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, ex);
                }
            }

            /// <summary>
            /// Crear o actualizar nueva venta
            /// </summary>
            /// <param name="sales"></param>
            /// <returns></returns>
            [HttpPost("CreateSales", Name = "CreateSales")]
            public async Task Post([FromBody] Sales sales)
            {
                await _repository.StoreOrUpdateSales(sales);
            }

            /// <summary>
            /// Elimina venta
            /// </summary>
            /// <param name="pidSales"></param>
            /// <returns></returns>
            [HttpDelete("DeleteSales/{pidSales}", Name = "DeleteSales")]
            public async Task<IActionResult> Delete(int pidSales)
            {
                try
                {
                    int result = await _repository.DeleteSales(pidSales);
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, ex);
                }
            }
        }
    }
}
