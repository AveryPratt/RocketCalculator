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
            string connectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string createUser = "INSERT INTO myUsers (PersonID, UserName, Password) values(, " + CreateUserNameTextBox.Text + ", " + CreatePasswordTextBox.Text + ")'" + CreateUserNameTextBox.Text + "'";
                SqlCommand createNewUser = new SqlCommand(createUser, connection);
                connection.Open();
                int temp = Convert.ToInt32(createNewUser.ExecuteScalar().ToString());
                connection.Close();
                if (temp > 0)
                {
                    connection.Open();
                    string checkPasswordQuery = "SELECT Password FROM myUsers WHERE UserName='" + CreateUserNameTextBox.Text + "'";
                    SqlCommand PWcmd = new SqlCommand(checkPasswordQuery, connection);

                    string password = PWcmd.ExecuteScalar().ToString();
                    if (password == CreatePasswordTextBox.Text)
                    {
                        Session["New"] = CreateUserNameTextBox.Text;
                        Response.Write("Password is correct.");
                    }
                    else
                    {
                        Response.Write("Password is incorrect");
                    }
                }
                else
                {
                    Response.Write("Username is incorrect.");
                }
            }
        }
    }
}