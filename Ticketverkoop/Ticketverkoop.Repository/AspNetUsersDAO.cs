using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ticketverkoop.Domain.Entities;

namespace Ticketverkoop.Repository
{
    public class AspNetUsersDAO
    {
        private readonly TicketverkoopContext _dbContext;

        public AspNetUsersDAO()
        {
            _dbContext = new TicketverkoopContext();
        }

        public AspNetUsers GetUserById(string id)
        {
            return _dbContext.AspNetUsers
                .Where(u => u.Id == id)
                .Include(u => u.AspNetUserClaims)
                .Include(u => u.AspNetUserLogins)
                .Include(u => u.AspNetUserRoles)
                .Include(u => u.AspNetUserTokens)
                .Include(u => u.Bestelling)
                .Include(u => u.Ticket)
                .Include(u => u.Voucher)
                .First();
        }

        public IEnumerable<AspNetUsers> GetAll()
        {
            return _dbContext.AspNetUsers
                .Include(c => c.AspNetUserClaims)
                .Include(l => l.AspNetUserLogins)
                .Include(r => r.AspNetUserRoles)
                .Include(t => t.AspNetUserTokens)
                .Include(b => b.Bestelling)
                .Include(w => w.Ticket)
                .Include(v => v.Voucher)
                .ToList();
        }
    }
}
