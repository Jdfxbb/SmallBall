/*
TODO
Login Logout Page
SQL Server teams
Save function
Photo by Francisco Gonzalez on Unsplash

 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Web.ApplicationServices;


namespace SmallBall
{
    public partial class Practice : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Game_Manager(Game game)
        {
            ScoreBoard s = new ScoreBoard();
            DataGrid Board = this.form1.FindControl("BoxScore") as DataGrid;
            Board.DataSource = s.CreateBoxScore(game.HomeTeam, game.AwayTeam);
            Board.DataBind();

            TextBox Balls = this.form1.FindControl("Balls") as TextBox;
            TextBox Strikes = this.form1.FindControl("Strikes") as TextBox;
            TextBox Outs = this.form1.FindControl("Outs") as TextBox;
            TextBox Feed = this.form1.FindControl("GameFeed") as TextBox;

            Balls.Text = game.Balls.ToString();
            Strikes.Text = game.Strikes.ToString();
            Outs.Text = game.Outs.ToString();
            Feed.Text = game.GameFeed.Text;

            RadioButton First = this.form1.FindControl("First") as RadioButton;
            RadioButton Second = this.form1.FindControl("Second") as RadioButton;
            RadioButton Third = this.form1.FindControl("Third") as RadioButton;

            First.Checked = game.First.Checked;
            Second.Checked = game.Second.Checked;
            Third.Checked = game.Third.Checked;

            if (game.isHome)
            {
                if(game.Inning.Half == Inning.Side.Top)
                {
                    Take.Visible = false;
                    GuessBB.Visible = false;
                    GuessFB.Visible = false;
                    GuessOS.Visible = false;
                    PitchOut.Visible = true;
                    Fastball.Visible = true;
                    OffSpeed.Visible = true;
                    BreakingBall.Visible = true;
                }
                else
                {
                    Take.Visible = true;
                    GuessBB.Visible = true;
                    GuessFB.Visible = true;
                    GuessOS.Visible = true;
                    PitchOut.Visible = false;
                    Fastball.Visible = false;
                    OffSpeed.Visible = false;
                    BreakingBall.Visible = false;
                }
            }

            if (game.GameOver)
            {
                NewGame.Visible = true;
                // show SHOW RESULTS ETC ===================================================================================================================================================
            }
        }

        protected void NewGame_Click(object sender, EventArgs e)
        {
            NewGame.Visible = false;
            Team user = new Team("KC", "Bears");
            Team opp = new Team("NY", "Knights");
            Game game = new Game(user, opp, true);
            Application.Contents.Remove("game");
            Application.Contents.Add("game", game);

            Game_Manager(game);
        }

        protected void Take_Click(object sender, EventArgs e)
        {
            Game game = (Game)Application.Contents.Get("game");            
            game.Take();
            Game_Manager(game);
        }

        protected void GuessFB_Click(object sender, EventArgs e)
        {
            Game game = (Game)Application.Contents.Get("game");
            game.GuessFB();
            Game_Manager(game);
        }

        protected void GuessBB_Click(object sender, EventArgs e)
        {
            Game game = (Game)Application.Contents.Get("game");
            game.GuessBB();
            Game_Manager(game);
        }

        protected void GuessOS_Click(object sender, EventArgs e)
        {
            Game game = (Game)Application.Contents.Get("game");
            game.GuessOS();
            Game_Manager(game);
        }

        protected void PitchOut_Click(object sender, EventArgs e)
        {
            Game game = (Game)Application.Contents.Get("game");
            game.PitchOut();
            Game_Manager(game);
        }

        protected void Fastball_Click(object sender, EventArgs e)
        {
            Game game = (Game)Application.Contents.Get("game");
            game.PitchFB();
            Game_Manager(game);
        }

        protected void OffSpeed_Click(object sender, EventArgs e)
        {
            Game game = (Game)Application.Contents.Get("game");
            game.PitchOS();
            Game_Manager(game);
        }

        protected void BreakingBall_Click(object sender, EventArgs e)
        {
            Game game = (Game)Application.Contents.Get("game");
            game.PitchBB();
            Game_Manager(game);
        }
    }

    public class Game
    {

        private Team[] Teams { get; set; } = new Team[2];
        public Inning Inning { get; private set; } = new Inning();
        private ScoreBoard BoxScore { get; }
        public int Balls { get; private set; }
        public int Strikes { get; private set; }
        public int Outs { get; private set; }
        public RadioButton First { get; set; }
        public RadioButton Second { get; set; }
        public RadioButton Third { get; set; }
        public RadioButton[] Bases { get; set; } = new RadioButton[3];
        public enum Swings { Take, Fastball, BreakingBall, OffSpeed };
        public enum Pitches { PitchOut, Fastball, BreakingBall, OffSpeed };
        public Swings swings;
        public Pitches pitches;
        public Random random = new Random();
        public Team HomeTeam { get; set; }
        public Team AwayTeam { get; set; }
        public bool GameOver { get; private set; } = false;
        public TextBox GameFeed { get; private set; }
        public bool isHome { get; private set; } = true;

        // Initialize game
        public Game(Team UserTeam, Team Opponent, bool userHome)
        {
            isHome = userHome;
            if (isHome)
            {
                HomeTeam = UserTeam;
                AwayTeam = Opponent;
            }
            else
            {
                HomeTeam = Opponent;
                AwayTeam = UserTeam;
            }

            HomeTeam.NewGame();
            AwayTeam.NewGame();

            Teams[0] = AwayTeam;
            Teams[1] = HomeTeam;

            AwayTeam.Box.Insert(0, 0);

            First = new RadioButton();
            Second = new RadioButton();
            Third = new RadioButton();
            GameFeed = new TextBox();

            First.Checked = Second.Checked = Third.Checked = false;

            Bases[0] = First;
            Bases[1] = Second;
            Bases[2] = Third;
        }

        public Game()
        {
            //HomeTeam = new Team("", "");
            //AwayTeam = new Team("", "");

            HomeTeam.NewGame();
            AwayTeam.NewGame();

            Teams[0] = AwayTeam;
            Teams[1] = HomeTeam;

            AwayTeam.Box.Insert(0, 0);

            First = new RadioButton();
            Second = new RadioButton();
            Third = new RadioButton();

            First.Checked = Second.Checked = Third.Checked = false;

            Bases[0] = First;
            Bases[1] = Second;
            Bases[2] = Third;
        }

        //Strike pitched
        public void Strike()
        {
            Strikes++;
            if (Strikes == 3)
            {
                Strikes = 0;
                UpdateFeed("Strike out");
                Out();
            }
        }

        // Ball pitched
        public void Ball()
        {
            Balls++;
            if (Balls == 4)
            {
                Balls = 0;
                Strikes = 0;
                UpdateFeed("Walk");
                Walk();
            }
        }

        // Out made
        public void Out()
        {
            Outs++;
            Balls = 0;
            Strikes = 0;
            if (Outs == 3)
            {
                NextInning();
            }
        }

        public void Walk()
        {
            AdvanceBases(1);
        }

        public void Hit(int n)
        {
            AdvanceBases(n);
            Teams[(int)Inning.Half].Hit();
        }

        // Reset count for next batter
        private void ResetCount()
        {
            Strikes = Balls = 0;
        }

        // reset inning to start new one, check winning conditions
        private void NextInning()
        {
            if (HomeTeam.Runs > AwayTeam.Runs && Inning.Num >= 9)
            {
                EndGame();
            }
            else if (AwayTeam.Runs > HomeTeam.Runs && Inning.Num >= 9)
            {
                EndGame();
            }
            else
            {
                Inning++;
            }
            Outs = 0;
            ResetCount();
            ClearBases();
            if (Inning.Half == Inning.Side.Bottom)
            {
                HomeTeam.NextInning();
            }
            else
            {
                AwayTeam.NextInning();
            }

        }

        // assign win/ loss to team
        private void EndGame()
        {
            if (HomeTeam.Runs > AwayTeam.Runs)
            {
                HomeTeam.Win();
                AwayTeam.Lose();
                GameOver = true;
            }
            else
            {
                AwayTeam.Win();
                HomeTeam.Lose();
                GameOver = true;
            }
        }

        // advance n bases; 0 < n < 5
        public void AdvanceBases(int n)
        {
            int runsScored = 0;
            for (int i = 0; i < n; i++)
            {
                //if runner on third, clear third, score run
                if (Bases[2].Checked == true)
                {
                    Bases[2].Checked = false;
                    Run();
                    runsScored++;
                }
                // if runner on second, runner on third, clear second
                if (Bases[1].Checked == true)
                {
                    Bases[1].Checked = false;
                    Bases[2].Checked = true;
                }
                // if runner on first, clear first, runner on second
                if (Bases[0].Checked == true)
                {
                    Bases[0].Checked = false;
                    Bases[1].Checked = true;
                }
                // runner starts at first
                if (i == 0)
                {
                    Bases[0].Checked = true;
                }
            }
            if (runsScored > 0)
            {
                if (runsScored == 1)
                {
                    UpdateFeed("1 run scores");
                }
                else
                {
                    UpdateFeed(runsScored.ToString() + " runs score");
                }
            }
        }

        public void ClearBases()
        {
            for (int i = 0; i < 3; i++)
            {
                Bases[i].Checked = false;
            }
        }

        // assign run to team
        private void Run()
        {
            Teams[(int)Inning.Half].Run(Inning.Num);
        }

        public Swings OppSwing()
        {
            int n = random.Next(3);
            return (Swings)n;
        }

        public Pitches OppPitch()
        {
            int n = random.Next(3);
            return (Pitches)n;
        }

        public void Take()
        {
            Pitches p = OppPitch();
            int n = random.Next(100);
            if (p == Pitches.PitchOut)
            {
                Ball();
            }
            else if (p == Pitches.Fastball)
            {
                if (n < 75)
                {
                    Strike();
                }
                else
                {
                    Ball();
                }
            }
            else if (p == Pitches.BreakingBall)
            {
                if (n < 65)
                {
                    Strike();
                }
                else
                {
                    Ball();
                }
            }
            else if (p == Pitches.OffSpeed)
            {
                if (n < 85)
                {
                    Strike();
                }
                else
                {
                    Ball();
                }
            }
        }

        public void GuessFB()
        {
            Pitches p = OppPitch();
            int n = random.Next(100);
            if (p == Pitches.PitchOut)
            {
                if (n < 80)
                {
                    Strike();
                }
                else
                {
                    int hit = random.Next(1, 4);
                    Hit(hit);
                    UpdateFeed(hit);
                }
            }
            else if (p == Pitches.Fastball)
            {
                if (n < 35)
                {
                    Strike();
                }
                else
                {
                    int hit = random.Next(1, 4);
                    Hit(hit);
                    UpdateFeed(hit);
                }
            }
            else if (p == Pitches.BreakingBall)
            {
                if (n < 75)
                {
                    Strike();
                }
                else
                {
                    int hit = random.Next(1, 4);
                    Hit(hit);
                    UpdateFeed(hit);
                }
            }
            else if (p == Pitches.OffSpeed)
            {
                if (n < 75)
                {
                    Strike();
                }
                else
                {
                    int hit = random.Next(1, 4);
                    Hit(hit);
                    UpdateFeed(hit);
                }
            }
        }

        public void GuessBB()
        {
            Pitches p = OppPitch();
            int n = random.Next(100);
            if (p == Pitches.PitchOut)
            {
                if (n < 80)
                {
                    Strike();
                }
                else
                {
                    int hit = random.Next(1, 4);
                    Hit(hit);
                    UpdateFeed(hit);
                }
            }
            else if (p == Pitches.Fastball)
            {
                if (n < 75)
                {
                    Strike();
                }
                else
                {
                    int hit = random.Next(1, 4);
                    Hit(hit);
                    UpdateFeed(hit);
                }
            }
            else if (p == Pitches.BreakingBall)
            {
                if (n < 35)
                {
                    Strike();
                }
                else
                {
                    int hit = random.Next(1, 4);
                    Hit(hit);
                    UpdateFeed(hit);
                }
            }
            else if (p == Pitches.OffSpeed)
            {
                if (n < 75)
                {
                    Strike();
                }
                else
                {
                    int hit = random.Next(1, 4);
                    Hit(hit);
                    UpdateFeed(hit);
                }
            }
        }

        public void GuessOS()
        {
            Pitches p = OppPitch();
            int n = random.Next(100);
            if (p == Pitches.PitchOut)
            {
                if (n < 80)
                {
                    Strike();
                }
                else
                {
                    int hit = random.Next(1, 4);
                    Hit(hit);
                    UpdateFeed(hit);
                }
            }
            else if (p == Pitches.Fastball)
            {
                if (n < 75)
                {
                    Strike();
                }
                else
                {
                    int hit = random.Next(1, 4);
                    Hit(hit);
                    UpdateFeed(hit);
                }
            }
            else if (p == Pitches.BreakingBall)
            {
                if (n < 75)
                {
                    Strike();
                }
                else
                {
                    int hit = random.Next(1, 4);
                    Hit(hit);
                    UpdateFeed(hit);
                }
            }
            else if (p == Pitches.OffSpeed)
            {
                if (n < 35)
                {
                    Strike();
                }
                else
                {
                    int hit = random.Next(1, 4);
                    Hit(hit);
                    UpdateFeed(hit);
                }
            }
        }

        public void PitchOut()
        {
            Swings p = OppSwing();
            int n = random.Next(100);
            if (p == Swings.Take)
            {
                if (n < 80)
                {
                    Strike();
                }
                else
                {
                    Ball();
                }
            }
            else if (p == Swings.Fastball)
            {
                if (n < 80)
                {
                    Strike();
                }
                else
                {
                    int hit = random.Next(1, 4);
                    Hit(hit);
                    UpdateFeed(hit);
                }
            }
            else if (p == Swings.BreakingBall)
            {
                if (n < 80)
                {
                    Strike();
                }
                else
                {
                    int hit = random.Next(1, 4);
                    Hit(hit);
                    UpdateFeed(hit);
                }
            }
            else if (p == Swings.OffSpeed)
            {
                if (n < 80)
                {
                    Strike();
                }
                else
                {
                    int hit = random.Next(1, 4);
                    Hit(hit);
                    UpdateFeed(hit);
                }
            }
        }

        public void PitchFB()
        {
            Swings p = OppSwing();
            int n = random.Next(100);
            if (p == Swings.Take)
            {
                if (n < 80)
                {
                    Strike();
                }
                else
                {
                    Ball();
                }
            }
            else if (p == Swings.Fastball)
            {
                if (n < 35)
                {
                    Strike();
                }
                else
                {
                    int hit = random.Next(1, 4);
                    Hit(hit);
                    UpdateFeed(hit);
                }
            }
            else if (p == Swings.BreakingBall)
            {
                if (n < 75)
                {
                    Strike();
                }
                else
                {
                    int hit = random.Next(1, 4);
                    Hit(hit);
                    UpdateFeed(hit);
                }
            }
            else if (p == Swings.OffSpeed)
            {
                if (n < 75)
                {
                    Strike();
                }
                else
                {
                    int hit = random.Next(1, 4);
                    Hit(hit);
                    UpdateFeed(hit);
                }
            }
        }

        public void PitchBB()
        {
            Swings p = OppSwing();
            int n = random.Next(100);
            if (p == Swings.Take)
            {
                if (n < 80)
                {
                    Strike();
                }
                else
                {
                    Ball();
                }
            }
            else if (p == Swings.Fastball)
            {
                if (n < 75)
                {
                    Strike();
                }
                else
                {
                    int hit = random.Next(1, 4);
                    Hit(hit);
                    UpdateFeed(hit);
                }
            }
            else if (p == Swings.BreakingBall)
            {
                if (n < 35)
                {
                    Strike();
                }
                else
                {
                    int hit = random.Next(1, 4);
                    Hit(hit);
                    UpdateFeed(hit);
                }
            }
            else if (p == Swings.OffSpeed)
            {
                if (n < 75)
                {
                    Strike();
                }
                else
                {
                    int hit = random.Next(1, 4);
                    Hit(hit);
                    UpdateFeed(hit);
                }
            }
        }

        public void PitchOS()
        {
            Swings p = OppSwing();
            int n = random.Next(100);
            if (p == Swings.Take)
            {
                if (n < 80)
                {
                    Strike();
                }
                else
                {
                    Ball();
                }
            }
            else if (p == Swings.Fastball)
            {
                if (n < 75)
                {
                    Strike();
                }
                else
                {
                    int hit = random.Next(1, 4);
                    Hit(hit);
                    UpdateFeed(hit);
                }
            }
            else if (p == Swings.BreakingBall)
            {
                if (n < 75)
                {
                    Strike();
                }
                else
                {
                    int hit = random.Next(1, 4);
                    Hit(hit);
                    UpdateFeed(hit);
                }
            }
            else if (p == Swings.OffSpeed)
            {
                if (n < 35)
                {
                    Strike();
                }
                else
                {
                    int hit = random.Next(1, 4);
                    Hit(hit);
                    UpdateFeed(hit);
                }
            }
        }

        public void UpdateFeed(string e)
        {
            string feed;
            feed = Inning.Half.ToString() + " of ";
            feed += Inning.Num.ToString() + ": ";
            feed += e;
            GameFeed.Text = feed + "\n" + GameFeed.Text;
        }

        public void UpdateFeed(int e)
        {
            if(e == 1)
            {
                UpdateFeed("Single");
            }
            else if(e == 2)
            {
                UpdateFeed("Double");
            }
            else if(e == 3)
            {
                UpdateFeed("Triple");
            }
            else if(e == 4)
            {
                UpdateFeed("Homerun");
            }
        }
    }

    public class Inning
    {
        public enum Side { Top, Bottom };

        public int Num { get; private set; } = 1;
        public Side Half { get; private set; } = Side.Top;
        private Team Offense { get; set; }
        private Team Defense { get; set; }

        public Inning() { }

        public static Inning operator ++(Inning i)
        {
            Inning result = new Inning();
            i.Offense = result.Defense;
            i.Defense = result.Offense;

            if (i.Half == Side.Bottom)
            {
                result.Num = i.Num + 1;
                result.Half = Side.Top;
            }

            else
            {
                result.Half = Side.Bottom;
                result.Num = i.Num;
            }

            return result;
        }
    }

    public class Team
    {
        public string City { get; }
        public string Name { get; }
        public int Runs { get; private set; }
        public int Hits { get; private set; }
        public int Errors { get; private set; }
        public List<int> Box { get; set; }
        private int Wins { get; set; } = 0;
        private int Losses { get; set; } = 0;

        public Team(string city, string name)
        {
            this.City = city;
            this.Name = name;
            Box = new List<int>();
        }

        public Team(Team t)
        {
            this.City = t.City;
            this.Name = t.Name;
            Box = new List<int>();
        }

        public void Win()
        {
            Wins++;
        }

        public void Lose()
        {
            Losses++;
        }

        public int GamesPlayed()
        {
            return Wins + Losses;
        }

        public void NewGame()
        {
            Runs = Hits = Errors = 0;
        }

        public void Run(int n)
        {
            Runs++;
            Box[n - 1]++;
        }

        public void Hit()
        {
            Hits++;
        }

        public void Error()
        {
            Errors++;
        }

        public void NextInning()
        {
            Box.Insert(Box.Count(), 0);
        }
    }

    public class Player
    {
        public string Name { get; private set; }
        public int Bat { get; private set; }
        public int Pitch { get; private set; }
        public int Def { get; private set; }

        Player(string Name)
        {
            this.Name = Name;
            //IMPLEMENT RANDOM GENERATOR FOR STATS
        }

        Player(string Name, int Bat, int Pitch, int Def)
        {
            this.Name = Name;
            this.Bat = Bat;
            this.Pitch = Pitch;
            this.Def = Def;
        }
    }

    public class ScoreBoard
    {
        public DataGrid BoxScore = new DataGrid();

        public ScoreBoard() { }

        public ICollection CreateBoxScore(Team Home, Team Away)
        {
            DataTable DT = new DataTable();
            DataRow hr, ar;
            hr = DT.NewRow();
            ar = DT.NewRow();

            DT.Columns.Add(new DataColumn(" ", typeof(String)));

            for (int i = 0; i < Away.Box.Count(); i++)
            {
                DT.Columns.Add(new DataColumn((i + 1).ToString(), typeof(int)));
            }

            DT.Columns.Add(new DataColumn("R", typeof(int)));
            DT.Columns.Add(new DataColumn("H", typeof(int)));
            DT.Columns.Add(new DataColumn("E", typeof(int)));



            ar[" "] = Away.Name;
            ar["R"] = Away.Runs;
            ar["H"] = Away.Hits;
            ar["E"] = Away.Errors;

            hr[" "] = Home.Name;
            hr["R"] = Home.Runs;
            hr["H"] = Home.Hits;
            hr["E"] = Home.Errors;

            for (int i = 0; i < Away.Box.Count(); i++)
            {
                ar[(i + 1).ToString()] = Away.Box[i];
            }

            for (int i = 0; i < Home.Box.Count(); i++)
            {
                hr[(i + 1).ToString()] = Home.Box[i];
            }



            DT.Rows.Add(ar);
            DT.Rows.Add(hr);

            DataView dv = new DataView(DT);
            return dv;
        }
    }

    public class City
    {
        public string CityName { get; private set; }
        public string State { get; private set; }
        public string Division { get; private set; }

        public City()
        {
            CityName = State = Division = "";
        }

        public City(string city, string st, string div)
        {
            CityName = city;
            State = st;
            Division = div;
        }
    }
}
