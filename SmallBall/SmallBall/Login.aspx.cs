using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace SmallBall
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            System.Web.UI.ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
        }

        protected void LogInBtn_Click(object sender, EventArgs e)
        {
            ErrorLabel.Visible = false;
            // attempt to login, validated by sql database
            SQL_Connect connect = new SQL_Connect();
            ErrorLabel.Text = connect.Login(UserName.Text, Password.Text);
            ErrorLabel.Visible = true;
            RegisterBtn.Visible = true;
            if(ErrorLabel.Text == "Login Successful")
            {
                // reset application.contents before adding this user data
                Application.Contents.Clear();
                Application.Contents.Add("UserName", UserName.Text);
                Server.Transfer("MainMenu.aspx");
            }
        }

        protected void RegisterBtn_Click(object sender, EventArgs e)
        {
            Server.Transfer("Register.aspx");
        }
    }

    
}