using System;
using System.Configuration;
using System.Data.SqlClient;

namespace RocketryWebApp.Login
{
    public partial class LoginWebForm : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["Local"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            LoginUserNameResponse.Text = string.Empty;
            LoginPasswordResponse.Text = string.Empty;
        }

        protected void LoginButton_Clicked(object sender, EventArgs e)
        {
            string userName = LoginUserNameTextBox.Text;
            string password = LoginPasswordTextBox.Text;
            
            using (SqlConnection connection = new SqlConnection(connectionString))
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
                        Response.Redirect("~/CalculatorWebForm/default.aspx/", false);
                    }
                    else
                    {
                        LoginPasswordResponse.Text = "<error> Password is incorrect. </error>";
                    }
                }
                else
                {
                    LoginUserNameResponse.Text = "<error> Username does not exist. </error>";
                }
            }
        }

        protected void HomeButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("default.aspx", false);
        }

        protected void CreateAccountButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("CreateAccount.aspx", false);
        }
    }
}