using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SUD.Models
{
    [Table("tbl_SaleDetails")]
    public class SaleDetail
    {
        [Key]
        public long SaleDetailId { get; set; }

        [Display(Name = "Descripcion")]
        [Required(ErrorMessage = "Ingrese una descripcion")]
        [MaxLength(140, ErrorMessage = "Texto muy largo")]
        public string Description { get; set; }

        // El valor de esta propiedad debe recibir el valor actual de Product.Price
        [Display(Name = "Precio", Description = "0.00")]
        [Required(ErrorMessage = "Precio Requerido")]
        public decimal Price { get; set; }

        [Display(Name = "Cantidad", Description = "0.00")]
        [Required(ErrorMessage = "Cantidad Requerida")]
        public double Quantity { get; set; }
        //TODO Modificar las Expresiones regulares para que acepten decimales.
        [Required(ErrorMessage ="Cantidad Requerida")]
        [Display(Name = "Porcentaje de IVA", Description = "0.00")]
        //[RegularExpression(@"^[0-9]\d*(\.\d +)?$", ErrorMessage = "Porcentaje Invalido, solo se permiten numeros enteros.")]
        public decimal? IVAPercentage { get; set; }

        [Required(ErrorMessage = "Cantidad Requerida")]
        [Display(Name = "Porcentaje de descuento", Description = "0.00")]
       // [RegularExpression(@"^[0-9]\d*(\.\d +)?$", ErrorMessage = "Porcentaje Invalido, solo se permiten numeros enteros.")]
        public decimal? DiscountRate { get; set; }

        [Required]
        [Display(Name ="Venta")]
        public long SaleId { get; set; }

        [Required]
        [Display(Name ="Producto")]
        public int ProductId { get; set; }

        //[Required]
        //[Display(Name ="Kardex")]
        //public int KardexId { get; set; }

        public virtual Sale Sale { get; set; }
        public virtual Product Product { get; set; }
       // public virtual Kardex Kardex { get; set; }

    }
}