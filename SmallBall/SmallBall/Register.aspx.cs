using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SmallBall
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            System.Web.UI.ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
        }

        protected void ConfirmRegister_Click(object sender, EventArgs e)
        {
            ErrorLabel.Visible = false;
            // attempt to register, username must be unique
            SQL_Connect connect = new SQL_Connect();
            ErrorLabel.Text = connect.Register(UserName.Text, Password.Text, Email.Text);
            ErrorLabel.Visible = true;
            if(ErrorLabel.Text == "Registration successful")
            {
                // reset application.contents before adding new user data
                Application.Contents.Clear();
                Application.Contents.Add("UserName", UserName.Text);
                Server.Transfer("MainMenu.aspx");
            }
        }
    }
}