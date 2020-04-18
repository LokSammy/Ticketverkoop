using System;
using System.Collections.Generic;

namespace Ticketverkoop.Entities
{
    public partial class Wedstrijd
    {
        public Wedstrijd()
        {
            StadionVak = new HashSet<StadionVak>();
            Ticket = new HashSet<Ticket>();
        }

        public int Id { get; set; }
        public int ThuisClubId { get; set; }
        public int UitClubId { get; set; }
        public DateTime Datum { get; set; }

        public virtual Club ThuisClub { get; set; }
        public virtual Club UitClub { get; set; }
        public virtual ICollection<StadionVak> StadionVak { get; set; }
        public virtual ICollection<Ticket> Ticket { get; set; }
    }
}
