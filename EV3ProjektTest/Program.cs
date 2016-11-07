using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
using System.Threading;
//using System.Threading.Tasks;
using MonoBrick.EV3;

namespace EV3ProjektTest
{
    class Program
    {
        static Brick<Sensor, Sensor, Sensor, Sensor> ev3;
        static void Main(string[] args)
        {
            /*Position control
            ev3 = new Brick<Sensor, Sensor, Sensor, Sensor>("COM40");
            try
            {
                ev3.Connection.Open();
                ev3.MotorA.ResetTacho();
                ev3.MotorA.On(50, 6 * 360, true);
                WaitForMotorToStop();
                Console.WriteLine("Position: " + ev3.MotorA.GetTachoCount());
                ev3.MotorA.On(-50, 9 * 360, true);
                WaitForMotorToStop();
                Console.WriteLine("Position: " + ev3.MotorA.GetTachoCount());
                ev3.MotorA.MoveTo(50, 0, true);
                WaitForMotorToStop();
                ev3.MotorA.Off();
                Console.WriteLine("Position: " + ev3.MotorA.GetTachoCount());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                Console.WriteLine("Error: " + e.Message);
                Console.WriteLine("Press any key to end...");
                Console.ReadKey();
            }
            finally
            {
                ev3.Connection.Close();
            }
        }

        static void WaitForMotorToStop()
        {
            Thread.Sleep(500);
            while (ev3.MotorA.IsRunning()) { Thread.Sleep(50); }
        }*/
            /*ramp profile
            var ev3 = new Brick<Sensor, Sensor, Sensor, Sensor>("COM40");
            ev3.Connection.Open();
            ev3.MotorA.SpeedProfileStep(50, 300, 600, 300, true);
            ev3.Connection.Close();*/

            /*Synchronising motors
            var ev3 = new Brick<Sensor, Sensor, Sensor, Sensor>("COM40");
            ev3.Connection.Open();
            ev3.MotorSync.BitField = OutputBitfield.OutA | OutputBitfield.OutD;
            ev3.MotorSync.On(50, 0);
            System.Threading.Thread.Sleep(3000);
            ev3.MotorSync.On(50, 50);//Motor A runs twice as fast as motor D   
            System.Threading.Thread.Sleep(3000);
            ev3.MotorSync.Brake();
            System.Threading.Thread.Sleep(1000);
            ev3.MotorSync.StepSync(-50, 0, 6 * 360, false);//Move to a position  
            ev3.Connection.Close();  */

            //Controling a vehicle
            var ev3 = new Brick<Sensor, Sensor, Sensor, Sensor>("COM40");
            try
            {
                ev3.Connection.Open();
                ev3.Vehicle.LeftPort = MotorPort.OutA;
                ev3.Vehicle.RightPort = MotorPort.OutD;
                ev3.Vehicle.ReverseLeft = false;
                ev3.Vehicle.ReverseRight = false;
                sbyte speed = 50;
                ConsoleKeyInfo cki;
                Console.WriteLine("Press T to quit");
                do
                {
                    cki = Console.ReadKey(true); //press a key    
                    switch (cki.Key)
                    {
                        case ConsoleKey.W:
                            ev3.Vehicle.Forward(speed);
                            break;
                        case ConsoleKey.S:
                            ev3.Vehicle.Backward(speed);
                            break;
                        case ConsoleKey.D:
                            ev3.Vehicle.SpinRight(speed);
                            break;
                        case ConsoleKey.A:
                            ev3.Vehicle.SpinLeft(speed);
                            break;
                        case ConsoleKey.Q:
                            ev3.Vehicle.TurnLeftForward(speed, 50);
                            break;
                        case ConsoleKey.E:
                            ev3.Vehicle.TurnRightForward(speed, 50);
                            break;
                        case ConsoleKey.C:
                            ev3.Vehicle.TurnRightReverse(speed, 50);
                            break;
                        case ConsoleKey.Z:
                            ev3.Vehicle.TurnLeftReverse(speed, 50);
                            break;
                        case ConsoleKey.Spacebar:
                            ev3.Vehicle.Off();
                            break;
                    }
                } while (cki.Key != ConsoleKey.T);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                Console.WriteLine("Error: " + e.Message);
                Console.WriteLine("Press any key to end...");
                Console.ReadKey();
            }
            finally
            {
                ev3.Connection.Close();
            }  

            /*Reading sensors
            var ev3 = new Brick<Sensor, Sensor, Sensor, Sensor>("COM40");
            ev3.Connection.Open();
            ev3.Sensor1 = new IRSensor(IRMode.Proximity);
            ev3.Sensor2 = new TouchSensor();
            ev3.Sensor3 = new ColorSensor(ColorMode.Color);
            ev3.Sensor4 = new GyroSensor(GyroMode.Angle);
            ConsoleKeyInfo cki;
            Console.WriteLine("Press Q to quit");
            do
            {
                cki = Console.ReadKey(true); //press a key    
                switch (cki.Key)
                {
                    case ConsoleKey.D1://1 is pressed      
                        Console.WriteLine("S1: " + ev3.Sensor1.ReadAsString());
                        break;
                    case ConsoleKey.D2://2 is pressed      
                        Console.WriteLine("S2: " + ev3.Sensor2.ReadAsString());
                        break;
                    case ConsoleKey.D3://3 is pressed      
                        Console.WriteLine("S3: " + ev3.Sensor3.ReadAsString());
                        break;
                    case ConsoleKey.D4://4 is pressed      
                        Console.WriteLine("S4: " + ev3.Sensor4.ReadAsString());
                        break;
                }
            } while (cki.Key != ConsoleKey.Q);
            ev3.Connection.Close();  */
        }
    }
}
