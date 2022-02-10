using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API_RestFull_Prueba.Models.LoginUser
{
    public class LoginUserDTO
    {
        [Required(ErrorMessage = "El usuario es obligatorio")]
        public string Email { get; set; }

        [Required(ErrorMessage = "La Contraseña es obligatoria")]
        public string password { get; set; }
    }
    public class Usuario
    {
        public int idUsuario { get; set; }
        public string Email { get; set; }
        public string password { get; set; }
        public int idDocument { get; set; }
        public string nombreCompleto { get; set; }
        public bool Valido { get; set; }
        public Resultado Result { get; set; }

    }

}
