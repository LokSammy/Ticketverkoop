using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Ticketverkoop.Domain.Entities;
using Ticketverkoop.Service;
using Ticketverkoop.ViewModels;

namespace Ticketverkoop.Controllers
{
    public class WedstrijdController : Controller
    {
        private IMapper _mapper;

        private readonly WedstrijdService wedstrijdService;

        public WedstrijdController(IMapper mapper)
        {
            _mapper = mapper;
            wedstrijdService = new WedstrijdService();
        }

        public IActionResult Index()
        {
            var list = wedstrijdService.GetAll();

            var listVM = _mapper.Map<List<WedstrijdVM>>(list);

            return View(listVM);
        }
    }
}