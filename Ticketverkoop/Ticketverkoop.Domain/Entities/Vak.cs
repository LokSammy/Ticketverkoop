using System;
using System.Collections.Generic;

namespace Ticketverkoop.Domain.Entities
{
    public partial class Vak
    {
        public Vak()
        {
            StadionVak = new HashSet<StadionVak>();
            Ticket = new HashSet<Ticket>();
        }

        public int Id { get; set; }
        public string Naam { get; set; }
        public int StadionId { get; set; }
        public int AantalPlaatsen { get; set; }
        public decimal Prijs { get; set; }

        public virtual Stadion Stadion { get; set; }
        public virtual ICollection<StadionVak> StadionVak { get; set; }
        public virtual ICollection<Ticket> Ticket { get; set; }
    }
}
