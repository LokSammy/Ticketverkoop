using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ticketverkoop.Domain.Entities;
using Ticketverkoop.Extensions;
using Ticketverkoop.Service;
using Ticketverkoop.ViewModels;

namespace Ticketverkoop.Controllers
{
    public class ClubController : Controller
    {
        private readonly IMapper _mapper;

        private ClubService clubService;
        private VakService vakService;
        private StadionService stadionService;
        private StadionVakService stadionVakService;

        public ClubController(IMapper mapper)
        {
            _mapper = mapper;
            clubService = new ClubService();
            vakService = new VakService();
            stadionService = new StadionService();
            stadionVakService = new StadionVakService();
        }

        public IActionResult Index()
        {
            var list = clubService.GetAll();

            var listVM = _mapper.Map<List<ClubVM>>(list);

            return View(listVM);
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Club club = clubService.GetClubById(Convert.ToInt32(id));

            if (club == null)
            {
                return NotFound();
            }

            var clubDetails = new ClubVM();
            clubDetails = _mapper.Map<ClubVM>(club);

            
                clubDetails.Vakken = new SelectList(vakService.GetAll(), "Id", "Omschrijving");
            

            return View(clubDetails);
        }

        public IActionResult Koop(int? aboClubId, int? vakId)
        {
            ClubVM getClubVM(int? id)
            {
                Club clubDetails = clubService.GetClubById(Convert.ToInt32(id));

                var clubVM = new ClubVM();
                clubVM = _mapper.Map<ClubVM>(clubDetails);


                clubVM.Vakken = new SelectList(vakService.GetAll(), "Id", "Omschrijving");

                return clubVM;
            }

            if (vakId == null || vakId == 0 || aboClubId == 0 || aboClubId == null)
            {
                if (aboClubId == null || aboClubId == 0)
                {
                    return NotFound();
                   
                }
                else {
                    ModelState.AddModelError("error", "Er moet een vak gekozen worden.");
                    return View("Details", getClubVM(aboClubId));
                }
            }

            Club club = clubService.GetClubById(Convert.ToInt32(aboClubId));
            Stadion stadion = stadionService.GetStadionById(Convert.ToInt32(club.StadionId));
            Vak vak = vakService.GetVakById(Convert.ToInt32(vakId));
            StadionVak stadionVak = stadionVakService.GetStadionVakByStadIdAndVakId(Convert.ToInt32(stadion.Id),Convert.ToInt32(vak.Id));
            decimal? kostprijs = stadionVak.AbonnementPrijs;
            //abonementprijs is null mag niet moet nog aangepast worden in database
            kostprijs = 100;

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

            Boolean shoppingcartHeeftEenAbo()
            {
                Boolean isAbo = false;
                foreach (CartVM cart in shopping.ShoppingCart)
                {
                    if (cart.WedstrijdId.Equals(null)) { isAbo = true; }
                }
                return isAbo;
            }

            if (shoppingcartVol())
            {
                ModelState.AddModelError("error", "Winkelkar zit vol");


                return View("Details", getClubVM(club.Id));
            }
            else if (shoppingcartHeeftEenAbo())
            {
                ModelState.AddModelError("error", "Je mag geen twee abonementen in je winkelmandje hebben");


                return View("Details", getClubVM(club.Id));
            }
            else
            {
                CartVM item = new CartVM
                {
                    ThuisClubId = club.Id,
                    ThuisClubNaam = club.Naam,
                    StadiumNaam = stadion.Naam,
                    UitCLubNaam = null,
                    VakNaam = vak.Omschrijving,
                    VakId = vak.Id,
                    WedstrijdId = null,
                    WedstrijdDatum = null,
                    Aantal = 1,
                    Prijs = (decimal)kostprijs

                };

                shopping.ShoppingCart.Add(item);
                HttpContext.Session.SetObject("ShoppingCart", shopping);
                return RedirectToAction("Index", "ShoppingCart");

            }
        }
    }
}
