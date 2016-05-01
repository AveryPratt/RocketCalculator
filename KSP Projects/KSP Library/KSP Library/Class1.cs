using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSP_Library
{

    #region Rocket

    public class Stage
    {
        // read-write properties
        public double WetMass { get; set; }
        public double DryMass { get; set; }
        public int Isp { get; set; }
        public double Thrust { get; set; }

        // read-only fields
        private double _deltaV;
        private double _maxTWR;
        private double _minTWR;

        // read-only methods
        public double GetDeltaV()
        {
            _deltaV = Math.Log(WetMass / DryMass) * 9.81 * Isp;
            return _deltaV;
        }

        public double GetMinTWR()
        {
            _minTWR = Thrust / (WetMass * (9.81));
            return _minTWR;
        }

        public double GetMinTWR(long GM, double Radius)
        {
            _minTWR = Thrust / (WetMass * (GM / Math.Pow(Radius, 2)));
            return _minTWR;
        }

        public double GetMaxTWR()
        {
            _maxTWR = Thrust / (DryMass * (9.81));
            return _maxTWR;
        }

        public double GetMaxTWR(long GM, double Radius)
        {
            _maxTWR = Thrust / (DryMass * (GM / Math.Pow(Radius, 2)));
            return _maxTWR;
        }

        public void GetTWR(out double minTWR, out double maxTWR)
        {
            minTWR = GetMinTWR();
            maxTWR = GetMaxTWR();
        }

        public void GetTWR(long GM, double Radius, out double minTWR, out double maxTWR)
        {
            minTWR = GetMinTWR(GM, Radius);
            maxTWR = GetMaxTWR(GM, Radius);
        }

        public void GetStageInfo(out double deltaV, out double minTWR, out double maxTWR)
        {
            deltaV = GetDeltaV();
            minTWR = GetMinTWR();
            maxTWR = GetMaxTWR();
        }

        public void GetStageInfo(long GM, double Radius, out double deltaV, out double minTWR, out double maxTWR)
        {
            deltaV = GetDeltaV();
            minTWR = GetMinTWR(GM, Radius);
            maxTWR = GetMaxTWR(GM, Radius);
        }
    }

    #endregion

    #region Systems

    public static class KerbolSystem
    {
        public static Body SystemBodies(string body)
        {
            Body[] bodies = new Body[17];

            bodies[0] = new Body
            {
                Name = "KERBOL",
                Radius = 261600000,
                GM = 1172332800000000000
            };

            bodies[1] = new Body
            {
                Name = "MOHO",
                Radius = 250000,
                GM = 168609380000
            };

            bodies[2] = new Body
            {
                Name = "EVE",
                Radius = 700000,
                GM = 8171730200000
            };

            bodies[3] = new Body
            {
                Name = "GILLY",
                Radius = 13000,
                GM = 8289450
                // Actual GM == 8289449.8
            };

            bodies[4] = new Body
            {
                Name = "KERBIN",
                Radius = 600000,
                GM = 3531600000000
            };

            bodies[5] = new Body
            {
                Name = "MUN",
                Radius = 200000,
                GM = 65138398000
            };

            bodies[6] = new Body
            {
                Name = "MINMUS",
                Radius = 60000,
                GM = 1765800000
            };

            bodies[7] = new Body
            {
                Name = "DUNA",
                Radius = 320000,
                GM = 301363210000
            };

            bodies[8] = new Body
            {
                Name = "IKE",
                Radius = 130000,
                GM = 18568369000
            };

            bodies[9] = new Body
            {
                Name = "DRES",
                Radius = 138000,
                GM = 21484489000
            };

            bodies[10] = new Body
            {
                Name = "JOOL",
                Radius = 6000000,
                GM = 282528000000000
            };

            bodies[11] = new Body
            {
                Name = "LAYTHE",
                Radius = 500000,
                GM = 1962000000000
            };

            bodies[12] = new Body
            {
                Name = "VALL",
                Radius = 300000,
                GM = 207481500000
            };

            bodies[13] = new Body
            {
                Name = "TYLO",
                Radius = 600000,
                GM = 2825280000000
            };

            bodies[14] = new Body
            {
                Name = "BOP",
                Radius = 65000,
                GM = 2486834900
            };

            bodies[15] = new Body
            {
                Name = "POL",
                Radius = 44000,
                GM = 721702080
            };

            bodies[16] = new Body
            {
                Name = "EELOO",
                Radius = 210000,
                GM = 74410815000
            };

            foreach (Body targetBody in bodies)
            {
                if (body.ToUpper() != targetBody.Name)
                {
                    continue;
                }
                return targetBody;
            }
            Body NoBody = new Body();
            NoBody.Name = "NOBODY";
            return NoBody;
        }
    }

    public class Body
    {
        public string Name { get; set; }
        public int Radius { get; set; }
        public long GM { get; set; }
    }

    #endregion

}
