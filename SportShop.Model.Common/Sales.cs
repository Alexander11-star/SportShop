using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SportShop.Model.Common
{
    public class Sales
    {
        /// <summary>
        /// Id venta
        /// </summary>
        public int IdSales { get; set; }
        /// <summary>
        /// Id producto
        /// </summary>
        public int IdSProduct { get; set; }
        /// <summary>
        /// Id de usuario
        /// </summary>
        public int IdUser { get; set; }
        /// <summary>
        /// Id perfil
        /// </summary>
        public int IdProfile { get; set; }
        /// <summary>
        /// Cantidad
        /// </summary>
        public int Quantity { get; set; }
        /// <summary>
        /// fecha venta
        /// </summary>
        public DateTime DateSales { get; set; }
        /// <summary>
        /// Nombre del producto
        /// </summary>
        public string NameSProduct { get; set; }
        /// <summary>
        /// descripcion
        /// </summary>
        public string DescriptionSProduct { get; set; }
        /// <summary>
        /// Costo del producto
        /// </summary>
        public decimal CostSProduct { get; set; }
        /// <summary>
        /// imagen del producto
        /// </summary>
        public string ImageSProduct { get; set; }
        /// <summary>
        /// Nombre usuario
        /// </summary>
        public string NameUser { get; set; }
        /// <summary>
        /// Password
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Nombre del perfil
        /// </summary>
        public string NameProfile { get; set; }
    }
}
