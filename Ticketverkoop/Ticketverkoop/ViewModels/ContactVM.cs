using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ticketverkoop.ViewModels
{
    public class ContactVM
    {
        [Required(ErrorMessage = "Naam is verplicht.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "De naam moet minstens 3 en maximum 50 karakters bevatten.")]
        public string Naam { get; set; }

        [Required(ErrorMessage = "Voornaam is verplicht.")]
        public string Voornaam { get; set; }

        [Required(ErrorMessage = "E-mailadres is verplicht.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Vul een geldig e-mailadres in.")]
        public string Email { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Het berichtveld is verplicht.")]
        public string Message { get; set; }
    }
}
