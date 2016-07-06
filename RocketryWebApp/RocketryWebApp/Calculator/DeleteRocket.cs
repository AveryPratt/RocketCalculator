using KSP_Library;
using KSP_Library.Rocketry;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.IO;
using System.Configuration;

namespace RocketryWebApp.Calculator
{
    public partial class CalculatorWebForm
    {
        protected void UserRocketsGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand deleteRocketCommand = new SqlCommand("DELETE FROM Rockets WHERE KerbalID = " + 
                    selectKerbalID(connectionString).ToString() + " AND RocketID = " +
                    e.Values[0] + ";",
                    connection);
                connection.Open();
                deleteRocketCommand.ExecuteNonQuery();
            }
            getUserRockets();
        }
    }
}