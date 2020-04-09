using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ticketverkoop.Service;
using Ticketverkoop.ViewModels;

namespace Ticketverkoop.Controllers
{
    public class ClubController : Controller
    {
        private readonly IMapper _mapper;

        private ClubService clubService;

        public ClubController(IMapper mapper)
        {
            _mapper = mapper;
            clubService = new ClubService();
        }

        public IActionResult Index()
        {
            var list = clubService.GetAll();

            var listVM = _mapper.Map<List<ClubVM>>(list);

            return View(listVM);
        }
    }
}
