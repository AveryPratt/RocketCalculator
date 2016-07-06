using KSP_Library;
using KSP_Library.Rocketry;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.IO;
using System.Configuration;
using System.Data;

namespace RocketryWebApp.Calculator
{
    public partial class CalculatorWebForm
    {
        protected void UserRocketsGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if(e.CommandName != "Delete")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                string rocketID = ((GridView)e.CommandSource).Rows[rowIndex].Cells[2].Text;
                GridView rocketData = getSavedValues(rocketID);
                int stageNumber = rocketData.Rows.Count;
                List<Stage> stageList = new List<Stage>();
                for (int i = 0; i < stageNumber; i++)
                {
                    Stage stage = new Stage();
                    stageList.Add(stage);
                }
                foreach (Stage stage in stageList)
                {
                    stage.ID = stageList.IndexOf(stage);
                    stage.WetMass = Convert.ToDouble(rocketData.Rows[stage.ID].Cells[3].Text);
                    stage.DryMass = Convert.ToDouble(rocketData.Rows[stage.ID].Cells[4].Text);
                    stage.Isp = Convert.ToInt32(rocketData.Rows[stage.ID].Cells[5].Text);
                    stage.DeltaV = Convert.ToInt32(rocketData.Rows[stage.ID].Cells[6].Text);
                    stage.Thrust = Convert.ToDouble(rocketData.Rows[stage.ID].Cells[7].Text);
                    stage.MinTWR = Convert.ToDouble(rocketData.Rows[stage.ID].Cells[8].Text);
                    stage.MaxTWR = Convert.ToDouble(rocketData.Rows[stage.ID].Cells[9].Text);
                }
                double payloadMass = Convert.ToDouble(((GridView)e.CommandSource).Rows[rowIndex].Cells[4].Text);
                PayloadTextBox.Text = payloadMass.ToString();
                RocketNameTextBox.Text = ((GridView)e.CommandSource).Rows[rowIndex].Cells[3].Text;
                Conversions.ConvertStageListToWholeTable(stageList, payloadMass, RocketTable, setTextBoxText);
                getUserRockets();
                RocketTableVisible = true;
                StageNumber = Convert.ToInt32(((GridView)e.CommandSource).Rows[rowIndex].Cells[6].Text);
                setTable();
            }
        }

        private GridView getSavedValues(string rocketID)
        {
            GridView gridView = new GridView();

            string connectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand selectRocketsCommand = new SqlCommand("SELECT * FROM Stages WHERE RocketID = " + 
                    rocketID.ToString() + ";",
                    connection);

                SqlDataAdapter dataAdapter = new SqlDataAdapter(selectRocketsCommand);
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet);
                gridView.DataSource = dataSet;

                connection.Open();
                gridView.DataBind();
            }
            return gridView;
        }
    }
}