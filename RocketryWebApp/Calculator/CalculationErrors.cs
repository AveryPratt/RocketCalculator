using KSP_Library.Rocketry;
using System.Collections.Generic;
using System.Text;

namespace RocketryWebApp.Calculator
{
    public partial class CalculatorWebForm
    {
        public static class CalculationErrors
        {
            // Stage calculation errors
            public static string CheckWetMassPositive(Stage stage)
            {
                return checkSingleValueFunction(
                    stage.WetMass, 
                    o => (double)o > 0, 
                    "<error> Stage " + (stage.ID + 1).ToString() + "'s wet mass must be greater than 0. </error><br/>");
            }
            public static string CheckDryMassPositive(Stage stage)
            {
                return checkSingleValueFunction(
                    stage.DryMass, 
                    o => (double)o > 0, 
                    "<error> Stage " + (stage.ID + 1).ToString() + "'s dry mass must be greater than 0. </error><br/>");
            }
            public static string CheckIspPositive(Stage stage)
            {
                return checkSingleValueFunction(
                    stage.Isp, 
                    o => (int)o > 0, 
                    "<error> Stage " + (stage.ID + 1).ToString() + "'s Isp should be positive. </error><br/>");
            }
            public static string CheckDeltaVPositive(Stage stage)
            {
                return checkSingleValueFunction(
                    stage.DeltaV, 
                    o => (int)o > 0, 
                    "<error> Stage " + (stage.ID + 1).ToString() + "'s Δv should be positive. </error><br/>");
            }
            public static string CheckThrustPositive(Stage stage)
            {
                return checkSingleValueFunction(
                    stage.Thrust, 
                    o => (double)o >= 0, 
                    "<error> Stage " + (stage.ID + 1).ToString() + "'s thrust should be positive. </error><br/>");
            }
            public static string CheckMinTWRPositive(Stage stage)
            {
                return checkSingleValueFunction(
                    stage.MinTWR, 
                    o => (double)o >= 0, 
                    "<error> Stage " + (stage.ID + 1).ToString() + "'s min. TWR should be positive. </error><br/>");
            }
            public static string CheckMaxTWRPositive(Stage stage)
            {
                return checkSingleValueFunction(
                    stage.MaxTWR, 
                    o => (double)o >= 0, 
                    "<error> Stage " + (stage.ID + 1).ToString() + "'s max. TWR should be positive. </error><br/>");
            }
            private static string checkSingleValueFunction(object stageItem, SingleValueBool func, string error)
            {
                if (!func(stageItem))
                {
                    return error;
                }
                else
                {
                    return string.Empty;
                }
            }

            public static string CheckWetMassGreaterThanDryMass(Stage stage)
            {
                return checkDoubleValueFunction(
                    stage.WetMass, 
                    stage.DryMass, 
                    (o, p) => (double)o >= (double)p, 
                    "<error> Stage " + (stage.ID + 1).ToString() + "'s wet mass should not be less than its dry mass. </error><br/>");
            }
            public static string CheckMinTWRLessThanMaxTWR(Stage stage)
            {
                return checkDoubleValueFunction(
                    stage.MaxTWR, 
                    stage.MinTWR, 
                    (o, p) => (double)o >= (double)p, 
                    "<error> Stage " + (stage.ID + 1).ToString() + "'s min. TWR should not be less than its max. TWR. </error><br/>");
            }
            private static string checkDoubleValueFunction(object stageItem1, object stageItem2, DoubleValueBool func, string error)
            {
                if (!func(stageItem1, stageItem2))
                {
                    return error;
                }
                else
                {
                    return string.Empty;
                }
            }

