using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Football.Data;
using Football.Engine;

namespace FootballGUI
{
    public partial class TeamStats : Form
    {
        private Team team = null;
        private List<Player> playersShown = new List<Player>();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="team">Team</param>
        public TeamStats(Team team)
        {
            InitializeComponent();
            this.team = team;
            Text = this.team.Name;
            LoadTeam();
        }

        /// <summary>
        /// 
        /// </summary>
        private void LoadTeam()
        {
            for (int i = 0; i < (int)StatTypes.Unknown; ++i)
                comboBox1.Items.Add((StatTypes)i);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listView1.Clear();
            playersShown.Clear();
           
            SetListView((StatTypes)comboBox1.SelectedItem,team.TeamOffense);
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="statType">StatTypes</param>
        /// <param name="teamOffense">Offense</param>
        private void SetListView(StatTypes statType,Offense teamOffense)
        {
            switch (statType)
            {
                case StatTypes.Defense:
                    break;
                case StatTypes.Kicking:
                    LoadKickingStats(teamOffense);
                    break;
                case StatTypes.Pass:
                    LoadPassStats(teamOffense);
                    break;
                case StatTypes.Receive:
                    LoadReceiveStats(teamOffense);
                    break;
                case StatTypes.Return:
                    LoadRunStats(teamOffense, statType);
                    break;
                case StatTypes.Run:
                    LoadRunStats(teamOffense,statType);
                    break;
                case StatTypes.Unknown:
                    break;
                case StatTypes.WonLoss:
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="teamOffense">Offense</param>
        private void LoadKickingStats(Offense teamOffense)
        {
            listView1.Columns.Add("Player");
            listView1.Columns.Add("xpa");
            listView1.Columns.Add("xpm");
            listView1.Columns.Add("pct");
            listView1.Columns.Add("fga");
            listView1.Columns.Add("fgm");
            listView1.Columns.Add("pct");
            listView1.Columns.Add("lg");

            Player[] players = teamOffense.GetPlayers();

            for (int i = 0; i < players.Length; ++i)
            {
                KickPlayStatSheet stats = (KickPlayStatSheet)players[i].Stats[StatTypes.Kicking];
                if ((!playersShown.Contains(players[i]) && (stats.ExtraPointsAttempted > 0)))
                {
                    listView1.Items.Add(new ListViewItem(new string[] { players[i].Name, stats.ExtraPointsAttempted.ToString(), stats.ExtraPointsMade.ToString(), stats.ExtraPointPercentage.ToString("0.0"), stats.FieldGoalsAttempted.ToString(), stats.FieldGoalsMade.ToString(),stats.FieldGoalPercentage.ToString("0.0"),stats.LongPlay.ToString()}));
                    playersShown.Add(players[i]);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="teamOffense">Offense</param>
        private void LoadReceiveStats(Offense teamOffense)
        {
            listView1.Columns.Add("Player");
            listView1.Columns.Add("pc");
            listView1.Columns.Add("yards");
            listView1.Columns.Add("avg");
            listView1.Columns.Add("td");
            listView1.Columns.Add("lg");
            listView1.Columns.Add("fmb");

            Player[] players = teamOffense.GetPlayers();

            for (int i = 0; i < players.Length; ++i)
            {
                CarryStatSheet stats = (CarryStatSheet)players[i].Stats[StatTypes.Receive];
                if ((!playersShown.Contains(players[i]) && (stats.Touches > 0)))
                {
                    listView1.Items.Add(new ListViewItem(new string[] { players[i].Name, stats.Touches.ToString(), stats.Yards.ToString(), stats.Average.ToString("0.0"), stats.Touchdowns.ToString(), stats.LongPlay.ToString(), stats.Fumbles.ToString() }));
                    playersShown.Add(players[i]);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="teamOffense">Offense</param>
        private void LoadPassStats(Offense teamOffense)
        {
            listView1.Columns.Add("Player");
            listView1.Columns.Add("pa");
            listView1.Columns.Add("pc");
            listView1.Columns.Add("pct");
            listView1.Columns.Add("yds");
            listView1.Columns.Add("td");
            listView1.Columns.Add("int");
            listView1.Columns.Add("lg");
           
            Player[] players = teamOffense.GetPlayers();

            for (int i = 0; i < players.Length; ++i)
            {
                PassPlayStatSheet stats = (PassPlayStatSheet)players[i].Stats[StatTypes.Pass];
                if ((!playersShown.Contains(players[i]) && (stats.Passes > 0)))
                {
                    double compPct = stats.CompletionPct*100.0d;
                    listView1.Items.Add(new ListViewItem(new string[] { players[i].Name, stats.Passes.ToString(),stats.Completions.ToString(),compPct.ToString("0.0"), stats.Yards.ToString(), stats.Touchdowns.ToString(), stats.Interceptions.ToString(),stats.LongPlay.ToString() }));
                    playersShown.Add(players[i]);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="teamOffense">Offense</param>
        /// <param name="runType">StatTypes</param>
        private void LoadRunStats(Offense teamOffense,StatTypes runType)
        {
            listView1.Columns.Add("Player");
            listView1.Columns.Add("tcb");
            listView1.Columns.Add("yards");
            listView1.Columns.Add("avg");
            listView1.Columns.Add("td");
            listView1.Columns.Add("lg");
            listView1.Columns.Add("fmb");

            Player[] players = teamOffense.GetPlayers();
            
            for (int i = 0; i < players.Length; ++i)
            {
                CarryStatSheet stats=null;
                switch (runType)
                {
                
                    case StatTypes.Return:
                        stats = (CarryStatSheet)players[i].Stats[StatTypes.Return];
                        break;
                    case StatTypes.Run:
                        stats= (CarryStatSheet)players[i].Stats[StatTypes.Run];
                        break;
               
                }
                
                if ((!playersShown.Contains(players[i]) && (stats.Touches > 0)))
                {
                    listView1.Items.Add(new ListViewItem(new string[] { players[i].Name, stats.Touches.ToString(), stats.Yards.ToString(), stats.Average.ToString("0.0"), stats.Touchdowns.ToString(), stats.LongPlay.ToString(), stats.Fumbles.ToString() }));
                    playersShown.Add(players[i]);
                }
            }
        }
    }
}
