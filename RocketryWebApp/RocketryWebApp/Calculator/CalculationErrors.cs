using KSP_Library.Rocketry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace RocketryWebApp.Calculator
{
    public partial class CalculatorWebForm
    {
        public static class CalculationErrors
        {
            // Stage calculation errors
            public delegate bool SingleValueBool(object value);
            public static void CheckWetMassPositive(Stage stage, List<string> errorList, out List<string> errorListAdded)
            {
                checkSingleValueFunction(stage.WetMass, o => (double)o > 0, "<error> Stage " + (stage.ID + 1).ToString() + "'s wet mass must be greater than 0. </error><br/>", errorList, out errorList);
                errorListAdded = errorList;
            }
            public static void CheckDryMassPositive(Stage stage, List<string> errorList, out List<string> errorListAdded)
            {
                checkSingleValueFunction(stage.DryMass, o => (double)o > 0, "<error> Stage " + (stage.ID + 1).ToString() + "'s dry mass must be greater than 0. </error><br/>", errorList, out errorList);
                errorListAdded = errorList;
            }
            public static void CheckIspPositive(Stage stage, List<string> errorList, out List<string> errorListAdded)
            {
                checkSingleValueFunction(stage.Isp, o => (int)o > 0, "<error> Stage " + (stage.ID + 1).ToString() + "'s Isp should be positive. </error><br/>", errorList, out errorList);
                errorListAdded = errorList;
            }
            public static void CheckDeltaVPositive(Stage stage, List<string> errorList, out List<string> errorListAdded)
            {
                checkSingleValueFunction(stage.DeltaV, o => (int)o > 0, "<error> Stage " + (stage.ID + 1).ToString() + "'s Δv should be positive. </error><br/>", errorList, out errorList);
                errorListAdded = errorList;
            }
            public static void CheckThrustPositive(Stage stage, List<string> errorList, out List<string> errorListAdded)
            {
                checkSingleValueFunction(stage.Thrust, o => (double)o >= 0, "<error> Stage " + (stage.ID + 1).ToString() + "'s thrust should be positive. </error><br/>", errorList, out errorList);
                errorListAdded = errorList;
            }
            public static void CheckMinTWRPositive(Stage stage, List<string> errorList, out List<string> errorListAdded)
            {
                checkSingleValueFunction(stage.MinTWR, o => (double)o >= 0, "<error> Stage " + (stage.ID + 1).ToString() + "'s min. TWR should be positive. </error><br/>", errorList, out errorList);
                errorListAdded = errorList;
            }
            public static void CheckMaxTWRPositive(Stage stage, List<string> errorList, out List<string> errorListAdded)
            {
                checkSingleValueFunction(stage.MaxTWR, o => (double)o >= 0, "<error> Stage " + (stage.ID + 1).ToString() + "'s max. TWR should be positive. </error><br/>", errorList, out errorList);
                errorListAdded = errorList;
            }
            private static void checkSingleValueFunction(object stageItem, SingleValueBool func, string error, List<string> errorList, out List<string> errorListAdded)
            {
                if (!func(stageItem))
                {
                    if (!errorList.Contains(error))
                    {
                        errorList.Add(error);
                    }
                }
                errorListAdded = errorList;
            }

            public delegate bool DoubleValueBool(object value1, object value2);
            public static void CheckWetMassGreaterThanDryMass(Stage stage, List<string> errorList, out List<string> errorListAdded)
            {
                checkDoubleValueFunction(stage.WetMass, stage.DryMass, (o, p) => (double)o >= (double)p, "<error> Stage " + (stage.ID + 1).ToString() + "'s wet mass should not be less than its dry mass. </error><br/>", errorList, out errorList);
                errorListAdded = errorList;
            }
            public static void CheckMinTWRLessThanMaxTWR(Stage stage, List<string> errorList, out List<string> errorListAdded)
            {
                checkDoubleValueFunction(stage.MaxTWR, stage.MinTWR, (o, p) => (double)o >= (double)p, "<error> Stage " + (stage.ID + 1).ToString() + "'s min. TWR should not be less than its max. TWR. </error><br/>", errorList, out errorList);
                errorListAdded = errorList;
            }
            private static void checkDoubleValueFunction(object stageItem1, object stageItem2, DoubleValueBool func, string error, List<string> errorList, out List<string> errorListAdded)
            {
                if (!func(stageItem1, stageItem2))
                {
                    if (!errorList.Contains(error))
                    {
                        errorList.Add(error);
                    }
                }
                errorListAdded = errorList;
            }

            // Rocket calculation errors
            public static void CheckWetMassGreaterThanPreviousStageWetMass(Stage stage, double previousWetMass, List<string> errorList, out List<string> errorListAdded)
            {
                if (stage.ID > 0)
                {
                    checkDoubleValueFunction(
                        stage.WetMass,
                        previousWetMass,
                        (o, p) => (double)o >= (double)p,
                        "<error> Stage " + (stage.ID + 1).ToString() + "'s wet mass should be greater than Stage " + stage.ID.ToString() + "'s wet Mass. </error><br/>",
                        errorList,
                        out errorList);
                }
                errorListAdded = errorList;
            }
            public static void CheckDryMassGreaterThanPreviousStageWetMass(Stage stage, double previousWetMass, List<string> errorList, out List<string> errorListAdded)
            {
                if (stage.ID > 0)
                {
                    checkDoubleValueFunction(
                        stage.DryMass,
                        previousWetMass,
                        (o, p) => (double)o >= (double)p,
                        "<error> Stage " + (stage.ID + 1).ToString() + "'s dry mass should be greater than Stage " + stage.ID.ToString() + "'s wet Mass. </error><br/>",
                        errorList,
                        out errorList);
                }
                errorListAdded = errorList;
            }
            public static void CheckDryMassGreaterThanPreviousStageDryMass(Stage stage, double previousDryMass, List<string> errorList, out List<string> errorListAdded)
            {
                if (stage.ID > 0)
                {
                    checkDoubleValueFunction(
                        stage.DryMass,
                        previousDryMass,
                        (o, p) => (double)o >= (double)p,
                        "<error> Stage " + (stage.ID + 1).ToString() + "'s dry mass should be greater than Stage " + stage.ID.ToString() + "'s dry Mass. </error><br/>",
                        errorList,
                        out errorList);
                }
                errorListAdded = errorList;
            }
        }
    }
}