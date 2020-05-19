using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ticketverkoop.Domain.Entities;
using Ticketverkoop.Extensions;
using Ticketverkoop.Service;
using Ticketverkoop.ViewModels;

namespace Ticketverkoop.Controllers
{
    public class ShoppingCartController : Controller
    {

        private AspNetUsersService usersService;
        public ShoppingCartController()
        {
            usersService = new AspNetUsersService();
        }


        public IActionResult Index()
        {
            ShoppingCartVM cartList =
              HttpContext.Session.GetObject<ShoppingCartVM>("ShoppingCart");

            // call SessionID
            var SessionId = HttpContext.Session.Id;

            return View(cartList);
        }
        public IActionResult Delete(int? Item)
        {
            if (Item == null)
            {
                return NotFound();
            }

            ShoppingCartVM cartList =
                HttpContext.Session.GetObject<ShoppingCartVM>("ShoppingCart");
            
            // call SessionID
            var SessionId = HttpContext.Session.Id;
            var itemToRemove = cartList.ShoppingCart[Convert.ToInt32(Item)];
            if (itemToRemove != null)
            {
                cartList.ShoppingCart.Remove(itemToRemove);
                if (cartList.ShoppingCart.Count == 0)
                {
                    cartList = null;
                }
                HttpContext.Session.SetObject("ShoppingCart", cartList);
            }

            return View("Index", cartList);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Payment(ShoppingCartVM shoppingCartVM)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            AspNetUsers userNow = usersService.GetUserById(userId);
            if (!userNow.EmailConfirmed)
            {
                ModelState.AddModelError("error", "U moet eerst uw email bevestigen.");
                return View("Index", shoppingCartVM);
            }
            else
            {
                for (int i = 0; i < shoppingCartVM.ShoppingCart.Count; i++)
                {
                    CartVM cart = shoppingCartVM.ShoppingCart[i];

                    shoppingCartVM.ShoppingCart[i].WedstrijdId = cart.WedstrijdId;
                    shoppingCartVM.ShoppingCart[i].ThuisClubId = cart.ThuisClubId;
                    shoppingCartVM.ShoppingCart[i].ThuisClubNaam = cart.ThuisClubNaam;
                    shoppingCartVM.ShoppingCart[i].UitCLubNaam = cart.UitCLubNaam;
                    shoppingCartVM.ShoppingCart[i].StadiumNaam = cart.StadiumNaam;
                    shoppingCartVM.ShoppingCart[i].VakNaam = cart.VakNaam;
                    shoppingCartVM.ShoppingCart[i].WedstrijdDatum = cart.WedstrijdDatum;
                    shoppingCartVM.ShoppingCart[i].Prijs = cart.Prijs;
                    shoppingCartVM.ShoppingCart[i].Aantal = cart.Aantal;

                }


            }

            return View();
        }
    }
}