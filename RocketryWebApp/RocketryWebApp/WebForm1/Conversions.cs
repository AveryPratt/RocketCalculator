using KSP_Library.Rocketry;
using KSP_Library.Systems;
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
        public static class Conversions
        {
            // Table to Rocket conversion
            public delegate string GetTextBoxTextDelegate(TableCell cell);
            public static List<Stage> ConvertTableToStageList(Table rocketTable, GetTextBoxTextDelegate getTextBoxText, out string conversionErrorsAdded)
            {
                List<Stage> stageList = setStageList(rocketTable, getTextBoxText, out conversionErrorsAdded);
                return stageList;
            }
            private static List<Stage> setStageList(Table rocketTable, GetTextBoxTextDelegate getTextBoxText, out string errorMessageAdded)
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
                                stage.WetMass = ConversionErrors.DoubleConversion(getTextBoxText(cell), rowIndex, (int)InputValue.WetMass, out error);
                                break;
                            case (int)ColumnName.DryMass:
                                stage.DryMass = ConversionErrors.DoubleConversion(getTextBoxText(cell), rowIndex, (int)InputValue.DryMass, out error);
                                break;
                            case (int)ColumnName.Isp:
                                stage.Isp = ConversionErrors.IntConversion(getTextBoxText(cell), rowIndex, (int)InputValue.Isp, out error);
                                break;
                            case (int)ColumnName.DeltaV:
                                stage.DeltaV = ConversionErrors.IntConversion(getTextBoxText(cell), rowIndex, (int)InputValue.DeltaV, out error);
                                break;
                            case (int)ColumnName.Thrust:
                                stage.Thrust = ConversionErrors.DoubleConversion(getTextBoxText(cell), rowIndex, (int)InputValue.Thrust, out error);
                                break;
                            case (int)ColumnName.MinTWR:
                                stage.MinTWR = ConversionErrors.DoubleConversion(getTextBoxText(cell), rowIndex, (int)InputValue.MinTWR, out error);
                                break;
                            case (int)ColumnName.MaxTWR:
                                stage.MaxTWR = ConversionErrors.DoubleConversion(getTextBoxText(cell), rowIndex, (int)InputValue.MaxTWR, out error);
                                break;
                            default:
                                break;
                        }
                        errorMessage.Append(error);
                    }
                    stageList.Add(stage);
                }
                errorMessageAdded = errorMessage.ToString();
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