using KSP_Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;

namespace RocketryWebApp.WebForm1
{
    public partial class WebForm1
    {

        // calculation methods
        private void DeltaVCalculation(Stage stage, int i, out bool calculated)
        {
            while (true)
            {
                // error handling
                string error;
                if (DeltaVError(stage, out error))
                {
                    Response.Write(error);
                    calculated = false;
                    break;
                }
                // calculation
                string deltaVText = stage.GetDeltaV().ToString();
                TableColumns.SetTextBoxText(deltaVText, i, TableColumns.DeltaVColumn);
                calculated = true;
                break;
            }
        }
        private void IspCalculation(Stage stage, int i, out bool calculated)
        {
            while (true)
            {
                // error handling
                string error;
                if (IspError(stage, out error))
                {
                    Response.Write(error);
                    calculated = false;
                    break;
                }
                // calculation
                string ispText = stage.GetIsp().ToString();
                TableColumns.SetTextBoxText(ispText, i, TableColumns.IspColumn);
                calculated = true;
                break;
            }
        }
        private void ThrustCalculation(Stage stage, Body myBody, int i, out bool calculated)
        {
            while (true)
            {
                // error handling
                string error;
                if (ThrustError(stage, out error))
                {
                    Response.Write(error);
                    calculated = false;
                    break;
                }
                // calculation
                else if (MinTWRCheckBox.Checked && MaxTWRCheckBox.Checked)
                {
                    // checks for wetMass/minTWR properties first
                    if (stage.WetMass == 0 || stage.MinTWR == 0)
                    {
                        string thrustText = stage.GetThrustFromMaxTWR(myBody.GM, myBody.Radius).ToString();
                        TableColumns.SetTextBoxText(thrustText, i, TableColumns.ThrustColumn);
                    }
                    // then checks for dryMass/maxTWR properties
                    else
                    {
                        string thrustText = stage.GetThrustFromMinTWR(myBody.GM, myBody.Radius).ToString();
                        TableColumns.SetTextBoxText(thrustText, i, TableColumns.ThrustColumn);
                    }
                }
                else if (MinTWRCheckBox.Checked)
                {
                    string thrustText = stage.GetThrustFromMinTWR(myBody.GM, myBody.Radius).ToString();
                    TableColumns.SetTextBoxText(thrustText, i, TableColumns.ThrustColumn);
                }
                else if (MaxTWRCheckBox.Checked)
                {
                    string thrustText = stage.GetThrustFromMaxTWR(myBody.GM, myBody.Radius).ToString();
                    TableColumns.SetTextBoxText(thrustText, i, TableColumns.ThrustColumn);
                }
                calculated = true;
                break;
            }
        }
        private void TWRCalculation(Stage stage, Body myBody, int i, out bool minCalculated, out bool maxCalculated)
        {
            while (true)
            {
                // error handling
                string error;
                bool? minOrMax;
                if (TWRError(stage, out error, out minOrMax))
                {
                    Response.Write(error);
                    if (minOrMax == null)
                    {
                        minCalculated = false;
                        maxCalculated = false;
                    }
                    else if (minOrMax == true)
                    {
                        minCalculated = false;
                        maxCalculated = true;
                    }
                    else
                    {
                        maxCalculated = false;
                        minCalculated = true;
                    }
                    break;
                }
                // calculation
                // Min
                if (MinTWRCheckBox.Checked)
                {
                    string minTWRText = stage.GetMinTWR(myBody.GM, myBody.Radius).ToString();
                    TableColumns.SetTextBoxText(minTWRText, i, TableColumns.MinTWRColumn);
                }
                // Max
                if (MaxTWRCheckBox.Checked)
                {
                    string maxTWRText = stage.GetMaxTWR(myBody.GM, myBody.Radius).ToString();
                    TableColumns.SetTextBoxText(maxTWRText, i, TableColumns.MaxTWRColumn);
                }
                minCalculated = true;
                maxCalculated = true;
                break;
            }
        }

        private void SetHeaderColors(bool deltaV, bool isp, bool thrust, bool min, bool max)
        {
            // DeltaV
            if (DeltaVCheckBox.Checked)
            {
                if (deltaV == false)
                {
                    TableColumns.DeltaVColumn[0].ForeColor = System.Drawing.Color.Red;
                }
                else TableColumns.DeltaVColumn[0].ForeColor = System.Drawing.Color.Gold;
            }

            // Isp
            if (IspCheckBox.Checked)
            {
                if (isp == false)
                {
                    TableColumns.IspColumn[0].ForeColor = System.Drawing.Color.Red;
                }
                else TableColumns.IspColumn[0].ForeColor = System.Drawing.Color.Gold;
            }

            // Thrust
            if (ThrustCheckBox.Checked)
            {
                if (thrust == false)
                {
                    TableColumns.ThrustColumn[0].ForeColor = System.Drawing.Color.Red;
                }
                else TableColumns.ThrustColumn[0].ForeColor = System.Drawing.Color.Gold;
            }

            if (TWRCheckBox.Checked)
            {
                // Min TWR
                if (MinTWRCheckBox.Checked)
                {
                    if (min == false)
                    {
                        TableColumns.MinTWRColumn[0].ForeColor = System.Drawing.Color.Red;
                    }
                    else TableColumns.MinTWRColumn[0].ForeColor = System.Drawing.Color.Gold;
                }
                // Max TWR
                if (MaxTWRCheckBox.Checked)
                {
                    if (max == false)
                    {
                        TableColumns.MaxTWRColumn[0].ForeColor = System.Drawing.Color.Red;
                    }
                    else TableColumns.MaxTWRColumn[0].ForeColor = System.Drawing.Color.Gold;
                }
            }
        }

        // gets a List of Stages in which Stage Properties are given by RocketTable text
        private List<Stage> getStages()
        {
            // creates stageList for {StageNumber} Stages
            List<Stage> stageList = new List<Stage>();
            for (int i = 0; i < StageNumber; i++)
            {
                double wetMass;
                double dryMass;
                double thrust;
                int isp;
                double minTWR;
                double maxTWR;
                int deltaV;

                // finds appropriate TextBox.Text and TryParses Text into fields
                bool IsWetMassDouble = double.TryParse(TableColumns.GetTextBoxText(i, TableColumns.WetMassColumn), out wetMass);
                bool IsDryMassDouble = double.TryParse(TableColumns.GetTextBoxText(i, TableColumns.DryMassColumn), out dryMass);
                bool IsThrustDouble = double.TryParse(TableColumns.GetTextBoxText(i, TableColumns.ThrustColumn), out thrust);
                bool IsIspDouble = int.TryParse(TableColumns.GetTextBoxText(i, TableColumns.IspColumn), out isp);
                bool IsMinTWRDouble = double.TryParse(TableColumns.GetTextBoxText(i, TableColumns.MinTWRColumn), out minTWR);
                bool IsMaxTWRDouble = double.TryParse(TableColumns.GetTextBoxText(i, TableColumns.MaxTWRColumn), out maxTWR);
                bool IsDeltaVInt = int.TryParse(TableColumns.GetTextBoxText(i, TableColumns.DeltaVColumn), out deltaV);

                Stage stage = new Stage("Stage" + (i + 1).ToString())
                {
                    WetMass = wetMass,
                    DryMass = dryMass,
                    Thrust = thrust,
                    Isp = isp,
                    MinTWR = minTWR,
                    MaxTWR = maxTWR,
                    DeltaV = deltaV
                };
                stageList.Add(stage);
            }
            return stageList;
        }
    }
}