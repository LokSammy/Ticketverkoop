﻿using System;
using System.Collections.Generic;

namespace Ticketverkoop.Domain.Entities
{
    public partial class Vak
    {
        public Vak()
        {
            StadionVak = new HashSet<StadionVak>();
            Ticket = new HashSet<Ticket>();
        }

        public int Id { get; set; }
        public string Omschrijving { get; set; }

        public virtual ICollection<StadionVak> StadionVak { get; set; }
        public virtual ICollection<Ticket> Ticket { get; set; }
    }
}
