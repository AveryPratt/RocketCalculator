using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace KSP_Library
{
    namespace Rocketry
    {
        public class Rocket
        {
            // properties
            public string RocketName { get; set; }
            public int ID { get; set; }
            public List<Stage> StageList { get; set; }

            // constructors
            public Rocket()
            {
                StageList = new List<Stage>();
            }
            public Rocket(string rocketName)
            {
                RocketName = rocketName;
            }
            public Rocket(int id)
            {
                ID = id;
            }
            public Rocket(string name, int id)
            {
                ID = id;
            }
            public Rocket(List<Stage> stageList)
            {
                StageList = stageList;
            }
            public Rocket(string rocketName, List<Stage> stageList)
            {
                RocketName = rocketName;
                StageList = stageList;
            }
            public Rocket(int id, List<Stage> stageList)
            {
                ID = id;
                StageList = stageList;
            }
            public Rocket(string name, int id, List<Stage> stageList)
            {
                ID = id;
                StageList = stageList;
            }

            // private methods
            //private object[] indexStageProperty(string propertyName)
            //{

            //}
        }
        public class Stage
        {
            // properties
            public string StageName { get; set; }
            public int ID { get; set; }
            public double WetMass { get; set; }
            public double DryMass { get; set; }
            public int Isp { get; set; }
            public int DeltaV { get; set; }
            public double Thrust { get; set; }
            public double MinTWR { get; set; }
            public double MaxTWR { get; set; }

            // constructors
            public Stage() { }
            public Stage(string stageName)
            {
                StageName = stageName;
            }
            public Stage(int id)
            {
                ID = id;
            }
            public Stage(string stageName, int id)
            {
                StageName = stageName;
                ID = id;
            }

            // methods
            public void CalculateDeltaV()
            {
                DeltaV = (int)Math.Round(Math.Log(WetMass / DryMass) * 9.81 * Isp);
            }
            public void CalculateIsp()
            {
                Isp = (int)Math.Round(DeltaV / (Math.Log(WetMass / DryMass) * 9.81));
            }
            public void CalculateMinTWR()
            {
                MinTWR = Thrust / (WetMass * (9.81));
            }
            public void CalculateMaxTWR()
            {
                MaxTWR = Thrust / (DryMass * (9.81));
            }
            public void CalculateMinTWR(long GM, double Radius)
            {
                MinTWR = Thrust / (WetMass * (GM / Math.Pow(Radius, 2)));
            }
            public void CalculateMaxTWR(long GM, double Radius)
            {
                MaxTWR = Thrust / (DryMass * (GM / Math.Pow(Radius, 2)));
            }
            public void CalculateThrustFromMinTWR()
            {
                Thrust = MinTWR * (WetMass * (9.81));
            }
            public void CalculateThrustFromMaxTWR()
            {
                Thrust = MaxTWR * (DryMass * (9.81));
            }
            public void CalculateThrustFromMinTWR(long GM, double Radius)
            {
                Thrust = MinTWR * (WetMass * (GM / Math.Pow(Radius, 2)));
            }
            public void CalculateThrustFromMaxTWR(long GM, double Radius)
            {
                Thrust = MaxTWR * (DryMass * (GM / Math.Pow(Radius, 2)));
            }
            public void CalculateTWR()
            {
                MinTWR = GetMinTWR();
                MaxTWR = GetMaxTWR();
            }
            public void CalculateTWR(long GM, double Radius)
            {
                MinTWR = GetMinTWR(GM, Radius);
                MaxTWR = GetMaxTWR(GM, Radius);
            }

            public int GetDeltaV()
            {
                DeltaV = (int)Math.Round(Math.Log(WetMass / DryMass) * 9.81 * Isp);
                return DeltaV;
            }
            public int GetIsp()
            {
                Isp = (int)Math.Round(DeltaV / (Math.Log(WetMass / DryMass) * 9.81));
                return Isp;
            }
            public double GetMinTWR()
            {
                MinTWR = Thrust / (WetMass * (9.81));
                return MinTWR;
            }
            public double GetMaxTWR()
            {
                MaxTWR = Thrust / (DryMass * (9.81));
                return MaxTWR;
            }
            public double GetMinTWR(long GM, double Radius)
            {
                MinTWR = Thrust / (WetMass * (GM / Math.Pow(Radius, 2)));
                return MinTWR;
            }
            public double GetMaxTWR(long GM, double Radius)
            {
                MaxTWR = Thrust / (DryMass * (GM / Math.Pow(Radius, 2)));
                return MaxTWR;
            }
            public double GetThrustFromMinTWR()
            {
                Thrust = MinTWR * (WetMass * (9.81));
                return Thrust;
            }
            public double GetThrustFromMaxTWR()
            {
                Thrust = MaxTWR * (DryMass * (9.81));
                return Thrust;
            }
            public double GetThrustFromMinTWR(long GM, double Radius)
            {
                Thrust = MinTWR * (WetMass * (GM / Math.Pow(Radius, 2)));
                return Thrust;
            }
            public double GetThrustFromMaxTWR(long GM, double Radius)
            {
                Thrust = MaxTWR * (DryMass * (GM / Math.Pow(Radius, 2)));
                return Thrust;
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
        }
    }


}
