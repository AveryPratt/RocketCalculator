using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RocketryWebApp.WebForm1
{
    public partial class WebForm1
    {
        public enum InputValue
        {
            WetMass,
            DryMass,
            Isp,
            DeltaV,
            Thrust,
            MinTWR,
            MaxTWR,
        }
        public enum ColumnName
        {
            StageName,
            WetMass,
            DryMass,
            Isp,
            DeltaV,
            Thrust,
            MinTWR,
            MaxTWR,
            Buttons
        }
    }
}