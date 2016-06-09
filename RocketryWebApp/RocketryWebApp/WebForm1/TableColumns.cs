using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;

namespace RocketryWebApp.WebForm1
{
    [Obsolete]
    public static class TableColumns
    {

        // =========== TextBox Columns ============
        public static List<TextBox[]> Columns { get; }

        public static TextBox[] WetMassColumn { get; }
        public static TextBox[] DryMassColumn { get; }
        public static TextBox[] IspColumn { get; }
        public static TextBox[] DeltaVColumn { get; }
        public static TextBox[] ThrustColumn { get; }
        public static TextBox[] MinTWRColumn { get; }
        public static TextBox[] MaxTWRColumn { get; }

        public static string GetTextBoxText(int i, TextBox[] column)
        {
            return column[i].Text;
        }
        public static void SetTextBoxText(string text, int i, TextBox[] column)
        {
            column[i].Text = text;
        }
        public static void SetColumns(Table RocketTable)
        {
            foreach (TableRow row in RocketTable.Rows)
            {
                if (row.ID == "0" || row.ID == "10")
                {
                    continue;
                }
                foreach (TableCell cell in row.Cells)
                {
                    switch (row.Cells.GetCellIndex(cell))
                    {
                        case 0:
                            continue;
                        case 1:
                            cell.Controls.CopyTo(WetMassColumn, RocketTable.Rows.GetRowIndex(row));
                            continue;
                        case 2:
                            cell.Controls.CopyTo(DryMassColumn, RocketTable.Rows.GetRowIndex(row));
                            continue;
                        case 3:
                            cell.Controls.CopyTo(IspColumn, RocketTable.Rows.GetRowIndex(row));
                            continue;
                        case 4:
                            cell.Controls.CopyTo(DeltaVColumn, RocketTable.Rows.GetRowIndex(row));
                            continue;
                        case 5:
                            cell.Controls.CopyTo(ThrustColumn, RocketTable.Rows.GetRowIndex(row));
                            continue;
                        case 6:
                            cell.Controls.CopyTo(MinTWRColumn, RocketTable.Rows.GetRowIndex(row));
                            continue;
                        case 7:
                            cell.Controls.CopyTo(MaxTWRColumn, RocketTable.Rows.GetRowIndex(row));
                            continue;
                        case 8:
                            continue;
                        default:
                            continue;
                    }
                }
            }
            SetColumnList();
        }
        private static void SetColumnList()
        {
            Columns.Clear();

            Columns.Add(WetMassColumn);
            Columns.Add(DryMassColumn);
            Columns.Add(IspColumn);
            Columns.Add(DeltaVColumn);
            Columns.Add(ThrustColumn);
            Columns.Add(MinTWRColumn);
            Columns.Add(MaxTWRColumn);
        }

        // =========== TableCell Columns ============
        public static List<TableCell[]> TCColumns { get; set; }

        public static TableCell[] TCWetMassColumn { get; set; }
        public static TableCell[] TCDryMassColumn { get; set; }
        public static TableCell[] TCIspColumn { get; set; }
        public static TableCell[] TCDeltaVColumn { get; set; }
        public static TableCell[] TCThrustColumn { get; set; }
        public static TableCell[] TCMinTWRColumn { get; set; }
        public static TableCell[] TCMaxTWRColumn { get; set; }

        public static void SetTCColumns(Table RocketTable)
        {
            TCWetMassColumn = new TableCell[11];
            TCDryMassColumn = new TableCell[11];
            TCIspColumn = new TableCell[11];
            TCDeltaVColumn = new TableCell[11];
            TCThrustColumn = new TableCell[11];
            TCMinTWRColumn = new TableCell[11];
            TCMaxTWRColumn = new TableCell[11];

            foreach (TableRow row in RocketTable.Rows)
            {
                foreach (TableCell cell in row.Cells)
                {
                    switch (row.Cells.GetCellIndex(cell))
                    {
                        case 0:
                            continue;
                        case 1:
                            TCWetMassColumn[RocketTable.Rows.GetRowIndex(row)] = cell;
                            continue;
                        case 2:
                            TCDryMassColumn[RocketTable.Rows.GetRowIndex(row)] = cell;
                            continue;
                        case 3:
                            TCIspColumn[RocketTable.Rows.GetRowIndex(row)] = cell;
                            continue;
                        case 4:
                            TCDeltaVColumn[RocketTable.Rows.GetRowIndex(row)] = cell;
                            continue;
                        case 5:
                            TCThrustColumn[RocketTable.Rows.GetRowIndex(row)] = cell;
                            continue;
                        case 6:
                            TCMinTWRColumn[RocketTable.Rows.GetRowIndex(row)] = cell;
                            continue;
                        case 7:
                            TCMaxTWRColumn[RocketTable.Rows.GetRowIndex(row)] = cell;
                            continue;
                        case 8:
                            continue;
                        default:
                            continue;
                    }
                }
            }
            SetTCColumnList();
        }
        private static void SetTCColumnList()
        {
            TCColumns = new List<TableCell[]>();
            TCColumns.Clear();

            TCColumns.Add(TCWetMassColumn);
            TCColumns.Add(TCDryMassColumn);
            TCColumns.Add(TCIspColumn);
            TCColumns.Add(TCDeltaVColumn);
            TCColumns.Add(TCThrustColumn);
            TCColumns.Add(TCMinTWRColumn);
            TCColumns.Add(TCMaxTWRColumn);
        }
    }
}