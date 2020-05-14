using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ticketverkoop.ViewModels
{
    public class WeerVM
    {
        public Current current { get; set; }

        public class Current
        {
            public decimal temperature { get; set; }
            public List<string> weather_descriptions { get; set; }
            public List<string> weather_icons { get; set; }
            public decimal wind_speed { get; set; }
        }


    }
}
