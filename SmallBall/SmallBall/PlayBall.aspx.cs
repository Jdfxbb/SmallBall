﻿using System;
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
                Game game = new Game(user, opponent);

                //BoxScore.DataSource = CreateBoxScore(user, opponent);
                //BoxScore.DataBind();
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

        public Game(Team HomeTeam, Team AwayTeam)
        {
            this.HomeTeam = HomeTeam;
            this.AwayTeam = AwayTeam;
            Update();
        }

        private void Update()
        {
            DataGrid BoxScore = new DataGrid();
            BoxScore = (DataGrid)BoxScore.FindControl("BoxScore");
            BoxScore.DataSource = CreateBoxScore();
            BoxScore.DataBind();
        }

        private void Strike()
        {
            Strikes++;
            if (Strikes == 3)
            {
                Out();
            }
        }

        private void Ball()
        {
            Balls++;
            if (Balls == 4)
            {
                Walk();
            }
        }

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

        }

        private void ResetCount()
        {
            Strikes = Balls = 0;
        }

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

        private void EndGame()
        {
            if (HomeScore > AwayScore)
            {
                HomeTeam.win();
                AwayTeam.lose();
            }
            else
            {
                AwayTeam.win();
                HomeTeam.lose();
            }
        }

        ICollection CreateBoxScore()
        {
            DataTable DT = new DataTable();
            DataRow hr, ar;
            hr = DT.NewRow();
            ar = DT.NewRow();

            DT.Columns.Add(new DataColumn(" ", typeof(String)));

            for (int i = 1; i < HomeBox.Count() - 2; i++)
            {
                DT.Columns.Add(new DataColumn(i.ToString(), typeof(int)));
            }
            DT.Columns.Add(new DataColumn("R", typeof(int)));
            DT.Columns.Add(new DataColumn("H", typeof(int)));
            DT.Columns.Add(new DataColumn("E", typeof(int)));


            hr[" "] = HomeTeam.Name;
            hr["R"] = HomeBox[0];
            hr["H"] = HomeBox[1];
            hr["E"] = HomeBox[2];

            ar[" "] = AwayTeam.Name;
            ar["R"] = AwayBox[0];
            ar["H"] = AwayBox[1];
            ar["E"] = AwayBox[2];

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

    class Team
    {
        public string City { get; }
        public string Name { get; }
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

        public void win()
        {
            Wins++;
        }

        public void lose()
        {
            Losses++;
        }

        public int gamesPlayed()
        {
            return Wins + Losses;
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
}