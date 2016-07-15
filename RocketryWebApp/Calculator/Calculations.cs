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
                double payloadMass = Conversions.ConvertPayloadToInt(PayloadTextBox.Text, out payloadConversionErrors);
                if (payloadConversionErrors != string.Empty)
                {
                    errors.Append(payloadConversionErrors);
                    break;
                }
                Rocket rocket = Conversions.ConvertScreenedTableToRocket(RocketTable, getTextBoxText, out conversionErrors);
                if (conversionErrors != string.Empty)
                {
                    errors.Append("<error> Input Conversion Unsuccessful. </error><br/>");
                    errors.Append(conversionErrors);
                    break;
                }
                errors.Append("<noerror> Input Conversion Successful. </noerror><br/>");
                calculationErrors = calculateStageErrors(rocket, payloadMass);
                if(calculationErrors != string.Empty)
                {
                    errors.Append("<error> Stage Calculation Unsuccessful. </error><br/>");
                    errors.Append(calculationErrors);
                    break;
                }
                Conversions.ConvertRocketToScreenedTable(rocket, payloadMass, RocketTable, setTextBoxText);
                errors.Append("<noerror> Stage Calculation Successful. </noerror><br/>");
                postCalculationErrors = calculateFooterErrors(rocket);
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
        private StringBuilder postCalculate(Stage stage, double previousWetMass, StringBuilder errorList)
        {
            List<string> postCalculationErrors = new List<string>();
            if (DeltaVCheckBox.Checked || IspCheckBox.Checked)
            {
                CalculationErrors.CheckWetMassGreaterThanPreviousStageWetMass(stage, previousWetMass, postCalculationErrors, out postCalculationErrors);
                CalculationErrors.CheckDryMassGreaterThanPreviousStageWetMass(stage, previousWetMass, postCalculationErrors, out postCalculationErrors);
            }
            if (ThrustCheckBox.Checked)
            {
                if (MinTWRCheckBox.Checked)
                {
                    CalculationErrors.CheckWetMassGreaterThanPreviousStageWetMass(stage, previousWetMass, postCalculationErrors, out postCalculationErrors);
                }
                else if (MaxTWRCheckBox.Checked)
                {
                    CalculationErrors.CheckDryMassGreaterThanPreviousStageDryMass(stage, previousWetMass, postCalculationErrors, out postCalculationErrors);
                }
            }
            else if (TWRCheckBox.Checked)
            {
                if (MinTWRCheckBox.Checked && MaxTWRCheckBox.Checked)
                {
                    CalculationErrors.CheckWetMassGreaterThanPreviousStageWetMass(stage, previousWetMass, postCalculationErrors, out postCalculationErrors);
                    CalculationErrors.CheckDryMassGreaterThanPreviousStageWetMass(stage, previousWetMass, postCalculationErrors, out postCalculationErrors);
                }
                else if (MinTWRCheckBox.Checked)
                {
                    CalculationErrors.CheckWetMassGreaterThanPreviousStageWetMass(stage, previousWetMass, postCalculationErrors, out postCalculationErrors);
                }
                else if (MaxTWRCheckBox.Checked)
                {
                    CalculationErrors.CheckDryMassGreaterThanPreviousStageDryMass(stage, previousWetMass, postCalculationErrors, out postCalculationErrors);
                }
            }
            foreach (string error in postCalculationErrors)
            {
                errorList.Append(error);
            }
            return errorList;
        }
        private string preCalculate(Stage stage)
        {
            StringBuilder errorList = new StringBuilder();
            List<string> preCalculationErrors = new List<string>();
            if (DeltaVCheckBox.Checked)
            {
                CalculationErrors.CheckIspPositive(stage, preCalculationErrors, out preCalculationErrors);
                CalculationErrors.CheckWetMassPositive(stage, preCalculationErrors, out preCalculationErrors);
                CalculationErrors.CheckDryMassPositive(stage, preCalculationErrors, out preCalculationErrors);
                CalculationErrors.CheckWetMassGreaterThanDryMass(stage, preCalculationErrors, out preCalculationErrors);
            }
            else if (IspCheckBox.Checked)
            {
                CalculationErrors.CheckDeltaVPositive(stage, preCalculationErrors, out preCalculationErrors);
                CalculationErrors.CheckWetMassPositive(stage, preCalculationErrors, out preCalculationErrors);
                CalculationErrors.CheckDryMassPositive(stage, preCalculationErrors, out preCalculationErrors);
                CalculationErrors.CheckWetMassGreaterThanDryMass(stage, preCalculationErrors, out preCalculationErrors);
            }
            if (ThrustCheckBox.Checked)
            {
                if (MinTWRCheckBox.Checked)
                {
                    CalculationErrors.CheckMinTWRPositive(stage, preCalculationErrors, out preCalculationErrors);
                    CalculationErrors.CheckWetMassPositive(stage, preCalculationErrors, out preCalculationErrors);
                }
                else if (MaxTWRCheckBox.Checked)
                {
                    CalculationErrors.CheckMaxTWRPositive(stage, preCalculationErrors, out preCalculationErrors);
                    CalculationErrors.CheckDryMassPositive(stage, preCalculationErrors, out preCalculationErrors);
                }
            }
            else if (TWRCheckBox.Checked)
            {
                CalculationErrors.CheckThrustPositive(stage, preCalculationErrors, out preCalculationErrors);
                if (MinTWRCheckBox.Checked && MaxTWRCheckBox.Checked)
                {
                    CalculationErrors.CheckWetMassPositive(stage, preCalculationErrors, out preCalculationErrors);
                    CalculationErrors.CheckDryMassPositive(stage, preCalculationErrors, out preCalculationErrors);
                    CalculationErrors.CheckWetMassGreaterThanDryMass(stage, preCalculationErrors, out preCalculationErrors);

                }
                else if (MinTWRCheckBox.Checked)
                {
                    CalculationErrors.CheckWetMassPositive(stage, preCalculationErrors, out preCalculationErrors);
                }
                else if (MaxTWRCheckBox.Checked)
                {
                    CalculationErrors.CheckDryMassPositive(stage, preCalculationErrors, out preCalculationErrors);
                }
            }
            foreach (string error in preCalculationErrors)
            {
                errorList.Append(error);
            }
            return errorList.ToString();
        }
        private void calculate(Stage stage)
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
        private string calculateStageErrors(Rocket rocket, double payloadMass)
        {
            StringBuilder errorList = new StringBuilder();
            foreach (Stage stage in rocket.StageList)
            {
                stage.WetMass += payloadMass;
                stage.DryMass += payloadMass;
                errorList.Append(preCalculate(stage));
                calculate(stage);
            }
            return errorList.ToString();
        }
        private string calculateFooterErrors(Rocket rocket)
        {
            StringBuilder errorList = new StringBuilder();
            double previousWetMass = 0;
            foreach (Stage stage in rocket.StageList)
            {
                errorList.Append(postCalculate(stage, previousWetMass, errorList));
                previousWetMass = stage.WetMass;
            }
            return errorList.ToString();
        }
        private void setFooters(Rocket rocket)
        {
            FooterWetMass.Text = calculateRowTotals(rocket, InputValue.WetMass).ToString();
            FooterDryMass.Text = calculateRowTotals(rocket, InputValue.DryMass).ToString();
            FooterIsp.Text = calculateRowTotals(rocket, InputValue.Isp).ToString();
            FooterDeltaV.Text = calculateDeltaVRowTotals(rocket).ToString();
            FooterThrust.Text = calculateRowTotals(rocket, InputValue.Thrust).ToString();
            FooterMinTWR.Text = calculateRowTotals(rocket, InputValue.MinTWR).ToString();
            FooterMaxTWR.Text = calculateRowTotals(rocket, InputValue.MaxTWR).ToString();
            Reference.Text = "↑: (stage) high<br/>↓: (stage) low<br/>↕: avg. (* # = total)";
        }
        private object calculateRowTotals(Rocket rocket, InputValue value)
        {
            object lowValue = string.Empty;
            object highValue = string.Empty;
            object averageValue = string.Empty;
            object lowValueStage = string.Empty;
            object highValueStage = string.Empty;
            switch (value)
            {
                case InputValue.WetMass:
                    averageValue = rocket.AverageWetMass().ToString();
                    lowValueStage = rocket.LowestWetMass(out lowValue).ToString();
                    highValueStage = rocket.HighestWetMass(out highValue).ToString();
                    break;
                case InputValue.DryMass:
                    averageValue = rocket.AverageDryMass().ToString();
                    lowValueStage = rocket.LowestDryMass(out lowValue).ToString();
                    highValueStage = rocket.HighestDryMass(out highValue).ToString();
                    break;
                case InputValue.Isp:
                    averageValue = rocket.AverageIsp().ToString();
                    lowValueStage = rocket.LowestIsp(out lowValue).ToString();
                    highValueStage = rocket.HighestIsp(out highValue).ToString();
                    break;
                case InputValue.Thrust:
                    averageValue = rocket.AverageThrust().ToString();
                    lowValueStage = rocket.LowestThrust(out lowValue).ToString();
                    highValueStage = rocket.HighestThrust(out highValue).ToString();
                    break;
                case InputValue.MinTWR:
                    averageValue = rocket.AverageMinTWR().ToString();
                    lowValueStage = rocket.LowestMinTWR(out lowValue).ToString();
                    highValueStage = rocket.HighestMinTWR(out highValue).ToString();
                    break;
                case InputValue.MaxTWR:
                    averageValue = rocket.AverageMaxTWR().ToString();
                    lowValueStage = rocket.LowestMaxTWR(out lowValue).ToString();
                    highValueStage = rocket.HighestMaxTWR(out highValue).ToString();
                    break;
                default:
                    break;
            }
            return "↑: (" +
                Truncate(highValueStage.ToString(), 6) + ") " +
                Truncate(highValue.ToString(), 6) + "<br/>↓: (" +
                Truncate(lowValueStage.ToString(), 6) + ") " +
                Truncate(lowValue.ToString(), 6) + "<br/>↕: " +
                Truncate(averageValue.ToString(), 6);
        }
        private string calculateDeltaVRowTotals(Rocket rocket)
        {
            object lowValue;
            object highValue;
            double averageValue = rocket.AverageDeltaV();
            object lowValueStage = rocket.LowestDeltaV(out lowValue);
            object highValueStage = rocket.HighestDeltaV(out highValue);
            return "↑: (" +
                highValueStage.ToString() + ") " +
                highValue.ToString() + "<br/>↓: (" +
                lowValueStage.ToString() + ") " +
                lowValue.ToString() + "<br/>↕: " +
                averageValue.ToString() + " * " +
                rocket.StageList.Count().ToString() + " = " +
                rocket.TotalDeltaV().ToString();
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