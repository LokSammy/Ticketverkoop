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
        private readonly TicketVerkoopContext _dbContext;

        public WedstrijdDAO()
        {
            _dbContext = new TicketVerkoopContext();
        }

        public IEnumerable<Wedstrijd> GetAll()
        {
            return _dbContext.Wedstrijd
                .Where(s => s.Datum > DateTime.Now)
                .Include(s => s.ThuisClub)
                .Include(s => s.UitClub)
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
