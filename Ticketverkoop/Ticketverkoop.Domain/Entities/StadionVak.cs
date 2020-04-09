using System;
using System.Collections.Generic;

namespace Ticketverkoop.Domain.Entities
{
    public partial class StadionVak
    {
        public int Id { get; set; }
        public int StadionId { get; set; }
        public int VakId { get; set; }

        public virtual Stadion Stadion { get; set; }
        public virtual Vak Vak { get; set; }
    }
}
