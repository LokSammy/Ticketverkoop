using System;
using System.Collections.Generic;
using System.Text;
using Ticketverkoop.Domain.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Ticketverkoop.Repository
{
    public class WedstrijdDAO
    {
        private readonly TicketverkoopContext _dbContext;

        public WedstrijdDAO()
        {
            _dbContext = new TicketverkoopContext();
        }

        public IEnumerable<Wedstrijd> GetAll()
        {
            return _dbContext.Wedstrijd
                //.Where(s => s.Datum > DateTime.Now)
                .Include(s => s.UitClub)
                .Include(s => s.ThuisClub.Stadion.StadionVak)
                .OrderBy(s => s.Datum)
                .ToList();
        }

        public Wedstrijd GetWedstrijdById(int Id)
        {
            return _dbContext.Wedstrijd
                .Where(s => s.Id == Id)
                .Include(s => s.ThuisClub)
                .Include(s => s.UitClub).First();
        }

        public IEnumerable<Wedstrijd> GetWedstrijdenByClub(int? ClubId)
        {
            return _dbContext.Wedstrijd
                .Where(s => (s.ThuisClubId == ClubId || s.UitClubId == ClubId))
                .Include(s => s.ThuisClub)
                .Include(s => s.UitClub)
                .ToList();
        }
    }
}
