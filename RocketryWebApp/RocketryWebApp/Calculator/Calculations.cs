using KSP_Library.Rocketry;
using KSP_Library.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;

namespace RocketryWebApp.Calculator
{
    public partial class CalculatorWebForm
    {
        public static class Calculations
        {
            // Stage calculations
            public static void CalculateDeltaV(Stage stage)
            {
                stage.GetDeltaV();
            }
            public static void CalculateIsp(Stage stage)
            {
                stage.GetIsp();
            }
            public static void CalculateThrustFromMinTWR(Stage stage, Body body)
            {
                stage.GetThrustFromMinTWR(body.GM, body.Radius);
            }
            public static void CalculateThrustFromMaxTWR(Stage stage, Body body)
            {
                stage.GetThrustFromMaxTWR(body.GM, body.Radius);
            }
            public static void CalculateMinTWR(Stage stage, Body body)
            {
                stage.GetMinTWR(body.GM, body.Radius);
            }
            public static void CalculateMaxTWR(Stage stage, Body body)
            {
                stage.GetMaxTWR(body.GM, body.Radius);
            }

            // Rocket calculations
            public static int CalculateHighestWetMass(List<Stage> stageList, out double value)
            {
                return calculateHighest(stageList, InputValue.WetMass, out value);
            }
            public static int CalculateLowestWetMass(List<Stage> stageList, out double value)
            {
                return calculateLowest(stageList, InputValue.WetMass, out value);
            }
            public static double CalculateAverageWetMass(List<Stage> stageList)
            {
                return calculateAverage(stageList, InputValue.WetMass);
            }

            public static int CalculateHighestDryMass(List<Stage> stageList, out double value)
            {
                return calculateHighest(stageList, InputValue.DryMass, out value);
            }
            public static int CalculateLowestDryMass(List<Stage> stageList, out double value)
            {
                return calculateLowest(stageList, InputValue.DryMass, out value);
            }
            public static double CalculateAverageDryMass(List<Stage> stageList)
            {
                return calculateAverage(stageList, InputValue.DryMass);
            }

            public static double CalculateHighestMassRatio(List<Stage> stageList)
            {
                return calculateRatio(stageList, InputValue.WetMass, InputValue.DryMass).Max();
            }
            public static double CalculateLowestMassRatio(List<Stage> stageList)
            {
                return calculateRatio(stageList, InputValue.WetMass, InputValue.DryMass).Min();
            }

            public static int CalculateTotalDeltaV(List<Stage> stageList)
            {
                return (int)calculateTotal(stageList, InputValue.DeltaV);
            }
            public static int CalculateHighestDeltaV(List<Stage> stageList, out int value)
            {
                return calculateHighest(stageList, InputValue.DeltaV, out value);
            }
            public static int CalculateLowestDeltaV(List<Stage> stageList, out int value)
            {
                return calculateLowest(stageList, InputValue.DeltaV, out value);
            }
            public static int CalculateAverageDeltaV(List<Stage> stageList)
            {
                return (int)calculateAverage(stageList, InputValue.DeltaV);
            }

            public static int CalculateHighestIsp(List<Stage> stageList, out int value)
            {
                return calculateHighest(stageList, InputValue.Isp, out value);
            }
            public static int CalculateLowestIsp(List<Stage> stageList, out int value)
            {
                return calculateLowest(stageList, InputValue.Isp, out value);
            }
            public static int CalculateAverageIsp(List<Stage> stageList)
            {
                return (int)calculateAverage(stageList, InputValue.Isp);
            }

            public static int CalculateHighestThrust(List<Stage> stageList, out double value)
            {
                return calculateHighest(stageList, InputValue.Thrust, out value);
            }
            public static int CalculateLowestThrust(List<Stage> stageList, out double value)
            {
                return calculateLowest(stageList, InputValue.Thrust, out value);
            }
            public static double CalculateAverageThrust(List<Stage> stageList)
            {
                return calculateAverage(stageList, InputValue.Thrust);
            }

            public static int CalculateHighestMinTWR(List<Stage> stageList, out double value)
            {
                return calculateHighest(stageList, InputValue.MinTWR, out value);
            }
            public static int CalculateLowestMinTWR(List<Stage> stageList, out double value)
            {
                return calculateLowest(stageList, InputValue.MinTWR, out value);
            }
            public static double CalculateAverageMinTWR(List<Stage> stageList)
            {
                return calculateAverage(stageList, InputValue.MinTWR);
            }

            public static int CalculateHighestMaxTWR(List<Stage> stageList, out double value)
            {
                return calculateHighest(stageList, InputValue.MaxTWR, out value);
            }
            public static int CalculateLowestMaxTWR(List<Stage> stageList, out double value)
            {
                return calculateLowest(stageList, InputValue.MaxTWR, out value);
            }
            public static double CalculateAverageMaxTWR(List<Stage> stageList)
            {
                return calculateAverage(stageList, InputValue.MaxTWR);
            }

