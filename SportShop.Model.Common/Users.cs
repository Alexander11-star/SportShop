using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SportShop.Model.Common
{
    public class Users
    {
        /// <summary>
        /// Id de usuario
        /// </summary>
        [Key]
        public int IdUser { get; set; }
        /// <summary>
        /// Id perfil
        /// </summary>
        public int IdProfile { get; set; }
        /// <summary>
        /// Nombre usuario
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Password
        /// </summary>
        public string Password { get; set; }
    }
}
