using System;
using System.Collections.Generic;

namespace Ticketverkoop.Domain.Entities
{
    public partial class Zitplaats
    {
        public Zitplaats()
        {
            Voucher = new HashSet<Voucher>();
        }

        public int Id { get; set; }

        public virtual ICollection<Voucher> Voucher { get; set; }
    }
}
