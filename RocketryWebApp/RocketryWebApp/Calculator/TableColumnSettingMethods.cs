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
        /// <summary>
        /// Column Setting methods for RocketTable
        /// </summary>
        /// <param name="columnNumber"></param>
        private void setColumnVisibility()
        {
            setDefaultColumnVisibility();

            if (DeltaVCheckBox.Checked)
            {
                setColumnsVisible(new ColumnName[]
                {
                ColumnName.WetMass,
                ColumnName.DryMass,
                ColumnName.DeltaV,
                ColumnName.Isp
                });
                setColumnDisabled(ColumnName.DeltaV);
            }

            if (IspCheckBox.Checked)
            {
                setColumnsVisible(new ColumnName[]
                {
                ColumnName.WetMass,
                ColumnName.DryMass,
                ColumnName.DeltaV,
                ColumnName.Isp
                });
                setColumnDisabled(ColumnName.Isp);
            }

            if (ThrustCheckBox.Checked)
            {
                setColumnVisible(ColumnName.Thrust);
                setColumnDisabled(ColumnName.Thrust);

                if (MinTWRCheckBox.Checked)
                {
                    setColumnsVisible(new ColumnName[]
                    {
                ColumnName.WetMass,
                ColumnName.MinTWR
                    });
                }
                else if (MaxTWRCheckBox.Checked)
                {
                    setColumnsVisible(new ColumnName[]
                    {
                ColumnName.DryMass,
                ColumnName.MaxTWR
                    });
                }
            }

            if (TWRCheckBox.Checked)
            {
                setColumnVisible(ColumnName.Thrust);

                if (MinTWRCheckBox.Checked)
                {
                    setColumnsVisible(new ColumnName[]
                    {
                    ColumnName.WetMass,
                    ColumnName.MinTWR
                    });
                    setColumnDisabled(ColumnName.MinTWR);
                }
                if (MaxTWRCheckBox.Checked)
                {
                    setColumnsVisible(new ColumnName[]
                    {
                    ColumnName.DryMass,
                    ColumnName.MaxTWR
                    });
                    setColumnDisabled(ColumnName.MaxTWR);
                }
            }
        }
        private void setDefaultColumnVisibility()
        {
            setColumnsVisible(new ColumnName[]
            {
                ColumnName.StageName,
                ColumnName.Buttons
            });
            setColumnsInvisible(new ColumnName[]
            {
                ColumnName.WetMass,
                ColumnName.DryMass,
                ColumnName.DeltaV,
                ColumnName.Isp,
                ColumnName.Thrust,
                ColumnName.MinTWR,
                ColumnName.MaxTWR
            });
            setColumnsEnabled();
        }

        private void setColumnVisible(ColumnName columnNumber)
        {
            foreach (TableRow row in RocketTable.Rows)
            {
                foreach (TableCell cell in row.Cells)
                {
                    if (row.Cells.GetCellIndex(cell) == (int)columnNumber)
                    {
                        cell.Visible = true;
                    }
                }
            }
        }
        private void setColumnsVisible()
        {
            foreach (TableRow row in RocketTable.Rows)
            {
                foreach (TableCell cell in row.Cells)
                {
                    cell.Visible = true;
                }
            }
        }
        private void setColumnsVisible(ColumnName[] columns)
        {
            foreach (TableRow row in RocketTable.Rows)
            {
                foreach (TableCell cell in row.Cells)
                {
                    foreach (InputValue column in columns)
                    {
                        if (row.Cells.GetCellIndex(cell) == (int)column)
                        {
                            cell.Visible = true;
                        }
                    }
                }
            }
        }

        private void setColumnInvisible(ColumnName columnNumber)
        {
            foreach (TableRow row in RocketTable.Rows)
            {
                foreach (TableCell cell in row.Cells)
                {
                    if (row.Cells.GetCellIndex(cell) == (int)columnNumber)
                    {
                        cell.Visible = false;
                    }
                }
            }
        }
        private void setColumnsInvisible()
        {
            foreach (TableRow row in RocketTable.Rows)
            {
                foreach (TableCell cell in row.Cells)
                {
                    cell.Visible = false;
                }
            }
        }
        private void setColumnsInvisible(ColumnName[] columns)
        {
            foreach (TableRow row in RocketTable.Rows)
            {
                foreach (TableCell cell in row.Cells)
                {
                    foreach (InputValue column in columns)
                    {
                        if (row.Cells.GetCellIndex(cell) == (int)column)
                        {
                            cell.Visible = false;
                        }
                    }
                }
            }
        }

        private void setColumnEnabled(ColumnName columnNumber)
        {
            foreach (TableRow row in RocketTable.Rows)
            {
                foreach (TableCell cell in row.Cells)
                {
                    if (row.Cells.GetCellIndex(cell) == (int)columnNumber)
                    {
                        cell.Enabled = true;
                    }
                }
            }
        }
        private void setColumnsEnabled()
        {
            foreach (TableRow row in RocketTable.Rows)
            {
                foreach (TableCell cell in row.Cells)
                {
                    cell.Enabled = true;
                }
            }
        }
        private void setColumnsEnabled(ColumnName[] columns)
        {
            foreach (TableRow row in RocketTable.Rows)
            {
                foreach (TableCell cell in row.Cells)
                {
                    foreach (InputValue column in columns)
                    {
                        if (row.Cells.GetCellIndex(cell) == (int)column)
                        {
                            cell.Enabled = true;
                        }
                    }
                }
            }
        }

        private void setColumnDisabled(ColumnName columnNumber)
        {
            foreach (TableRow row in RocketTable.Rows)
            {
                foreach (TableCell cell in row.Cells)
                {
                    if (row.Cells.GetCellIndex(cell) == (int)columnNumber)
                    {
                        cell.Enabled = false;
                    }
                }
            }
        }
        private void setColumnsDisabled()
        {
            foreach (TableRow row in RocketTable.Rows)
            {
                foreach (TableCell cell in row.Cells)
                {
                    cell.Enabled = false;
                }
            }
        }
        private void setColumnsDisabled(ColumnName[] columns)
        {
            foreach (TableRow row in RocketTable.Rows)
            {
                foreach (TableCell cell in row.Cells)
                {
                    foreach (InputValue column in columns)
                    {
                        if (row.Cells.GetCellIndex(cell) == (int)column)
                        {
                            cell.Enabled = false;
                        }
                    }
                }
            }
        }

        private void setHeaderColor(ColumnName columnNumber, System.Drawing.Color color)
        {
            foreach (TableCell cell in HeaderRow.Cells)
            {
                if (HeaderRow.Cells.GetCellIndex(cell) == (int)columnNumber)
                {
                    cell.ForeColor = color;
                }
            }
        }
        private void setHeadersColor(System.Drawing.Color color)
        {
            foreach (TableCell cell in HeaderRow.Cells)
            {
                cell.ForeColor = color;
            }
        }
        private void setHeadersColor(ColumnName[] columns, System.Drawing.Color color)
        {
            foreach (TableCell cell in HeaderRow.Cells)
            {
                foreach (InputValue column in columns)
                {
                    if (HeaderRow.Cells.GetCellIndex(cell) == (int)column)
                    {
                        cell.ForeColor = color;
                    }
                }
            }
        }
    }
}