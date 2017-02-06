using System;



namespace LEGO_Server.Models
{
    public class BrickUnit
    {
        public int ComPort { get; set; }
        public string ID { get; set; }
        public bool IsConnected { get; set; }
    }

    public class Divice
    {
        public string Port { get; set; }
    }
    public class Motor : Divice { }

    public class Sensor : Divice { }



}