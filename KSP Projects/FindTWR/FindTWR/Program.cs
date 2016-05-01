using KSP_Library;
using System;

namespace FindTWR
{
    class Program
    {
        static void Main(string[] args)
        {
            Stage stage = new Stage();

            // stage fields
            double wetMass;
            double dryMass;
            double thrust;
            double minTWR;
            double maxTWR;

            // celestial body fields
            int radius;
            long gm;
            string body;

            Console.WriteLine("TWR for Kerbin? Y/N");
            ConsoleKeyInfo forKerbin = Console.ReadKey();
            Console.WriteLine();

            if (forKerbin.KeyChar != 'y')
            {
                // Retrieves celestial body info (name, GM, radius)
                Console.WriteLine("What celestial body are you trying to find TWR for?");
                body = Console.ReadLine();

                CheckBody(body, out radius, out gm);

                // Retrieves stage info (wet mass, dry mass, thrust) from user
                GetStageInfo(out wetMass, out dryMass, out thrust);

                // Sets stage info to local variables
                stage.WetMass = wetMass;
                stage.DryMass = dryMass;
                stage.Thrust = thrust;

                stage.GetTWR(gm, radius, out minTWR, out maxTWR);
            }
            else
            {
                // GetTWR constructor bypasses need for Body instance
                GetStageInfo(out wetMass, out dryMass, out thrust);
                stage.WetMass = wetMass;
                stage.DryMass = dryMass;
                stage.Thrust = thrust;

                stage.GetTWR(out minTWR, out maxTWR);
            }

            Console.WriteLine("Minimum TWR = {0}\nMaximum TWR = {1}", minTWR, maxTWR);
        }

        static void CheckBody(string body, out int radius, out long gm)
        {
            // Checks KSP_Library.KerbolSystem.SystemBodies() for Body named (body)
            Body LocalBody = KerbolSystem.SystemBodies(body);
            if (LocalBody.Name != "NOBODY")
            {
                radius = LocalBody.Radius;
                gm = LocalBody.GM;
            }

            // If celestial body is not found, asks user for body info (GM, radius)
            else
            {
                Console.WriteLine("Unrecognized celestial body.");
                Console.WriteLine("What is the celestial body's radius?");
                bool isRadiusInt = int.TryParse(Console.ReadLine(), out radius);
                if (isRadiusInt != true)
                {
                    throw new Exception("Unrecognized celestial body radius");
                }

                Console.WriteLine("What is the celestial body's standard gravitational parameter?");
                bool isGMLong = long.TryParse(Console.ReadLine(), out gm);
                if (isGMLong != true)
                {
                    throw new Exception("Unrecognized standard gravitational paramter");
                }
            }
        }

        static void GetStageInfo(out double wetMass, out double dryMass, out double thrust)
        {
            while (true)
            {
                while (true)
                {
                    Console.WriteLine("What is the rocket's wet mass? (in metric tons)");
                    bool isWetMassDouble = double.TryParse(Console.ReadLine(), out wetMass);
                    if (isWetMassDouble != true || wetMass <= 0)
                    {
                        Console.WriteLine("Unrecognized input. Wet mass must be a positive floating point number");
                        continue;
                    }
                    break;
                }
                while (true)
                {
                    Console.WriteLine("What is the rocket's dry mass? (in metric tons)");
                    bool isDryMassDouble = double.TryParse(Console.ReadLine(), out dryMass);
                    if (isDryMassDouble != true || dryMass <= 0)
                    {
                        Console.WriteLine("Unrecognized input. Dry mass must be a positive floating point number");
                        continue;
                    }
                    break;
                }
                if(dryMass > wetMass)
                {
                    Console.WriteLine("Dry mass cannot be greater than wet mass.");
                    continue;
                }
                else
                {
                    break;
                }
            }
            while (true)
            {
                Console.WriteLine("What is the rocket's thrust? (in kilonewtons)");
                bool isThrustDouble = double.TryParse(Console.ReadLine(), out thrust);
                if (isThrustDouble != true)
                {
                    Console.WriteLine("Unrecognized input. Thrust must be a floating point number");
                    continue;
                }
                break;
            }
        }
    }
}