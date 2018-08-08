///*
//TODO
//Login Logout Page
//SQL Server teams
//Save function
//Photo by Francisco Gonzalez on Unsplash
// */

//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Data;
//using System.Web;
//using System.Web.UI;
//using System.Web.UI.WebControls;
//using System.Collections;
//using System.Web.ApplicationServices;

//namespace SmallBall
//{
//    public partial class PlayBall : System.Web.UI.Page
//    {
//        protected void Page_Load(object sender, EventArgs e)
//        {

//        }

//        protected void Take_Click(object sender, EventArgs e)
//        {
//            Game game = (Game)Application.Contents.Get("game");
//            game.Take();
//            Game_Manager(game);
//        }

//        protected void GuessFB_Click(object sender, EventArgs e)
//        {
//            Game game = (Game)Application.Contents.Get("game");
//            game.GuessFB();
//            Game_Manager(game);
//        }

//        protected void GuessBB_Click(object sender, EventArgs e)
//        {
//            Game game = (Game)Application.Contents.Get("game");
//            game.GuessBB();
//            Game_Manager(game);
//        }

//        protected void GuessOS_Click(object sender, EventArgs e)
//        {
//            Game game = (Game)Application.Contents.Get("game");
//            game.GuessOS();
//            Game_Manager(game);
//        }

//        protected void PitchOut_Click(object sender, EventArgs e)
//        {
//            Game game = (Game)Application.Contents.Get("game");
//            game.PitchOut();
//            Game_Manager(game);
//        }

//        protected void Fastball_Click(object sender, EventArgs e)
//        {
//            Game game = (Game)Application.Contents.Get("game");
//            game.PitchFB();
//            Game_Manager(game);
//        }

//        protected void OffSpeed_Click(object sender, EventArgs e)
//        {
//            Game game = (Game)Application.Contents.Get("game");
//            game.PitchOS();
//            Game_Manager(game);
//        }

//        protected void BreakingBall_Click(object sender, EventArgs e)
//        {
//            Game game = (Game)Application.Contents.Get("game");
//            game.PitchBB();
//            Game_Manager(game);
//        }

//        protected void NewGame_Click(object sender, EventArgs e)
//        {

//        }

//        protected void TeamName_TextChanged(object sender, EventArgs e)
//        {

//        }

//        protected void NewCareer_Click(object sender, EventArgs e)
//        {

//        }

//        protected void LoadCareer_Click(object sender, EventArgs e)
//        {

//        }

//        protected void Practice_Click(object sender, EventArgs e)
//        {
//            //Team user = new Team("KC", "Bears");
//            //Team opp = new Team("NY", "Knights");
//            //Game game = new Game(user, opp, true);
//            //Application.Contents.Add("game", game);

//            //Game_Manager(game);
//        }

//        protected void Game_Manager(Game game)
//        {
//            ScoreBoard s = new ScoreBoard();
//            DataGrid Board = this.form1.FindControl("BoxScore") as DataGrid;
//            Board.DataSource = s.CreateBoxScore(game.HomeTeam, game.AwayTeam);
//            Board.DataBind();

//            TextBox Balls = this.form1.FindControl("Balls") as TextBox;
//            TextBox Strikes = this.form1.FindControl("Strikes") as TextBox;
//            TextBox Outs = this.form1.FindControl("Outs") as TextBox;

//            Balls.Text = game.Balls.ToString();

//            Strikes.Text = game.Strikes.ToString();

//            Outs.Text = game.Outs.ToString();

//            RadioButton First = this.form1.FindControl("First") as RadioButton;
//            RadioButton Second = this.form1.FindControl("Second") as RadioButton;
//            RadioButton Third = this.form1.FindControl("Third") as RadioButton;

//            First.Checked = game.First.Checked;
//            Second.Checked = game.Second.Checked;
//            Third.Checked = game.Third.Checked;

//            if (game.GameOver)
//            {

//            }
//        }

//        protected void Career_Manager()
//        {

//        }
//    }
//}

    