            // helper methods
            private static double[] indexStagePropertyValues(List<Stage> stageList, InputValue stageProperty)
            {
                double[] values = new double[stageList.Count];
                foreach (Stage stage in stageList)
                {
                    switch (stageProperty)
                    {
                        case InputValue.WetMass:
                            values[stage.ID] = stage.WetMass - values.Sum(); // subtracting the sum of the object values before it means that each stage's wet mass will be individual and not cumulative.
                            break;
                        case InputValue.DryMass:
                            values[stage.ID] = stage.DryMass - values.Sum(); // subtracting the sum of the object values before it means that each stage's wet mass will be individual and not cumulative.
                            break;
                        case InputValue.Isp:
                            values[stage.ID] = stage.Isp;
                            break;
                        case InputValue.DeltaV:
                            values[stage.ID] = stage.DeltaV;
                            break;
                        case InputValue.Thrust:
                            values[stage.ID] = stage.Thrust;
                            break;
                        case InputValue.MinTWR:
                            values[stage.ID] = stage.MinTWR;
                            break;
                        case InputValue.MaxTWR:
                            values[stage.ID] = stage.MaxTWR;
                            break;
                        default:
                            break;
                    }
                }
                return values;
            }

            private static double calculateTotal(List<Stage> stageList, InputValue stageProperty)
            {
                double[] values = indexStagePropertyValues(stageList, stageProperty);
                return values.Sum();
            }
            private static double calculateAverage(List<Stage> stageList, InputValue stageProperty)
            {
                double[] values = indexStagePropertyValues(stageList, stageProperty);
                return values.Average();
            }
            private static int calculateHighest(List<Stage> stageList, InputValue stageProperty, out double value)
            {
                double[] values = indexStagePropertyValues(stageList, stageProperty);
                value = values.Max();
                return Array.IndexOf(values, value) + 1;
            }
            private static int calculateLowest(List<Stage> stageList, InputValue stageProperty, out double value)
            {
                double[] values = indexStagePropertyValues(stageList, stageProperty);
                value = values.Min();
                return Array.IndexOf(values, value) + 1;
            }
            private static int calculateHighest(List<Stage> stageList, InputValue stageProperty, out int value)
            {
                double[] values = indexStagePropertyValues(stageList, stageProperty);
                value = (int)values.Max();
                return Array.IndexOf(values, value) + 1;
            }
            private static int calculateLowest(List<Stage> stageList, InputValue stageProperty, out int value)
            {
                double[] values = indexStagePropertyValues(stageList, stageProperty);
                value = (int)values.Min();
                return Array.IndexOf(values, value) + 1;
            }
            private static double[] calculateRatio(List<Stage> stageList, InputValue numerator, InputValue denominator)
            {
                double[] numeratorValues = indexStagePropertyValues(stageList, numerator);
                double[] denominatorValues = indexStagePropertyValues(stageList, denominator);
                double[] ratio = new double[stageList.Count()];
                for (int i = 0; i < stageList.Count(); i++)
                {
                    ratio[i] = numeratorValues[i] / denominatorValues[i];
                }
                return ratio;
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
                }
                List<Stage> stageList = Conversions.ConvertScreenedTableToStageList(RocketTable, getTextBoxText, out conversionErrors);
                if (conversionErrors != string.Empty)
                {
                    errors.Append("<error> Input Conversion Unsuccessful. </error><br/>");
                    errors.Append(conversionErrors);
                    break;
                }
                errors.Append("<noerror> Input Conversion Successful. </noerror><br/>");
                calculateStages(stageList, payloadMass, out calculationErrors);
                if(calculationErrors != string.Empty)
                {
                    errors.Append("<error> Stage Calculation Unsuccessful. </error><br/>");
                    errors.Append(calculationErrors);
                    break;
                }
                Conversions.ConvertStageListToTable(stageList, payloadMass, RocketTable, setTextBoxText);
                errors.Append("<noerror> Stage Calculation Successful. </noerror><br/>");
                calculateFooters(stageList, out postCalculationErrors);
                if (postCalculationErrors != string.Empty)
                {
                    errors.Append("<error> Totals Calculation Unsuccessful. </error><br/>");
                    errors.Append(postCalculationErrors);
                    break;
                }
                errors.Append("<noerror> Totals Calculation Successful. </noerror><br/>");
                setFooters(stageList);
                SaveButton.Visible = true;
                break;
            }
            ErrorMessage.InnerHtml = errors.ToString();
            ErrorMessage.Visible = true;
        }
        private void postCalculate(Stage stage, double previousWetMass, StringBuilder errorList, out StringBuilder errorListAdded)
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
            errorListAdded = errorList;
        }
        private void preCalculate(Stage stage, StringBuilder errorList, out StringBuilder errorListAdded)
        {
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
            errorListAdded = errorList;
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
        private void calculateStages(List<Stage> stageList, double payloadMass, out string calculationErrors)
        {
            StringBuilder errorList = new StringBuilder();
            foreach (Stage stage in stageList)
            {
                stage.WetMass += payloadMass;
                stage.DryMass += payloadMass;
                preCalculate(stage, errorList, out errorList);
                calculate(stage);
            }
            calculationErrors = errorList.ToString();
        }
        private void calculateFooters(List<Stage> stageList, out string errorListAdded)
        {
            StringBuilder errorList = new StringBuilder();
            double previousWetMass = 0;
            foreach (Stage stage in stageList)
            {
                postCalculate(stage, previousWetMass, errorList, out errorList);
                previousWetMass = stage.WetMass;
            }
            errorListAdded = errorList.ToString();
        }
        private void setFooters(List<Stage> stageList)
        {
            FooterWetMass.Text = calculateDoubleRowTotals(stageList, Calculations.CalculateAverageWetMass, Calculations.CalculateHighestWetMass, Calculations.CalculateLowestWetMass);
            FooterDryMass.Text = calculateDoubleRowTotals(stageList, Calculations.CalculateAverageDryMass, Calculations.CalculateHighestDryMass, Calculations.CalculateLowestDryMass);
            FooterIsp.Text = calculateIntRowTotals(stageList, Calculations.CalculateAverageIsp, Calculations.CalculateHighestIsp, Calculations.CalculateLowestIsp);
            FooterDeltaV.Text = calculateDeltaVRowTotals(stageList, Calculations.CalculateAverageDeltaV, Calculations.CalculateHighestDeltaV, Calculations.CalculateLowestDeltaV);
            FooterThrust.Text = calculateDoubleRowTotals(stageList, Calculations.CalculateAverageThrust, Calculations.CalculateHighestThrust, Calculations.CalculateLowestThrust);
            FooterMinTWR.Text = calculateDoubleRowTotals(stageList, Calculations.CalculateAverageMinTWR, Calculations.CalculateHighestMinTWR, Calculations.CalculateLowestMinTWR);
            FooterMaxTWR.Text = calculateDoubleRowTotals(stageList, Calculations.CalculateAverageMaxTWR, Calculations.CalculateHighestMaxTWR, Calculations.CalculateLowestMaxTWR);
            Reference.Text = "↑: (stage) high<br/>↓: (stage) low<br/>↕: avg. (* # = total)";
        }
        public delegate double CalculateDoubleFunction(List<Stage> stageList);
        public delegate int CalculateIntFunction(List<Stage> stageList);
        public delegate int CalculateHighLowDoubleFunction(List<Stage> stageList, out double value);
        public delegate int CalculateHighLowIntFunction(List<Stage> stageList, out int value);
        private string calculateDoubleRowTotals(List<Stage> stageList, CalculateDoubleFunction averageCalculation, CalculateHighLowDoubleFunction highCalculation, CalculateHighLowDoubleFunction lowCalculation)
        {
            double lowValue;
            double highValue;
            double averageValue = averageCalculation(stageList);
            int lowValueStage = lowCalculation(stageList, out lowValue);
            int highValueStage = highCalculation(stageList, out highValue);
            return "↑: (" +
                Truncate(highValueStage.ToString(), 6) + ") " +
                Truncate(highValue.ToString(), 6) + "<br/>↓: (" +
                Truncate(lowValueStage.ToString(), 6) + ") " +
                Truncate(lowValue.ToString(), 6) + "<br/>↕: " +
                Truncate(averageValue.ToString(), 6);
        }
        private string calculateDeltaVRowTotals(List<Stage> stageList, CalculateIntFunction averageCalculation, CalculateHighLowIntFunction highCalculation, CalculateHighLowIntFunction lowCalculation)
        {
            int lowValue;
            int highValue;
            double averageValue = averageCalculation(stageList);
            int lowValueStage = lowCalculation(stageList, out lowValue);
            int highValueStage = highCalculation(stageList, out highValue);
            return "↑: (" +
                highValueStage.ToString() + ") " +
                highValue.ToString() + "<br/>↓: (" +
                lowValueStage.ToString() + ") " +
                lowValue.ToString() + "<br/>↕: " +
                averageValue.ToString() + " * " +
                stageList.Count().ToString() + " = " +
                (averageValue * stageList.Count()).ToString();
        }
        private string calculateIntRowTotals(List<Stage> stageList, CalculateIntFunction averageCalculation, CalculateHighLowIntFunction highCalculation, CalculateHighLowIntFunction lowCalculation)
        {
            int lowValue;
            int highValue;
            double averageValue = averageCalculation(stageList);
            int lowValueStage = lowCalculation(stageList, out lowValue);
            int highValueStage = highCalculation(stageList, out highValue);
            return "↑: (" +
                highValueStage.ToString() + ") " +
                highValue.ToString() + "<br/>↓: (" +
                lowValueStage.ToString() + ") " +
                lowValue.ToString() + "<br/>↕: " + 
                averageValue.ToString();
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