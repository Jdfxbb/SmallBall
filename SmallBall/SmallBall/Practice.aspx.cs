/*
TODO
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
using System.Data.SqlClient;


namespace SmallBall
{
    public partial class Practice : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                return;
            }
            System.Web.UI.ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;

            SQL_Connect connect = new SQL_Connect();
            HomeTeamDDL.DataSource = connect.populateTeamNames();
            HomeTeamDDL.DataBind();
            AwayTeamDDL.DataSource = connect.populateTeamNames();
            AwayTeamDDL.DataBind();
        }

        protected void Game_Manager(Game game)
        {
            ScoreBoard s = new ScoreBoard();
            s.BindBoxScore(BoxScore, game.HomeTeam, game.AwayTeam);

            UpdateCount(game);
            
            First.Visible = game.Bases[0];
            Second.Visible = game.Bases[1];
            Third.Visible = game.Bases[2];

            if (game.Inning.Half == Inning.Side.Top)
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


            if (game.GameOver)
            {
                Take.Visible = false;
                GuessBB.Visible = false;
                GuessFB.Visible = false;
                GuessOS.Visible = false;
                PitchOut.Visible = false;
                Fastball.Visible = false;
                OffSpeed.Visible = false;
                BreakingBall.Visible = false;
                StartGame.Visible = true;
                Result.Visible = true;
                Result.Text = "Final Score: " + game.Result;
                First.Visible = Second.Visible = Third.Visible = false;
            }
        }

        protected void NextGame_Click(object sender, EventArgs e)
        {
            HomeTeamDDL.Visible = AwayTeamDDL.Visible = false;
            HomeLabel.Visible = AwayLabel.Visible = false;
            StartGame.Visible = false;
            Result.Visible = false;
            Team Home = new Team(new City(), HomeTeamDDL.SelectedValue);
            Team Away = new Team(new City(), AwayTeamDDL.SelectedValue);
            Game game = new Game(Home, Away);
            game.Initialize();
            Heading.Text = Home.DisplayName;
            Application.Contents.Remove("PracticeGame");
            Application.Contents.Add("PracticeGame", game);
            Game_Manager(game);
        }

        public void Take_Click(object sender, EventArgs e)
        {
            Game game = (Game)Application.Contents.Get("PracticeGame");            
            game.Take();
            Game_Manager(game);
        }

        public void GuessFB_Click(object sender, EventArgs e)
        {
            Game game = (Game)Application.Contents.Get("PracticeGame");
            game.GuessFB();
            Game_Manager(game);
        }

        public void GuessBB_Click(object sender, EventArgs e)
        {
            Game game = (Game)Application.Contents.Get("PracticeGame");
            game.GuessBB();
            Game_Manager(game);
        }

        public void GuessOS_Click(object sender, EventArgs e)
        {
            Game game = (Game)Application.Contents.Get("PracticeGame");
            game.GuessOS();
            Game_Manager(game);
        }

        public void PitchOut_Click(object sender, EventArgs e)
        {
            Game game = (Game)Application.Contents.Get("PracticeGame");
            game.PitchOut();
            Game_Manager(game);
        }

        public void Fastball_Click(object sender, EventArgs e)
        {
            Game game = (Game)Application.Contents.Get("PracticeGame");
            game.PitchFB();
            Game_Manager(game);
        }

        public void OffSpeed_Click(object sender, EventArgs e)
        {
            Game game = (Game)Application.Contents.Get("PracticeGame");
            game.PitchOS();
            Game_Manager(game);
        }

        public void BreakingBall_Click(object sender, EventArgs e)
        {
            Game game = (Game)Application.Contents.Get("PracticeGame");
            game.PitchBB();
            Game_Manager(game);
        }

        protected void UpdateCount(Game game)
        {
            if (Balls.Text != game.Balls.ToString() && game.Balls.ToString() != "0")
            {
                Balls.Text = game.Balls.ToString();
                Balls.BackColor = System.Drawing.Color.LightBlue;
            }
            else
            {
                Balls.Text = game.Balls.ToString();
                Balls.BackColor = System.Drawing.Color.White;
            }

            if (Strikes.Text != game.Strikes.ToString() && game.Strikes.ToString() != "0")
            {
                Strikes.Text = game.Strikes.ToString();
                Strikes.BackColor = System.Drawing.Color.LightBlue;
            }
            else
            {
                Strikes.Text = game.Strikes.ToString();
                Strikes.BackColor = System.Drawing.Color.White;
            }

            Outs.Text = game.Outs.ToString();
            GameFeed.Text = game.GameFeed.Text;
            FireWorks1.Visible = FireWorks2.Visible = FireWorks3.Visible = game.HomeRun;
            game.HomeRun = false;
        }

        protected void Quit_Click(object sender, EventArgs e)
        {
            Server.Transfer("MainMenu.aspx");
        }
    }

    public class Game
    {
        // game proceedings
        public int Balls { get; set; }
        public int Strikes { get; set; }
        public int Outs { get; set; }
        public enum Swings { Take, Fastball, BreakingBall, OffSpeed };
        public enum Pitches { PitchOut, Fastball, BreakingBall, OffSpeed };
        public Swings swings;
        public Pitches pitches;
        public bool GameOver { get; set; } = false;
        public Inning Inning { get; set; } = new Inning();

        // team properties
        public Team[] Teams { get; set; } = new Team[2];
        public Team HomeTeam { get; set; }
        public Team AwayTeam { get; set; }
        public static Random random = new Random();

        // UI properties
        public bool[] Bases { get; set; }
        public int HomeScore { get; set; } = 0;
        public int AwayScore { get; set; } = 0;
        public TextBox GameFeed { get; set; }
        public ScoreBoard BoxScore { get; set; }
        public string Result { get; set; } = "";
        public bool Initialized { get; set; } = false;
        public bool HomeRun { get; set; } = false;

        public Game(Team Home, Team Away)
        {
            HomeTeam = Home;
            AwayTeam = Away;
        }

        public Game()
        {

        }

        public void Initialize()
        {
            HomeTeam.NewGame();
            AwayTeam.NewGame();

            AwayTeam.Box.Insert(0, 0);

            GameFeed = new TextBox();
            GameFeed.Text = "";
            Bases = new bool[3];
            for(int i = 0; i < 3; i++)
            {
                Bases[i] = false;
            }

            Initialized = true;
        }

        public void Strike()
        {
            Strikes++;
            if (Strikes == 3)
            {
                ResetCount();
                UpdateFeed("Strike out");
                Out();
            }
        }

        public void Ball()
        {
            Balls++;
            if (Balls == 4)
            {
                Walk();
            }
        }

        public void Out()
        {
            Outs++;
            ResetCount();
            if (Outs == 3)
            {
                NextInning();
            }
        }

        public void Out(int outType)
        {
            switch (outType)
            {
                case 9: UpdateFeed("Ground out to pitcher"); break;
                case 10: UpdateFeed("Ground out to first"); break;
                case 11: UpdateFeed("Ground out to second"); break;
                case 12: UpdateFeed("Ground out to shortstop"); break;
                case 13: UpdateFeed("Ground out to third"); break;
                case 14: UpdateFeed("Line out to pitcher"); break;
                case 15: UpdateFeed("Line out to first"); break;
                case 16: UpdateFeed("Line out to second"); break;
                case 17: UpdateFeed("Line out to shortstop"); break;
                case 18: UpdateFeed("Line out to third"); break;
                case 19: UpdateFeed("Fly out to shallow right"); break;
                case 20: UpdateFeed("Fly out to shallow center"); break;
                case 21: UpdateFeed("Fly out to shallow left"); break;
                case 22: UpdateFeed("Fly out to deep right"); break;
                case 23: UpdateFeed("Fly out to deep center"); break;
                case 24: UpdateFeed("Fly out to deep left"); break;
                case 25: UpdateFeed("Fly out to catcher"); break;
            }
            Out();
        }

        public void Walk()
        {
            ResetCount();
            UpdateFeed("Walk");
            AdvanceBases(1);
        }

        public void Hit(int hitSuccess)
        {
            if (hitSuccess < 9)
            {
                int hitType = random.Next(101);
                if(hitType < 65)
                {
                    AdvanceBases(1);
                    UpdateFeed(1);
                }
                else if(hitType < 85)
                {
                    AdvanceBases(2);
                    UpdateFeed(2);
                }
                else if(hitType < 90)
                {
                    AdvanceBases(3);
                    UpdateFeed(3);
                }
                else
                {
                    AdvanceBases(4);
                    UpdateFeed(4);
                }

                if (Inning.Half == Inning.Side.Top)
                {
                    AwayTeam.Hit();
                }
                else
                {
                    HomeTeam.Hit();
                }
            }
            else
            {
                Out(hitSuccess);
            }
            ResetCount();
        }

        private void ResetCount()
        {
            Strikes = Balls = 0;
        }

        private void NextInning()
        {
            if (HomeScore > AwayScore && Inning.Num >= 9)
            {
                EndGame();
                return;
            }
            else if (AwayScore > HomeScore && Inning.Num >= 9 && Inning.Half == Inning.Side.Bottom)
            {
                EndGame();
                return;
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

        private void EndGame()
        {
            if (HomeTeam.Runs > AwayTeam.Runs)
            {
                HomeTeam.Win();
                AwayTeam.Lose();
            }
            else
            {
                AwayTeam.Win();
                HomeTeam.Lose();
            }
            GameOver = true;
            Result = AwayTeam.DisplayName + " " + AwayScore + " - " + HomeScore + " " + HomeTeam.DisplayName + '\n';
            GameFeed.Text = "";
        }

        public void CheckEndGame()
        {
            if(Inning.Num >= 9 && Inning.Half == Inning.Side.Bottom && HomeScore > AwayScore)
            {
                EndGame();
            }
        }

        public void AdvanceBases(int n)
        {
            int runsScored = 0;
            for (int i = 0; i < n; i++)
            {
                //if runner on third, clear third, score run
                if (Bases[2] == true)
                {
                    Bases[2] = false;
                    Run();
                    runsScored++;
                }
                // if runner on second, runner on third, clear second
                if (Bases[1] == true)
                {
                    Bases[1] = false;
                    Bases[2] = true;
                }
                // if runner on first, clear first, runner on second
                if (Bases[0] == true)
                {
                    Bases[0] = false;
                    Bases[1] = true;
                }
                // runner starts at first
                if (i == 0)
                {
                    Bases[0] = true;
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
                Bases[i] = false;
            }
        }

        private void Run()
        {
            if(Inning.Half == Inning.Side.Top)
            {
                AwayTeam.Run(Inning.Num);
                AwayScore++;
            }
            else
            {
                HomeTeam.Run(Inning.Num);
                HomeScore++;
                CheckEndGame();
            }
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
                    int hit = random.Next(25);
                    Hit(hit);
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
                    int hit = random.Next(25);
                    Hit(hit);
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
                    int hit = random.Next(25);
                    Hit(hit);
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
                    int hit = random.Next(25);
                    Hit(hit);
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
                    int hit = random.Next(25);
                    Hit(hit);
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
                    int hit = random.Next(25);
                    Hit(hit);
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
                    int hit = random.Next(25);
                    Hit(hit);
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
                    int hit = random.Next(25);
                    Hit(hit);
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
                    int hit = random.Next(25);
                    Hit(hit);
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
                    int hit = random.Next(25);
                    Hit(hit);
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
                    int hit = random.Next(25);
                    Hit(hit);
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
                    int hit = random.Next(25);
                    Hit(hit);
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
                    int hit = random.Next(25);
                    Hit(hit);
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
                    int hit = random.Next(25);
                    Hit(hit);
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
                    int hit = random.Next(25);
                    Hit(hit);
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
                    int hit = random.Next(25);
                    Hit(hit);
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
                    int hit = random.Next(25);
                    Hit(hit);
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
                    int hit = random.Next(25);
                    Hit(hit);
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
                    int hit = random.Next(25);
                    Hit(hit);
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
                    int hit = random.Next(25);
                    Hit(hit);
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
                    int hit = random.Next(25);
                    Hit(hit);
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
                    int hit = random.Next(25);
                    Hit(hit);
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
                    int hit = random.Next(25);
                    Hit(hit);
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
                    int hit = random.Next(25);
                    Hit(hit);
                }
            }
        }

        public void UpdateFeed(string e)
        {
            if(GameFeed == null)
            {
                GameFeed = new TextBox { Text = "" };
            }
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
                UpdateFeed("HOMERUN");
                HomeRun = true;
            }
        }

        public void Sim()
        {
            Initialize();
            while (!GameOver)
            {
                switch (random.Next(3))
                {
                    case 0: Take(); break;
                    case 1: GuessBB(); break;
                    case 2: GuessFB(); break;
                    case 3: GuessOS(); break;
                }
            }
        }
    }

    public class Inning
    {
        public enum Side { Top, Bottom };

        public int Num { get; set; } = 1;
        public Side Half { get; set; } = Side.Top;
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
        public City TeamCity { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public int Runs { get; set; }
        public int Hits { get; set; }
        public int Errors { get; set; }
        public List<int> Box { get; set; }
        public int Wins { get; set; } = 0;
        public int Losses { get; set; } = 0;

        public Team(City city, string name)
        {
            TeamCity = new City(city.CityName, city.State, city.Division);
            Name = name;
            DisplayName = TeamCity.CityName + " " + name;
            Box = new List<int>();
        }

        public Team(Team t)
        {
            TeamCity = t.TeamCity;
            this.Name = t.Name;
            Box = new List<int>();
        }

        public Team()
        {
            TeamCity = new City();
            DisplayName = TeamCity.CityName + " " + Name;
            Box = new List<int>();
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            Team t = (Team)obj;
            return DisplayName == t.DisplayName;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
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

        public string WinPct()
        {
            if (GamesPlayed() == 0 || Wins == 0)
            {
                return ".000";
            }
            else if (Losses == 0)
            {
                return "1.000";
            }
            else
            {
                decimal pct = (decimal)Wins / (decimal)GamesPlayed();
                string result = pct.ToString(".000");
                return result;
            }
        }

        public void NewGame()
        {
            Runs = Hits = Errors = 0;
            Box.Clear();
        }

        public bool PlaysIn(Game game)
        {
            return Equals(game.HomeTeam) || Equals(game.AwayTeam);
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

    public class ScoreBoard
    {
        public DataGrid BoxScore = new DataGrid();

        public ScoreBoard() { }

        public void BindBoxScore(Control c, Team Home, Team Away)
        {
            ScoreBoard s = new ScoreBoard();
            DataGrid Board = (DataGrid)c;
            Board.DataSource = s.CreateBoxScore(Home, Away);
            Board.DataBind();
        }

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



            ar[" "] = Away.DisplayName;
            ar["R"] = Away.Runs;
            ar["H"] = Away.Hits;
            ar["E"] = Away.Errors;

            hr[" "] = Home.DisplayName;
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
        public string CityName { get; set; }
        public string State { get; set; }
        public string Division { get; set; }
        public string StringID { get; set; }

        public City()
        {
            CityName = State = Division = "";
        }

        public City(string city, string st, string div)
        {
            CityName = city;
            State = st;
            Division = div;
            StringID = CityName + "," + State + "," + Division;
        }

        public override string ToString()
        {
            return CityName + " " + State;
        }
    }

    public class Standings
    {
        public DataGrid StandingTable = new DataGrid();
        public Team UserTeam;

        public Standings() { }

        public void Create(Control c, List<Team> Teams)
        {
            List<Team> SortedTeams = Teams.OrderByDescending(o => o.WinPct()).ToList();
            Standings s = new Standings();
            DataGrid Grid = (DataGrid)c;
            Grid.DataSource = s.CreateStandings(SortedTeams);
            Grid.DataBind();
        }

        public ICollection CreateStandings(List<Team> Teams)
        {
            DataTable DT = new DataTable();
            DataRow r = DT.NewRow();
            

            DT.Columns.Add(new DataColumn("Team", typeof(string)));
            DT.Columns.Add(new DataColumn("W", typeof(int)));
            DT.Columns.Add(new DataColumn("L", typeof(int)));
            DT.Columns.Add(new DataColumn("Win%", typeof(string)));

            for (int i = 0; i < Teams.Count(); i++)
            {
                r["Team"] = Teams[i].DisplayName;
                r["W"] = Teams[i].Wins;
                r["L"] = Teams[i].Losses;
                r["Win%"] = Teams[i].WinPct();
                DT.Rows.Add(r);
                r = DT.NewRow();
            }

            DataView dv = new DataView(DT);
            return dv;
        }

    }

    class SQL_Connect
    {
        private SqlConnection connection;

        public SQL_Connect()
        {
            Initialize();
        }

        private void Initialize()
        {
            connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Joshua\\source\\repos\\SmallBall\\SmallBall\\SmallBall\\App_Data\\SmallBallDB.mdf;Integrated Security=True;Connect Timeout=30");
            test();
        }

        private bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (SqlException exception)
            {
                System.Diagnostics.Debug.WriteLine(exception.Message);
                return false;
            }
        }

        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (SqlException exception)
            {
                System.Diagnostics.Debug.WriteLine(exception.Message);
                return false;
            }
        }

        public void test()
        {
            OpenConnection();
            CloseConnection();
        }

        public string RandomTeamName()
        {
            string query = "SELECT Name from TeamNames ORDER BY RAND() LIMIT 1";
            if (OpenConnection())
            {
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader dataReader = command.ExecuteReader();
                dataReader.Read();
                string result = (string)dataReader["Name"];
                CloseConnection();
                dataReader.Close();
                return result;
            }
            else
            {
                return "";
            }
        }

        public City RandomCity()
        {
            string query = "SELECT * from Cities ORDER BY RAND() LIMIT 1";
            City result = new City();

            if (OpenConnection())
            {
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader dataReader = command.ExecuteReader();
                dataReader.Read();
                string c = (string)dataReader["City"];
                string st = (string)dataReader["State"];
                string div = (string)dataReader["Division"];
                dataReader.Close();
                CloseConnection();
                result = new City(c, st, div);
                return result;
            }
            else
            {
                return result;
            }
        }

        public List<string> populateTeamNames()
        {
            List<string> teams = new List<string>();
            string query = "SELECT * FROM TeamNames ORDER BY Name";
            if (OpenConnection())
            {
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    teams.Add((string)dataReader["Name"]);
                }
                dataReader.Close();
                CloseConnection();
                return teams;
            }
            else
            {
                return teams;
            }
        }

        public List<City> populateCities()
        {
            List<City> cities = new List<City>();
            string query = "SELECT * FROM Cities ORDER BY State";
            if (OpenConnection())
            {
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader dataReader = command.ExecuteReader();
                while(dataReader.Read())
                {
                    string c = (string)dataReader["City"];
                    string st = (string)dataReader["State"];
                    string div = (string)dataReader["Division"];
                    cities.Add(new City(c, st, div));
                }
                dataReader.Close();
                CloseConnection();
                return cities;
            }
            else
            {
                return cities;
            }
        }

        public string Login(string UserName, string Password)
        {
            if (OpenConnection())
            {
                string s = "DECLARE @result NVARCHAR(250) EXEC Login @Id='" + UserName + "', @Password='" + Password + "'";
                SqlCommand command = new SqlCommand(s, connection);
                if (command.ExecuteScalar() == null)
                {
                    return "Invalid Login";
                }
                else
                {
                    return "Login Successful";
                }
            }
            else
            {
                return "Failed to connect to server";
            }
        }

        public string Register(string UserName, string Password, string Email)
        {
            if (OpenConnection())
            {
                string s = "EXEC AddUser @Id='" + UserName + "',@Password='" + Password + "',@Email='" + Email + "'";
                SqlCommand command = new SqlCommand(s, connection);
                try
                {
                    command.ExecuteNonQuery();
                    return "Registration successful";
                }
                catch (SqlException exception)
                {
                    switch (exception.Number)
                    {
                        case 2627: return "Username already exists";
                        default: return exception.Message;
                    }
                }
            }
            else
            {
                return "Could not connect to server";
            }
        }

        public List<List<Team>> BuildLeagues()
        {
            List<List<Team>> Leagues = new List<List<Team>>();
            List<Team> East = new List<Team>();
            List<Team> West = new List<Team>();
            List<Team> Cal = new List<Team>();
            List<Team> Des = new List<Team>();
            List<Team> Mtn = new List<Team>();
            List<Team> South = new List<Team>();
            List<Team> North = new List<Team>();
            List<Team> Tex = new List<Team>();
            List<string> TeamNames = new List<string>();
            Random random = new Random();
            if (OpenConnection())
            {
                string queryCities = "SELECT * FROM Cities ORDER BY Division";
                string queryTeamNames = "SELECT* FROM TeamNames";
                SqlCommand command = new SqlCommand(queryTeamNames, connection);
                SqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    TeamNames.Add((string)dataReader["Name"]);
                }
                dataReader.Close();

                command = new SqlCommand(queryCities, connection);
                dataReader = command.ExecuteReader();


                while (dataReader.Read())
                {
                    switch ((string)dataReader["Division"])
                    {
                        case "East": East.Add(new Team(new City((string)dataReader["City"], (string)dataReader["State"], (string)dataReader["Division"]), TeamNames[random.Next(TeamNames.Count())])); break;
                        case "West": West.Add(new Team(new City((string)dataReader["City"], (string)dataReader["State"], (string)dataReader["Division"]), TeamNames[random.Next(TeamNames.Count())])); break;
                        case "California": Cal.Add(new Team(new City((string)dataReader["City"], (string)dataReader["State"], (string)dataReader["Division"]), TeamNames[random.Next(TeamNames.Count())])); break;
                        case "Desert": Des.Add(new Team(new City((string)dataReader["City"], (string)dataReader["State"], (string)dataReader["Division"]), TeamNames[random.Next(TeamNames.Count())])); break;
                        case "Texas": Tex.Add(new Team(new City((string)dataReader["City"], (string)dataReader["State"], (string)dataReader["Division"]), TeamNames[random.Next(TeamNames.Count())])); break;
                        case "Mountain": Mtn.Add(new Team(new City((string)dataReader["City"], (string)dataReader["State"], (string)dataReader["Division"]), TeamNames[random.Next(TeamNames.Count())])); break;
                        case "South": South.Add(new Team(new City((string)dataReader["City"], (string)dataReader["State"], (string)dataReader["Division"]), TeamNames[random.Next(TeamNames.Count())])); break;
                        case "North": North.Add(new Team(new City((string)dataReader["City"], (string)dataReader["State"], (string)dataReader["Division"]), TeamNames[random.Next(TeamNames.Count())])); break;
                    }
                }
                Leagues.Add(Cal);
                Leagues.Add(Des);
                Leagues.Add(East);
                Leagues.Add(Mtn);
                Leagues.Add(North);
                Leagues.Add(South);
                Leagues.Add(Tex);
                Leagues.Add(West);
                
                dataReader.Close();
                connection.Close();
                return Leagues;
            }
            return Leagues;
        }
    }
}
