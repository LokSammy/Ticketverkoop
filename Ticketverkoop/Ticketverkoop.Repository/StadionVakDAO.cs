using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ticketverkoop.Domain.Entities;

namespace Ticketverkoop.Repository
{
    public class StadionVakDAO
    {
        private readonly TicketverkoopContext _dbContext;

        public StadionVakDAO()
        {
            _dbContext = new TicketverkoopContext();
        }

        public IEnumerable<StadionVak> GetAll()
        {
            return _dbContext.StadionVak.ToList();
        }

        public StadionVak GetStadionVakByStadIdAndVakId(int stadId, int vakId)
        {
                return _dbContext.StadionVak
                .Where(s => (s.StadionId == stadId && s.VakId == vakId))
                .Include(s => s.Stadion)
                .Include(s => s.Vak).First();
        }
    }
}
