using System;
using System.Collections.Generic;
using System.Text;
using Ticketverkoop.Repository;
using Ticketverkoop.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Ticketverkoop.Repository
{
    public class StadionDAO
    {
        private readonly TicketVerkoopContext _dbContext;

        public StadionDAO()
        {
            _dbContext = new TicketVerkoopContext();
        }

        public IEnumerable<Stadion> GetAll()
        {
            return _dbContext.Stadion.ToList();
        }

        public Stadion GetStadionById(int id)
        {
            return _dbContext.Stadion
                .Where(s => s.Id == id)
                .First();
        }
    }
}
