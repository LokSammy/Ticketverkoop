using System;
using System.Collections.Generic;

namespace Ticketverkoop.Domain.Entities
{
    public partial class Bestelling
    {
        public int Id { get; set; }
        public int Aantal { get; set; }
        public int TicketId { get; set; }
        public int AbonnementId { get; set; }
        public string GebruikerId { get; set; }

        public virtual Abonnement Abonnement { get; set; }
        public virtual Ticket Ticket { get; set; }
    }
}
