using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ticketverkoop.Extensions;
using Ticketverkoop.ViewModels;

namespace Ticketverkoop.Controllers
{
    public class ShoppingCartController : Controller
    {
        public IActionResult Index()
        {

            ShoppingCartVM cartList =
              HttpContext.Session.GetObject<ShoppingCartVM>("ShoppingCart");

            // call SessionID
            var SessionId = HttpContext.Session.Id;


            return View(cartList);
        }
        public IActionResult Delete(int? WedstrijdId)
        {
            if (WedstrijdId == null)
            {
                return NotFound();
            }

            ShoppingCartVM cartList =
                HttpContext.Session.GetObject<ShoppingCartVM>("ShoppingCart");
            // call SessionID
            var itemToRemove = cartList.Cart.FirstOrDefault(r => r.WedstrijdId == WedstrijdId);
            if (itemToRemove != null)
            {
                cartList.Cart.Remove(itemToRemove);
                HttpContext.Session.SetObject("ShoppingCart", cartList);
            }

            return View("Index", cartList);
        }

        [HttpPost]
        public IActionResult Gegevens(ShoppingCartVM shoppingCartVM)
        {
            ShoppingCartVM cartList =
                HttpContext.Session.GetObject<ShoppingCartVM>("ShoppingCart");
            for (int i = 0; i < cartList.Cart.Count; i++)
            {
                CartVM cart = cartList.Cart[i];

                shoppingCartVM.Cart[i].ThuisClub = cart.ThuisClub;
                shoppingCartVM.Cart[i].UitCLub = cart.UitCLub;
                shoppingCartVM.Cart[i].WedstrijdDatum = cart.WedstrijdDatum;
                shoppingCartVM.Cart[i].Prijs = cart.Prijs;
            }
            HttpContext.Session.SetObject("ShoppingCart", shoppingCartVM);
            return View("Index", shoppingCartVM);
        }
    }
}