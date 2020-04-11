using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ticketverkoop.ViewModels
{
    public class ClubVM
    {
        public int Id { get; set; }
        public string Naam { get; set; }
        public string StadionId { get; set; }
        public string Logo { get; set; }
        [Display(Name = "Vak")]
        public IEnumerable<SelectListItem> Vakken { get; set; }
    }
}
