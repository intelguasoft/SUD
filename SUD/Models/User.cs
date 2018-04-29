﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SUD.Models
{
    [Table("tbl_Users")]
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Nombre")]
        public string Name { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Apellido")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(255, ErrorMessage = "Debe contener entre 8 caracteres", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [Display(Name = "Fecha de Modificación de la Contraseña")]
        [DataType(DataType.Date)]
        public DateTime ModificationDatePassword { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Rol:")]
        public int RolId { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(255, ErrorMessage = "Debe contener entre 8 y 25 caracteres", MinimumLength = 8)]
        [RegularExpression("^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-.]+$", ErrorMessage = "Debe ser un correo electronico valido")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Correo Electronico")]
        //[Index ("User_Email_Index", IsUnique = true)]
        public string Email { get; set; }

        public virtual Rol Rol { get; set; }

    }

}