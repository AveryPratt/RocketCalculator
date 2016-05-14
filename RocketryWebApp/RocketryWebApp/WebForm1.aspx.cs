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
using System.Text;

namespace RocketryWebApp
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        // determines total number of stages being displayed
        int StageNumber;
        // determines RocketTable visibility (altered in Page_Load() and setTable() methods)
        bool ShowStages;
        #region Columns
        TableCell[] wetMassColumn = new TableCell[10];
        TableCell[] dryMassColumn = new TableCell[10];
        TableCell[] thrustColumn = new TableCell[10];
        TableCell[] ispColumn = new TableCell[10];
        TableCell[] minTWRColumn = new TableCell[10];
        TableCell[] maxTWRColumn = new TableCell[10];
        TableCell[] deltaVColumn = new TableCell[10];
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // hides RocketTable until SetStagesButton is clicked
                ShowStages = false;
                ViewState["ShowStages"] = ShowStages;
            }
            else
            {
                // determines if RocketTable is visible
                ShowStages = (bool)ViewState["ShowStages"];
            }

            // sets StageNumber to TextBox.Text if text is of type int
            // sets StageNumber to 0 and gives error message if TextBox.Text is not of type int
            bool IsStageNumberInt = int.TryParse(StageNumberTextBox.Text, out StageNumber);
            if (!IsStageNumberInt && IsPostBack)
            {
                Response.Write("Unrecognized number of stages.");
                StageNumber = 0;
            }
            // initializes RocketTable visibility to false;
            RocketTable.Visible = false;
        }

        // determines which stage variables in RocketTable are being set by user and which are being calculated
        #region CheckBox Logic

        //  CheckedChanged events all invoke setTable() method and postback
        protected void DeltaVCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            IspCheckBox.Checked = false;
            setTable();
        }
        protected void IspCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            DeltaVCheckBox.Checked = false;
            setTable();
        }
        protected void TWRCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            ThrustCheckBox.Checked = false;
            TWRSettings();
            setTable();
        }
        protected void ThrustCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            TWRCheckBox.Checked = false;
            TWRSettings();
            setTable();
        }
        protected void MinTWRCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            setTable();
        }
        protected void MaxTWRCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            setTable();
        }

        // enables ParentBodyDropDownList and min/max CheckBoxes only when TWRCheckBox is checked
        private void TWRSettings()
        {
            if (TWRCheckBox.Checked || ThrustCheckBox.Checked)
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
        }
        #endregion

        protected void SetStagesButton_Clicked(object sender, EventArgs e)
        {
            ShowStages = true;
            ViewState["ShowStages"] = ShowStages;
            setTable();
        }

        // configures RocketTable visibility and enablement specifications provided by StageNumber (int), ShowStages (bool), and CheckBoxes
        // Each TextBox in RocketTable has its own ID and ViewState.
        private void setTable()
        {
            #region Row Visibility
            foreach (TableRow row in RocketTable.Rows)
            {
                // default visibility
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
            #endregion
            #region Columns

            foreach (TableRow row in RocketTable.Rows)
            {
                // ============== USE SWITCH ==============

                foreach (TableCell cell in row.Cells)
                {
                    if (row.Cells.GetCellIndex(cell) == 0)
                    {
                        continue;
                    }
                    if (row.Cells.GetCellIndex(cell) == 1)
                    {
                        wetMassColumn[RocketTable.Rows.GetRowIndex(row)] = cell;
                    }
                    else if (row.Cells.GetCellIndex(cell) == 2)
                    {
                        dryMassColumn[RocketTable.Rows.GetRowIndex(row)] = cell;
                    }
                    else if (row.Cells.GetCellIndex(cell) == 3)
                    {
                        thrustColumn[RocketTable.Rows.GetRowIndex(row)] = cell;
                    }
                    else if (row.Cells.GetCellIndex(cell) == 4)
                    {
                        ispColumn[RocketTable.Rows.GetRowIndex(row)] = cell;
                    }
                    else if (row.Cells.GetCellIndex(cell) == 5)
                    {
                        minTWRColumn[RocketTable.Rows.GetRowIndex(row)] = cell;
                    }
                    else if (row.Cells.GetCellIndex(cell) == 6)
                    {
                        maxTWRColumn[RocketTable.Rows.GetRowIndex(row)] = cell;
                    }
                    else if (row.Cells.GetCellIndex(cell) == 7)
                    {
                        deltaVColumn[RocketTable.Rows.GetRowIndex(row)] = cell;
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
            }
            #endregion
            #region Column Visibility
            // default visibility
            foreach (TableCell cell in wetMassColumn) { cell.Visible = true; }
            foreach (TableCell cell in dryMassColumn) { cell.Visible = true; }
            foreach (TableCell cell in thrustColumn) { cell.Visible = false; }
            foreach (TableCell cell in ispColumn) { cell.Visible = false; }
            foreach (TableCell cell in minTWRColumn) { cell.Visible = false; }
            foreach (TableCell cell in maxTWRColumn) { cell.Visible = false; }
            foreach (TableCell cell in deltaVColumn) { cell.Visible = false; }

            // TWR & Thrust
            if (TWRCheckBox.Checked || ThrustCheckBox.Checked)
            {
                if (MinTWRCheckBox.Checked)
                {
                    foreach (TableCell cell in minTWRColumn) { cell.Visible = true; }
                }
                if (MaxTWRCheckBox.Checked)
                {
                    foreach (TableCell cell in maxTWRColumn) { cell.Visible = true; }
                }
                foreach (TableCell cell in thrustColumn) { cell.Visible = true; }
            }

            // Isp & DeltaV
            if (IspCheckBox.Checked || DeltaVCheckBox.Checked)
            {
                foreach (TableCell cell in deltaVColumn) { cell.Visible = true; }
                foreach (TableCell cell in ispColumn) { cell.Visible = true; }
            }
            #endregion
            #region Column Enabling
            // default enabling
            foreach (TableCell cell in wetMassColumn) { cell.Enabled = true; }
            foreach (TableCell cell in dryMassColumn) { cell.Enabled = true; }
            foreach (TableCell cell in thrustColumn) { cell.Enabled = true; }
            foreach (TableCell cell in ispColumn) { cell.Enabled = true; }
            foreach (TableCell cell in minTWRColumn) { cell.Enabled = true; }
            foreach (TableCell cell in maxTWRColumn) { cell.Enabled = true; }
            foreach (TableCell cell in deltaVColumn) { cell.Enabled = true; }

            // Thrust
            if (ThrustCheckBox.Checked)
            {
                foreach (TableCell cell in thrustColumn) { cell.Enabled = false; }
                foreach (TableCell cell in minTWRColumn) { cell.Enabled = true; }
                foreach (TableCell cell in maxTWRColumn) { cell.Enabled = true; }
            }
            if (TWRCheckBox.Checked)
            {
                // MinTWR
                if (MinTWRCheckBox.Checked)
                {
                    foreach (TableCell cell in minTWRColumn) { cell.Enabled = false; }
                }
                // MaxTWR
                if (MaxTWRCheckBox.Checked)
                {
                    foreach (TableCell cell in maxTWRColumn) { cell.Enabled = false; }
                }
                foreach (TableCell cell in thrustColumn) { cell.Enabled = true; }
            }
            // Isp & DeltaV
            if (IspCheckBox.Checked)
            {
                foreach (TableCell cell in ispColumn) { cell.Enabled = false; }
            }
            if (DeltaVCheckBox.Checked)
            {
                foreach (TableCell cell in deltaVColumn) { cell.Enabled = false; }
            }
            #endregion
            #region Table Visibility
            // shows {StageNumber} stages when SetStagesButton is clicked
            if (ShowStages)
            {
                RocketTable.Visible = true;
                CalculateButton.Visible = true;
            }
            #endregion
        }

        protected void CalculateButton_Clicked(object sender, EventArgs e)
        {
            setTable();
            // gets a list of Stages by invocing helper method
            List<Stage> stageList = getStages();
            
            // foreach loop below increments by i
            int i = 0;

            // calculates and sets values in TextBoxes according to which CheckBoxes are checked:
            foreach (Stage stage in stageList)
            {
                #region Calculations
                // DeltaV
                if (DeltaVCheckBox.Checked)
                {
                    string deltaVText = stage.GetDeltaV().ToString();
                    setTextBoxText(deltaVText, i, deltaVColumn);
                }

                // Isp
                if (IspCheckBox.Checked)
                {
                    string ispText = stage.GetIsp().ToString();
                    setTextBoxText(ispText, i, ispColumn);
                }

                // Thrust
                if (ThrustCheckBox.Checked)
                {
                    if (MinTWRCheckBox.Checked)
                    {
                        string thrustText = stage.GetThrustFromMinTWR().ToString();
                        setTextBoxText(thrustText, i, thrustColumn);
                    }
                    else if (MaxTWRCheckBox.Checked)
                    {
                        string thrustText = stage.GetThrustFromMaxTWR().ToString();
                        setTextBoxText(thrustText, i, thrustColumn);
                    }
                }

                if (TWRCheckBox.Checked)
                {
                    // Min TWR
                    if (MinTWRCheckBox.Checked)
                    {
                        string minTWRText = stage.GetMinTWR().ToString();
                        setTextBoxText(minTWRText, i, minTWRColumn);
                    }
                    // Max TWR
                    if (MaxTWRCheckBox.Checked)
                    {
                        string maxTWRText = stage.GetMaxTWR().ToString();
                        setTextBoxText(maxTWRText, i, maxTWRColumn);
                    }
                }
                #endregion
                i++;
            }
        }

        // gets a List of Stages in which Stage Properties are given by RocketTable text
        private List<Stage> getStages()
        {
            // creates stageList for {StageNumber} Stages
            List<Stage> stageList = new List<Stage>();
            for (int i = 0; i < StageNumber; i++)
            {
                double wetMass;
                double dryMass;
                double thrust;
                int isp;
                double minTWR;
                double maxTWR;
                double deltaV;

                // finds appropriate TextBox.Text and TryParses Text into fields
                bool IsWetMassDouble = double.TryParse(getTextBoxText(i, wetMassColumn), out wetMass);
                bool IsDryMassDouble = double.TryParse(getTextBoxText(i, dryMassColumn), out dryMass);
                bool IsThrustDouble = double.TryParse(getTextBoxText(i, thrustColumn), out thrust);
                bool IsIspDouble = int.TryParse(getTextBoxText(i, ispColumn), out isp);
                bool IsMinTWRDouble = double.TryParse(getTextBoxText(i, minTWRColumn), out minTWR);
                bool IsMaxTWRDouble = double.TryParse(getTextBoxText(i, maxTWRColumn), out maxTWR);
                bool IsDeltaVDouble = double.TryParse(getTextBoxText(i, deltaVColumn), out deltaV);

                Stage stage = new Stage("Stage" + (i + 1).ToString())
                {
                    WetMass = wetMass,
                    DryMass = dryMass,
                    Thrust = thrust,
                    Isp = isp,
                    MinTWR = minTWR,
                    MaxTWR = maxTWR,
                    DeltaV = deltaV
                };
                stageList.Add(stage);
            }
            return stageList;
        }

        // finds the text associated with a certain stage and property, represented by 'i' and 'tbarray' respectively
        private string getTextBoxText(int i, TableCell[] tbarray)
        {
            StringBuilder varID = new StringBuilder(tbarray[(i + 1)].ID);
            varID.Remove(0, 2);
            varID.Insert(0, "TextBox");
            return ((TextBox)tbarray[(i + 1)].FindControl(varID.ToString())).Text;
        }

        // gives calculated response to appropriate TextBox for output
        private void setTextBoxText(string text, int i, TableCell[] tbarray)
        {
            StringBuilder varID = new StringBuilder(tbarray[(i + 1)].ID);
            varID.Remove(0, 2);
            varID.Insert(0, "TextBox");
            ((TextBox)FindControl(varID.ToString())).Text = text;
        }
    }
}