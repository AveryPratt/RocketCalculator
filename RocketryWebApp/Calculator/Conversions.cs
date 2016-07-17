using KSP_Library.Rocketry;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;

namespace RocketryWebApp.Calculator
{
    public partial class CalculatorWebForm
    {

        public static class Conversions
        {
            // Table to Rocket conversion
            public delegate string GetTextBoxTextDelegate(TableCell cell);

            public static Rocket ConvertTableToRocket(Table rocketTable, GetTextBoxTextDelegate getTextBoxText)
            {
                Rocket rocket = new Rocket();

                List<TableRow> rowList = screenVisibleRows(rocketTable);
                foreach (TableRow row in rowList)
                {
                    int rowIndex = rowList.FindIndex(r => r == row);
                    Stage stage = new Stage(rocketTable.Rows.GetRowIndex(row) - 1);

                    List<TableCell> cellList = screenVisibleCells(row);
                    foreach (TableCell cell in cellList)
                    {
                        string error = string.Empty;
                        switch (row.Cells.GetCellIndex(cell))
                        {
                            case (int)ColumnName.WetMass:
                                stage.WetMass = double.Parse(getTextBoxText(cell));
                                break;
                            case (int)ColumnName.DryMass:
                                stage.DryMass = double.Parse(getTextBoxText(cell));
                                break;
                            case (int)ColumnName.Isp:
                                stage.Isp = int.Parse(getTextBoxText(cell));
                                break;
                            case (int)ColumnName.DeltaV:
                                stage.DeltaV = int.Parse(getTextBoxText(cell));
                                break;
                            case (int)ColumnName.Thrust:
                                stage.Thrust = double.Parse(getTextBoxText(cell));
                                break;
                            case (int)ColumnName.MinTWR:
                                stage.MinTWR = double.Parse(getTextBoxText(cell));
                                break;
                            case (int)ColumnName.MaxTWR:
                                stage.MaxTWR = double.Parse(getTextBoxText(cell));
                                break;
                            default:
                                break;
                        }
                    }
                    rocket.StageList.Add(stage);
                }
                return rocket;
            }

