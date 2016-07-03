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
        private GridView getUserRockets()
        {
            GridView rocketList = new GridView();
            string connectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand selectRocketsCommand = new SqlCommand("SELECT RocketName, Payload, StageNumber FROM Kerbals INNER JOIN Rockets ON Kerbals.KerbalID = Rockets.KerbalID WHERE Kerbals.UserName = '" + (string)Session["UserName"] + "'", connection);
                connection.Open();
                rocketList.DataSource = selectRocketsCommand.ExecuteReader();
                rocketList.DataBind();
            }
            return rocketList;
        }

        private void saveRocket()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;

            int kerbalID = selectKerbalID(connectionString);
            insertRocket(connectionString, kerbalID);
            int rocketID = selectRocketID(connectionString);
            insertStages(connectionString, rocketID);
        }
        private int selectKerbalID(string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand selectKerbalIDCommand = new SqlCommand("SELECT (KerbalID) FROM Kerbals WHERE UserName = '" + (string)Session["UserName"] + "';", connection);
                connection.Open();
                return Convert.ToInt32(selectKerbalIDCommand.ExecuteScalar());
            }
        }
        private void insertRocket(string connectionString, int kerbalID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand insertRocketCommand = new SqlCommand("INSERT INTO Rockets VALUES ('" + RocketNameTextBox.Text + "', '" + PayloadTextBox.Text + "', '" + StageNumber.ToString() + "', '" + kerbalID.ToString() + "');", connection);
                connection.Open();
                insertRocketCommand.ExecuteNonQuery();
            }
        }
        private int selectRocketID(string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand selectRocketIDCommand = new SqlCommand("SELECT (RocketID) FROM Rockets WHERE RocketName = '" + RocketNameTextBox.Text + "';", connection);
                connection.Open();
                return Convert.ToInt32(selectRocketIDCommand.ExecuteScalar());
            }
        }
        private void insertStages(string connectionString, int rocketID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                StringBuilder insertStages = new StringBuilder();
                List<Stage> stageList = Conversions.ConvertWholeTableToStageList(RocketTable, getTextBoxText);
                foreach (Stage stage in stageList)
                {
                    insertStages.Append("INSERT INTO Stages VALUES(" +
                        rocketID.ToString() + "," +
                        (stage.ID + 1).ToString() + ", " +
                        stage.WetMass.ToString() + ", " +
                        stage.DryMass.ToString() + ", " +
                        stage.Isp.ToString() + ", " +
                        stage.DeltaV.ToString() + ", " +
                        stage.Thrust.ToString() + ", " +
                        stage.MinTWR.ToString() + ", " +
                        stage.MaxTWR.ToString() + ");");
                }
                SqlCommand insertStagesCommand = new SqlCommand(insertStages.ToString(), connection);
                connection.Open();
                insertStagesCommand.ExecuteNonQuery();
            }
        }
    }
}