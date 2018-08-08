using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.ApplicationServices;

namespace SmallBall
{
    public partial class NewCareer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                return;
            }
            
            //populate drop down lists
            SQL_Connect connect = new SQL_Connect();
            CityList.DataSource = connect.populateCities();
            CityList.DataTextField = "StringID";
            CityList.DataValueField = "CityName";
            CityList.DataBind();

            TeamNameList.DataSource = connect.populateTeamNames();
            TeamNameList.DataBind();
        }

        protected void CityList_SelectedIndexChanged(object sender, EventArgs e)
        {
            UserTeam.Text = CityList.SelectedValue + " " + TeamNameList.SelectedValue;
        }

        protected void TeamNameList_SelectedIndexChanged(object sender, EventArgs e)
        {
            UserTeam.Text = CityList.SelectedValue + " " + TeamNameList.SelectedValue;
        }

        protected void Confirm_Click(object sender, EventArgs e)
        {
            // create new team with selected items
            SQL_Connect connect = new SQL_Connect();
            City UserCity = new City(CityList.SelectedItem.Text.Split(',')[0], CityList.SelectedItem.Text.Split(',')[1],CityList.SelectedItem.Text.Split(',')[2]);
            Team UserTeam = new Team(UserCity, TeamNameList.SelectedValue);
            Application.Contents.Add("UserTeam", UserTeam);
            Server.Transfer("CareerHome.aspx");
        }
    }
}