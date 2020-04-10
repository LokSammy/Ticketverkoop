using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ticketverkoop.ViewModels
{
    public class WedstrijdVM
    {
        public int Id { get; set; }
        public string ThuisClubNaam { get; set; }
        public string UitClubNaam { get; set; }
        public DateTime Datum { get; set; }
    }
}
