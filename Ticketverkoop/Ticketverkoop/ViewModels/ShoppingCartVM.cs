using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ticketverkoop.ViewModels
{
    public class ShoppingCartVM
    {
        public List<CartVM> Cart { get; set; }
    }

    public class CartVM
    {
        public int WedstrijdId { get; set; }
        public string ThuisClub { get; set; }
        public string UitCLub { get; set; }
        public float Prijs { get; set; }
        public DateTime WedstrijdDatum { get; set; }
    }
}
