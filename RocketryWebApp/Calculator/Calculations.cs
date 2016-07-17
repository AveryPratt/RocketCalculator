using KSP_Library.Rocketry;
using KSP_Library.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RocketryWebApp.Calculator
{
    public partial class CalculatorWebForm
    {
        public static class Calculations
        {
            // Stage calculations
            public static void CalculateDeltaV(Stage stage)
            {
                stage.CalculateDeltaV();
            }
            public static void CalculateIsp(Stage stage)
            {
                stage.CalculateIsp();
            }
            public static void CalculateThrustFromMinTWR(Stage stage, Body body)
            {
                stage.CalculateThrustFromMinTWR(body.GM, body.Radius);
            }
            public static void CalculateThrustFromMaxTWR(Stage stage, Body body)
            {
                stage.CalculateThrustFromMaxTWR(body.GM, body.Radius);
            }
            public static void CalculateMinTWR(Stage stage, Body body)
            {
                stage.CalculateMinTWR(body.GM, body.Radius);
            }
            public static void CalculateMaxTWR(Stage stage, Body body)
            {
                stage.CalculateMaxTWR(body.GM, body.Radius);
            }
        }

        private void runCalculations()
        {
            StringBuilder errors = new StringBuilder();
            string payloadConversionErrors;
            string conversionErrors;
            string calculationErrors;
            string postCalculationErrors;
            while (true)
            {
                Rocket rocket = Conversions.ConvertTableToRocket(RocketTable, getTextBoxText, out conversionErrors);
                rocket.PayloadMass = Conversions.ConvertPayloadToInt(PayloadTextBox.Text, out payloadConversionErrors);
                if (payloadConversionErrors != string.Empty)
                {
                    errors.Append(payloadConversionErrors);
                    break;
                }
                if (conversionErrors != string.Empty)
                {
                    errors.Append("<error> Input Conversion Unsuccessful. </error><br/>");
                    errors.Append(conversionErrors);
                    break;
                }
                errors.Append("<noerror> Input Conversion Successful. </noerror><br/>");
                calculationErrors = preCalculate(rocket);
                if(calculationErrors != string.Empty)
                {
                    errors.Append("<error> Stage Calculation Unsuccessful. </error><br/>");
                    errors.Append(calculationErrors);
                    break;
                }
                calculate(rocket);
                Conversions.ConvertRocketToScreenedTable(rocket, RocketTable, setTextBoxText);
                errors.Append("<noerror> Stage Calculation Successful. </noerror><br/>");
                postCalculationErrors = postCalculate(rocket);
                if (postCalculationErrors != string.Empty)
                {
                    errors.Append("<error> Totals Calculation Unsuccessful. </error><br/>");
                    errors.Append(postCalculationErrors);
                    break;
                }
                errors.Append("<noerror> Totals Calculation Successful. </noerror><br/>");
                setFooters(rocket);
                SaveButton.Visible = true;
                break;
            }
            ErrorMessage.InnerHtml = errors.ToString();
            ErrorMessage.Visible = true;
        }
        private void calculate(Rocket rocket)
        {
            rocket.AddPayloadMassToStages();
            foreach (Stage stage in rocket.StageList)
            {
                if (DeltaVCheckBox.Checked)
                {
                    Calculations.CalculateDeltaV(stage);
                }
                else if (IspCheckBox.Checked)
                {
                    Calculations.CalculateIsp(stage);
                }
                if (ThrustCheckBox.Checked)
                {
                    Body body = selectBody();
                    if (MinTWRCheckBox.Checked)
                    {
                        Calculations.CalculateThrustFromMinTWR(stage, body);
                    }
                    else if (MaxTWRCheckBox.Checked)
                    {
                        Calculations.CalculateThrustFromMaxTWR(stage, body);
                    }
                }
                else if (TWRCheckBox.Checked)
                {
                    Body body = selectBody();
                    if (MinTWRCheckBox.Checked && MaxTWRCheckBox.Checked)
                    {
                        Calculations.CalculateMinTWR(stage, body);
                        Calculations.CalculateMaxTWR(stage, body);
                    }
                    else if (MinTWRCheckBox.Checked)
                    {
                        Calculations.CalculateMinTWR(stage, body);
                    }
                    else if (MaxTWRCheckBox.Checked)
                    {
                        Calculations.CalculateMaxTWR(stage, body);
                    }
                }
            }
            rocket.SubtractPayloadMassFromStages();
        }

        private static string Truncate(string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }
            else
            {
                return value.Length <= maxLength ? value : value.Substring(0, maxLength);
            }
        }
    }
}