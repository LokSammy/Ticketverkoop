using System;
using System.Collections.Generic;

namespace Ticketverkoop.Domain.Entities
{
    public partial class Club
    {
        public Club()
        {
            WedstrijdThuisClub = new HashSet<Wedstrijd>();
            WedstrijdUitClub = new HashSet<Wedstrijd>();
        }

        public int Id { get; set; }
        public string Naam { get; set; }
        public int StadionId { get; set; }
        public string Logo { get; set; }

        public virtual Stadion Stadion { get; set; }
        public virtual ICollection<Wedstrijd> WedstrijdThuisClub { get; set; }
        public virtual ICollection<Wedstrijd> WedstrijdUitClub { get; set; }
    }
}
