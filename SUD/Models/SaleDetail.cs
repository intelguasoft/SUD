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
        public int SaleDetailId { get; set; }
        

        [Display(Name = "Descripcion")]
        [Required(ErrorMessage = "Ingrese una descripcion")]
        [MaxLength(140, ErrorMessage = "Texto muy largo")]
        public string Description { get; set; }

        [Display(Name = "Precio", Description = "0.00")]
        [Required(ErrorMessage = "Precio Requerido")]
        public float Price { get; set; }


        [Display(Name = "Cantidad", Description = "0.00")]
        [Required(ErrorMessage = "Cantidad Requerida")]
        public int Quantity { get; set; }
        

        [Display(Name = "Porcentaje de IVA", Description = "0.00")]
        [RegularExpression(@"^[1-9]\d*(\.\d +)?$", ErrorMessage = "Porcentaje Invalido, solo se permiten numeros.")]
        public float IVAPercentage { get; set; }

        [Display(Name = "Porcentaje de descuento", Description = "0.00")]
        [RegularExpression(@"^[1-9]\d*(\.\d +)?$", ErrorMessage = "Porcentaje Invalido, solo se permiten numeros.")]
        public float DiscountRate { get; set; }



        public int SaleId { get; set; }
        public int ProductId { get; set; }
        public int KardexId { get; set; }

        public virtual Sale Sale { get; set; }
        public virtual Product Product { get; set; }

    }
}