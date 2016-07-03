using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace KSP_Library
{
    namespace Systems
    {
        internal interface ISystem
        {
            Body GetSystemBody(string bodyName);
            Body GetSystemBody(int bodyID);
            Body[] GetSystemBodies();
        }

        public abstract class SolarSystem : ISystem
        {
            public Body[] bodies { get; set; }
            public virtual Body GetSystemBody(string bodyName)
            {
                return bodies.Single(b => b.Name == bodyName.ToUpper());
            }
            public virtual Body GetSystemBody(int bodyIndex)
            {
                return bodies[bodyIndex];
            }
            public virtual Body[] GetSystemBodies()
            {
                return bodies;
            }
        }

        public class Body
        {
            public string Name { get; set; }
            public int Radius { get; set; }
            public long GM { get; set; }
        }
        public class Star : Body
        {
            new public BigInteger GM { get; set; }
        }

        public struct BigGM
        {
            public static BigInteger ENotation(double value, double displacement)
            {
                return (BigInteger)value * (BigInteger)Math.Pow(10, displacement);
            }
        }
    }
}
