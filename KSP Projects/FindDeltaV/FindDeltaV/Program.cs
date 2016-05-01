using KSP_Library;
using System;

namespace FindDeltaV
{
    class Program
    {
        static void Main(string[] args)
        {
            // Multi-stage fields
            int stages;
            double TotalDeltaV = 0;

            // Gets number of stages
            while (true)
            {
                Console.WriteLine("How many stages is your rocket?");
                bool IsStagesInt = Int32.TryParse(Console.ReadLine(), out stages);
                if (IsStagesInt != true)
                {
                    Console.WriteLine("Unrecognized input. Number of stages must be a positive integer.");
                    continue;
                }
                break;
            }

            // Creates an array of integers representing the number of each stage
            int[] Stages = new int[stages];
            for (int i = 0; i < stages; i++)
            {
                Stages[i] = i + 1;
            }

            // Gets info for each stage (wet mass, dry mass, isp)
            foreach (int stageNumber in Stages)
            {
                Stage stage = new Stage();

                // Stage fields
                double stageDeltaV;
                double wetMass = stage.WetMass;
                double dryMass = stage.DryMass;
                int isp = stage.Isp;

                // Checks dry mass !> wet mass
                while (true)
                {
                    // Checks wet mass
                    while (true)
                    {
                        Console.WriteLine("Enter stage {0} wet mass:", stageNumber);
                        bool isWetMassDouble = Double.TryParse(Console.ReadLine(), out wetMass);
                        stage.WetMass = wetMass;
                        if (isWetMassDouble != true || wetMass <= 0)
                        {
                            Console.WriteLine("Unrecognized input. Wet mass must be a positive floating point number");
                            continue;
                        }
                        break;
                    }

                    // Checks dry mass
                    while (true)
                    {
                        Console.WriteLine("Enter stage {0} dry mass:", stageNumber);
                        bool isDryMassDouble = Double.TryParse(Console.ReadLine(), out dryMass);
                        stage.DryMass = dryMass;
                        if (isDryMassDouble != true || dryMass <= 0)
                        {
                            Console.WriteLine("Unrecognized input. Dry mass must be a positive floating point number");
                            continue;
                        }
                        break;
                    }
                    if (dryMass > wetMass)
                    {
                        Console.WriteLine("Dry mass cannot be greater than wet mass.");
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }

                // Checks Isp
                while (true)
                {
                    Console.WriteLine("Enter stage {0} engine Isp:", stageNumber);
                    bool isISPInt = Int32.TryParse(Console.ReadLine(), out isp);
                    stage.Isp = isp;
                    if (isISPInt != true || isp < 0)
                    {
                        Console.WriteLine("Unrecognized input. Isp must be a positive floating point number");
                        continue;
                    }
                    break;
                }

                // Uses stage info to find Stage (and Total) DeltaV
                stageDeltaV = stage.GetDeltaV();
                Console.WriteLine("Stage {0} deltaV = {1}", stageNumber, stageDeltaV);
                TotalDeltaV += stageDeltaV;
                if (stageNumber > 1)
                {
                    Console.WriteLine("Total deltaV = {0}", TotalDeltaV);
                }
            }
        }
    }
}
