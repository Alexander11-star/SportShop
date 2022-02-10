using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API_RestFull_Prueba.Models.User
{
    public class UpdateUserDTO
    {
        [Key]
        public int ersreg { get; set; }
        public int erspro { get; set; }
        public string ersnom { get; set; }
        public string erspass { get; set; }

    }

}
