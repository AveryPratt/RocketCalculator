using System;
using System.Configuration;
using System.Data.SqlClient;

namespace RocketryWebApp.CreateAccount
{
    public partial class CreateAccountWebForm : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["Local"].ConnectionString;

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
                            Response.Redirect("default.aspx", false);
                        }
                    }
                }
            }
        }

        protected void HomeButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("default.aspx", false);
        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx", false);
        }
    }
}