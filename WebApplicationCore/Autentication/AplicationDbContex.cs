using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using API_RestFull_Prueba.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_RestFull_Prueba.Models.User;

namespace API_RestFull_Prueba.Autentication
{
    public class AplicationDbContex : IdentityDbContext<AplicationUser>
    {
        public AplicationDbContex(DbContextOptions<AplicationDbContex> options)
            : base(options)
        {

        }
    }
}
