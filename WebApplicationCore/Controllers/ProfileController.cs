using API_RestFull_Prueba.Models.User;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApplicationCore.Controllers
{

    namespace API_RestFull_Prueba.Controllers
    {
        [Route("api/[controller]")]
        [ApiController]
        public class ProfileController : ControllerBase
        {
            private readonly UsersDAL _repository;
            public List<UpdateUserDTO> ListUsers { get; set; }
            public ProfileController(UsersDAL repository1)
            {
                _repository = repository1 ?? throw new ArgumentNullException(nameof(repository1));
            }

            /// <summary>
            /// Trae la Lista de Perfiles
            /// </summary>
            /// <returns></returns>
            [HttpGet("getProfiles")]
            public async Task<IActionResult> GetProfileAll()
            {
                try
                {
                    var result = await _repository.getProfiles();
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
