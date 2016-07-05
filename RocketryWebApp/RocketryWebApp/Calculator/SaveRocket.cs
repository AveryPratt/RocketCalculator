using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
using System.Web.UI.WebControls;
using KSP_Library.Rocketry;
using System.Data.Common;

namespace RocketryWebApp.Calculator
{
    public partial class CalculatorWebForm
    {
        private void getUserRockets()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand selectRocketsCommand = new SqlCommand("SELECT RocketID, RocketName, Payload, ParentBody, StageNumber FROM Kerbals INNER JOIN Rockets ON Kerbals.KerbalID = Rockets.KerbalID WHERE Kerbals.UserName = '" + 
                    (string)Session["UserName"] + "';", 
                    connection);
                SqlDataAdapter dataAdapter = new SqlDataAdapter(selectRocketsCommand);
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet);

                connection.Open();
                UserRocketsGridView.DataSource = dataSet;
                UserRocketsGridView.DataBind();
            }
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
                SqlCommand selectKerbalIDCommand = new SqlCommand("SELECT (KerbalID) FROM Kerbals WHERE UserName = '" + 
                    (string)Session["UserName"] + "';", 
                    connection);
                connection.Open();
                return Convert.ToInt32(selectKerbalIDCommand.ExecuteScalar());
            }
        }
        private void insertRocket(string connectionString, int kerbalID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string parentBody;
                if (RssParentBodyDropDownList.Visible)
                {
                    parentBody = RssParentBodyDropDownList.SelectedItem.Text;
                }
                else
                {
                    parentBody = KspParentBodyDropDownList.SelectedItem.Text;
                }
                SqlCommand insertRocketCommand = new SqlCommand("INSERT INTO Rockets VALUES ('" +
                    kerbalID.ToString() + "', '" +
                    RocketNameTextBox.Text + "', '" + 
                    PayloadTextBox.Text + "', '" +
                    parentBody + "', '" +
                    StageNumber.ToString() + "');",
                    connection);
                connection.Open();
                insertRocketCommand.ExecuteNonQuery();
            }
        }
        private int selectRocketID(string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand selectRocketIDCommand = new SqlCommand("SELECT IDENT_CURRENT('Rockets');",
                    connection);
                //SqlCommand selectRocketIDCommand = new SqlCommand("SELECT (RocketID) FROM Rockets WHERE RocketName = '" +
                //    rocketName + "';",
                //    connection);
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