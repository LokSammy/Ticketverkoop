using System;
using System.Collections.Generic;

namespace Ticketverkoop.Domain.Entities
{
    public partial class Stadion
    {
        public Stadion()
        {
            Club = new HashSet<Club>();
            StadionVak = new HashSet<StadionVak>();
            Vak = new HashSet<Vak>();
        }

        public int Id { get; set; }
        public string Naam { get; set; }
        public string Adres { get; set; }

        public virtual ICollection<Club> Club { get; set; }
        public virtual ICollection<StadionVak> StadionVak { get; set; }
        public virtual ICollection<Vak> Vak { get; set; }
    }
}