            public static Rocket ConvertTableToRocket(Table rocketTable, GetTextBoxTextDelegate getTextBoxText, out string conversionErrorsAdded)
            {
                Rocket rocket = new Rocket();
                StringBuilder errorMessage = new StringBuilder();

                List<TableRow> rowList = screenVisibleRows(rocketTable);
                foreach (TableRow row in rowList)
                {
                    int rowIndex = rowList.FindIndex(r => r == row);
                    Stage stage = new Stage(rocketTable.Rows.GetRowIndex(row) - 1);

                    List<TableCell> cellList = screenEnabledCells(row);
                    foreach (TableCell cell in cellList)
                    {
                        string error = string.Empty;
                        switch (row.Cells.GetCellIndex(cell))
                        {
                            case (int)ColumnName.WetMass:
                                stage.WetMass = DoubleConversion(getTextBoxText(cell), rowIndex, (int)InputValue.WetMass, out error);
                                break;
                            case (int)ColumnName.DryMass:
                                stage.DryMass = DoubleConversion(getTextBoxText(cell), rowIndex, (int)InputValue.DryMass, out error);
                                break;
                            case (int)ColumnName.Isp:
                                stage.Isp = IntConversion(getTextBoxText(cell), rowIndex, (int)InputValue.Isp, out error);
                                break;
                            case (int)ColumnName.DeltaV:
                                stage.DeltaV = IntConversion(getTextBoxText(cell), rowIndex, (int)InputValue.DeltaV, out error);
                                break;
                            case (int)ColumnName.Thrust:
                                stage.Thrust = DoubleConversion(getTextBoxText(cell), rowIndex, (int)InputValue.Thrust, out error);
                                break;
                            case (int)ColumnName.MinTWR:
                                stage.MinTWR = DoubleConversion(getTextBoxText(cell), rowIndex, (int)InputValue.MinTWR, out error);
                                break;
                            case (int)ColumnName.MaxTWR:
                                stage.MaxTWR = DoubleConversion(getTextBoxText(cell), rowIndex, (int)InputValue.MaxTWR, out error);
                                break;
                            default:
                                break;
                        }
                        errorMessage.Append(error);
                    }
                    rocket.StageList.Add(stage);
                }
                conversionErrorsAdded = errorMessage.ToString();
                return rocket;
            }
            private static List<TableRow> screenVisibleRows(Table rocketTable)
            {
                List<TableRow> screenedRows = new List<TableRow>();
                foreach (TableRow row in rocketTable.Rows)
                {
                    if (row.Visible == false || 
                        rocketTable.Rows.GetRowIndex(row) == 0 || 
                        rocketTable.Rows.GetRowIndex(row) == 10)
                    {
                        continue;
                    }
                    screenedRows.Add(row);
                }
                return screenedRows;
            }
            private static List<TableRow> screenRows(Table rocketTable, int stageNumber)
            {
                List<TableRow> screenedRows = new List<TableRow>();
                foreach (TableRow row in rocketTable.Rows)
                {
                    if (rocketTable.Rows.GetRowIndex(row) == 0 ||
                        rocketTable.Rows.GetRowIndex(row) > stageNumber)
                    {
                        continue;
                    }
                    screenedRows.Add(row);
                }
                return screenedRows;
            }
            private static List<TableCell> screenEnabledCells(TableRow row)
            {
                List<TableCell> screenedCells = new List<TableCell>();
                foreach (TableCell cell in row.Cells)
                {
                    if (cell.Visible == false || 
                        cell.Enabled == false ||
                        row.Cells.GetCellIndex(cell) == 0 ||
                        row.Cells.GetCellIndex(cell) == 8)
                    {
                        continue;
                    }
                    screenedCells.Add(cell);
                }
                return screenedCells;
            }
            private static List<TableCell> screenVisibleCells(TableRow row)
            {
                List<TableCell> screenedCells = new List<TableCell>();
                foreach (TableCell cell in row.Cells)
                {
                    if (cell.Visible == false ||
                        row.Cells.GetCellIndex(cell) == 0 ||
                        row.Cells.GetCellIndex(cell) == 8)
                    {
                        continue;
                    }
                    screenedCells.Add(cell);
                }
                return screenedCells;
            }
            private static List<TableCell> screenAllCells(TableRow row)
            {
                List<TableCell> screenedCells = new List<TableCell>();
                foreach (TableCell cell in row.Cells)
                {
                    if (row.Cells.GetCellIndex(cell) == 0 ||
                        row.Cells.GetCellIndex(cell) == 8)
                    {
                        continue;
                    }
                    screenedCells.Add(cell);
                }
                return screenedCells;
            }

            public static double ConvertPayloadToInt(string value, out string errorMessage)
            {
                double payloadMass;
                bool isValueDouble = double.TryParse(value, out payloadMass);
                if (!isValueDouble && value != string.Empty)
                {
                    errorMessage = "<error> Payload mass must be an integer or decimal to be recognized. </error><br/>";
                }
                else if (payloadMass < 0)
                {
                    errorMessage = "<error> Payload mass cannot be negative. </error><br/>";
                }
                else errorMessage = string.Empty;
                return payloadMass;
            }

            public static int IntConversion(string value, int rowNumber, int cellNumber, out string errorMessage)
            {
                int result;
                bool isValueInt = int.TryParse(value, out result);
                if (!isValueInt)
                {
                    errorMessage = "<error> Stage " + (rowNumber + 1).ToString() + "'s " + Enum.GetName(typeof(InputValue), cellNumber) + " must be an integer. </error><br/>";
                }
                else errorMessage = null;
                return result;
            }
            public static double DoubleConversion(string value, int rowNumber, int cellNumber, out string errorMessage)
            {
                double result;
                bool isValueDouble = double.TryParse(value, out result);
                if (!isValueDouble)
                {
                    errorMessage = "<error> Stage " + (rowNumber + 1).ToString() + "'s " + Enum.GetName(typeof(InputValue), cellNumber) + " must be an integer or a decimal. </error><br/>";
                }
                else errorMessage = null;
                return result;
            }

