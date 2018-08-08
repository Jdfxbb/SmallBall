using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SmallBall
{
    public partial class MainMenu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                return;
            }
            System.Web.UI.ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
        }

        protected void NewCareer_Click(object sender, EventArgs e)
        {
            Server.Transfer("NewCareer.aspx");
        }

        protected void LoadCareer_Click(object sender, EventArgs e)
        {
            string UserName = (string)Application.Contents.Get("UserName");

            // pull loaded values from hidden fields
            string LoadLeague = LoadedLeague.Value.ToString();
            string LoadUserTeam = LoadedUserTeam.Value.ToString();
            string LoadLastPlayed = LoadedLastPlayed.Value.ToString();
            string LoadCurrentGame = LoadedCurrentGame.Value.ToString();

            // if any values are null, the data is corrupted or doesn't exist
            if(LoadLeague == "" || LoadUserTeam == "" || LoadLastPlayed == "" || LoadCurrentGame == "")
            {
                WarningLabel.Text = "Unable to load";
                WarningLabel.Visible = true;
                return;
            }
            
            // use a javascript serializer to deserialize JSON data
            System.Web.Script.Serialization.JavaScriptSerializer JS_Serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<List<Team>> League = JS_Serializer.Deserialize<List<List<Team>>>(LoadLeague);
            Team UserTeam = JS_Serializer.Deserialize<Team>(LoadUserTeam);
            int LastPlayed = JS_Serializer.Deserialize<int>(LoadLastPlayed);
            Game CurrentGame = JS_Serializer.Deserialize<Game>(LoadCurrentGame);

            Application.Contents.Add("League", League);
            Application.Contents.Add("UserTeam", UserTeam);
            Application.Contents.Add("LastPlayed", LastPlayed);
            Application.Contents.Add("CurrentGame", CurrentGame);

            Server.Transfer("CareerHome.aspx");
        }
        
        public string GetUserName()
        {
            // function called by javascript procedures
            string u = (string)Application.Contents.Get("UserName");
            return u;
        }

        protected void Practice_Click(object sender, EventArgs e)
        {
            Server.Transfer("Practice.aspx");
        }

        protected void Logout_Click(object sender, EventArgs e)
        {
            Server.Transfer("StartPage.aspx");
        }
    }
}