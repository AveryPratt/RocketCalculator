using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RocketryWebApp.CreateAccount
{
    public partial class CreateAccountWebForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void CreateAccountButton_Clicked(object sender, EventArgs e)
        {
            if (CreatePasswordTextBox.Text != ConfirmPasswordTextBox.Text)
            {
                Response.Write("Passwords do not match");
            }
            else
            {
                string connectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand checkUserNameString = new SqlCommand("SELECT COUNT(*) FROM Kerbals WHERE UserName = '" + CreateUserNameTextBox.Text.Trim() + "';", connection);
                    SqlCommand createNewUser = new SqlCommand("INSERT INTO Kerbals (UserName, Password) values('" + CreateUserNameTextBox.Text.Trim() + "', '" + CreatePasswordTextBox.Text.Trim() + "');", connection);
                    connection.Open();
                    if ((int)checkUserNameString.ExecuteScalar() == 0)
                    {
                        createNewUser.ExecuteNonQuery();
                        Response.Redirect("~/Calculator/CalculatorWebForm.aspx");
                    }
                    else
                    {
                        Response.Write("UserName already exists");
                    }
                    connection.Close();
                }
            }
        }
    }
}