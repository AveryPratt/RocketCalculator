using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CelestialBodies
{
    public class Body
    {
        int Rad;
        long GM;
        int SOI;
        bool IsAtmospherePresent;
        ParentBody parent = new ParentBody();
    }

    public class Sun : Body
    {
        
    }

    public class Planet : Body
    {

    }

    public class Moon : Body
    {

    }

    enum ParentBody
    {
        Kerbol,
        Moho,
        Eve,
        Kerbin,
        Duna,
        Dres,
        Jool,
        Eeloo
    }
}
