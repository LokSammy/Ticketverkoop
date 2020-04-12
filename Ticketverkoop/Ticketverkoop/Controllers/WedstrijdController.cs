using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Ticketverkoop.Domain.Entities;
using Ticketverkoop.Extensions;
using Ticketverkoop.Service;
using Ticketverkoop.ViewModels;

namespace Ticketverkoop.Controllers
{
    public class WedstrijdController : Controller
    {
        private readonly IMapper _mapper;

        private  WedstrijdService wedstrijdService;
        private  VakService vakService;
        private  ClubService clubService;

        public WedstrijdController(IMapper mapper)
        {
            _mapper = mapper;
            wedstrijdService = new WedstrijdService();
            vakService = new VakService();
            clubService = new ClubService();
        }

        public IActionResult Index()
        {
            ViewBag.lstClubs = new SelectList(clubService.GetAll(), "Id", "Naam");

            var list = wedstrijdService.GetAll();

            var listVM = _mapper.Map<List<WedstrijdVM>>(list);

            foreach(WedstrijdVM wedstrijdVM in listVM)
            {
                wedstrijdVM.Vakken = new SelectList(vakService.GetAll(), "Id", "Omschrijving");
            }

            return View(listVM);
        }

        [HttpPost]
        public IActionResult Index(int? clubId)
        {
            if (clubId != null)
            {
                var clublist = wedstrijdService.GetWedstrijdenByClub(Convert.ToInt32(clubId));

                List<WedstrijdVM> listClubVM = _mapper.Map<List<WedstrijdVM>>(clublist);

                return View(listClubVM);
            }

            var list = wedstrijdService.GetAll();

            var listVM = _mapper.Map<List<WedstrijdVM>>(list);

            return View(listVM);
        }

        public IActionResult Koop(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Wedstrijd wedstrijd = wedstrijdService.GetWedstrijdById(Convert.ToInt32(id));

            CartVM item = new CartVM
            {
                WedstrijdId = wedstrijd.Id,
                Prijs = 15,
                WedstrijdDatum = wedstrijd.Datum,
                UitCLub = wedstrijd.UitClub.Naam,
                ThuisClub = wedstrijd.ThuisClub.Naam
            };

            ShoppingCartVM shopping;

             if (HttpContext.Session.GetObject<ShoppingCartVM>("ShoppingCart") != null)
            {
                shopping = HttpContext.Session.GetObject<ShoppingCartVM>("ShoppingCart");
            }
            else
            {
                shopping = new ShoppingCartVM();
                shopping.Cart = new List<CartVM>();
            }

            Boolean wedstrijdZitAlInShoppingCart = false;
            foreach (CartVM cart in shopping.Cart)
            {
                if (cart.WedstrijdId == item.WedstrijdId)
                {
                    wedstrijdZitAlInShoppingCart = true;
                    ViewBag.Message = "U kan niet 2 keer dezelfde wedstrijd boeken.";
                }
            }
            if (wedstrijdZitAlInShoppingCart == false)
            {
                shopping.Cart.Add(item);
            }
            HttpContext.Session.SetObject("ShoppingCart", shopping);
            return RedirectToAction("Index", "ShoppingCart");

        }
    }
}