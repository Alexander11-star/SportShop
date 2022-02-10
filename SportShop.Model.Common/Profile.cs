using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SportShop.Model.Common
{
    public class Profile
    {
        /// <summary>
        /// Id producto
        /// </summary>
        [Key]
        public int IdProfile { get; set; }
        /// <summary>
        /// Nombre del perfil
        /// </summary>
        [Required(ErrorMessage = "El perfil es obligatorio")]
        public string NameProfile { get; set; }
    }
}
