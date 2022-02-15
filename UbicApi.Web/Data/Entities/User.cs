using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using UbicApi.Common.Enums;

namespace UbicApi.Web.Data.Entities
{
    public class User : IdentityUser
    {
        [Display(Name = "Módulo")]
        [MaxLength(20, ErrorMessage = "El campo {0} no puede tener más de {1} carácteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Modulo { get; set; }

        [Display(Name = "Nombre")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener más de {1} carácteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string FirstName { get; set; }

        [Display(Name = "Apellido")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener más de {1} carácteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string LastName { get; set; }

        [Display(Name = "Documento")]
        [MaxLength(20, ErrorMessage = "El campo {0} no puede tener más de {1} carácteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Document { get; set; }

        [Display(Name = "Dirección 1")]
        [MaxLength(150, ErrorMessage = "El campo {0} no puede tener más de {1} carácteres.")]
        public string Address1 { get; set; }

        [Display(Name = "Latitud 1")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public double Latitude1 { get; set; }

        [Display(Name = "Longitud 1")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public double Longitude1 { get; set; }

        [Display(Name = "Dirección 2")]
        [MaxLength(150, ErrorMessage = "El campo {0} no puede tener más de {1} carácteres.")]
        public string Address2 { get; set; }

        [Display(Name = "Latitud 2")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public double Latitude2 { get; set; }

        [Display(Name = "Longitud 2")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public double Longitude2 { get; set; }

        [Display(Name = "Dirección 3")]
        [MaxLength(150, ErrorMessage = "El campo {0} no puede tener más de {1} carácteres.")]
        public string Address3 { get; set; }

        [Display(Name = "Latitud 3")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public double Latitude3 { get; set; }

        [Display(Name = "Longitud 3")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public double Longitude3 { get; set; }

        [Display(Name = "Foto")]
        public string ImageId { get; set; }


        //TODO: Corregir ruta
        [Display(Name = "Foto")]
        public string ImageFullPath => string.IsNullOrEmpty(ImageId)
                     ? "http://keypress.serveftp.net:88/UbicApiApi/Images/nouser.png"
            : $"http://keypress.serveftp.net:88/UbicApiApi{ImageId.Substring(1)}";

        [Display(Name = "Tipo de usuario")]
        public UserType UserType { get; set; }

        [Display(Name = "Usuario")]
        public string FullName => $"{FirstName} {LastName}";
    }
}
