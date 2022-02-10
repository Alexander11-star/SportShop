using System.ComponentModel.DataAnnotations;

namespace SportShop.Model.Common
{
    public class Products
    {
        /// <summary>
        /// Id producto
        /// </summary>
        [Key]
        public int IdProduct { get; set; }
        /// <summary>
        /// cod producto
        /// </summary>
        public string CodProduct { get; set; }
        /// <summary>
        /// Nombre del producto
        /// </summary>
        [Required(ErrorMessage = "El nombre del producto es obligatorio")]
        public string NameProduct { get; set; }
        /// <summary>
        /// descripcion
        /// </summary>
        [Required(ErrorMessage = "La descripción del producto es obligatoria")]
        public string DescriptionProduct { get; set; }
        /// <summary>
        /// Costo del producto
        /// </summary>
        [Required(ErrorMessage = "El costo del producto es obligatorio")]
        public decimal CostProduct { get; set; }
        /// <summary>
        /// imagen del producto
        /// </summary>
        public string ImageProduct { get; set; }

    }
}
