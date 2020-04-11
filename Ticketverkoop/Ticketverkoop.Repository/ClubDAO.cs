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

        public Club GetClubById(int Id)
        {
            return _dbContext.Club
                .Where(s => s.Id == Id)
                .Include(s => s.Stadion).First();
        }

        public IEnumerable<Club> GetAll()
        {
            return _dbContext.Club.ToList();
        }
    }
}
