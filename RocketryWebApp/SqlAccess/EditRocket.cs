using KSP_Library.Rocketry;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace RocketryWebApp.Calculator
{
    public partial class CalculatorWebForm
    {
        protected void UserRocketsGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if(e.CommandName != "Delete")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                string rocketID = ((GridView)e.CommandSource).Rows[rowIndex].Cells[1].Text;
                GridView rocketData = getSavedValues(rocketID);
                int stageNumber = rocketData.Rows.Count;
                Rocket rocket = new Rocket();
                for (int i = 0; i < stageNumber; i++)
                {
                    Stage stage = new Stage(i);
                    rocket.StageList.Add(stage);
                }
                foreach (Stage stage in rocket.StageList)
                {
                    stage.ID = rocket.StageList.IndexOf(stage);
                    stage.WetMass = Convert.ToDouble(rocketData.Rows[stage.ID].Cells[3].Text);
                    stage.DryMass = Convert.ToDouble(rocketData.Rows[stage.ID].Cells[4].Text);
                    stage.Isp = Convert.ToInt32(rocketData.Rows[stage.ID].Cells[5].Text);
                    stage.DeltaV = Convert.ToInt32(rocketData.Rows[stage.ID].Cells[6].Text);
                    stage.Thrust = Convert.ToDouble(rocketData.Rows[stage.ID].Cells[7].Text);
                    stage.MinTWR = Convert.ToDouble(rocketData.Rows[stage.ID].Cells[8].Text);
                    stage.MaxTWR = Convert.ToDouble(rocketData.Rows[stage.ID].Cells[9].Text);
                }
                double payloadMass = Convert.ToDouble(((GridView)e.CommandSource).Rows[rowIndex].Cells[3].Text);
                PayloadTextBox.Text = payloadMass.ToString();
                RocketNameTextBox.Text = ((GridView)e.CommandSource).Rows[rowIndex].Cells[2].Text;
                Conversions.ConvertRocketToWholeTable(rocket, RocketTable, setTextBoxText);
                getUserRockets();
                RocketTableVisible = true;
                StageNumber = Convert.ToInt32(((GridView)e.CommandSource).Rows[rowIndex].Cells[5].Text);
                setTable();
            }
        }

        private GridView getSavedValues(string rocketID)
        {
            GridView gridView = new GridView();
            
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