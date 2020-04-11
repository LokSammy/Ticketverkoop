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
    }
}
