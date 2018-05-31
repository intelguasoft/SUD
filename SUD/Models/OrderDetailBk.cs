using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SUD.Models
{

    [Table("tbl_OrderDetailsBk")]

    public class OrderDetailBk
    {
        [Key]
        public int OrderDetailId { get; set; }

        [Display(Name = "Usuario")]
        public string User { get; set; }

        [Display(Name = "Producto")]
        public int ProductId { get; set; }

        // Descripcion del producto agregado
        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }

        // El valor de esta propiedad debe recibir el valor actual de Product.Price
        [Required(ErrorMessage = "Precio Requerido")]
        [Display(Name = "Precio")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Display(Name = "Cantidad")]
        [Required(ErrorMessage = "Cantidad Requerida")]
        public double Quantity { get; set; }

        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        [DataType(DataType.Currency)]
        public decimal Value { get { return Price * (decimal)Quantity; } }

        [Display(Name = "Porcentaje de IVA", Description = "0.00")]
        //[RegularExpression(@"^[1-9]\d*(\.\d +)?$", ErrorMessage = "Porcentaje Invalido, solo se permiten numeros.")]
        public float IVAPercentage { get; set; }

        [Display(Name = "Porcentaje de descuento", Description = "0.00")]
        //[RegularExpression(@"^[1-9]\d*(\.\d +)?$", ErrorMessage = "Porcentaje Invalido, solo se permiten numeros.")]
        [DataType(DataType.Currency)]
        public float DiscountRate { get; set; }

        public virtual Product Product { get; set; }

    }
}