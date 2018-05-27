using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using Football.Data;
using Football.Engine;

namespace FootballGUI
{
    public partial class MainForm : Form
    {
        Team home = null;
        Team away = null;
        Game game = null;
        Weather weatherReport = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="players">Player[]</param>
        void InitStatSheets(ref Player[] players)
        {
            for (int i = 0; i < players.Length; ++i)
            {
                players[i].Stats.AddStatSheet(StatTypes.Run, new CarryStatSheet(players[i]));
                players[i].Stats.AddStatSheet(StatTypes.Receive, new CarryStatSheet(players[i]));
                players[i].Stats.AddStatSheet(StatTypes.Pass, new PassPlayStatSheet(players[i]));
                players[i].Stats.AddStatSheet(StatTypes.Kicking, new KickPlayStatSheet(players[i]));
                players[i].Stats.AddStatSheet(StatTypes.Return, new CarryStatSheet(players[i]));
                players[i].Stats.AddStatSheet(StatTypes.Defense, new DefensiveStatSheet(players[i]));
                players[i].Stats.AddStatSheet(StatTypes.WonLoss, new WonLossRecord(players[i]));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="teamOffense">Offense</param>
        /// <param name="availableReceivers">Player</param>
        void InitReceivers(Offense teamOffense, params Player[] availableReceivers)
        {
            teamOffense.InitAvailableReceivers(/*availableReceivers*/);
        }



        /// <summary>
        /// Clears stat sheets.
        /// </summary>
        /// <param name="players"></param>
        void ClearStatSheets(Player[] players)
        {
            for (int i = 0; i < players.Length; ++i)
            {
                players[i].Stats.Clear();
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:FootballGUI.MainForm"/> class.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Loads the teams.
        /// </summary>
        void LoadTeams()
        {
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.InitialDirectory = ConfigReader.GetConfigurationValue("TEAM_DATA_LOCATION");
                //dialog.InitialDirectory = @"/home/mike/Desktop";
                dialog.Title = "Choose home team";
                dialog.Filter = ConfigReader.GetConfigurationValue("INITIAL FILTER");
                dialog.RestoreDirectory = true;
                if (dialog.ShowDialog() == DialogResult.OK)
                {

                    home = new TeamLoader(dialog.FileName).GetTeam();
                }
                Player[] homePlayers = home.ToArray();
                InitStatSheets(ref homePlayers);

                dialog.Title = "Choose away team";
                if (dialog.ShowDialog() == DialogResult.OK)
                {

                    away = new TeamLoader(dialog.FileName).GetTeam();
                }
                Player[] awayPlayers = away.ToArray();
                InitStatSheets(ref awayPlayers);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void LoadGameEnvironment()
        {
            try
            {
                weatherReport = new Weather(100, Wind.None, Rain.None, Snow.None, Fog.None);
                game = new Game(1, home.FieldName, home.FieldCapacity, away, home, "Bob Tabor", new TimeSpan(17, 0, 0), weatherReport);
                game.GameAnnouncer.ReportNamesInUpperCase = true;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        void button1_Click(object sender, EventArgs e)
        {
            LoadTeams();
            LoadGameEnvironment();
            GameCenter gameForm = new GameCenter(game);
            gameForm.Show();

        }

    }
}
