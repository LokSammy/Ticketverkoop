using System;
using System.Collections.Generic;

namespace Ticketverkoop.Entities
{
    public partial class Voucher
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public int ZitplaatsId { get; set; }
        public string GebruikerId { get; set; }

        public virtual AspNetUsers Gebruiker { get; set; }
        public virtual Ticket Ticket { get; set; }
        public virtual Zitplaats Zitplaats { get; set; }
    }
}
