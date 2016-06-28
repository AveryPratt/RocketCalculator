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
            using (SqlConnection con = new SqlConnection(CS))
            {
                string checkUser = "SELECT count(*) FROM myUsers WHERE UserName='" + LoginUserNameTextBox.Text + "'";
                SqlCommand cmd = new SqlCommand(checkUser, con);
                con.Open();
                int temp = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                con.Close();
                if(temp > 0)
                {
                    con.Open();
                    string checkPasswordQuery = "SELECT Password FROM myUsers WHERE UserName='" + LoginUserNameTextBox.Text + "'";
                    SqlCommand PWcmd = new SqlCommand(checkPasswordQuery, con);

                    string password = PWcmd.ExecuteScalar().ToString();
                    if(password == LoginPasswordTextBox.Text)
                    {
                        Session["New"] = LoginUserNameTextBox.Text;
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