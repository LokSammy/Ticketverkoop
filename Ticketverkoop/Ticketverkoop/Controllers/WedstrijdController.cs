using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
        private StadionService stadionService;
        private StadionVakService stadionVakService;

        public WedstrijdController(IMapper mapper)
        {
            _mapper = mapper;
            wedstrijdService = new WedstrijdService();
            vakService = new VakService();
            clubService = new ClubService();
            stadionService = new StadionService();
            stadionVakService = new StadionVakService();
        }

        public IActionResult Index(int? clubId)
        {
            ViewBag.lstClubs = new SelectList(clubService.GetAll(), "Id", "Naam");

            if (clubId != null)
            {
                var clublist = wedstrijdService.GetWedstrijdenByClub(Convert.ToInt32(clubId));

                List<WedstrijdVM> listClubVM = _mapper.Map<List<WedstrijdVM>>(clublist);

                foreach (WedstrijdVM wedstrijdVM in listClubVM)
                {
                    int wedId = wedstrijdVM.Id;
                    string stadNaam = stadionService.GetStadionById(wedstrijdService.GetWedstrijdById(Convert.ToInt32(wedId)).ThuisClub.StadionId).Naam;
                    wedstrijdVM.StadionNaam = stadNaam;
                    wedstrijdVM.Vakken = new SelectList(vakService.GetAll(), "Id", "Omschrijving");
                }

                return View(listClubVM);
            }

            var list = wedstrijdService.GetAll();

            var listVM = _mapper.Map<List<WedstrijdVM>>(list);

            foreach (WedstrijdVM wedstrijdVM in listVM)
            {
                int wedId = wedstrijdVM.Id;
                string stadNaam = stadionService.GetStadionById(wedstrijdService.GetWedstrijdById(Convert.ToInt32(wedId)).ThuisClub.StadionId).Naam;
                wedstrijdVM.StadionNaam = stadNaam;
                wedstrijdVM.Vakken = new SelectList(vakService.GetAll(), "Id", "Omschrijving");
            }

            return View(listVM);
        }


        public IActionResult Koop(int? wedstrijdId, int? vakId, int atlTickets)
        {
            //geeft listVM terug voor alle wedstrijden
            IEnumerable<WedstrijdVM> getListWedstrijdVMs()
            {
                ViewBag.lstClubs = new SelectList(clubService.GetAll(), "Id", "Naam");


                var list = wedstrijdService.GetAll();

                var listVM = _mapper.Map<List<WedstrijdVM>>(list);

                foreach (WedstrijdVM wedstrijdVM in listVM)
                {
                    int wedId = wedstrijdVM.Id;
                    string stadNaam = stadionService.GetStadionById(wedstrijdService.GetWedstrijdById(Convert.ToInt32(wedId)).ThuisClub.StadionId).Naam;
                    wedstrijdVM.StadionNaam = stadNaam;
                    wedstrijdVM.Vakken = new SelectList(vakService.GetAll(), "Id", "Omschrijving");
                }
                return listVM;
            }

            if (wedstrijdId == null || vakId == null || atlTickets == 0)
            {
                if (vakId == null || atlTickets == 0) 
                {
                    if (atlTickets == 0) { ModelState.AddModelError("error", "Aantal mag niet nul zijn."); }
                    if (vakId == null) { ModelState.AddModelError("error", "Er moet een vak gekozen worden."); }
                    return View("Index", getListWedstrijdVMs());
                }
                else { return NotFound(); }

            }

            Wedstrijd wedstrijd = wedstrijdService.GetWedstrijdById(Convert.ToInt32(wedstrijdId));
            Club thuisclub = clubService.GetClubById(Convert.ToInt32(wedstrijd.ThuisClubId));
            Club uitclub = clubService.GetClubById(Convert.ToInt32(wedstrijd.UitClubId));
            Stadion stadion = stadionService.GetStadionById(Convert.ToInt32(thuisclub.StadionId));
            Vak vak = vakService.GetVakById(Convert.ToInt32(vakId));
            StadionVak stadionVak = stadionVakService.GetStadionVakByStadIdAndVakId(Convert.ToInt32(stadion.Id), Convert.ToInt32(vak.Id));
            ShoppingCartVM shopping;
            int atlItems = 0;

            if (HttpContext.Session.GetObject<ShoppingCartVM>("ShoppingCart") != null)
            {
                shopping = HttpContext.Session.GetObject<ShoppingCartVM>("ShoppingCart");
            }
            else
            {
                shopping = new ShoppingCartVM();
                shopping.ShoppingCart = new List<CartVM>();
            }

            Boolean shoppingcartVol()
            {
                foreach (CartVM cart in shopping.ShoppingCart)
                {
                    atlItems += cart.Aantal;
                }
                if (atlItems >= 10)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            Boolean zelfdeWedstrijdDatum()
            { 
                foreach(CartVM cart in shopping.ShoppingCart)
                {
                    if(cart.WedstrijdDatum == wedstrijd.Datum)
                    {
                        return false;
                    }
                }

                return true;
            }

            if (!zelfdeWedstrijdDatum())
            {
                ModelState.AddModelError("error", "Op deze datum hebt u al een ticket besteld.");

                return View("Index", getListWedstrijdVMs());
            }

            if (shoppingcartVol())
            {
                ModelState.AddModelError("error", "Winkelkar zit vol");


                return View("Index", getListWedstrijdVMs());
            }
            else if((atlItems + atlTickets) > 10)
            {
                ModelState.AddModelError("error", "Winkelkar raakt vol men kan slecht nog " + (10 - atlItems) + " wedstrijdtickets bestellen");
                return View("Index", getListWedstrijdVMs());
            } 
            else
            {
                CartVM item = new CartVM
                {
                    WedstrijdId = wedstrijd.Id,
                    ThuisClubId = thuisclub.Id,
                    ThuisClubNaam = thuisclub.Naam,
                    UitCLubNaam = uitclub.Naam,
                    StadiumNaam = stadion.Naam,
                    VakNaam = vak.Omschrijving,
                    WedstrijdDatum = wedstrijd.Datum,
                    Prijs = (decimal) stadionVak.Prijs,
                    Aantal = atlTickets,
                    VakId = vak.Id
                };

                shopping.ShoppingCart.Add(item);
                HttpContext.Session.SetObject("ShoppingCart", shopping);
                return RedirectToAction("Index", "ShoppingCart");
            }
        }
    }
}