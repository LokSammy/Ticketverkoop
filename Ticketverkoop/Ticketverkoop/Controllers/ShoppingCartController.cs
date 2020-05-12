using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
            var SessionId = HttpContext.Session.Id;
            var itemToRemove = cartList.ShoppingCart.FirstOrDefault(r => r.WedstrijdId == WedstrijdId);
            if (itemToRemove != null)
            {
                cartList.ShoppingCart.Remove(itemToRemove);
                HttpContext.Session.SetObject("ShoppingCart", cartList);
            }

            return View("Index", cartList);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Payment(ShoppingCartVM shoppingCartVM)
        {
            ShoppingCartVM cartList =
                HttpContext.Session.GetObject<ShoppingCartVM>("ShoppingCart");


            for (int i = 0; i < cartList.ShoppingCart.Count; i++)
            {
                CartVM cart = cartList.ShoppingCart[i];

                shoppingCartVM.ShoppingCart[i].WedstrijdId = cart.WedstrijdId;
                shoppingCartVM.ShoppingCart[i].ThuisClubId = cart.ThuisClubId;
                shoppingCartVM.ShoppingCart[i].ThuisClubNaam = cart.ThuisClubNaam;
                shoppingCartVM.ShoppingCart[i].UitCLubNaam = cart.UitCLubNaam;
                shoppingCartVM.ShoppingCart[i].StadiumNaam = cart.StadiumNaam;
                shoppingCartVM.ShoppingCart[i].VakNaam = cart.VakNaam;
                shoppingCartVM.ShoppingCart[i].WedstrijdDatum = cart.WedstrijdDatum;
                shoppingCartVM.ShoppingCart[i].StukPrijs = cart.StukPrijs;
                shoppingCartVM.ShoppingCart[i].Aantal = cart.Aantal;
                
            }

            HttpContext.Session.SetObject("ShoppingCart", shoppingCartVM);
            return View("Index", shoppingCartVM);
        }
    }
}