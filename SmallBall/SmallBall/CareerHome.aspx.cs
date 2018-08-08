using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.ApplicationServices;
using System.Data;
using System.Collections;

namespace SmallBall
{
    public partial class CareerHome : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                return;
            }

            bool Loaded = (Application.Contents.Get("CurrentGame") != null);

            if(!Loaded)
            {
                // if this is a new career, follow initialization steps
                BuildCareer();
            }

            // pull items from application.contents
            List<List<Team>> League = (List<List<Team>>)Application.Contents.Get("League");
            Team UserTeam = (Team)Application.Contents.Get("UserTeam");
            int LastPlayed = (int)Application.Contents.Get("LastPlayed");
            List<Team> Teams = BuildTeamList(League);
            Application.Contents.Add("Teams", Teams);

            if (Loaded)
            {
                // if this is a loaded career, get current game 
                List<Game> LoadedGames = GenerateSchedule(League, 2);
                Application.Contents.Add("Games", LoadedGames);
                Game CurrentGame = (Game)Application.Contents.Get("CurrentGame");
                LoadedGames[LastPlayed] = CurrentGame;
            }

            // build/update standings 
            List<Game> Games = (List<Game>)Application.Contents.Get("Games");
            CreateDivisions(UserTeam.TeamCity.Division);
            UpdateDivisionStandings();

            // pass game to game manager
            Heading.Text = UserTeam.DisplayName;
            Game_Manager(Games[LastPlayed]);
        }

        protected void BuildCareer()
        {
            // build the randomized league
            Team UserTeam = (Team)Application.Contents.Get("UserTeam");
            List<Game> Games = new List<Game>();
            List<List<Team>> League = BuildLeague();

            // insert user selected team into league
            AddUserTeam(League, UserTeam);
            Games = GenerateSchedule(League, 2);
            int lastPlayed = SimGames(UserTeam, Games, -1);

            // send everything to application.contents
            Application.Contents.Add("Games", Games);
            Application.Contents.Add("League", League);
            Application.Contents.Add("LastPlayed", lastPlayed);
        }

        protected void UpdateStandings(List<Team> Teams)
        {
            // regenerate standings table
            Standings s = new Standings();
            s.Create(StandingTable, Teams);
        }

        protected List<Game> GenerateSchedule(List<List<Team>> league, int Rounds)
        {
            List<Game> Schedule = new List<Game>();
            Team[] first, second;

            // number of rounds (how many games vs each team)
            for (int n = 0; n < Rounds; n++)
            {
                // each division will play within their division
                for (int i = 0; i < league.Count(); i++)
                {
                    // split division into two arrays
                    int mid = league[i].Count() / 2;
                    first = new Team[mid];
                    second = new Team[mid];
                    for (int j = 0; j < mid; j++)
                    {
                        first[j] = league[i][j];
                    }
                    for (int k = mid; k < league[i].Count(); k++)
                    {
                        second[k - mid] = league[i][k];
                    }
                    // iterations will always be count -1 (i.e teams don't play themselves)
                    for (int p = 0; p < league[i].Count() - 1; p++)
                    {
                        // parallel teams are paired up each round
                        for (int m = 0; m < first.Count(); m++)
                        {
                            if (n % 2 == 0)
                            {
                                Schedule.Add(new Game(first[m], second[m]));
                            }
                            else
                            {
                                Schedule.Add(new Game(second[m], first[m]));
                            }   
                        }
                        // 'rotates' the arrays
                        Rotate(first, second);
                    }
                }
            }
            return Schedule;
        }

        protected void Rotate(Team[] first, Team[] second)
        {
            // last element of first is shifted to second
            Team temp = first[first.Count() - 1];
            // all elements of first (except 0) are shifted up
            for (int i = first.Count() - 1; i > 0; i--)
            {
                first[i] = first[i - 1];
            }
            // element 0 of second become element 1 of first
            first[1] = second[0];
            // all elements of second are shifted down
            for (int j = 1; j < second.Count(); j++)
            {
                second[j - 1] = second[j];
            }
            //last elemnt of first becomes last element of second
            second[second.Count() - 1] = temp;
        }

        protected void Divisions_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateDivisionStandings();
        }

        protected void UpdateDivisionStandings()
        {
            // this function updates the standings while keeping the currently selected division in focus
            List<List<Team>> league = (List<List<Team>>)Application.Contents.Get("League");

            switch (Divisions.Text)
            {
                case "California": UpdateStandings(league[0]); break;
                case "Desert": UpdateStandings(league[1]); break;
                case "East": UpdateStandings(league[2]); break;
                case "Mountain": UpdateStandings(league[3]); break;
                case "North": UpdateStandings(league[4]); break;
                case "South": UpdateStandings(league[5]); break;
                case "Texas": UpdateStandings(league[6]); break;
                case "West": UpdateStandings(league[7]); break;
                case "All":
                    List<Team> teams = new List<Team>();
                    for (int i = 0; i < league.Count(); i++)
                    {
                        for (int j = 0; j < league[i].Count(); j++)
                        {
                            teams.Add(league[i][j]);
                        }
                    }
                    UpdateStandings(teams); break;
            }
        }

        protected List<List<Team>> BuildLeague()
        {
            // pull cities and team names from server and randomly generate league
            SQL_Connect connect = new SQL_Connect();
            List<List<Team>> league = connect.BuildLeagues();
            Application.Contents.Add("league", league);
            return league;
        }

        protected void CreateDivisions(string UserDivision)
        {
            // generate divisions for dropdown list
            List<string> d = new List<string> { "California", "Desert", "East", "Mountain", "North", "South", "Texas", "West", "All" };

            Divisions.DataSource = d;
            Divisions.DataBind();
        }

        protected void AddUserTeam(List<List<Team>> league, Team UserTeam)
        {
            // replace generated team with user's created team
            for (int i = 0; i < league.Count(); i++)
            {
                for (int j = 0; j < league[i].Count(); j++)
                {
                    if (league[i][j].TeamCity.CityName == UserTeam.TeamCity.CityName && league[i][j].TeamCity.State == UserTeam.TeamCity.State)
                    {
                        league[i][j] = UserTeam;
                        return;
                    }
                }
            }
        }

        protected void Game_Manager(Game game)
        {
            // Game_Manager is the main communication between the game/team objects and the UI
            // it is responsible for keeping the UI up to date to reflect events related to the objects
            // the game object is passed back to Game_Manager after every game event(button click)

            if (!game.Initialized)
            {
                game.Initialize();
            }

            // pull elements from application.contents
            int lastPlayed = (int)Application.Contents.Get("LastPlayed");
            List<Game> Games = (List<Game>)Application.Contents.Get("Games");
            List<Team> Teams = (List<Team>)Application.Contents.Get("Teams");
            Team UserTeam = (Team)Application.Contents.Get("UserTeam");

            // build score board
            ScoreBoard s = new ScoreBoard();
            s.BindBoxScore(BoxScore, game.HomeTeam, game.AwayTeam);

            // provides visual cues for in-game count changes
            UpdateCount(game);

            // updates baserunners
            First.Visible = game.Bases[0];
            Second.Visible = game.Bases[1];
            Third.Visible = game.Bases[2];

            // only show buttons relevant to user
            UpdateButtons(game);

            // clean up finished game
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
                NextGame.Visible = true;
                Result.Visible = true;
                Result.Text = "Final Score: " + game.Result;
                First.Visible = Second.Visible = Third.Visible = false;

                UpdateDivisionStandings();
                GameResults.Text = game.Result + GameResults.Text;
            }
        }

        protected void UpdateButtons(Game game)
        {
            // hide offense buttons if user is pitching etc.
            Team UserTeam = (Team)Application.Contents.Get("UserTeam");
            if (game.HomeTeam.Equals(UserTeam))
            {
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
            }
            else if (game.AwayTeam.Equals(UserTeam))
            {
                if (game.Inning.Half == Inning.Side.Bottom)
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
        }

        protected void UpdateCount(Game game)
        {
            // highlight textbox that changes
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

        protected int SimGames(Team UserTeam, List<Game> Games, int LastPlayed)
        {
            // simulate up to next user game
            for(int i = LastPlayed + 1; i < Games.Count(); i++)
            {
                if (Games[i].GameOver)
                {
                    continue;
                }
                else if (UserTeam.PlaysIn(Games[i]))
                {
                    return i;
                }
                else
                {
                    Games[i].Sim();
                    GameResults.Text = Games[i].Result + GameResults.Text;
                }
            }
            return Games.Count();
        }

        protected void StartGame_Click(object sender, EventArgs e)
        {
            // initate game process
            Team UserTeam = (Team)Application.Contents.Get("User");
            List<Game> games = (List<Game>)Application.Contents.Get("Games");
            int lastPlayed = (int)Application.Contents.Get("LastPlayed");
            Game_Manager(games[SimGames(UserTeam, games, lastPlayed)]);
        }

        protected List<Team> BuildTeamList(List<List<Team>> league)
        {
            List<Team> teams = new List<Team>();
            for(int i = 0; i < league.Count(); i++)
            {
                for(int j = 0; j < league[i].Count(); j++)
                {
                    teams.Add(league[i][j]);
                }
            }
            return teams;
        }
        
        //button events

        protected void SaveGame_Click(object sender, EventArgs e)
        {
            List<object> SavedList = new List<object>();
            List<Game> games = (List<Game>)Application.Contents.Get("Games");
            string UserName = (string)Application.Contents.Get("UserName");
            int LastPlayed = (int)Application.Contents.Get("LastPlayed");

            List<List<Team>> League = (List<List<Team>>)Application.Contents.Get("League");
            Team UserTeam = (Team)Application.Contents.Get("UserTeam");
            Game CurrentGame = games[LastPlayed];

            System.Web.Script.Serialization.JavaScriptSerializer JS_Serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            string S_League = JS_Serializer.Serialize(League);
            System.Diagnostics.Debug.WriteLine(S_League);
            string S_UserTeam = JS_Serializer.Serialize(UserTeam);
            string S_LastPlayed = JS_Serializer.Serialize(LastPlayed);
            string S_CurrentGame = JS_Serializer.Serialize(CurrentGame);

            Page.ClientScript.RegisterStartupScript(this.GetType(), "league", "Insert(\"League" + UserName + "\"," + S_League + ");", true);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "userteam", "Insert(\"UserTeam" + UserName + "\"," + S_UserTeam + ");", true);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "lastplayed", "Insert(\"LastPlayed" + UserName + "\"," + S_LastPlayed + ");", true);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "currentgame", "Insert(\"CurrentGame" + UserName + "\"," + S_CurrentGame + ");", true);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "sucess", "Success();", true);
        }

        protected void Quit_Click(object sender, EventArgs e)
        {
            Server.Transfer("MainMenu.aspx");
        }

        protected void NextGame_Click(object sender, EventArgs e)
        {
            Result.Visible = false;
            NextGame.Visible = false;
            int lastPlayed = (int)Application.Contents.Get("LastPlayed");
            List<Game> Games = (List<Game>)Application.Contents.Get("Games");
            Team UserTeam = (Team)Application.Contents.Get("UserTeam");

            lastPlayed = SimGames(UserTeam, Games, lastPlayed);
            Application.Contents.Set("LastPlayed", lastPlayed);

            Game_Manager(Games[lastPlayed]);
        }

        public void Take_Click(object sender, EventArgs e)
        {
            int lastPlayed = (int)Application.Contents.Get("LastPlayed");
            List<Game> Games = (List<Game>)Application.Contents.Get("Games");
            Games[lastPlayed].Take();
            Game_Manager(Games[lastPlayed]);
        }

        public void GuessFB_Click(object sender, EventArgs e)
        {
            int lastPlayed = (int)Application.Contents.Get("LastPlayed");
            List<Game> Games = (List<Game>)Application.Contents.Get("Games");
            Games[lastPlayed].GuessFB();
            Game_Manager(Games[lastPlayed]);
        }

        public void GuessBB_Click(object sender, EventArgs e)
        {
            int lastPlayed = (int)Application.Contents.Get("LastPlayed");
            List<Game> Games = (List<Game>)Application.Contents.Get("Games");
            Games[lastPlayed].GuessBB();
            Game_Manager(Games[lastPlayed]);
        }

        public void GuessOS_Click(object sender, EventArgs e)
        {
            int lastPlayed = (int)Application.Contents.Get("LastPlayed");
            List<Game> Games = (List<Game>)Application.Contents.Get("Games");
            Games[lastPlayed].GuessOS();
            Game_Manager(Games[lastPlayed]);
        }

        public void PitchOut_Click(object sender, EventArgs e)
        {
            int lastPlayed = (int)Application.Contents.Get("LastPlayed");
            List<Game> Games = (List<Game>)Application.Contents.Get("Games");
            Games[lastPlayed].PitchOut();
            Game_Manager(Games[lastPlayed]);
        }

        public void Fastball_Click(object sender, EventArgs e)
        {
            int lastPlayed = (int)Application.Contents.Get("LastPlayed");
            List<Game> Games = (List<Game>)Application.Contents.Get("Games");
            Games[lastPlayed].PitchFB();
            Game_Manager(Games[lastPlayed]);
        }

        public void OffSpeed_Click(object sender, EventArgs e)
        {
            int lastPlayed = (int)Application.Contents.Get("LastPlayed");
            List<Game> Games = (List<Game>)Application.Contents.Get("Games");
            Games[lastPlayed].PitchOS();
            Game_Manager(Games[lastPlayed]);
        }

        public void BreakingBall_Click(object sender, EventArgs e)
        {
            int lastPlayed = (int)Application.Contents.Get("LastPlayed");
            List<Game> Games = (List<Game>)Application.Contents.Get("Games");
            Games[lastPlayed].PitchBB();
            Game_Manager(Games[lastPlayed]);
        }
    }
}