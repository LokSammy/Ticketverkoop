using System;
using System.Collections.Generic;
using System.Text;
using Ticketverkoop.Domain.Entities;
using Ticketverkoop.Repository;

namespace Ticketverkoop.Service
{
    public class AspNetUsersService
    {
        private AspNetUsersDAO _userDAO;

        public AspNetUsersService()
        {
            _userDAO = new AspNetUsersDAO();
        }

        public IEnumerable<AspNetUsers> GetAll()
        {
            return _userDAO.GetAll();
        }

        public AspNetUsers GetUserById(string id)
        {
            return _userDAO.GetUserById(id);
        }
    }
}
