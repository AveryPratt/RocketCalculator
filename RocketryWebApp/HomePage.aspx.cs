using RocketryWebApp.Properties;
using System;
using System.IO;

namespace RocketryWebApp.Calculator
{
    public partial class CalculatorWebForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RocketTableVisible = false;
                StageNumber = 0;
            }
            CalculatorDiv.Visible = RocketTableVisible;

            setHeaderButtons();

            UserRocketsLabel.InnerHtml = UserNameSession + "'s Rockets";
            getDefaultRockets();
            getUserRockets();
            SaveButton.Visible = false;
            int stageNumber;
            int.TryParse(StageNumberTextBox.Text, out stageNumber);
            StageNumber = stageNumber;
            ErrorMessage.Visible = false;
            StringReader descriptionReader = new StringReader(Resources.Description);
            Description.InnerHtml = descriptionReader.ReadToEnd();
            descriptionReader.Close();
            StringReader instructionsReader = new StringReader(Resources.Instructions);
            Instructions.InnerHtml = instructionsReader.ReadToEnd();
            instructionsReader.Close();
        }

        #region CheckBoxes
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
        protected void ThrustCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            TWRCheckBox.Checked = false;
            thrustTWRCalculationSettings();
            setTable();
        }
        protected void TWRCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            ThrustCheckBox.Checked = false;
            thrustTWRCalculationSettings();
            setTable();
        }
        protected void MinCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            setTable();
        }
        protected void MaxCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            setTable();
        }
        private void thrustTWRCalculationSettings()
        {
            if (TWRCheckBox.Checked || ThrustCheckBox.Checked)
            {
                RssParentBodyDropDownList.Enabled = true;
                KspParentBodyDropDownList.Enabled = true;
                MinTWRCheckBox.Enabled = true;
                MaxTWRCheckBox.Enabled = true;
                SolarSystemSelector.Enabled = true;
            }
            else
            {
                RssParentBodyDropDownList.Enabled = false;
                KspParentBodyDropDownList.Enabled = false;
                MinTWRCheckBox.Enabled = false;
                MaxTWRCheckBox.Enabled = false;
                SolarSystemSelector.Enabled = false;
            }
        }
        #endregion

        protected void SolarSystemSelector_CheckedChanged(object sender, EventArgs e)
        {
            parentBodyDropDownListVisibity();
            setTable();
        }

        protected void CreateRocketButton_Clicked(object sender, EventArgs e)
        {
            RocketTableVisible = true;
            setTable();
        }

        protected void AddStageButton_Clicked(object sender, EventArgs e)
        {
            raiseStageNumber();
            setTable();
        }

        #region DeleteButtons
        protected void Button1_Clicked(object sender, EventArgs e)
        {
            deleteButtonHandler(1);
        }
        protected void Button2_Clicked(object sender, EventArgs e)
        {
            deleteButtonHandler(2);
        }
        protected void Button3_Clicked(object sender, EventArgs e)
        {
            deleteButtonHandler(3);
        }
        protected void Button4_Clicked(object sender, EventArgs e)
        {
            deleteButtonHandler(4);
        }
        protected void Button5_Clicked(object sender, EventArgs e)
        {
            deleteButtonHandler(5);
        }
        protected void Button6_Clicked(object sender, EventArgs e)
        {
            deleteButtonHandler(6);
        }
        protected void Button7_Clicked(object sender, EventArgs e)
        {
            deleteButtonHandler(7);
        }
        protected void Button8_Clicked(object sender, EventArgs e)
        {
            deleteButtonHandler(8);
        }
        protected void Button9_Clicked(object sender, EventArgs e)
        {
            deleteButtonHandler(9);
        }
        private void deleteButtonHandler(int stageDeleted)
        {
            cascadeAboveRows(stageDeleted);
            setTable();
        }
        #endregion

        protected void CalculateButton_Clicked(object sender, EventArgs e)
        {
            setTable();
            runCalculations();
        }

        protected void SaveRocketButton_Clicked(object sender, EventArgs e)
        {
            setTable();
            runCalculations();
            if (ErrorMessage.InnerHtml == "<noerror> Input Conversion Successful. </noerror><br/><noerror> Stage Calculation Successful. </noerror><br/><noerror> Totals Calculation Successful. </noerror><br/>")
            {
                if((string)Session["UserName"] != null)
                {
                    saveRocket();
                    ErrorMessage.InnerHtml += "<noerror> Save Successful. </noerror><br/>";
                }
                else
                {
                    ErrorMessage.InnerHtml += "<error> Must be logged in to save rocket. </error><br/>";
                }
            }
            else
            {
                ErrorMessage.InnerHtml += "<error> Save Unsuccessful. </error><br/>";
            }
            getUserRockets();
        }

        protected void LogoutButton_Click(object sender, EventArgs e)
        {
            UserNameSession = null;
            setHeaderButtons();
            getUserRockets();
            setTable();
        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx", false);
        }

        protected void CreateAccountButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("CreateAccount.aspx", false);
        }
    }
}