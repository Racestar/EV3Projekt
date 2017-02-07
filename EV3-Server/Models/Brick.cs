using System;
using System.Collections.Generic;
using MonoBrick.EV3;


namespace EV3_Server.Models
{
    public class BrickUnit
    {
        public int ComPort { get; set; }
        public string ID { get; set; }
        public Brick<Sensor, Sensor, Sensor, Sensor> EV3 { get; set; }
        public List<SensorUnit> SensorList { get; set; }
    }

    public class BrickView
    {
        public int ComPort { get; set; }
        public string ID { get; set; }
        public List<SensorUnit> SensorList { get; set; }
    }
}
