using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using API_RestFull_Prueba.Autentication;
using API_RestFull_Prueba.Models.LoginUser;

namespace API_RestFull_Prueba.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AutenticarUserControllers : ControllerBase
    {
        private readonly AplicationDbContex context;
        private IConfiguration _ConnectionString;
        private UserManager<API_RestFull_Prueba.Models.AplicationUser> _userManager;
        public AutenticarUserControllers(AplicationDbContex vcontext, IConfiguration Configuration, UserManager<API_RestFull_Prueba.Models.AplicationUser> userManager)
        {
            this.context = vcontext;
            this._ConnectionString = Configuration;
            _userManager = userManager;
        }


        [HttpPost("/AutenticarUser", Name = "AuntenticarUser")]
        public async Task<ActionResult<Usuario>> Post([FromBody] LoginUserDTO Login)
        {
            Usuario usuario = new Usuario();
            try
            {
                LoginUserDAL objLogueo = new LoginUserDAL(_ConnectionString.GetConnectionString("DefaultConnection"));
                usuario = objLogueo.AutenticarUsuario(Login);
                var userId = ((System.Security.Claims.ClaimsIdentity)User.Identity).Name;
                var user = await _userManager.FindByNameAsync(userId);


            }
            catch (Exception ex)
            {
                usuario.Result.Valor = 500;
                usuario.Result.Mensaje = ex.Message;
                return usuario;
            }
            return usuario;
        }

    }
}
