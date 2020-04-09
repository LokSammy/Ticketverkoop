using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Ticketverkoop.Service;
using Ticketverkoop.ViewModels;

namespace Ticketverkoop.Controllers
{
    public class StadionController : Controller
    {
        private readonly IMapper _mapper;

        private StadionService stadionService;

        public StadionController(IMapper mapper)
        {
            _mapper = mapper;
            stadionService = new StadionService();
        }

        public IActionResult Index()
        {
            var list = stadionService.GetAll();

            var listVM = _mapper.Map<List<StadionVM>>(list);

            return View(listVM);
        }
    }
}