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

        public ClubController(IMapper mapper)
        {
            _mapper = mapper;
            clubService = new ClubService();
            vakService = new VakService();
        }

        public IActionResult Index()
        {
            var list = clubService.GetAll();

            var listVM = _mapper.Map<List<ClubVM>>(list);

            foreach (ClubVM clubVM in listVM)
            {
                clubVM.Vakken = new SelectList(vakService.GetAll(), "Id", "Omschrijving");
            }

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

            return View(clubDetails);
        }
    }
}
