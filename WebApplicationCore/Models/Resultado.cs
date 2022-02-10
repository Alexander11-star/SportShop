using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_RestFull_Prueba.Models
{
    public class Resultado
    {
        public int Valor { get; set; }
        public string Mensaje { get; set; }
        public Resultado()
        {

        }
        public Resultado(int pValor, string pMensaje)
        {
            Valor = pValor;
            Mensaje = pMensaje;
        }
    }
}
