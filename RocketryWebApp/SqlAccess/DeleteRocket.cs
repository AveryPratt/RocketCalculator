using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace RocketryWebApp.Calculator
{
    public partial class CalculatorWebForm
    {
        protected void UserRocketsGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand deleteRocketCommand = new SqlCommand("DELETE FROM Rockets WHERE KerbalID = " + 
                    selectKerbalID().ToString() + " AND RocketID = " +
                    e.Values[0] + ";",
                    connection);
                connection.Open();
                deleteRocketCommand.ExecuteNonQuery();
            }
            getUserRockets();
        }
    }
}