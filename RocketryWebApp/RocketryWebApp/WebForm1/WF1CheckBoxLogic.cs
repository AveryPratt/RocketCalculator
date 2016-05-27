using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace RocketryWebApp.WebForm1
{
    public partial class WebForm1
    {
        /// <summary>
        /// CheckBox Logic
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DeltaVCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            TableColumns.SetTCColumns(RocketTable);
            IspCheckBox.Checked = false;
            foreach (TableCell cell in TableColumns.TCIspColumn)
            {
                cell.Visible = true;
            }
            foreach (TableCell cell in TableColumns.TCDeltaVColumn)
            {
                cell.Visible = true;
                cell.Enabled = false;
            }
        }
        protected void IspCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            TableColumns.SetTCColumns(RocketTable);
            DeltaVCheckBox.Checked = false;
            foreach (TableCell cell in TableColumns.TCIspColumn)
            {
                cell.Visible = true;
                cell.Enabled = false;
            }
            foreach (TableCell cell in TableColumns.TCDeltaVColumn)
            {
                cell.Visible = true;
            }
        }
        protected void ThrustCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            TableColumns.SetTCColumns(RocketTable);
            TWRCheckBox.Checked = false;
            TWRSettings();
            foreach (TableCell cell in TableColumns.TCThrustColumn)
            {
                cell.Visible = true;
                cell.Enabled = false;
            }
            if (MinTWRCheckBox.Checked)
            {
                foreach (TableCell cell in TableColumns.TCMinTWRColumn)
                {
                    cell.Visible = true;
                }
            }
            if (MaxTWRCheckBox.Checked)
            {
                foreach (TableCell cell in TableColumns.TCMaxTWRColumn)
                {
                    cell.Visible = true;
                }
            }
        }
        protected void TWRCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            TableColumns.SetTCColumns(RocketTable);
            ThrustCheckBox.Checked = false;
            TWRSettings();
            foreach (TableCell cell in TableColumns.TCThrustColumn)
            {
                cell.Visible = true;
            }
            if (MinTWRCheckBox.Checked)
            {
                foreach (TableCell cell in TableColumns.TCMinTWRColumn)
                {
                    cell.Visible = true;
                    cell.Enabled = false;
                }
            }
            if (MaxTWRCheckBox.Checked)
            {
                foreach (TableCell cell in TableColumns.TCMaxTWRColumn)
                {
                    cell.Visible = true;
                    cell.Enabled = false;
                }
            }
        }
        
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
    }
}