using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LEGO_Server.Models;


namespace LEGO_Server.Repository
{
    public interface IBrickRepository
    {
        void Add(BrickUnit Brick);
        IEnumerable<BrickUnit> GetAll();
        BrickUnit Find(string key);
        void Remove(string key);
        void Update(BrickUnit item);
    }
}