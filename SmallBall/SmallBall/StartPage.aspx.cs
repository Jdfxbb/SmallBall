using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace SmallBall
{
    public partial class StartPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            System.Web.UI.ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
        }

        protected void Login_Click(object sender, EventArgs e)
        {
            Server.Transfer("Login.aspx");
        }

        protected void Register_Click(object sender, EventArgs e)
        {
            Server.Transfer("Register.aspx");
        }
    }
}

