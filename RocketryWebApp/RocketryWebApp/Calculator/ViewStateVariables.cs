using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RocketryWebApp.Calculator
{
    public partial class CalculatorWebForm
    {
        private int StageNumber;

        private void getStageNumber()
        {
            StageNumber = (int)ViewState["StageNumber"];
        }
        private void setStageNumber()
        {
            ViewState["StageNumber"] = StageNumber;
        }
        private void setStageNumber(int stageNumber)
        {
            ViewState["StageNumber"] = stageNumber;
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