using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace SmallBall
{
    public partial class MainMenu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            System.Web.UI.ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
            
        }

        protected void NewCareer_Click(object sender, EventArgs e)
        {
            Server.Transfer("NewCareer.aspx");
        }

        protected void LoadCareer_Click(object sender, EventArgs e)
        {
            // load career from server =========================================================================================================
            Server.Transfer("CareerHome.aspx");
        }

        protected void Practice_Click(object sender, EventArgs e)
        {
            Server.Transfer("Practice.aspx");
        }
    }
}