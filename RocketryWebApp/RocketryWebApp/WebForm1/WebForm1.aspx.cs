using KSP_Library;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;

namespace RocketryWebApp.WebForm1
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        // determines total number of stages being displayed
        int StageNumber;
        // determines RocketTable visibility (altered in Page_Load() and setTable() methods)
        bool ShowStages;

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
                //Response.Write("Unrecognized number of stages.");
                StageNumber = 0;
            }
            // initializes RocketTable visibility to false;
            RocketTable.Visible = false;
            AddStageButton.Visible = false;
            CalculateButton.Visible = false;
        }

        //// configures RocketTable visibility and enablement specifications provided by StageNumber (int), ShowStages (bool), and CheckBoxes
        //// Each TextBox in RocketTable has its own ID and ViewState.
        //private void setTable()
        //{
        //    #region Row Visibility
        //    // sets visibility to true for TableRows within StageNumber
        //    foreach (TableRow row in RocketTable.Rows)
        //    {
        //        if (RocketTable.Rows.GetRowIndex(row) == 0)
        //        {
        //            row.Visible = true;
        //        }
        //        else { row.Visible = false; }

        //        for (int i = 0; i < StageNumber + 1; i++)
        //        {
        //            if (RocketTable.Rows.GetRowIndex(row) == i)
        //            {
        //                row.Visible = true;
        //            }
        //        }
        //    }
        //    #endregion
        //    #region Column Visibility
        //    // default visibility
        //    foreach (TableCell cell in wetMassColumn) { cell.Visible = false; }
        //    foreach (TableCell cell in dryMassColumn) { cell.Visible = false; }
        //    foreach (TableCell cell in thrustColumn) { cell.Visible = false; }
        //    foreach (TableCell cell in ispColumn) { cell.Visible = false; }
        //    foreach (TableCell cell in minTWRColumn) { cell.Visible = false; }
        //    foreach (TableCell cell in maxTWRColumn) { cell.Visible = false; }
        //    foreach (TableCell cell in deltaVColumn) { cell.Visible = false; }

        //    // Thrust
        //    if (ThrustCheckBox.Checked)
        //    {
        //        if (MinTWRCheckBox.Checked)
        //        {
        //            foreach (TableCell cell in wetMassColumn) { cell.Visible = true; }
        //            foreach (TableCell cell in minTWRColumn) { cell.Visible = true; }
        //            foreach (TableCell cell in thrustColumn) { cell.Visible = true; }
        //        }
        //        if (MaxTWRCheckBox.Checked)
        //        {
        //            foreach (TableCell cell in dryMassColumn) { cell.Visible = true; }
        //            foreach (TableCell cell in maxTWRColumn) { cell.Visible = true; }
        //            foreach (TableCell cell in thrustColumn) { cell.Visible = true; }
        //        }
        //    }

        //    // TWR
        //    if (TWRCheckBox.Checked)
        //    {
        //        if (MinTWRCheckBox.Checked)
        //        {
        //            foreach (TableCell cell in wetMassColumn) { cell.Visible = true; }
        //            foreach (TableCell cell in minTWRColumn) { cell.Visible = true; }
        //        }
        //        if (MaxTWRCheckBox.Checked)
        //        {
        //            foreach (TableCell cell in dryMassColumn) { cell.Visible = true; }
        //            foreach (TableCell cell in maxTWRColumn) { cell.Visible = true; }
        //        }
        //        foreach (TableCell cell in thrustColumn) { cell.Visible = true; }
        //    }

        //    // Isp & DeltaV
        //    if (IspCheckBox.Checked || DeltaVCheckBox.Checked)
        //    {
        //        foreach (TableCell cell in wetMassColumn) { cell.Visible = true; }
        //        foreach (TableCell cell in dryMassColumn) { cell.Visible = true; }
        //        foreach (TableCell cell in deltaVColumn) { cell.Visible = true; }
        //        foreach (TableCell cell in ispColumn) { cell.Visible = true; }
        //    }
        //    #endregion
        //    #region Column Enabling
        //    // default enabling
        //    foreach (TableCell cell in wetMassColumn) { cell.Enabled = true; }
        //    foreach (TableCell cell in dryMassColumn) { cell.Enabled = true; }
        //    foreach (TableCell cell in thrustColumn) { cell.Enabled = true; }
        //    foreach (TableCell cell in ispColumn) { cell.Enabled = true; }
        //    foreach (TableCell cell in minTWRColumn) { cell.Enabled = true; }
        //    foreach (TableCell cell in maxTWRColumn) { cell.Enabled = true; }
        //    foreach (TableCell cell in deltaVColumn) { cell.Enabled = true; }

        //    // Thrust
        //    if (ThrustCheckBox.Checked)
        //    {
        //        foreach (TableCell cell in thrustColumn) { cell.Enabled = false; }
        //    }
        //    if (TWRCheckBox.Checked)
        //    {
        //        // MinTWR
        //        if (MinTWRCheckBox.Checked)
        //        {
        //            foreach (TableCell cell in minTWRColumn) { cell.Enabled = false; }
        //        }
        //        // MaxTWR
        //        if (MaxTWRCheckBox.Checked)
        //        {
        //            foreach (TableCell cell in maxTWRColumn) { cell.Enabled = false; }
        //        }
        //    }
        //    // Isp & DeltaV
        //    if (IspCheckBox.Checked)
        //    {
        //        foreach (TableCell cell in ispColumn) { cell.Enabled = false; }
        //    }
        //    if (DeltaVCheckBox.Checked)
        //    {
        //        foreach (TableCell cell in deltaVColumn) { cell.Enabled = false; }
        //    }
        //    #endregion
        //    #region Header Colors (default)
        //    // default colors are changed in CalculateButton_Clicked Event
        //    wetMassColumn[0].ForeColor = System.Drawing.Color.Black;
        //    dryMassColumn[0].ForeColor = System.Drawing.Color.Black;
        //    thrustColumn[0].ForeColor = System.Drawing.Color.Black;
        //    ispColumn[0].ForeColor = System.Drawing.Color.Black;
        //    minTWRColumn[0].ForeColor = System.Drawing.Color.Black;
        //    maxTWRColumn[0].ForeColor = System.Drawing.Color.Black;
        //    deltaVColumn[0].ForeColor = System.Drawing.Color.Black;
        //    #endregion
        //    #region Table Visibility
        //    // shows {StageNumber} stages when SetStagesButton is clicked
        //    if (ShowStages)
        //    {
        //        StageNumberTextBox.Text = StageNumber.ToString();
        //        RocketTable.Visible = true;
        //        if (StageNumber < 9)
        //        {
        //            AddStageButton.Visible = true;
        //        }
        //        CalculateButton.Visible = true;
        //    }
        //    #endregion
        //}

        #region CheckBox Logic

        //  CheckedChanged events all invoke setTable() method and postback
        protected void DeltaVCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            IspCheckBox.Checked = false;
            //setTable();
        }
        protected void IspCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            DeltaVCheckBox.Checked = false;
            //setTable();
        }
        protected void TWRCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            ThrustCheckBox.Checked = false;
            TWRSettings();
            //setTable();
        }
        protected void ThrustCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            TWRCheckBox.Checked = false;
            TWRSettings();
            //setTable();
        }
        protected void MinTWRCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            //setTable();
        }
        protected void MaxTWRCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            //setTable();
        }
        protected void ParentBodyDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //setTable();
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

        #region Stage Buttons
        protected void SetStagesButton_Clicked(object sender, EventArgs e)
        {
            ShowStages = true;
            ViewState["ShowStages"] = ShowStages;
            //setTable();
        }

        protected void AddStageButton_Clicked(object sender, EventArgs e)
        {
            ShowStages = true;
            ViewState["ShowStages"] = ShowStages;
            StageNumber++;
            StageNumberTextBox.Text = StageNumber.ToString();
            //setTable();
        }

        protected void Button1_Clicked(object sender, EventArgs e)
        {
            resetStage(1);
            StageNumber--;
            StageNumberTextBox.Text = StageNumber.ToString();
            //setTable();
        }
        protected void Button2_Clicked(object sender, EventArgs e)
        {
            resetStage(2);
            StageNumber--;
            StageNumberTextBox.Text = StageNumber.ToString();
            //setTable();
        }
        protected void Button3_Clicked(object sender, EventArgs e)
        {
            resetStage(3);
            StageNumber--;
            StageNumberTextBox.Text = StageNumber.ToString();
            //setTable();
        }
        protected void Button4_Clicked(object sender, EventArgs e)
        {
            resetStage(4);
            StageNumber--;
            StageNumberTextBox.Text = StageNumber.ToString();
            //setTable();
        }
        protected void Button5_Clicked(object sender, EventArgs e)
        {
            resetStage(5);
            StageNumber--;
            StageNumberTextBox.Text = StageNumber.ToString();
            //setTable();
        }
        protected void Button6_Clicked(object sender, EventArgs e)
        {
            resetStage(6);
            StageNumber--;
            StageNumberTextBox.Text = StageNumber.ToString();
            //setTable();
        }
        protected void Button7_Clicked(object sender, EventArgs e)
        {
            resetStage(7);
            StageNumber--;
            StageNumberTextBox.Text = StageNumber.ToString();
            //setTable();
        }
        protected void Button8_Clicked(object sender, EventArgs e)
        {
            resetStage(8);
            StageNumber--;
            StageNumberTextBox.Text = StageNumber.ToString();
            //setTable();
        }
        protected void Button9_Clicked(object sender, EventArgs e)
        {
            resetStage(9);
            StageNumber--;
            StageNumberTextBox.Text = StageNumber.ToString();
            //setTable();
        }

        // helper methods
        private void resetStage(int stageNumber)
        {
            List<TableCell[]> Columns = new List<TableCell[]>();
            foreach (TableCell[] Column in Columns)
            {
                resetTextBox(stageNumber, Column);
            }
        }
        private void resetTextBox(int stageNumber, TableCell[] Column)
        {
            for (int i = stageNumber; i < 9; i++)
            {
                getTextBox(i, Column).Text = getTextBox(i + 1, Column).Text;
            }
            getTextBox(9, Column).Text = "";
        }
        private TextBox getTextBox(int i, TableCell[] Column)
        {
            StringBuilder varID = new StringBuilder(Column[i].ID);
            varID.Remove(0, 2);
            varID.Insert(0, "TextBox");
            return (TextBox)Column[i].FindControl(varID.ToString());
        }
        #endregion

        #region Calculations
        protected void CalculateButton_Clicked(object sender, EventArgs e)
        {
            // creates celestial body from ParentBodyDropDownList for TWR and Thrust calculations
            Body myBody;
            myBody = KerbolSystem.GetSystemBody(ParentBodyDropDownList.SelectedItem.Text);

            //setTable();
            // gets a list of Stages by invocing helper method
            List<Stage> stageList = getStages();

            bool DeltaVCalculated = true;
            bool IspCalculated = true;
            bool ThrustCalculated = true;
            bool MinTWRCalculated = true;
            bool MaxTWRCalculated = true;

            // foreach loop below increments by i
            int i = 0;
            // calculates and sets values in TextBoxes according to which CheckBoxes are checked:
            foreach (Stage stage in stageList)
            {
                bool deltaVCalculated = true;
                bool ispCalculated = true;
                bool thrustCalculated = true;
                bool minTWRCalculated = true;
                bool maxTWRCalculated = true;
                if (DeltaVCheckBox.Checked) { DeltaVCalculation(stage, i, out deltaVCalculated); }
                if (IspCheckBox.Checked) { IspCalculation(stage, i, out ispCalculated); }
                if (ThrustCheckBox.Checked) { ThrustCalculation(stage, myBody, i, out thrustCalculated); }
                if (TWRCheckBox.Checked) { TWRCalculation(stage, myBody, i, out minTWRCalculated, out maxTWRCalculated); }

                if (deltaVCalculated == false) { DeltaVCalculated = false; }
                if (ispCalculated == false) { IspCalculated = false; }
                if (thrustCalculated == false) { ThrustCalculated = false; }
                if (minTWRCalculated == false) { MinTWRCalculated = false; }
                if (maxTWRCalculated == false) { MaxTWRCalculated = false; }
                i++;
            }
            SetHeaderColors(DeltaVCalculated, IspCalculated, ThrustCalculated, MinTWRCalculated, MaxTWRCalculated);
        }

        // calculation methods
        private void DeltaVCalculation(Stage stage, int i, out bool calculated)
        {
            while (true)
            {
                // error handling
                string error;
                if (DeltaVError(stage, out error))
                {
                    Response.Write(error);
                    calculated = false;
                    break;
                }
                // calculation
                string deltaVText = stage.GetDeltaV().ToString();
                setTextBoxText(deltaVText, i, deltaVColumn);
                calculated = true;
                break;
            }
        }
        private void IspCalculation(Stage stage, int i, out bool calculated)
        {
            while (true)
            {
                // error handling
                string error;
                if (IspError(stage, out error))
                {
                    Response.Write(error);
                    calculated = false;
                    break;
                }
                // calculation
                string ispText = stage.GetIsp().ToString();
                setTextBoxText(ispText, i, ispColumn);
                calculated = true;
                break;
            }
        }
        private void ThrustCalculation(Stage stage, Body myBody, int i, out bool calculated)
        {
            while (true)
            {
                // error handling
                string error;
                if (ThrustError(stage, out error))
                {
                    Response.Write(error);
                    calculated = false;
                    break;
                }
                // calculation
                else if (MinTWRCheckBox.Checked && MaxTWRCheckBox.Checked)
                {
                    // checks for wetMass/minTWR properties first
                    if (stage.WetMass == 0 || stage.MinTWR == 0)
                    {
                        string thrustText = stage.GetThrustFromMaxTWR(myBody.GM, myBody.Radius).ToString();
                        setTextBoxText(thrustText, i, thrustColumn);
                    }
                    // then checks for dryMass/maxTWR properties
                    else
                    {
                        string thrustText = stage.GetThrustFromMinTWR(myBody.GM, myBody.Radius).ToString();
                        setTextBoxText(thrustText, i, thrustColumn);
                    }
                }
                else if (MinTWRCheckBox.Checked)
                {
                    string thrustText = stage.GetThrustFromMinTWR(myBody.GM, myBody.Radius).ToString();
                    setTextBoxText(thrustText, i, thrustColumn);
                }
                else if (MaxTWRCheckBox.Checked)
                {
                    string thrustText = stage.GetThrustFromMaxTWR(myBody.GM, myBody.Radius).ToString();
                    setTextBoxText(thrustText, i, thrustColumn);
                }
                calculated = true;
                break;
            }
        }
        private void TWRCalculation(Stage stage, Body myBody, int i, out bool minCalculated, out bool maxCalculated)
        {
            while (true)
            {
                // error handling
                string error;
                bool? minOrMax;
                if (TWRError(stage, out error, out minOrMax))
                {
                    Response.Write(error);
                    if (minOrMax == null)
                    {
                        minCalculated = false;
                        maxCalculated = false;
                    }
                    else if (minOrMax == true)
                    {
                        minCalculated = false;
                        maxCalculated = true;
                    }
                    else
                    {
                        maxCalculated = false;
                        minCalculated = true;
                    }
                    break;
                }
                // calculation
                // Min
                if (MinTWRCheckBox.Checked)
                {
                    string minTWRText = stage.GetMinTWR(myBody.GM, myBody.Radius).ToString();
                    setTextBoxText(minTWRText, i, minTWRColumn);
                }
                // Max
                if (MaxTWRCheckBox.Checked)
                {
                    string maxTWRText = stage.GetMaxTWR(myBody.GM, myBody.Radius).ToString();
                    setTextBoxText(maxTWRText, i, maxTWRColumn);
                }
                minCalculated = true;
                maxCalculated = true;
                break;
            }
        }

        // error handling methods
        private bool DeltaVError(Stage stage, out string errorMessage)
        {
            if (stage.DryMass > stage.WetMass)
            {
                errorMessage = "Wet mass must be greater than dry mass.";
                return true;
            }
            else if (stage.WetMass <= 0 || stage.DryMass <= 0 || stage.Isp <= 0)
            {
                errorMessage = "Wet mass, dry mass, and isp must each be represented by a positive integer.";
                return true;
            }
            else
            {
                errorMessage = "";
                return false;
            }
        }
        private bool IspError(Stage stage, out string errorMessage)
        {
            if (stage.DryMass >= stage.WetMass)
            {
                errorMessage = "Wet mass must be greater than dry mass.";
                return true;
            }
            else if (stage.WetMass <= 0 || stage.DryMass <= 0 || stage.Isp <= 0)
            {
                errorMessage = "Wet mass and dry mass must each be represented by a positive integer.";
                return true;
            }
            else
            {
                errorMessage = "";
                return false;
            }
        }
        private bool ThrustError(Stage stage, out string errorMessage)
        {
            if (MinTWRCheckBox.Checked && MaxTWRCheckBox.Checked)
            {
                if ((stage.WetMass <= 0 || stage.MinTWR <= 0) && (stage.DryMass <= 0 || stage.MaxTWR <= 0))
                {
                    errorMessage = "Either wet mass or dry mass, and thrust, must be represented by a positive number greater than 0.";
                    return true;
                }
                else
                {
                    errorMessage = "";
                    return false;
                }
            }
            else if (MinTWRCheckBox.Checked)
            {
                if (stage.MinTWR <= 0 || stage.WetMass <= 0)
                {
                    errorMessage = "Wet mass and thrust must each be represented by a positive number greater than 0.";
                    return true;
                }
                else
                {
                    errorMessage = "";
                    return false;
                }
            }
            else if (MaxTWRCheckBox.Checked)
            {
                if (stage.MaxTWR <= 0 || stage.DryMass <= 0)
                {
                    errorMessage = "Dry mass and thrust must each be represented by a positive number greater than 0.";
                    return true;
                }
                else
                {
                    errorMessage = "";
                    return false;
                }
            }
            else
            {
                errorMessage = "Select either the 'min' or the 'max' checkboxes to input the stage variables required to calculate thrust.";
                return true;
            }
        }
        private bool TWRError(Stage stage, out string errorMessage, out bool? minOrMax)
        {
            if (MinTWRCheckBox.Checked && MaxTWRCheckBox.Checked)
            {
                minOrMax = null;
                if (stage.Thrust <= 0 || (stage.WetMass <= 0 && stage.DryMass <= 0))
                {
                    errorMessage = "Either wet mass or dry mass, and thrust, must be represented by a positive number greater than 0.";
                    return true;
                }
                else
                {
                    errorMessage = "";
                    return false;
                }
            }
            else if (MinTWRCheckBox.Checked)
            {
                minOrMax = true;
                if (stage.Thrust <= 0 || stage.WetMass <= 0)
                {
                    errorMessage = "Wet mass and thrust must each be represented by a positive number greater than 0.";
                    return true;
                }
                else
                {
                    errorMessage = "";
                    return false;
                }
            }
            else if (MaxTWRCheckBox.Checked)
            {
                minOrMax = false;
                if (stage.Thrust <= 0 || stage.DryMass <= 0)
                {
                    errorMessage = "Dry mass and thrust must each be represented by a positive number greater than 0.";
                    return true;
                }
                else
                {
                    errorMessage = "";
                    return false;
                }
            }
            else
            {
                errorMessage = "Select either the 'min' or the 'max' checkboxes to input the stage variables required to calculate TWR.";
                // necessary only for output parameter, not for value
                minOrMax = null;
                return true;
            }
        }

        private void SetHeaderColors(bool deltaV, bool isp, bool thrust, bool min, bool max)
        {
            // DeltaV
            if (DeltaVCheckBox.Checked)
            {
                if(deltaV == false)
                {
                    deltaVColumn[0].ForeColor = System.Drawing.Color.Red;
                }
                else deltaVColumn[0].ForeColor = System.Drawing.Color.Gold;
            }

            // Isp
            if (IspCheckBox.Checked)
            {
                if (isp == false)
                {
                    ispColumn[0].ForeColor = System.Drawing.Color.Red;
                }
                else ispColumn[0].ForeColor = System.Drawing.Color.Gold;
            }

            // Thrust
            if (ThrustCheckBox.Checked)
            {
                if (thrust == false)
                {
                    thrustColumn[0].ForeColor = System.Drawing.Color.Red;
                }
                else thrustColumn[0].ForeColor = System.Drawing.Color.Gold;
            }

            if (TWRCheckBox.Checked)
            {
                // Min TWR
                if (MinTWRCheckBox.Checked)
                {
                    if(min == false)
                    {
                        minTWRColumn[0].ForeColor = System.Drawing.Color.Red;
                    }
                    else minTWRColumn[0].ForeColor = System.Drawing.Color.Gold;
                }
                // Max TWR
                if (MaxTWRCheckBox.Checked)
                {
                    if(max == false)
                    {
                        maxTWRColumn[0].ForeColor = System.Drawing.Color.Red;
                    }
                    else maxTWRColumn[0].ForeColor = System.Drawing.Color.Gold;
                }
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
                int deltaV;

                // finds appropriate TextBox.Text and TryParses Text into fields
                bool IsWetMassDouble = double.TryParse(getTextBoxText(i, wetMassColumn), out wetMass);
                bool IsDryMassDouble = double.TryParse(getTextBoxText(i, dryMassColumn), out dryMass);
                bool IsThrustDouble = double.TryParse(getTextBoxText(i, thrustColumn), out thrust);
                bool IsIspDouble = int.TryParse(getTextBoxText(i, ispColumn), out isp);
                bool IsMinTWRDouble = double.TryParse(getTextBoxText(i, minTWRColumn), out minTWR);
                bool IsMaxTWRDouble = double.TryParse(getTextBoxText(i, maxTWRColumn), out maxTWR);
                bool IsDeltaVInt = int.TryParse(getTextBoxText(i, deltaVColumn), out deltaV);

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

        // finds the text of the TextBox associated with a certain stage and property, represented by 'i' and 'tbarray' respectively
        private string getTextBoxText(int i, TableCell[] TCArray)
        {
            StringBuilder varID = new StringBuilder(TCArray[(i + 1)].ID);
            varID.Remove(0, 2);
            varID.Insert(0, "TextBox");
            return ((TextBox)TCArray[(i + 1)].FindControl(varID.ToString())).Text;
        }

        // gives calculated response to appropriate TextBox for output
        private void setTextBoxText(string text, int i, TableCell[] TCArray)
        {
            StringBuilder varID = new StringBuilder(TCArray[(i + 1)].ID);
            varID.Remove(0, 2);
            varID.Insert(0, "TextBox");
            ((TextBox)FindControl(varID.ToString())).Text = text;
        }
        #endregion
    }
}