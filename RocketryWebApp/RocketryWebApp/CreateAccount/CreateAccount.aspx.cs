using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RocketryWebApp.CreateAccount
{
    public partial class CreateAccountWebForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CreateUserNameResponse.Text = string.Empty;
            CreatePasswordResponse.Text = string.Empty;
            ConfirmPasswordResponse.Text = string.Empty;
        }
        
        protected void CreateAccountButton_Clicked(object sender, EventArgs e)
        {
            string userName = CreateUserNameTextBox.Text;
            string password = CreatePasswordTextBox.Text;

            if (userName.Length < 3)
            {
                CreateUserNameResponse.Text = "<error> Name must be at least 3 characters long. </error>";
            }
            else if (password.Length < 6)
            {
                CreatePasswordResponse.Text = "<error> Password must be at least 6 characters long. </error>";
            }
            else
            {
                if (CreatePasswordTextBox.Text != ConfirmPasswordTextBox.Text)
                {
                    ConfirmPasswordResponse.Text = "<error> Passwords do not match. </error>";
                }
                else
                {
                    string connectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        SqlCommand selectUserCountCommand = new SqlCommand("SELECT COUNT(*) FROM Kerbals WHERE UserName = '" + userName + "';", connection);
                        SqlCommand insertNewUserCommand = new SqlCommand("INSERT INTO Kerbals (UserName, Password) values('" + userName + "', '" + password + "');", connection);
                        connection.Open();
                        int userCount = Convert.ToInt32(selectUserCountCommand.ExecuteScalar());
                        if (userCount != 0)
                        {
                            CreateUserNameResponse.Text = "<error> UserName already exists. </error>";
                        }
                        else
                        {
                            insertNewUserCommand.ExecuteNonQuery();
                            Session["UserName"] = userName;
                            Response.Redirect("~/Calculator/CalculatorWebForm.aspx", false);
                        }
                    }
                }
            }
        }

        protected void HomeButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Calculator/CalculatorWebForm.aspx", false);
        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Login/Login.aspx", false);
        }
    }
}