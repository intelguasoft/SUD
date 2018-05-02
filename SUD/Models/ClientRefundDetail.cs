using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SUD.Models
{
    public class ClientRefundDetail
    {
        [Key]
        public int ClientRefundDetailId { get; set; }

        [Display(Name ="Descripcion")]
        [Required(ErrorMessage = "Ingrese una descripcion")]
        [MaxLength(140, ErrorMessage ="Texto muy largo")]
        public string Description { get; set; }

        [Display(Name ="Precio", Description ="0.00")]
        [Required(ErrorMessage ="Precio Requerido")]
        public float price { get; set; }

        [Display(Name ="Cantidad", Description ="0")]
        [Required(ErrorMessage ="Cantidad Requerida")]
        [RegularExpression(@"^[0-9]{8}$", ErrorMessage ="Precio Invalido")]
        public int Cantidad { get; set; }

        [Display(Name ="Porcentaje de descuento", Description ="0.00")]
        [RegularExpression(@"^[1-9]\d*(\.\d +)?$", ErrorMessage = "Porcentaje Invalido, solo se permiten numeros.")]
        public float DiscountRate { get; set; }

        public int ClientRefundId { get; set; }
        public int ProductId { get; set; }
        public int KardexId { get; set; }






    }
}