using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KSP_Library.Systems;

namespace RocketryWebApp.Calculator
{
    public partial class CalculatorWebForm
    {
        private Body selectBody()
        {
            SolarSystem solarSystem;
            if (KspParentBodyDropDownList.Visible)
            {
                solarSystem = new KerbolSystem();
                Body body = solarSystem.GetSystemBody(KspParentBodyDropDownList.SelectedItem.Text);
                return body;
            }
            else if (RssParentBodyDropDownList.Visible)
            {
                solarSystem = new RealSolarSystem();
                Body body = solarSystem.GetSystemBody(RssParentBodyDropDownList.SelectedItem.Text);
                return body;
            }
            else return null;
        }

        private void parentBodyDropDownListVisibity()
        {
            if (SolarSystemSelector.Checked)
            {
                RssParentBodyDropDownList.Visible = true;
                KspParentBodyDropDownList.Visible = false;
            }
            else
            {
                RssParentBodyDropDownList.Visible = false;
                KspParentBodyDropDownList.Visible = true;
            }
        }
    }
}