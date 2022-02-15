using System.ComponentModel.DataAnnotations;

namespace UbicApi.Web.Data.Entities
{
    public class Event
    {
        public int Id { get; set; }

        [Display(Name = "Evento")]
        [MaxLength(30, ErrorMessage = "El campo {0} no puede tener más de {1} carácteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string EventName { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public double Kb { get; set; }

    }
}