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
    }
}