using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ticketverkoop.Domain.Entities;
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

        public ClubController(IMapper mapper)
        {
            _mapper = mapper;
            clubService = new ClubService();
            vakService = new VakService();
            stadionService = new StadionService();
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

        public IActionResult Koop(Club aboClub, Vak aboVak)
        {            
            double kostprijs = 100.00;

            Stadion stadion = stadionService.GetStadionById(aboClub.StadionId);


            CartVM item = new CartVM
            {
                ThuisClubId = aboClub.Id,
                ThuisClubNaam = aboClub.Naam,
                StadiumNaam = stadion.Naam,
                VakNaam = aboVak.Omschrijving,
                Aantal = 1,
                Prijs = 100              

            };
            return View();
        }
    }
}
