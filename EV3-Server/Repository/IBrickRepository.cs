using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EV3_Server.Models;


namespace EV3_Server.Repository
{
    public interface IBrickRepository
    {
        void Add(BrickUnit Brick);
        IEnumerable<BrickView> GetAll();
        BrickUnit Find(string key);
        void Remove(string key);
        void Update(BrickUnit item);
        IEnumerable<SensorUnit> GetSensor();
        SensorUnit FindSensor(string key);
    }
}