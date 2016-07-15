using KSP_Library.Rocketry;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace RocketryWebApp.Calculator
{
    public partial class CalculatorWebForm
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ActiveConnectionString"].ConnectionString;
        private void getDefaultRockets()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand selectDefaultRocketsCommand = new SqlCommand("SELECT RocketID, RocketName, Payload, ParentBody, StageNumber FROM Rockets WHERE Rockets.KerbalID = 1;",
                    connection);
                SqlDataAdapter dataAdapter = new SqlDataAdapter(selectDefaultRocketsCommand);
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet);

                connection.Open();
                DefaultRocketsGridView.DataSource = dataSet;
                DefaultRocketsGridView.DataBind();
            }
        }

        private void getUserRockets()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand selectUserRocketsCommand = new SqlCommand("SELECT RocketID, RocketName, Payload, ParentBody, StageNumber FROM Kerbals INNER JOIN Rockets ON Kerbals.KerbalID = Rockets.KerbalID WHERE Kerbals.UserName = '" + 
                    (string)Session["UserName"] + "';", 
                    connection);
                SqlDataAdapter dataAdapter = new SqlDataAdapter(selectUserRocketsCommand);
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet);

                connection.Open();
                UserRocketsGridView.DataSource = dataSet;
                UserRocketsGridView.DataBind();
            }
            setUserRocketVisibility();
        }

        private void saveRocket()
        {
            int kerbalID = selectKerbalID();
            insertRocket(kerbalID);
            int rocketID = selectRocketID();
            insertStages(rocketID);
        }
        private int selectKerbalID()
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
        private void insertRocket(int kerbalID)
        {
            string payload = PayloadTextBox.Text;
            if(payload == string.Empty)
            {
                payload = "0";
            }
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
                SqlCommand insertRocketCommand = new SqlCommand("EXECUTE spAddRocket " +
                    kerbalID.ToString() + ", '" +
                    RocketNameTextBox.Text + "', " + 
                    payload + ", '" +
                    parentBody + "', " +
                    StageNumber.ToString() + ";",
                    connection);
                connection.Open();
                insertRocketCommand.ExecuteNonQuery();
            }
        }
        private int selectRocketID()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand selectRocketIDCommand = new SqlCommand("SELECT IDENT_CURRENT('Rockets');",
                    connection);
                connection.Open();
                return Convert.ToInt32(selectRocketIDCommand.ExecuteScalar());
            }
        }
        private void insertStages(int rocketID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                StringBuilder insertStages = new StringBuilder();
                Rocket rocket = Conversions.ConvertWholeTableToRocket(RocketTable, getTextBoxText);
                foreach (Stage stage in rocket.StageList)
                {
                    insertStages.Append("EXECUTE spAddStage " +
                        rocketID.ToString() + "," +
                        (stage.ID + 1).ToString() + ", " +
                        stage.WetMass.ToString() + ", " +
                        stage.DryMass.ToString() + ", " +
                        stage.Isp.ToString() + ", " +
                        stage.DeltaV.ToString() + ", " +
                        stage.Thrust.ToString() + ", " +
                        stage.MinTWR.ToString() + ", " +
                        stage.MaxTWR.ToString() + ";");
                }
                SqlCommand insertStagesCommand = new SqlCommand(insertStages.ToString(), connection);
                connection.Open();
                insertStagesCommand.ExecuteNonQuery();
            }
        }
    }
}