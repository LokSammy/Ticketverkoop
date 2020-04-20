﻿using System;
using System.Collections.Generic;

namespace Ticketverkoop.Entities
{
    public partial class StadionVak
    {
        public int Id { get; set; }
        public int StadionId { get; set; }
        public int VakId { get; set; }
        public int? WedstrijdId { get; set; }
        public int AantalPlaatsen { get; set; }
        public decimal Prijs { get; set; }

        public virtual Stadion Stadion { get; set; }
        public virtual Vak Vak { get; set; }
        public virtual Wedstrijd Wedstrijd { get; set; }
    }
}