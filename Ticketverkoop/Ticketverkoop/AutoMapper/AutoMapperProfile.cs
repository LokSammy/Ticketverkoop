using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Ticketverkoop.Domain.Entities;
using Ticketverkoop.ViewModels;

namespace Ticketverkoop.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Stadion, StadionVM>();

            CreateMap<Club, ClubVM>();

            CreateMap<Wedstrijd, WedstrijdVM>()
                .ForMember(dest => dest.ThuisClubNaam, opts => opts.MapFrom(src => src.ThuisClub.Naam))
                .ForMember(dest => dest.UitClubNaam, opts => opts.MapFrom(src => src.UitClub.Naam));
        }
    }
}