            // Rocket to Table conversion
            public delegate void SetTextBoxTextDelegate(TableCell cell, string text);
            public static void ConvertRocketToScreenedTable(Rocket rocket, Table rocketTable, SetTextBoxTextDelegate setTextBoxText)
            {
                foreach (TableRow row in screenVisibleRows(rocketTable))
                {
                    int rowIndex = rocketTable.Rows.GetRowIndex(row) - 1;
                    convertStageToVisibleRow(rocket.StageList.Find(s => s.ID == rowIndex), rocket.PayloadMass, row, setTextBoxText);
                }
            }
            public static void ConvertRocketToWholeTable(Rocket rocket, Table rocketTable, SetTextBoxTextDelegate setTextBoxText)
            {
                foreach (TableRow row in screenRows(rocketTable, rocket.StageList.Count))
                {
                    int rowIndex = rocketTable.Rows.GetRowIndex(row) - 1;
                    convertStageToWholeRow(rocket.StageList.Find(s => s.ID == rowIndex), rocket.PayloadMass, row, setTextBoxText);
                }
            }

            private static void convertStageToVisibleRow(Stage stage, double payloadMass, TableRow row, SetTextBoxTextDelegate setTextBoxText)
            {
                foreach (TableCell cell in screenVisibleCells(row))
                {
                    switch (row.Cells.GetCellIndex(cell))
                    {
                        case (int)ColumnName.WetMass:
                            setTextBoxText(cell, stage.WetMass.ToString());
                            continue;
                        case (int)ColumnName.DryMass:
                            setTextBoxText(cell, stage.DryMass.ToString());
                            continue;
                        case (int)ColumnName.Isp:
                            setTextBoxText(cell, stage.Isp.ToString());
                            continue;
                        case (int)ColumnName.DeltaV:
                            setTextBoxText(cell, stage.DeltaV.ToString());
                            continue;
                        case (int)ColumnName.Thrust:
                            setTextBoxText(cell, stage.Thrust.ToString());
                            continue;
                        case (int)ColumnName.MinTWR:
                            setTextBoxText(cell, stage.MinTWR.ToString());
                            continue;
                        case (int)ColumnName.MaxTWR:
                            setTextBoxText(cell, stage.MaxTWR.ToString());
                            continue;
                        default:
                            continue;
                    }
                }
            }
            private static void convertStageToWholeRow(Stage stage, double payloadMass, TableRow row, SetTextBoxTextDelegate setTextBoxText)
            {
                foreach (TableCell cell in screenAllCells(row))
                {
                    switch (row.Cells.GetCellIndex(cell))
                    {
                        case (int)ColumnName.WetMass:
                            setTextBoxText(cell, stage.WetMass.ToString());
                            continue;
                        case (int)ColumnName.DryMass:
                            setTextBoxText(cell, stage.DryMass.ToString());
                            continue;
                        case (int)ColumnName.Isp:
                            setTextBoxText(cell, stage.Isp.ToString());
                            continue;
                        case (int)ColumnName.DeltaV:
                            setTextBoxText(cell, stage.DeltaV.ToString());
                            continue;
                        case (int)ColumnName.Thrust:
                            setTextBoxText(cell, stage.Thrust.ToString());
                            continue;
                        case (int)ColumnName.MinTWR:
                            setTextBoxText(cell, stage.MinTWR.ToString());
                            continue;
                        case (int)ColumnName.MaxTWR:
                            setTextBoxText(cell, stage.MaxTWR.ToString());
                            continue;
                        default:
                            continue;
                    }
                }
            }
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
                rocket.StageList.Count.ToString() + " = " +
                rocket.TotalDeltaV().ToString();
        }
    }
}