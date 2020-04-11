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
        private readonly TicketVerkoopContext _dbContext;

        public VakDAO()
        {
            _dbContext = new TicketVerkoopContext();
        }

        public List<Vak> GetAll()
        {
            try
            {
                return _dbContext.Vak.ToList();
            } 
            catch (Exception e)
            {
                throw e;
            }
        } 
    }
}
