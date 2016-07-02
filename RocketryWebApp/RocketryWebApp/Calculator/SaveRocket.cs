using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
using System.Web.UI.WebControls;
using KSP_Library.Rocketry;

namespace RocketryWebApp.Calculator
{
    public partial class CalculatorWebForm
    {
        //private string getRockets()
        //{
        //    StringBuilder rocketStringBuilder = new StringBuilder();
        //    string connectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        string getRocketsString = ""
        //        SqlCommand getRocketsCommand = new SqlCommand()
        //    }
        //}

        private void saveRocket()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string getKerbalIDString = "SELECT (KerbalID) FROM Kerbals WHERE UserName = '" + (string)Session["UserName"] + "';";
                SqlCommand getKerbalIDCommand = new SqlCommand(getKerbalIDString, connection);
                string kerbalID = getKerbalIDCommand.ExecuteScalar().ToString();
                string createRocketString = "INSERT INTO Rockets VALUES ('" + RocketNameTextBox.Text + "', '" + PayloadTextBox.Text + "', '" + StageNumber.ToString() + "', '" + kerbalID + "');";
                SqlCommand createRocketCommand = new SqlCommand(createRocketString, connection);
                createRocketCommand.ExecuteNonQuery();

                string getRocketIDString = "SELECT (RocketID) FROM Rockets WHERE RocketName = '" + RocketNameTextBox.Text + "';";
                SqlCommand getRocketIDCommand = new SqlCommand(getKerbalIDString, connection);
                int rocketID = Convert.ToInt32(getKerbalIDCommand.ExecuteScalar());

                StringBuilder insertStages = new StringBuilder();
                string errors;
                List<Stage> stageList = Conversions.ConvertTableToStageList(RocketTable, getTextBoxText, out errors);
                foreach (Stage stage in stageList)
                {
                    insertStages.Append("INSERT INTO Stages VALUES(" + 
                        rocketID + "," + 
                        stage.ID.ToString() + ", " + 
                        stage.WetMass.ToString() + ", " + 
                        stage.DryMass.ToString() + ", " + 
                        stage.Isp.ToString() + ", " + 
                        stage.DeltaV.ToString() + ", " + 
                        stage.Thrust.ToString() + ", " + 
                        stage.MinTWR.ToString() + ", " + 
                        stage.MaxTWR.ToString() + ");");
                }
                SqlCommand insertStagesCommand = new SqlCommand(insertStages.ToString(), connection);
                insertStagesCommand.ExecuteNonQuery();
            }
        }
    }
}