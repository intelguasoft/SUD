using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SUD.Models
{
    public class Client
    {
        [Key]
        public int IdClient { get; set; }

        [Required(ErrorMessage ="*Ingrese el DPI o NIT del cliente")]
        [Display (Name ="DPI o NIT", Description ="2117502980101")]
        [MaxLength(13)]
        [RegularExpression(@"^[0-9][a-z]{13}$", ErrorMessage ="No. de Documento invalido")]
        public String Document { get; set; }

        [Display(Name ="Nombre Comercial", Description ="Nombre Comercial")]
        public String ComertialName { get; set; }

        [Required(ErrorMessage ="*Se requiere el nombre")]
        [Display(Name ="Nombre", Description ="Nombre")]
        public String FirstNameContact { get; set; }

        [Required(ErrorMessage = "*Se requiere el apellido")]
        [Display(Name ="Apellido", Description ="Apellido")]
        public String LastNameContact { get; set; }

        [Required(ErrorMessage = "*Se requiere la Direccion")]
        [Display(Name ="Direccion", Description ="Direccion")]
        public String Address { get; set; }

        [Required(ErrorMessage ="*Ingrese un numero de telefono")]
        [Display(Name ="*Telefono 1", Description ="Telefono 1")]
        public int Telephone1 { get; set; }

        [Display(Name ="Telefono 2", Description ="Telefono 2 (Opcional)")]
        public int Telephone2 { get; set; }

        [Display(Name = "Correo Electronico", Description = "Correo Electronico")]
        [EmailAddress(ErrorMessage ="Ingrese un correo electronico valido")]
        public String Mail { get; set; }

        [Display(Name = "Notas", Description = "Notas")]
        public String Note { get; set; }

        public int IdDocumentType { get; set; }


        public virtual DocumentType DocumentType { get; set; }

        public virtual ICollection<Sale> Sales { get; set; }

    }
}