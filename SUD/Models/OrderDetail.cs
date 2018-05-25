using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SUD.Models
{
    [Table("tbl_OrderDetails")]

    public class OrderDetail
    {
        [Key]
        public int OrderDetailId { get; set; }

        [Display(Name = "Número de Orden")]
        public int OrderId { get; set; }

        [Display(Name = "Producto")]
        public int ProductId { get; set; }

        // El valor de esta propiedad debe recibir el valor actual de Product.Price
        [Display(Name = "Precio", Description = "0.00")]
        [Required(ErrorMessage = "Precio Requerido")]
        public float Price { get; set; }

        [Display(Name = "Cantidad")]
        [Required(ErrorMessage = "Cantidad Requerida")]
        public int Quantity { get; set; }

        [Display(Name = "Porcentaje de IVA", Description = "0.00")]
        [RegularExpression(@"^[1-9]\d*(\.\d +)?$", ErrorMessage = "Porcentaje Invalido, solo se permiten numeros.")]
        public float IVAPercentage { get; set; }

        [Display(Name = "Porcentaje de descuento", Description = "0.00")]
        [RegularExpression(@"^[1-9]\d*(\.\d +)?$", ErrorMessage = "Porcentaje Invalido, solo se permiten numeros.")]
        public float DiscountRate { get; set; }

        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }

    }
}