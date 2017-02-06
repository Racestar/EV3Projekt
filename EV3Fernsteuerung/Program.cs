using System;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using MonoBrick.EV3;

namespace SteuerungTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //Port für Kommunikation
            var ev3 = new Brick<Sensor, Sensor, Sensor, Sensor>("COM40");
            try
            {
                //Stelle Bluetoothverbindung her
                ev3.Connection.Open();

                //Motoren synchronisieren
                ev3.MotorSync.BitField = OutputBitfield.OutA | OutputBitfield.OutD;

                //Ports für Motoren
                ev3.Vehicle.LeftPort = MotorPort.OutA;
                ev3.Vehicle.RightPort = MotorPort.OutD;

                //Vorerst nur Vorwärts
                ev3.Vehicle.ReverseLeft = false;
                ev3.Vehicle.ReverseRight = false;

                //Default Geschwindigkeit
                sbyte speed = 50;
                sbyte revspeed = -50;//(sbyte)((int)speed*(-1));

                //Sensoren
                ev3.Sensor1 = new IRSensor(IRMode.Proximity);
                ev3.Sensor4 = new GyroSensor(GyroMode.Angle);

                ConsoleKeyInfo cki;
                float IRValue;
                float GValue;
                //float DValue;
                Console.WriteLine("Drücke U zum Verlassen.");
                //führe Schleife aus, solange bis U gedrückt wird
                do
                {
                    //Sensorwerte lesen
                    IRValue = ev3.Sensor1.Read();
                    GValue = ev3.Sensor4.Read();
                    cki = Console.ReadKey(true); //lese Eingabe
                    switch (cki.Key)
                    {
                        case ConsoleKey.W:
                            IRValue = ev3.Sensor1.Read();
                            //vor Hindernis stoppen
                            if (IRValue <= 20)
                            {
                                Console.WriteLine("Hindernis voraus! Bitte wenden.");
                                ev3.Vehicle.Brake();
                            }
                            else
                                ev3.Vehicle.Forward(speed);
                            break;
                        case ConsoleKey.S:
                                ev3.Vehicle.Backward(speed);
                                break;
                        case ConsoleKey.Q:
                            IRValue = ev3.Sensor1.Read();
                            //vor Hindernis stoppen
                            if (IRValue <= 20)
                            {
                                Console.WriteLine("Hindernis voraus! Bitte wenden.");
                                ev3.Vehicle.Brake();
                            }
                            else
                                ev3.MotorSync.On(speed, -100);
                            break;
                        case ConsoleKey.E:
                            IRValue = ev3.Sensor1.Read();
                            //vor Hindernis stoppen
                            if (IRValue <= 20)
                            {
                                Console.WriteLine("Hindernis voraus! Bitte wenden.");
                                ev3.Vehicle.Brake();
                            }
                            else
                            ev3.MotorSync.On(speed, 100);
                            break;
                        case ConsoleKey.A:
                            ev3.MotorSync.On(revspeed, -100);
                            break;
                        case ConsoleKey.D:
                            ev3.MotorSync.On(revspeed, 100);
                            break;
                        case ConsoleKey.T://180Grad-Drehung(auf glatter Holzoberfläche)
                            ev3.MotorA.On(50, 380, true);
                            ev3.MotorD.On(-50, 380, true);
                            /*GValue = ev3.Sensor4.Read();
                            DValue = GValue +180;
                            do
                            {
                                ev3.MotorA.On(50);
                                ev3.MotorD.On(-50);
                                GValue = ev3.Sensor4.Read();
                            }while (GValue < DValue);
                            ev3.Vehicle.Brake();*/
                            break;
                        case ConsoleKey.UpArrow://schneller
                            if(speed < 100)
                                speed = (sbyte)(speed + 10);
                                revspeed = (sbyte)((int)speed * (-1));
                                Console.WriteLine("Geschwindigkeit gesetzt auf " + speed);
                                //ev3.MotorSync.On(speed, 0);
                            break;
                        case ConsoleKey.DownArrow://langsamer
                            if(speed > 0)
                                speed = (sbyte)(speed - 10);
                                revspeed = (sbyte)((int)speed * (-1));
                                Console.WriteLine("Geschwindigkeit gesetzt auf " + speed);
                                //ev3.MotorSync.On(speed, 0);
                            break;
                        case ConsoleKey.Spacebar:
                            ev3.Vehicle.Brake();
                            break;
                        //Ausgabe Sensorwerte
                        case ConsoleKey.D1://1 auf normaler Tastatur
                            Console.WriteLine("S1: " + ev3.Sensor1.ReadAsString());
                            break;
                        case ConsoleKey.D4://4 auf normaler Tastatur     
                            Console.WriteLine("S4: " + ev3.Sensor4.ReadAsString());
                            break;
                    }
                } while (cki.Key != ConsoleKey.U);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                Console.WriteLine("Fehler: " + e.Message);
                Console.WriteLine("Drücke irgendeine Taste zum Beenden...");
                Console.ReadKey();
            }
            //Verbindung wird nach Beendigung des Vorgangs geschlossen
            finally
            {
                ev3.Connection.Close();
            }
        }
    }
}
