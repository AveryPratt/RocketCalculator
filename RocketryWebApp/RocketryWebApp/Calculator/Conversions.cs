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

        public static class Conversions
        {
            // Table to Rocket conversion
            public delegate string GetTextBoxTextDelegate(TableCell cell);

            public static List<Stage> ConvertWholeTableToStageList(Table rocketTable, GetTextBoxTextDelegate getTextBoxText)
            {
                List<Stage> stageList = new List<Stage>();

                List<TableRow> rowList = screenRows(rocketTable);
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
                    stageList.Add(stage);
                }
                return stageList;
            }

            public static List<Stage> ConvertScreenedTableToStageList(Table rocketTable, GetTextBoxTextDelegate getTextBoxText, out string conversionErrorsAdded)
            {
                List<Stage> stageList = new List<Stage>();
                StringBuilder errorMessage = new StringBuilder();

                List<TableRow> rowList = screenRows(rocketTable);
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
                    stageList.Add(stage);
                }
                conversionErrorsAdded = errorMessage.ToString();
                return stageList;
            }
            private static List<TableRow> screenRows(Table rocketTable)
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

            public static double ConvertPayloadToInt(string value, out string errorMessage)
            {
                double payloadMass;
                bool isValueDouble = double.TryParse(value, out payloadMass);
                if (!isValueDouble && value != string.Empty)
                {
                    errorMessage = "<error> Payload Mass must be an integer or decimal to be recognized. </error><br/>";
                }
                else errorMessage = null;
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
            public static void ConvertStageListToTable(List<Stage> stageList, double payloadMass, Table rocketTable, SetTextBoxTextDelegate setTextBoxText)
            {
                foreach (TableRow row in screenRows(rocketTable))
                {
                    int rowIndex = rocketTable.Rows.GetRowIndex(row) - 1;
                    convertStageToRow(stageList.Find(s => s.ID == rowIndex), payloadMass, row, setTextBoxText);
                }
            }

            private static void convertStageToRow(Stage stage, double payloadMass, TableRow row, SetTextBoxTextDelegate setTextBoxText)
            {
                stage.WetMass -= payloadMass;
                stage.DryMass -= payloadMass;
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
        }
    }
}