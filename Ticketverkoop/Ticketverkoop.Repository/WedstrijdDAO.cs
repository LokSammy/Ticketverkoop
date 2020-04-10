using System;
using System.Collections.Generic;
using System.Text;
using Ticketverkoop.Domain.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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
                .Include(s => s.ThuisClub)
                .Include(s => s.UitClub)
                .ToList();
        }
    }
}
