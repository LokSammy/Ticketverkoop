using System;
using System.Text;
using System.Linq;
using Ticketverkoop.Domain.Entities;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Ticketverkoop.Repository
{
    public class ClubDAO
    {
        private readonly TicketVerkoopContext _dbContext;

        public ClubDAO()
        {
            _dbContext = new TicketVerkoopContext();
        }

        public Club GetClubById(int id)
        {
            return _dbContext.Club
                .Where(s => s.Id == id)
                .Include(s => s.Stadion)
                .First();
        }

        public IEnumerable<Club> GetAll()
        {
            return _dbContext.Club
                .Include(s => s.Stadion)
                .ToList();
        }
    }
}