            // Rocket calculation errors
            public static string CheckWetMassGreaterThanPreviousStageWetMass(Stage stage, double previousWetMass)
            {
                if (stage.ID > 0)
                {
                    return checkDoubleValueFunction(
                        stage.WetMass,
                        previousWetMass,
                        (o, p) => (double)o >= (double)p,
                        "<error> Stage " + (stage.ID + 1).ToString() + "'s wet mass should be greater than Stage " + stage.ID.ToString() + "'s wet mass. </error><br/>");
                }
                else
                {
                    return string.Empty;
                }
            }
            public static string CheckDryMassGreaterThanPreviousStageWetMass(Stage stage, double previousWetMass)
            {
                if (stage.ID > 0)
                {
                    return checkDoubleValueFunction(
                        stage.DryMass,
                        previousWetMass,
                        (o, p) => (double)o >= (double)p,
                        "<error> Stage " + (stage.ID + 1).ToString() + "'s dry mass should be greater than Stage " + stage.ID.ToString() + "'s wet mass. </error><br/>");
                }
                else
                {
                    return string.Empty;
                }
            }
            public static string CheckDryMassGreaterThanPreviousStageDryMass(Stage stage, double previousDryMass)
            {
                if (stage.ID > 0)
                {
                    return checkDoubleValueFunction(
                        stage.DryMass,
                        previousDryMass,
                        (o, p) => (double)o >= (double)p,
                        "<error> Stage " + (stage.ID + 1).ToString() + "'s dry mass should be greater than Stage " + stage.ID.ToString() + "'s dry mass. </error><br/>");
                }
                else
                {
                    return string.Empty;
                }
            }

            // delegates
            public delegate bool SingleValueBool(object value);
            public delegate bool DoubleValueBool(object value1, object value2);
        }
        private string preCalculate(Rocket rocket)
        {
            StringBuilder errors = new StringBuilder();
            foreach (Stage stage in rocket.StageList)
            {
                if (WetMassCell.Visible)
                {
                    errors.Append(CalculationErrors.CheckWetMassPositive(stage));
                }
                if (DryMassCell.Visible)
                {
                    errors.Append(CalculationErrors.CheckDryMassPositive(stage));
                }
                if (IspCell.Visible && IspCell.Enabled)
                {
                    errors.Append(CalculationErrors.CheckIspPositive(stage));
                }
                if (DeltaVCell.Visible && DeltaVCell.Enabled)
                {
                    errors.Append(CalculationErrors.CheckDeltaVPositive(stage));
                }
                if (ThrustCell.Visible && ThrustCell.Enabled)
                {
                    errors.Append(CalculationErrors.CheckThrustPositive(stage));
                }
                if (MinTWRCell.Visible && MinTWRCell.Enabled)
                {
                    errors.Append(CalculationErrors.CheckMinTWRPositive(stage));
                }
                if (MaxTWRCell.Visible && MaxTWRCell.Enabled)
                {
                    errors.Append(CalculationErrors.CheckMaxTWRPositive(stage));
                }
                if (WetMassCell.Visible && DryMassCell.Visible)
                {
                    errors.Append(CalculationErrors.CheckWetMassGreaterThanDryMass(stage));
                }
                if (MinTWRCell.Visible && MinTWRCell.Enabled && MaxTWRCell.Visible && MaxTWRCell.Enabled)
                {
                    errors.Append(CalculationErrors.CheckMinTWRLessThanMaxTWR(stage));
                }
            }
            return errors.ToString();
        }
        private string postCalculate(Rocket rocket)
        {
            StringBuilder errors = new StringBuilder();
            foreach (Stage stage in rocket.StageList)
            {
                double previousWetMass = 0;
                if (stage.ID >= 1)
                {
                    previousWetMass = rocket.StageList.Find(s => s.ID == stage.ID - 1).WetMass;
                }
                if (WetMassCell.Visible)
                {
                    errors.Append(CalculationErrors.CheckWetMassGreaterThanPreviousStageWetMass(stage, previousWetMass));
                }
                if (WetMassCell.Visible && DryMassCell.Visible)
                {
                    errors.Append(CalculationErrors.CheckDryMassGreaterThanPreviousStageWetMass(stage, previousWetMass));
                }
                if (DryMassCell.Visible && !WetMassCell.Visible)
                {
                    errors.Append(CalculationErrors.CheckDryMassGreaterThanPreviousStageDryMass(stage, previousWetMass));
                }
            }
            return errors.ToString();
        }
    }
}