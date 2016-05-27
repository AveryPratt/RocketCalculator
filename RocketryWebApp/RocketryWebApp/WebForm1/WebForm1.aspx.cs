using KSP_Library;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;

namespace RocketryWebApp.WebForm1
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        int StageNumber;
        bool ShowStages;

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        
        protected void SetStagesButton_Clicked(object sender, EventArgs e)
        {

        }

        protected void AddStageButton_Clicked(object sender, EventArgs e)
        {
            ShowStages = true;
            ViewState["ShowStages"] = ShowStages;
            StageNumber++;
            StageNumberTextBox.Text = StageNumber.ToString();
        }

        // Delete Stage Buttons
        protected void Button1_Clicked(object sender, EventArgs e)
        {
            ResetStage(1);
            StageNumber--;
            StageNumberTextBox.Text = StageNumber.ToString();
        }
        protected void Button2_Clicked(object sender, EventArgs e)
        {
            ResetStage(2);
            StageNumber--;
            StageNumberTextBox.Text = StageNumber.ToString();
        }
        protected void Button3_Clicked(object sender, EventArgs e)
        {
            ResetStage(3);
            StageNumber--;
            StageNumberTextBox.Text = StageNumber.ToString();
        }
        protected void Button4_Clicked(object sender, EventArgs e)
        {
            ResetStage(4);
            StageNumber--;
            StageNumberTextBox.Text = StageNumber.ToString();
        }
        protected void Button5_Clicked(object sender, EventArgs e)
        {
            ResetStage(5);
            StageNumber--;
            StageNumberTextBox.Text = StageNumber.ToString();
        }
        protected void Button6_Clicked(object sender, EventArgs e)
        {
            ResetStage(6);
            StageNumber--;
            StageNumberTextBox.Text = StageNumber.ToString();
        }
        protected void Button7_Clicked(object sender, EventArgs e)
        {
            ResetStage(7);
            StageNumber--;
            StageNumberTextBox.Text = StageNumber.ToString();
        }
        protected void Button8_Clicked(object sender, EventArgs e)
        {
            ResetStage(8);
            StageNumber--;
            StageNumberTextBox.Text = StageNumber.ToString();
        }
        protected void Button9_Clicked(object sender, EventArgs e)
        {
            ResetStage(9);
            StageNumber--;
            StageNumberTextBox.Text = StageNumber.ToString();
        }

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
    }
}