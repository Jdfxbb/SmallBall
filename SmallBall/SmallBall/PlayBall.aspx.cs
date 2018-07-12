using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;

namespace SmallBall
{
    public partial class PlayBall : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void Take_Click(object sender, EventArgs e)
        {
            
        }

        protected void GuessFB_Click(object sender, EventArgs e)
        {
            
        }

        protected void GuessBB_Click(object sender, EventArgs e)
        {
            
        }

        protected void GuessOS_Click(object sender, EventArgs e)
        {
            
        }

        protected void NewGame_Click(object sender, EventArgs e)
        {
            if (TeamName.Text != "")
            {
                TeamName.Visible = false;
                Team user = new Team("Kansas City", TeamName.Text);
                Team opponent = new Team("Oakland", "Knights");
                List<int> HomeScore = new List<int>();
                Game game = new Game(ref user, ref opponent);
            }
            else
            {
                TeamName.Text = "Enter Team Name";
            }
        }

        protected void TeamName_TextChanged(object sender, EventArgs e)
        {

        }
    }

    class Game
    {
        private Team HomeTeam { get; }
        private Team AwayTeam { get; }
        private Inning Inning { get; set; }
        private List<int> HomeBox { get; set; }
        private List<int> AwayBox { get; set; }
        private int HomeScore { get; set; }
        private int AwayScore { get; set; }
        private int Balls { get; set; }
        private int Strikes { get; set; }
        private int Outs { get; set; }
        RadioButton First, Second, Third;
        private RadioButton[] Bases;

        // Initialize game
        public Game(ref Team HomeTeam, ref Team AwayTeam)
        {
            HomeTeam.NewGame();
            AwayTeam.NewGame();
            
            




            Update();

            First = (RadioButton)First.FindControl("First");
            Second = (RadioButton)Second.FindControl("Second");
            Third = (RadioButton)Third.FindControl("Third");
            
            Bases[0] = First;
            Bases[1] = Second;
            Bases[2] = Third;

        }

        // Update BoxScore
        private void Update()
        {
            DataGrid BoxScore = new DataGrid();
            BoxScore = (DataGrid)BoxScore.FindControl("BoxScore");
            BoxScore.DataSource = CreateBoxScore();
            BoxScore.DataBind();
        }

        //Strike pitched
        private void Strike()
        {
            Strikes++;
            if (Strikes == 3)
            {
                Out();
            }
        }

        // Ball pitched
        private void Ball()
        {
            Balls++;
            if (Balls == 4)
            {
                Walk();
            }
        }

        // Out made
        private void Out()
        {
            Outs++;
            if (Outs == 3)
            {
                NextInning();
            }
        }

        private void Walk()
        {
            AdvanceBases(1);
        }

        private void Hit(int n)
        {
            AdvanceBases(n);
        }

        // Reset count for next batter
        private void ResetCount()
        {
            Strikes = Balls = 0;
        }

        // reset inning to start new one, check winning conditions
        private void NextInning()
        {
            if (HomeScore > AwayScore && Inning.Num >= 9 && Inning.Half == Inning.Side.Top)
            {
                EndGame();
            }
            else if (AwayScore > HomeScore && Inning.Num >= 9 && Inning.Half == Inning.Side.Bottom)
            {
                EndGame();
            }
            else
            {
                Inning++;
            }
            Outs = 0;
            ResetCount();
        }

        // assign win/ loss to team
        private void EndGame()
        {
            if (HomeScore > AwayScore)
            {
                HomeTeam.Win();
                AwayTeam.Lose();
            }
            else
            {
                AwayTeam.Win();
                HomeTeam.Lose();
            }
        }

        // advance n bases; 0 < n < 5
        private void AdvanceBases(int n)
        {
            for (int i = 0; i < n; i++)
            {
                if(Bases[2].Checked == true)
                {
                    Bases[2].Checked = false;
                    Run();
                }
                if(Bases[1].Checked == true)
                {
                    Bases[1].Checked = false;
                    Bases[2].Checked = true;
                }
                if (Bases[0].Checked == true)
                {
                    Bases[1].Checked = true;
                }
            }

        }

        // assign run to team
        private void Run()
        {
            if(Inning.Half == Inning.Side.Bottom)
            {
                HomeScore++;
                HomeBox[Inning.Num]++;
            }
            else
            {
                AwayScore++;
                AwayBox[Inning.Num]++;
            }
        }

        ICollection CreateBoxScore()
        {
            DataTable DT = new DataTable();
            DataRow hr, ar;
            hr = DT.NewRow();
            ar = DT.NewRow();

            DT.Columns.Add(new DataColumn(" ", typeof(String)));

            for (int i = 0; i < HomeBox.Count(); i++)
            {
                DT.Columns.Add(new DataColumn(i.ToString() + 1, typeof(int)));
            }

            DT.Columns.Add(new DataColumn("R", typeof(int)));
            DT.Columns.Add(new DataColumn("H", typeof(int)));
            DT.Columns.Add(new DataColumn("E", typeof(int)));

            hr[" "] = HomeTeam.Name;
            hr["R"] = HomeTeam.Runs;
            hr["H"] = HomeTeam.Hits;
            hr["E"] = HomeTeam.Errors;

            ar[" "] = AwayTeam.Name;
            ar["R"] = AwayTeam.Runs;
            ar["H"] = AwayTeam.Hits;
            ar["E"] = AwayTeam.Errors;

            for (int i = 3; i < Math.Max(HomeBox.Count(), AwayBox.Count()); i++)
            {
                hr[i - 2] = HomeBox[i];
                ar[i - 2] = AwayBox[i];
            }

            DT.Rows.Add(ar);
            DT.Rows.Add(hr);

            DataView dv = new DataView(DT);
            return dv;
        }

    }

    class Inning
    {
        public enum Side { Top, Bottom };

        public int Num { get; private set; } = 1;
        public Side Half { get; private set; } = Side.Top;
        private Team Offense { get; set; }
        private Team Defense { get; set; }

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
            }

            return result;
        }


    }

    class Team
    {
        public string City { get; }
        public string Name { get; }
        public int Runs { get; private set; }
        public int Hits { get; private set; }
        public int Errors { get; private set; }
        private int Wins { get; set; } = 0;
        private int Losses { get; set; } = 0;
       
        

        public Team()
        {
            this.City = this.Name = "";
        }

        public Team(string city, string name)
        {
            this.City = city;
            this.Name = name;
        }

        public Team(Team t)
        {
            this.City = t.City;
            this.Name = t.Name;
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
    }

    class Player
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
}