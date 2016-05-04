using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RocketryWebApp
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        int stageNumber;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                stageNumber = 0;
            }

            bool isStageNumberInt = int.TryParse(StageNumberTextBox.Text, out stageNumber);

            Response.Write("Welcome to Rocket Planner");

            // Enables Parent Body drop down list only when TWR check box is checked
            if(TWRCheckBox.Checked == true)
            {
                ParentBodyDropDownList.Enabled = true;
            }
            else
            {
                ParentBodyDropDownList.Enabled = false;
            }
        }

        protected void StageNumberTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {

        }

        protected void DeltaVCheckBox_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void TWRCheckBox_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void ParentBodyDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void SetStagesButton_Clicked(object sender, EventArgs e)
        {
            RocketTable.Visible = true;

            for (int i = 0; i < stageNumber; i++)
            {
                RocketTable.Rows.Add(new TableRow());
            }

            foreach (TableRow myStage in RocketTable.Rows)
            {
                if(RocketTable.Rows.GetRowIndex(myStage).ToString() == "0")
                {
                    continue;
                }
                TableCell stageNameCell = new TableCell();
                TableCell deltaVCell = new TableCell();
                TableCell TWRCell = new TableCell();

                myStage.Cells.Add(stageNameCell);
                stageNameCell.Text = "Stage " + RocketTable.Rows.GetRowIndex(myStage).ToString();

                if (DeltaVCheckBox.Checked)
                {
                    myStage.Cells.Add(deltaVCell);
                    deltaVCell.Text = "Δv";
                }

                if (TWRCheckBox.Checked)
                {
                    myStage.Cells.Add(TWRCell);
                    TWRCell.Text = "TWR";
                }
            }
        }
    }
}