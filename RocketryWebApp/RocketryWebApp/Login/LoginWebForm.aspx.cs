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
            string userName = LoginUserNameTextBox.Text;
            string password = LoginPasswordTextBox.Text;

            string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(CS))
            {
                SqlCommand selectUserCountCommand = new SqlCommand("SELECT COUNT(*) FROM Kerbals WHERE UserName = '" + userName + "';", connection);
                SqlCommand passwordCommand = new SqlCommand("SELECT Password FROM Kerbals WHERE UserName='" + userName + "';", connection);
                connection.Open();
                int userCount = Convert.ToInt32(selectUserCountCommand.ExecuteScalar());
                if(userCount != 0)
                {
                    string userPassword = passwordCommand.ExecuteScalar().ToString();
                    if (userPassword == password)
                    {
                        Session["UserName"] = userName;
                        Response.Redirect("~/Calculator/CalculatorWebForm.aspx");
                    }
                    else
                    {
                        Response.Write("Password is incorrect.");
                    }
                }
                else
                {
                    Response.Write("Username does not exist.");
                }
            }
        }
    }
}