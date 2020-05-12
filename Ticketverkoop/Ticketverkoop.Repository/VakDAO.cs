using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ticketverkoop.Domain.Entities;
using Ticketverkoop.Repository;

namespace Ticketverkoop.Repository
{
    public class VakDAO
    {
        private readonly TicketverkoopContext _dbContext;

        public VakDAO()
        {
            _dbContext = new TicketverkoopContext();
        }

        public Vak GetVakById(int id)
        {
            
                return _dbContext.Vak
                .Where(s => s.Id == id)
                .First();
            
        }

        public List<Vak> GetAll()
        {
            
                return _dbContext.Vak.ToList();
            
            
        }
    }
}
