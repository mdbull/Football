using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Text;
using System.Windows.Forms;
using Football.Data;
using Football.Engine;

namespace FootballGUI
{
    public partial class GameCenter : Form
    {
        private Game game = null;
        private static int LABEL_SIZE = 40;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="game">Game</param>
        public GameCenter(Game game)
        {
            InitializeComponent();
            this.game = game;
            this.game.GameAnnouncer.reportAnnounced += new AnnounceReport(GameAnnouncer_reportAnnounced);
            this.game.Scoreboard.scoreboardUpdated += new ScoreboardUpdatedEventHandler(Scoreboard_scoreboardUpdated);
            this.game.Scoreboard.overtimeQuarterIncremented += new OvertimeEventHandler(Scoreboard_overtimeQuarterIncremented);
            Init();
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="currentQuarter">int</param>
        void Scoreboard_overtimeQuarterIncremented(int currentQuarter)
        {
            AddScoreLabel(flowLayoutPanel1, false);
            AddScoreLabel(flowLayoutPanel2, false);
        }

        private void Init()
        {
            this.richTextBox1.Clear();
            this.richTextBox2.Clear();
            this.groupBox1.Text = this.game.Scoreboard.RoadTeam.Name;
            this.groupBox2.Text = this.game.Scoreboard.HomeTeam.Name;
            this.label1.Text = this.groupBox1.Text;
            this.label2.Text = this.groupBox2.Text;
            this.richTextBox3.Text = "0";
            this.richTextBox4.Text = "0";
            for (int i = 0; i < 4; ++i)
            {
                AddScoreLabel(flowLayoutPanel1,false);
                AddScoreLabel(flowLayoutPanel2, false);
            }
            AddScoreLabel(flowLayoutPanel1,false);
            AddScoreLabel(flowLayoutPanel2, false);
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddScoreLabel(FlowLayoutPanel flp, bool isBold)
        {
            Label lbl = new Label();
            lbl.Size = new System.Drawing.Size(LABEL_SIZE, LABEL_SIZE);
            lbl.Text = "0";
            flp.Controls.Add(lbl);
            if (isBold)
                lbl.Font = new Font(this.Font, FontStyle.Bold);
        }

       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="scoreboard">Scoreboard</param>
        void Scoreboard_scoreboardUpdated(Scoreboard scoreboard)
        {
           
            richTextBox2.Text=scoreboard.GetScoringSummaryByQuarters();
            richTextBox2.ScrollToCaret();
            
            richTextBox3.Text = scoreboard.GetTeamScore(this.game.Scoreboard.RoadTeam).ToString();
            richTextBox4.Text = scoreboard.GetTeamScore(this.game.Scoreboard.HomeTeam).ToString();
           
            flowLayoutPanel1.Controls[scoreboard.CurrentQuarter].Text = scoreboard.GetTeamScoreByQuarter(scoreboard.CurrentQuarter, false).ToString();
            flowLayoutPanel1.Controls[flowLayoutPanel1.Controls.Count - 1].Text = scoreboard.GetTeamScore(game.Scoreboard.RoadTeam).ToString();
            flowLayoutPanel1.Controls[flowLayoutPanel1.Controls.Count - 1].Font = new System.Drawing.Font(this.Font, FontStyle.Bold);
            flowLayoutPanel2.Controls[scoreboard.CurrentQuarter].Text = scoreboard.GetTeamScoreByQuarter(scoreboard.CurrentQuarter, true).ToString();
            flowLayoutPanel2.Controls[flowLayoutPanel2.Controls.Count - 1].Text = scoreboard.GetTeamScore(game.Scoreboard.HomeTeam).ToString();
            flowLayoutPanel2.Controls[flowLayoutPanel2.Controls.Count - 1].Font = new System.Drawing.Font(this.Font, FontStyle.Bold);
            
           
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e">AnnounceReportEventArgs</param>
        void GameAnnouncer_reportAnnounced(AnnounceReportEventArgs e)
        {
           
            richTextBox1.AppendText(e.Report);
            richTextBox1.ScrollToCaret();
           
            
           
        }

        /// <summary>
        /// 
        /// </summary>
        private void ExecuteGame()
        {
            this.game.Execute();
        }

     
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        private void groupBox2_MouseHover(object sender, EventArgs e)
        {
            ShowTeamStats(this.game.Scoreboard.HomeTeam);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        private void groupBox1_MouseHover(object sender, EventArgs e)
        {
            ShowTeamStats(this.game.Scoreboard.RoadTeam);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="team">Team</param>
        private void ShowTeamStats(Team team)
        {
            TeamStats showStats = new TeamStats(team);
            showStats.ShowDialog();
        }

       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            ExecuteGame();  
        }

     

        /// <summary>
        /// 
        /// </summary>
        private void ToggleGameControls()
        {

            if (button1.Text == "Start")
                button1.Text = "Pause";
            else if (button1.Text == "Pause")
                button1.Text = "Resume";
            
        }

    }
}
