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
        public void SetTable()
        {

        }

        public void SetRowVisibility(int stageNumber, Table rocketTable)
        {
            // sets visibility to true for TableRows within StageNumber
            foreach (TableRow row in rocketTable.Rows)
            {
                if (rocketTable.Rows.GetRowIndex(row) == 0)
                {
                    row.Visible = true;
                }
                else { row.Visible = false; }

                for (int i = 0; i < stageNumber + 1; i++)
                {
                    if (rocketTable.Rows.GetRowIndex(row) == i)
                    {
                        row.Visible = true;
                    }
                }
            }
        }
        public void SetColumnVisible(TableCell[] column)
        {
            foreach (TableCell cell in column) { cell.Visible = true; }
        }
        public void SetColumnInvisible(TableCell[] column)
        {
            foreach (TableCell cell in column) { cell.Visible = false; }
        }
        public void SetColumnEnabled(TableCell[] column)
        {
            foreach (TableCell cell in column) { cell.Enabled = true; }
        }
        public void SetColumnDisabled(TableCell[] column)
        {
            foreach (TableCell cell in column) { cell.Enabled = false; }
        }
        public void SetHeaderForeColors(TableCell[] column, System.Drawing.Color color)
        {
            column[0].ForeColor = color;
        }

        public static void ResetStage(int stageNumber)
        {
            List<TextBox[]> columns = new List<TextBox[]>();
            foreach (TextBox[] column in columns)
            {
                SlideDown(stageNumber, column);
            }
        }
        private static void SlideDown(int stageNumber, TextBox[] column)
        {
            for (int i = stageNumber; i < 9; i++)
            {
                TextBox textBox = new TextBox();
                GetTextBox(i, column).Text = GetTextBox(i + 1, column).Text;
            }
            GetTextBox(9, column).Text = "";
        }
        private static TextBox GetTextBox(int i, TextBox[] column)
        {
            StringBuilder varID = new StringBuilder(column[i].ID);
            varID.Remove(0, 2);
            varID.Insert(0, "TextBox");
            return (TextBox)column[i].FindControl(varID.ToString());
        }

        // ============ resetStage Method using TableCells rather than TextBoxes ============
        //public static void resetStage(int stageNumber)
        //{
        //    List<TableCell[]> Columns = new List<TableCell[]>();
        //    foreach (TableCell[] Column in Columns)
        //    {
        //        resetTextBox(stageNumber, Column);
        //    }
        //}
        //private static void resetTextBox(int stageNumber, TableCell[] Column)
        //{
        //    for (int i = stageNumber; i < 9; i++)
        //    {
        //        TextBox textBox = new TextBox();
        //        getTextBox(i, Column).Text = getTextBox(i + 1, Column).Text;
        //    }
        //    getTextBox(9, Column).Text = "";
        //}
        //private static TextBox getTextBox(int i, TableCell[] Column)
        //{
        //    StringBuilder varID = new StringBuilder(Column[i].ID);
        //    varID.Remove(0, 2);
        //    varID.Insert(0, "TextBox");
        //    return (TextBox)Column[i].FindControl(varID.ToString());
        //}
    }
}