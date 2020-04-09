using System;
using System.Collections.Generic;

namespace Ticketverkoop.Domain.Entities
{
    public partial class Wedstrijd
    {
        public Wedstrijd()
        {
            Ticket = new HashSet<Ticket>();
        }

        public int Id { get; set; }
        public int ThuisClubId { get; set; }
        public int UitClubId { get; set; }
        public DateTime Datum { get; set; }

        public virtual Club ThuisClub { get; set; }
        public virtual Club UitClub { get; set; }
        public virtual ICollection<Ticket> Ticket { get; set; }
    }
}
