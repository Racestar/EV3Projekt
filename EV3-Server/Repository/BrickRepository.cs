using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using LEGO_Server.Models;

namespace LEGO_Server.Repository
{
    public class BrickRepository : IBrickRepository
    {
        static List<BrickUnit> BrickList = new List<BrickUnit>();
        void IBrickRepository.Add(BrickUnit Brick)
        {
            Brick.ID = Guid.NewGuid().ToString();
            BrickList.Add(Brick);
        }
        public BrickUnit Find(string key)
        {
            return BrickList
                .Where(e => e.ID.Equals(key))
                .SingleOrDefault();
        }

        public IEnumerable<BrickUnit> GetAll()
        {
            return BrickList;
        }

        public void Remove(string key)
        {
            var itemToRemove = BrickList.SingleOrDefault(r => r.ID == key);
            if (itemToRemove != null)
                BrickList.Remove(itemToRemove);
        }

        public void Update(BrickUnit item)
        {
            var itemToupdate = BrickList.SingleOrDefault(r => r.ID == item.ID);
            if (itemToupdate != null)
            {
                itemToupdate.ComPort = item.ComPort;
            }
        }

       
    }
}
