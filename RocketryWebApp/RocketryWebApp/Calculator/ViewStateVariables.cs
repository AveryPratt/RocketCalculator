using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace RocketryWebApp.Calculator
{
    public partial class CalculatorWebForm
    {
        public int StageNumber
        {
            get
            {
                return (int)ViewState["StageNumber"];
            }
            set
            {
                ViewState["StageNumber"] = value;
                StageNumberTextBox.Text = value.ToString();
            }
        }
        public bool RocketTableVisible {
            get
            {
                return (bool)ViewState["RocketTableVisible"];
            }
            set
            {
                ViewState["RocketTableVisible"] = value;
                TableDiv.Visible = value;
            }
        }
        public bool UserRocketsVisible {
            get
            {
                return (bool)ViewState["UserRocketsVisible"];
            }
            set
            {
                ViewState["UserRocketsVisible"] = value;
                RocketsDiv.Visible = value;
            }
        }
        public string UserNameSession
        {
            get
            {
                return (string)Session["UserName"];
            }
            set
            {
                Session["UserName"] = value;
            }
        }

        private void setHeaderButtons()
        {
            if ((string)Session["UserName"] == null)
            {
                UserNameDiv.Visible = false;
                LoginDiv.Visible = true;
            }
            else
            {
                UserName.InnerHtml = (string)Session["UserName"];
                UserNameDiv.Visible = true;
                LoginDiv.Visible = false;
            }
        }
        private void setUserRocketVisibility()
        {
            if (UserRocketsGridView.Rows.Count == 0)
            {
                UserRocketsVisible = false;
            }
            else
            {
                UserRocketsVisible = true;
            }
            RocketsDiv.Visible = UserRocketsVisible;
        }

        private void determineCalculateButtonVisibility()
        {
            if (StageNumber > 0 && (DeltaVCheckBox.Checked || IspCheckBox.Checked || TWRCheckBox.Checked || ThrustCheckBox.Checked))
            {
                CalculateButton.Visible = true;
            }
            else
            {
                CalculateButton.Visible = false;
            }
        }
    }
}