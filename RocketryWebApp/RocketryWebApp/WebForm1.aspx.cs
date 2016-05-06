using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KSP_Library;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace RocketryWebApp
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        int StageNumber;
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write("Welcome to Rocket Planner");

            // initializes StageNumber to 0 or retrieves from ViewState
            if (!IsPostBack)
            {
                StageNumber = 0;
            }
            else
            {
                StageNumber = (int)ViewState["Rows"];
            }

            // creates a new TableRow in RocketTable for each stage number
            for (int i = 0; i < StageNumber + 1; i++)
            {
                RocketTable.Rows.Add(new TableRow());
            }

            foreach (TableRow myStage in RocketTable.Rows)
            {
                // creates TableCells for stage properties
                TableCell stageNameCell, wetMassCell, dryMassCell, thrustCell, ispCell, minTWRCell, maxTWRCell, deltaVCell;
                stageNameCell = new TableCell();
                wetMassCell = new TableCell();
                dryMassCell = new TableCell();
                thrustCell = new TableCell();
                ispCell = new TableCell();
                minTWRCell = new TableCell();
                maxTWRCell = new TableCell();
                deltaVCell = new TableCell();

                // adds TableCells to the current TableRow
                myStage.Cells.Add(stageNameCell);
                myStage.Cells.Add(wetMassCell);
                myStage.Cells.Add(dryMassCell);
                myStage.Cells.Add(thrustCell);
                myStage.Cells.Add(ispCell);
                myStage.Cells.Add(minTWRCell);
                myStage.Cells.Add(maxTWRCell);
                myStage.Cells.Add(deltaVCell);

                // hides TableCells by default
                wetMassCell.Visible = false;
                dryMassCell.Visible = false;
                thrustCell.Visible = false;
                ispCell.Visible = false;
                minTWRCell.Visible = false;
                maxTWRCell.Visible = false;
                deltaVCell.Visible = false;

                // hard codes the header row TableCells
                if (RocketTable.Rows.GetRowIndex(myStage).ToString() == "0")
                {
                    stageNameCell.Text = "Rocket Stages:";
                    wetMassCell.Text = "Wet Mass:";
                    dryMassCell.Text = "Dry Mass:";
                    thrustCell.Text = "Thrust:";
                    ispCell.Text = "Isp:";
                    minTWRCell.Text = "Min. TWR:";
                    maxTWRCell.Text = "Max. TWR:";
                    deltaVCell.Text = "Δv";
                }
                else
                {
                    // creates TextBoxes to populate TableCells
                    TextBox wetMassTextBox, dryMassTextBox, thrustTextBox, ispTextBox, minTWRTextBox, maxTWRTextBox, deltaVTextBox;
                    wetMassTextBox = new TextBox();
                    dryMassTextBox = new TextBox();
                    thrustTextBox = new TextBox();
                    ispTextBox = new TextBox();
                    minTWRTextBox = new TextBox();
                    maxTWRTextBox = new TextBox();
                    deltaVTextBox = new TextBox();

                    // populates TableCells with TextBoxes
                    stageNameCell.Text = "Stage " + RocketTable.Rows.GetRowIndex(myStage).ToString() + ":";
                    wetMassCell.Controls.Add(wetMassTextBox);
                    dryMassCell.Controls.Add(dryMassTextBox);
                    thrustCell.Controls.Add(thrustTextBox);
                    ispCell.Controls.Add(ispTextBox);
                    minTWRCell.Controls.Add(minTWRTextBox);
                    maxTWRCell.Controls.Add(maxTWRTextBox);
                    deltaVCell.Controls.Add(deltaVTextBox);

                    // creates ViewStates for each TableCell's contents
                    ViewState["thrustViewState" + RocketTable.Rows.GetRowIndex(myStage).ToString()] = thrustTextBox.Text;
                    ViewState["dryMassViewState" + RocketTable.Rows.GetRowIndex(myStage).ToString()] = dryMassTextBox.Text;
                    ViewState["wetMassViewState" + RocketTable.Rows.GetRowIndex(myStage).ToString()] = wetMassTextBox.Text;
                    ViewState["ispViewState" + RocketTable.Rows.GetRowIndex(myStage).ToString()] = ispTextBox.Text;
                    ViewState["minTWRViewState" + RocketTable.Rows.GetRowIndex(myStage).ToString()] = maxTWRTextBox.Text;
                    ViewState["maxTWRViewState" + RocketTable.Rows.GetRowIndex(myStage).ToString()] = minTWRTextBox.Text;
                    ViewState["deltaVViewState" + RocketTable.Rows.GetRowIndex(myStage).ToString()] = deltaVTextBox.Text;
                }

                // enables ParentBodyDropDownList and min/max CheckBoxes only when TWRCheckBox is checked

                if (TWRCheckBox.Checked == true)
                {
                    ParentBodyDropDownList.Enabled = true;
                    MinTWRCheckBox.Enabled = true;
                    MaxTWRCheckBox.Enabled = true;
                }
                else
                {
                    ParentBodyDropDownList.Enabled = false;
                    MinTWRCheckBox.Enabled = false;
                    MaxTWRCheckBox.Enabled = false;
                }

                // makes TableCells visible according to which CheckBoxes are checked
                if (DeltaVCheckBox.Checked || 
                    (MinTWRCheckBox.Checked && 
                    MinTWRCheckBox.Enabled == true) || 
                    IspCheckBox.Checked || 
                    ThrustCheckBox.Checked)
                {
                    wetMassCell.Visible = true;
                }
                if (DeltaVCheckBox.Checked ||
                    (MaxTWRCheckBox.Checked && 
                    MaxTWRCheckBox.Enabled == true) || 
                    IspCheckBox.Checked || 
                    ThrustCheckBox.Checked)
                {
                    dryMassCell.Visible = true;
                }
                if (DeltaVCheckBox.Checked || 
                    IspCheckBox.Checked)
                {
                    ispCell.Visible = true;
                }
                if ((MinTWRCheckBox.Checked && 
                    MinTWRCheckBox.Enabled == true) || 
                    ThrustCheckBox.Checked)
                {
                    minTWRCell.Visible = true;
                }
                if ((MaxTWRCheckBox.Checked && 
                    MaxTWRCheckBox.Enabled == true) || 
                    ThrustCheckBox.Checked)
                {
                    maxTWRCell.Visible = true;
                }
                if (DeltaVCheckBox.Checked || 
                    IspCheckBox.Checked)
                {
                    deltaVCell.Visible = true;
                }
            }
            // end "foreach(TableRow)" segment

            // shows CalculateButton if stages are present
            if (StageNumber > 0)
            {
                CalculateButton.Visible = true;
            }
            // saves stageNumber as ViewState
            ViewState["Rows"] = StageNumber;
        }

        protected void SetStagesButton_Clicked(object sender, EventArgs e)
        {
            bool isStageNumberInt = Int32.TryParse(StageNumberTextBox.Text, out StageNumber);

            // saves stageNumber as ViewState
            ViewState["Rows"] = StageNumber;
        }






        protected void CalculateButton_Clicked(object sender, EventArgs e)
        {
            //// checks to see if stage number is an integer
            //bool isStageNumberInt = int.TryParse(StageNumberTextBox.Text, out stageNumber);
            //while (isStageNumberInt == false)
            //{
            //    Response.Write("You must enter an integer as the number of stages.");
            //}

            //stageNumber = (int)ViewState["Rows"];
            //// recreates table rows
            //for (int i = 0; i < stageNumber + 1; i++)
            //{
            //    RocketTable.Rows.Add(new TableRow());
            //}

            //foreach (TableRow myStage in RocketTable.Rows)
            //{
            //    // recreates table cells
            //    TableCell stageNameCell, wetMassCell, dryMassCell, thrustCell, ispCell;
            //    stageNameCell = new TableCell();
            //    wetMassCell = new TableCell();
            //    dryMassCell = new TableCell();
            //    thrustCell = new TableCell();
            //    ispCell = new TableCell();

            //    // adds cells to the current TableRow
            //    myStage.Cells.Add(stageNameCell);
            //    myStage.Cells.Add(wetMassCell);
            //    myStage.Cells.Add(dryMassCell);
            //    myStage.Cells.Add(thrustCell);
            //    myStage.Cells.Add(ispCell);

            //    // hides thrust and isp cells
            //    thrustCell.Visible = false;
            //    ispCell.Visible = false;

            //    // sets the header row
            //    if (RocketTable.Rows.GetRowIndex(myStage).ToString() == "0")
            //    {
            //        stageNameCell.Text = "Rocket Stages:";
            //        wetMassCell.Text = "Wet Mass:";
            //        dryMassCell.Text = "Dry Mass:";
            //        thrustCell.Text = "Thrust:";
            //        ispCell.Text = "Isp:";
            //    }
            //    else
            //    {
            //        stageNameCell.Text = "Stage " + RocketTable.Rows.GetRowIndex(myStage).ToString() + ":";
            //        wetMassCell.Text = (string)ViewState["wetMassViewState" + RocketTable.Rows.GetRowIndex(myStage).ToString()];
            //        dryMassCell.Text = (string)ViewState["dryMassViewState" + RocketTable.Rows.GetRowIndex(myStage).ToString()];
            //        thrustCell.Text = (string)ViewState["thrustViewState" + RocketTable.Rows.GetRowIndex(myStage).ToString()];
            //        ispCell.Text = (string)ViewState["ispViewState" + RocketTable.Rows.GetRowIndex(myStage).ToString()];
            //    }

            //    if (TWRCheckBox.Checked)
            //    {
            //        // shows thrust cell if TWRCheckBox is checked
            //        thrustCell.Visible = true;

            //        // creates TWR cell if TWRCheckBox is checked
            //        TableCell TWRCell = new TableCell();

            //        // adds cell to TableRow
            //        myStage.Cells.Add(TWRCell);

            //        // sets the header row
            //        if (RocketTable.Rows.GetRowIndex(myStage).ToString() == "0")
            //        {
            //            TWRCell.Text = "TWR:";
            //        }
            //        else
            //        {
            //            TWRCell.Text = "twr";
            //        }
            //    }

            //    if (DeltaVCheckBox.Checked)
            //    {
            //        // shows thrust cell if TWRCheckBox is checked
            //        ispCell.Visible = true;

            //        // creates TWR cell if TWRCheckBox is checked
            //        TableCell DeltaVCell = new TableCell();

            //        // adds cell to TableRow
            //        myStage.Cells.Add(DeltaVCell);

            //        // sets the header row
            //        if (RocketTable.Rows.GetRowIndex(myStage).ToString() == "0")
            //        {
            //            DeltaVCell.Text = "Δv:";
            //        }
            //        else
            //        {
            //            DeltaVCell.Text = "deltaV";
            //        }
            //    }
            //}




            // ALSO VERY BAD IDEA ======== VERY VERY VERY BAD IDEA

            //foreach (TableRow myStage in RocketTable.Rows)
            //{
            //    if (TWRCheckBox.Checked)
            //    {
            //        // creates TWR cell if TWRCheckBox is checked
            //        TableCell TWRCell = new TableCell();

            //        // adds cell to TableRow
            //        myStage.Cells.Add(TWRCell);

            //        // sets the header row
            //        if (RocketTable.Rows.GetRowIndex(myStage).ToString() == "0")
            //        {
            //            TWRCell.Text = "TWR:";
            //        }
            //        else
            //        {
            //            TWRCell.Text = "twr";
            //        }
            //    }

            //    if (DeltaVCheckBox.Checked)
            //    {
            //        // creates TWR cell if TWRCheckBox is checked
            //        TableCell DeltaVCell = new TableCell();

            //        // adds cell to TableRow
            //        myStage.Cells.Add(DeltaVCell);

            //        // sets the header row
            //        if (RocketTable.Rows.GetRowIndex(myStage).ToString() == "0")
            //        {
            //            DeltaVCell.Text = "Δv:";
            //        }
            //        else
            //        {
            //            DeltaVCell.Text = "deltaV";
            //        }
            //    }
            //}
        }
    }
}