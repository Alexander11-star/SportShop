using API_RestFull_Prueba.Models.User;
using Microsoft.AspNetCore.Mvc;
using SportShop.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_RestFull_Prueba.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UsersDAL _repository;
        public List<Users> ListUsers { get; set; }
        public UserController(UsersDAL repository1)
        {

            _repository = repository1 ?? throw new ArgumentNullException(nameof(repository1));

        }
        [HttpPost("CreateUser", Name = "CreateUser")]
        public async Task Post([FromBody] Users user)
        {
            await _repository.StoreOrUpdateUser(user);
        }

        /// <summary>
        /// Trae la Lista de Usuarios
        /// </summary>
        /// <returns></returns>
        [HttpGet("getUsers")]
        public async Task<IActionResult> GetUserAll()
        {
            try
            {
                var result = await _repository.getUsers();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }            
        }

        /// <summary>
        /// Filtra Usuario por id
        /// </summary>
        /// <param name="idUser"></param>
        /// <returns></returns>
        [HttpGet("getUser/{idUser}")]
        public async Task<IActionResult> GetUserById(int IdUser)
        {
            try
            {
                ListUsers = await _repository.getUsers();
                ListUsers = ListUsers.Where(x => x.IdUser == IdUser).ToList();

                return Ok(ListUsers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }          
        }

    }
}
