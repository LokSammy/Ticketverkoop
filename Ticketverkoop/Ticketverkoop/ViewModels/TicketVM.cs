using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ticketverkoop.ViewModels
{
    public class TicketShowVM
    {
        public string gebruikerId { get; set; }
        public string gebruikerNaam { get; set; }
        public string gebruikerVoornaam { get; set; }
        public string gerbuikerEmail { get; set; }
        public int WedstrijdId { get; set; }
        public int ThuisClubId { get; set; }
        public string ThuisClubNaam { get; set; }
        public string UitCLubNaam { get; set; }
        public string StadiumNaam { get; set; }
        public string VakNaam { get; set; }
        public int VakId { get; set; }
        public decimal Prijs { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd MMMM yyyy HH:mm}")]
        public DateTime WedstrijdDatum { get; set; }
    }

    public class TicketCreateVM
    {
        public int WedstrijdId { get; set; }
        public string gebruikerId { get; set; }
        public int VakId { get; set; }
        public decimal Prijs { get; set; }
    }
}
