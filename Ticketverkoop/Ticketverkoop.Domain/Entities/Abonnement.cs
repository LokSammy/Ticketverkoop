using System;
using System.Collections.Generic;

namespace Ticketverkoop.Domain.Entities
{
    public partial class Abonnement
    {
        public Abonnement()
        {
            Bestelling = new HashSet<Bestelling>();
        }

        public int Id { get; set; }
        public string GebruikerId { get; set; }
        public decimal Prijs { get; set; }
        public int VakId { get; set; }

        public virtual Vak Vak { get; set; }
        public virtual ICollection<Bestelling> Bestelling { get; set; }
    }
}
