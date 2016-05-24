using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;

namespace RocketryWebApp.WebForm1
{
    public class RocketTable
    {
        // Columns
        TableCell[] wetMassColumn = new TableCell[10];
        TableCell[] dryMassColumn = new TableCell[10];
        TableCell[] thrustColumn = new TableCell[10];
        TableCell[] ispColumn = new TableCell[10];
        TableCell[] minTWRColumn = new TableCell[10];
        TableCell[] maxTWRColumn = new TableCell[10];
        TableCell[] deltaVColumn = new TableCell[10];

        private void setColumns(Table RocketTable)
        {
            // populates Columns with TableCells from each TableRow
            foreach (TableRow row in RocketTable.Rows)
            {
                foreach (TableCell cell in row.Cells)
                {
                    switch (row.Cells.GetCellIndex(cell))
                    {
                        case 1:
                            wetMassColumn[RocketTable.Rows.GetRowIndex(row)] = cell;
                            break;
                        case 2:
                            dryMassColumn[RocketTable.Rows.GetRowIndex(row)] = cell;
                            break;
                        case 3:
                            ispColumn[RocketTable.Rows.GetRowIndex(row)] = cell;
                            break;
                        case 4:
                            deltaVColumn[RocketTable.Rows.GetRowIndex(row)] = cell;
                            break;
                        case 5:
                            thrustColumn[RocketTable.Rows.GetRowIndex(row)] = cell;
                            break;
                        case 6:
                            minTWRColumn[RocketTable.Rows.GetRowIndex(row)] = cell;
                            break;
                        case 7:
                            maxTWRColumn[RocketTable.Rows.GetRowIndex(row)] = cell;
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }

    public class TableSetupMethods
    {
        public void SetRowVisibility(int StageNumber, Table RocketTable)
        {
            // sets visibility to true for TableRows within StageNumber
            foreach (TableRow row in RocketTable.Rows)
            {
                if (RocketTable.Rows.GetRowIndex(row) == 0)
                {
                    row.Visible = true;
                }
                else { row.Visible = false; }

                for (int i = 0; i < StageNumber + 1; i++)
                {
                    if (RocketTable.Rows.GetRowIndex(row) == i)
                    {
                        row.Visible = true;
                    }
                }
            }
        }
        public void SetDefaultColumVisibility(TableCell[] Column, bool visibility = false)
        {
            foreach (TableCell cell in Column) { cell.Visible = true; }
        }
        public void SetColumnEnabling(TableCell[] Column, bool enabling = false)
        {
            foreach (TableCell cell in Column) { cell.Enabled = true; }
        }
        public void SetHeaderForeColors(TableCell[] Column, System.Drawing.Color color)
        {
            Column[0].ForeColor = color;
        }
    }
}