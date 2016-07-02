using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

namespace RocketryWebApp.Login
{
    public partial class LoginWebForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void LoginButton_Clicked(object sender, EventArgs e)
        {
            string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(CS))
            {
                string userName = LoginUserNameTextBox.Text.Trim();
                string password = LoginPasswordTextBox.Text;
                string selectUserQuery = "SELECT count(*) FROM Kerbals WHERE UserName='" + userName + "'";
                SqlCommand selectUserCommand = new SqlCommand(selectUserQuery, connection);
                connection.Open();
                int userCount = Convert.ToInt32(selectUserCommand.ExecuteScalar().ToString());
                connection.Close();
                if(userCount > 0)
                {
                    connection.Open();
                    string checkPasswordQuery = "SELECT Password FROM Kerbals WHERE UserName='" + userName + "'";
                    SqlCommand passwordCommand = new SqlCommand(checkPasswordQuery, connection);
                    string userPassword = passwordCommand.ExecuteScalar().ToString().Trim();
                    if (userPassword == password)
                    {
                        Session["UserName"] = userName;
                        Response.Redirect("~/Calculator/CalculatorWebForm.aspx");
                    }
                    else
                    {
                        Response.Write("Password is incorrect");
                    }
                }
                else
                {
                    Response.Write("Username not found");
                }
            }
        }
    }
}