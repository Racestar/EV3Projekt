using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using EV3_Server.Models;
using Microsoft.Extensions.Logging;
using MonoBrick.EV3;

namespace EV3_Server.Repository
{
    public class BrickRepository : IBrickRepository
    {
        static List<BrickUnit> BrickList = new List<BrickUnit>();

        void IBrickRepository.Add(BrickUnit Brick)
        {
            Brick.ID = Guid.NewGuid().ToString();
            BrickList.Add(Brick);
            Connect(Brick);
        }
        public BrickUnit Find(string key)
        {
            return BrickList
                .Where(e => e.ID.Equals(key))
                .SingleOrDefault();
        }

        public IEnumerable<BrickView> GetAll()
        {
            List<BrickView> View = new List<BrickView>();
            foreach (var item in BrickList)
            {
                BrickView BVtemp = new BrickView();
                BVtemp.ID = item.ID;
                BVtemp.ComPort = item.ComPort;
                //Lesen der aktuellen Sensorwerte für Sensor 1
                //!!! Prüfung ob Brick connected!!!
                try
                {
                    SensorUnit Sens = null;
                    Sens.Name = item.EV3.Sensor1.GetName();
                    Sens.Value = item.EV3.Sensor1.ReadAsString();
                    BVtemp.SensorList.Add(Sens);
                }
                finally { }
                //Lesen der aktuellen Sensorwerte für Sensor 2
                try
                {
                    SensorUnit Sens = null;
                    Sens.Name = item.EV3.Sensor2.GetName();
                    Sens.Value = item.EV3.Sensor2.ReadAsString();
                    BVtemp.SensorList.Add(Sens);
                }
                finally { }
                //Lesen der aktuellen Sensorwerte für Sensor 3
                try
                {
                    SensorUnit Sens = null;
                    Sens.Name = item.EV3.Sensor3.GetName();
                    Sens.Value = item.EV3.Sensor3.ReadAsString();
                    BVtemp.SensorList.Add(Sens);
                }
                finally { }
                //Lesen der aktuellen Sensorwerte für Sensor 4
                try
                {
                    SensorUnit Sens = null;
                    Sens.Name = item.EV3.Sensor4.GetName();
                    Sens.Value = item.EV3.Sensor4.ReadAsString();
                    BVtemp.SensorList.Add(Sens);
                }
                finally { }

                View.Add(BVtemp);
            }

                return View;
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
        private string Connect(BrickUnit item)
        {
            item.EV3 = new Brick<Sensor, Sensor, Sensor, Sensor>("COM"+item.ComPort);
            try
            {
                item.EV3.Connection.Open();

                Sensor newSensor = null;
                if (SensorHelper.SensorDictionary.TryGetValue(SensorHelper.TypeToKey(item.EV3.Sensor1.GetSensorType()), out newSensor)) item.EV3.Sensor1 = newSensor;
                if (SensorHelper.SensorDictionary.TryGetValue(SensorHelper.TypeToKey(item.EV3.Sensor2.GetSensorType()), out newSensor)) item.EV3.Sensor2 = newSensor;
                if (SensorHelper.SensorDictionary.TryGetValue(SensorHelper.TypeToKey(item.EV3.Sensor3.GetSensorType()), out newSensor)) item.EV3.Sensor3 = newSensor;
                if (SensorHelper.SensorDictionary.TryGetValue(SensorHelper.TypeToKey(item.EV3.Sensor4.GetSensorType()), out newSensor)) item.EV3.Sensor4 = newSensor;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return null;
        }

        public IEnumerable<SensorUnit> GetSensor()
        {
            throw new NotImplementedException();
        }

        public SensorUnit FindSensor(string key)
        {
            throw new NotImplementedException();
        }
    }
}


