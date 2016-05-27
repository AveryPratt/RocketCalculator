using KSP_Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RocketryWebApp.WebForm1
{
    public partial class WebForm1
    {
        // error handling methods
        private bool DeltaVError(Stage stage, out string errorMessage)
        {
            if (stage.DryMass > stage.WetMass)
            {
                errorMessage = "Wet mass must be greater than dry mass.";
                return true;
            }
            else if (stage.WetMass <= 0 || stage.DryMass <= 0 || stage.Isp <= 0)
            {
                errorMessage = "Wet mass, dry mass, and isp must each be represented by a positive integer.";
                return true;
            }
            else
            {
                errorMessage = "";
                return false;
            }
        }
        private bool IspError(Stage stage, out string errorMessage)
        {
            if (stage.DryMass >= stage.WetMass)
            {
                errorMessage = "Wet mass must be greater than dry mass.";
                return true;
            }
            else if (stage.WetMass <= 0 || stage.DryMass <= 0 || stage.Isp <= 0)
            {
                errorMessage = "Wet mass and dry mass must each be represented by a positive integer.";
                return true;
            }
            else
            {
                errorMessage = "";
                return false;
            }
        }
        private bool ThrustError(Stage stage, out string errorMessage)
        {
            if (MinTWRCheckBox.Checked && MaxTWRCheckBox.Checked)
            {
                if ((stage.WetMass <= 0 || stage.MinTWR <= 0) && (stage.DryMass <= 0 || stage.MaxTWR <= 0))
                {
                    errorMessage = "Either wet mass or dry mass, and thrust, must be represented by a positive number greater than 0.";
                    return true;
                }
                else
                {
                    errorMessage = "";
                    return false;
                }
            }
            else if (MinTWRCheckBox.Checked)
            {
                if (stage.MinTWR <= 0 || stage.WetMass <= 0)
                {
                    errorMessage = "Wet mass and thrust must each be represented by a positive number greater than 0.";
                    return true;
                }
                else
                {
                    errorMessage = "";
                    return false;
                }
            }
            else if (MaxTWRCheckBox.Checked)
            {
                if (stage.MaxTWR <= 0 || stage.DryMass <= 0)
                {
                    errorMessage = "Dry mass and thrust must each be represented by a positive number greater than 0.";
                    return true;
                }
                else
                {
                    errorMessage = "";
                    return false;
                }
            }
            else
            {
                errorMessage = "Select either the 'min' or the 'max' checkboxes to input the stage variables required to calculate thrust.";
                return true;
            }
        }
        private bool TWRError(Stage stage, out string errorMessage, out bool? minOrMax)
        {
            if (MinTWRCheckBox.Checked && MaxTWRCheckBox.Checked)
            {
                minOrMax = null;
                if (stage.Thrust <= 0 || (stage.WetMass <= 0 && stage.DryMass <= 0))
                {
                    errorMessage = "Either wet mass or dry mass, and thrust, must be represented by a positive number greater than 0.";
                    return true;
                }
                else
                {
                    errorMessage = "";
                    return false;
                }
            }
            else if (MinTWRCheckBox.Checked)
            {
                minOrMax = true;
                if (stage.Thrust <= 0 || stage.WetMass <= 0)
                {
                    errorMessage = "Wet mass and thrust must each be represented by a positive number greater than 0.";
                    return true;
                }
                else
                {
                    errorMessage = "";
                    return false;
                }
            }
            else if (MaxTWRCheckBox.Checked)
            {
                minOrMax = false;
                if (stage.Thrust <= 0 || stage.DryMass <= 0)
                {
                    errorMessage = "Dry mass and thrust must each be represented by a positive number greater than 0.";
                    return true;
                }
                else
                {
                    errorMessage = "";
                    return false;
                }
            }
            else
            {
                errorMessage = "Select either the 'min' or the 'max' checkboxes to input the stage variables required to calculate TWR.";
                // necessary only for output parameter, not for value
                minOrMax = null;
                return true;
            }
        }
    }
}