using System;
using System.Text;
using System.Linq;
using Ticketverkoop.Domain.Entities;
using System.Collections.Generic;

namespace Ticketverkoop.Repository
{
    public class ClubDAO
    {
        private readonly TicketverkoopContext _dbContext;

        public ClubDAO()
        {
            _dbContext = new TicketverkoopContext();
        }

        public IEnumerable<Club> GetAll()
        {
            return _dbContext.Club.ToList();
        }
    }
}
