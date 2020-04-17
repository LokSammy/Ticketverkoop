using System;
using System.Collections.Generic;
using System.Text;
using Ticketverkoop.Domain.Entities;
using Ticketverkoop.Repository;

namespace Ticketverkoop.Service
{
    public class StadionVakService
    {
        private StadionVakDAO _stadionVakDAO;

        public StadionVakService()
        {
            _stadionVakDAO = new StadionVakDAO();
        }

        public IEnumerable<StadionVak> GetAll()
        {
            return _stadionVakDAO.GetAll();
        }

        public StadionVak GetStadionVakByStadIdAndVakId(int stadId, int vakId)
        {
            return _stadionVakDAO.GetStadionVakByStadIdAndVakId(stadId, vakId);
        }
    }
}
