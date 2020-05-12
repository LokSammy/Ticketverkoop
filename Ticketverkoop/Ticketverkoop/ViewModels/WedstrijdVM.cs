using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ticketverkoop.ViewModels
{
    public class WedstrijdVM
    {
        public int Id { get; set; }
        public string ThuisClubNaam { get; set; }
        public string UitClubNaam { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd MMMM yyyy HH:mm}")]
        public DateTime Datum { get; set; }

        [Display(Name = "Stadion")]
        public string StadionNaam { get; set; }
        
        [Display(Name = "Vak")]
        public IEnumerable<SelectListItem> Vakken { get; set; }
        public int GekozenVakId { get; set; }
        

    }
}
