using System;
using System.Collections.Generic;

namespace Ticketverkoop.Domain.Entities
{
    public partial class Ticket
    {
        public Ticket()
        {
            Bestelling = new HashSet<Bestelling>();
            Voucher = new HashSet<Voucher>();
        }

        public int Id { get; set; }
        public string GebruikerId { get; set; }
        public int WedstrijdId { get; set; }
        public int VakId { get; set; }
        public decimal Prijs { get; set; }

        public virtual AspNetUsers Gebruiker { get; set; }
        public virtual Vak Vak { get; set; }
        public virtual Wedstrijd Wedstrijd { get; set; }
        public virtual ICollection<Bestelling> Bestelling { get; set; }
        public virtual ICollection<Voucher> Voucher { get; set; }
    }
}
