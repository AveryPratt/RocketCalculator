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
        /// Row Setting methods for RocketTable
        /// </summary>
        /// <param name="stageNumber"></param>
        private void setDefaultRowVisibility()
        {
            foreach (TableRow row in RocketTable.Rows)
            {
                if (RocketTable.Rows.GetRowIndex(row) == 0 || RocketTable.Rows.GetRowIndex(row) == 10)
                {
                    row.Visible = true;
                }
                else
                {
                    row.Visible = false;
                }
            }
        }
        private void setRowVisibility()
        {
            foreach (TableRow row in RocketTable.Rows)
            {
                if (RocketTable.Rows.GetRowIndex(row) == 0 || RocketTable.Rows.GetRowIndex(row) == 10)
                {
                    row.Visible = true;
                }
                else if (RocketTable.Rows.GetRowIndex(row) <= StageNumber)
                {
                    row.Visible = true;
                }
                else
                {
                    row.Visible = false;
                }
            }
        }

        private void raiseStageNumber()
        {
            if (StageNumber < 9)
            {
                StageNumber++;
            }
            StageNumberTextBox.Text = StageNumber.ToString();
        }
        private void lowerStageNumber()
        {
            if (StageNumber > 0)
            {
                StageNumber--;
            }
            StageNumberTextBox.Text = StageNumber.ToString();
        }

        private void cascadeAboveRows(int stageDeleted)
        {
            foreach (TableRow row in RocketTable.Rows)
            {
                if(RocketTable.Rows.GetRowIndex(row) == 0 || RocketTable.Rows.GetRowIndex(row) == 10)
                {
                    continue;
                }
                else if (RocketTable.Rows.GetRowIndex(row) == 9)
                {
                    deleteRow(row);
                }
                else if (RocketTable.Rows.GetRowIndex(row) >= stageDeleted)
                {
                    cascadeRow(row);
                }
                else continue;
            }
            lowerStageNumber();
        }
        private void deleteAboveRows(int stageNumber)
        {
            foreach (TableRow row in RocketTable.Rows)
            {
                if (RocketTable.Rows.GetRowIndex(row) == 0 || RocketTable.Rows.GetRowIndex(row) == 10)
                {
                    continue;
                }
                else if (RocketTable.Rows.GetRowIndex(row) > stageNumber)
                {
                    deleteRow(row);
                }
                else continue;
            }
        }

        private void cascadeRow(TableRow row)
        {
            foreach (TableCell cell in row.Cells)
            {
                if (row.Cells.GetCellIndex(cell) != 0 && row.Cells.GetCellIndex(cell) != 8)
                {
                    setTextBoxText(cell, getNextStageText(cell));
                }
            }
        }
        private void deleteRow(TableRow row)
        {
            foreach (TableCell cell in row.Cells)
            {
                if (row.Cells.GetCellIndex(cell) != 0 && row.Cells.GetCellIndex(cell) != 8)
                {
                    setTextBoxText(cell, "");
                }
            }
        }

        private string getTextBoxText(TableCell cell)
        {
            StringBuilder varID = new StringBuilder(cell.ID);
            varID.Remove(0, 2);
            varID.Insert(0, "TextBox");
            return ((TextBox)cell.FindControl(varID.ToString())).Text;
        }
        private string getNextStageText(TableCell cell)
        {
            StringBuilder varID = new StringBuilder(cell.ID);
            varID.Remove(0, 2);
            int IDNumber = int.Parse(varID.ToString());
            IDNumber += 7;
            return ((TextBox)cell.FindControl("TextBox" + IDNumber.ToString())).Text;
        }
        private void setTextBoxText(TableCell cell, string text)
        {
            StringBuilder varID = new StringBuilder(cell.ID);
            varID.Remove(0, 2);
            varID.Insert(0, "TextBox");
            ((TextBox)cell.FindControl(varID.ToString())).Text = Truncate(text, 6);
        }

        private void setRocketName()
        {
            NameCell.Text = RocketNameTextBox.Text;
        }
        private void setFooterValues()
        {
            FooterWetMass.Text = "*<em>Combine mass from previous stages</em>";
            FooterDryMass.Text = "*<em>Combine mass from previous stages</em>";
            FooterIsp.Text = "";
            FooterDeltaV.Text = "";
            FooterThrust.Text = "";
            FooterMinTWR.Text = "";
            FooterMaxTWR.Text = "";
            Reference.Text = "";
        }
    }
}