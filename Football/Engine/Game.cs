using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Threading;
using Football.Data;

namespace Football.Engine
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class GameStats
    {
        private Dictionary<Team, List<StatHolder>> gameStats = new Dictionary<Team, List<StatHolder>>();
        
        /// <summary>
        ///
        /// </summary>
        public GameStats()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key">Team</param>
        public void AddTeamStats(Team team)
        {
            if (!gameStats.ContainsKey(team))
                gameStats.Add(team, new List<StatHolder>());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="team"></param>
        /// <param name="player"></param>
        public void AddPlayerStats(Team team, Player player)
        {
            if(gameStats.ContainsKey(team))
                gameStats[team].Add(new StatHolder(player));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="team">Team</param>
        /// <param name="playerIndex">int</param>
        /// <returns>StatHolder</returns>
        public StatHolder this[Team team, int playerIndex]
        {
            
            get 
            {
              
                if (playerIndex >= gameStats[team].Count)
                    playerIndex = gameStats[team].Count - 1;
                if (playerIndex < 0)
                    playerIndex = 0;
                return gameStats[team][playerIndex]; 
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class Game : FootballEntity, IExecutable
    {
        public static readonly int TIMER_SLEEP = Convert.ToInt32(ConfigReader.GetConfigurationValue("TIMER_SLEEP"));
        private GameAnnouncer announcer = null;
        private ChainCrew cc = null;
        private Field field = null;
        private Scoreboard scoreboard = null;
        private Team home = null;
        private Team road = null;
        private Team winner = null;
        private string finalScore=string.Empty;
        private GameStats gameStats = null;
        private TimeSpan gameTime = TimeSpan.MinValue;
        private Weather weatherReport = null;
        private System.Timers.Timer gameTimer = null;
        private bool gameOver = false;
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="team">Team</param>
        private void InitGameStats(Team team)
        {
            gameStats.AddTeamStats(team);
           
            Player [] players=team.ToArray();

            for (int i = 0; i < players.Length; ++i)
            {
                gameStats.AddPlayerStats(team,players[i]);
                gameStats[team, i].AddStatSheet(StatTypes.Run, new CarryStatSheet(players[i]));
                gameStats[team,i].AddStatSheet(StatTypes.Receive, new CarryStatSheet(players[i]));
                gameStats[team,i].AddStatSheet(StatTypes.Pass, new PassPlayStatSheet(players[i]));
                gameStats[team,i].AddStatSheet(StatTypes.Kicking, new KickPlayStatSheet(players[i]));
                gameStats[team,i].AddStatSheet(StatTypes.Return, new CarryStatSheet(players[i]));
                gameStats[team,i].AddStatSheet(StatTypes.Defense, new DefensiveStatSheet(players[i]));
                gameStats[team,i].AddStatSheet(StatTypes.WonLoss, new WonLossRecord(players[i]));
                
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="team">Team</param>
        /// <param name="stats">List</param>
        private void ReportEndOfGameStats(Team team)
        {

            Player [] players=team.ToArray();
            for (int i = 0; i < players.Length; ++i)
            {
                StatSheet.Aggregate(players[i][StatTypes.Run],gameStats[team,i][StatTypes.Run]);
                StatSheet.Aggregate(players[i][StatTypes.Receive], gameStats[team, i][StatTypes.Receive]);
                StatSheet.Aggregate(players[i][StatTypes.Pass], gameStats[team, i][StatTypes.Pass]);
                StatSheet.Aggregate(players[i][StatTypes.Kicking], gameStats[team, i][StatTypes.Kicking]);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">int</param>
        /// <param name="field">Field</param>
        /// <param name="home">Team</param>
        /// <param name="road">Team</param>
        /// <param name="announcerName">string</param>
        public Game(int id, string fieldName, int fieldCapacity,Team road, Team home, string announcerName, TimeSpan gameTime, Weather weatherReport)
            : base(String.Format("Game {0}", id))
        {

            this.gameStats = new GameStats();
            //this.gameTimer = new System.Timers.Timer(TIMER_SLEEP);
            //this.gameTimer.Elapsed += new ElapsedEventHandler(gameTimer_Elapsed);
            this.weatherReport = weatherReport;
            this.gameTime = gameTime;
            this.home = home;
            this.road = road;
            this.scoreboard = new Scoreboard(road, home);
            this.scoreboard.gameOver += new GameOverEventHandler(scoreboard_gameOver);
            this.field = new Field(fieldName, fieldCapacity, this.scoreboard);
            this.cc = new ChainCrew(this.field,this);
            this.announcer = new GameAnnouncer(cc, announcerName);
            this.announcer.reportAnnounced += new AnnounceReport(announcer_reportAnnounced);
            this.announcer.ReportGameOpening(this, this.field);
            InitGameStats(this.home);
            InitGameStats(this.road);
        }

        void scoreboard_gameOver()
        {
            gameOver = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ElapsedEventArgs</param>
        void gameTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            //Thread.Sleep(TIMER_SLEEP);
        }

       
        /// <summary>
        /// Restarts game after pause
        /// </summary>
        public void Restart()
        {
            gameTimer.Start();
        }


        /// <summary>
        /// Pauses game
        /// </summary>
        public void Pause()
        {
            gameTimer.Stop();
            
        }

        /// <summary>
        /// 
        /// </summary>
        private CoinFlipWinner CoinFlip()
        {
            CoinFlip flip = new CoinFlip();
            return flip.DeterminePossession(announcer, road, home, CoinFlipResult.Heads);
           
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e">AnnounceReportEventArgs</param>
        void announcer_reportAnnounced(AnnounceReportEventArgs e)
        {
            Console.WriteLine(e.Report);
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void Init()
        {
            
        }

        /// <summary>
        /// Begins the game.
        /// </summary>
        /// <returns>int</returns>
        public int Execute()
        {
            
            CoinFlipWinner coinFlipResult=CoinFlip();
            Thread.Sleep(TIMER_SLEEP);
            Kickoff(coinFlipResult);
            Thread.Sleep(TIMER_SLEEP);
            Loop();
            GetGameResults();
            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        private void GetGameResults()
        {
            ((WonLossRecord)home.Stats[StatTypes.WonLoss]).AddGameResult(new GameResult(road, field.Scoreboard.GetTeamScore(home), field.Scoreboard.GetTeamScore(road), true));
            ((WonLossRecord)road.Stats[StatTypes.WonLoss]).AddGameResult(new GameResult(home, field.Scoreboard.GetTeamScore(road), field.Scoreboard.GetTeamScore(home), false));
            ReportEndOfGameStats(home);
            ReportEndOfGameStats(road);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="coinFlipWinner">CoinFlipWinner</param>
        private void Kickoff(CoinFlipWinner coinFlipWinner)
        {
            Kickoff kickoff = new Kickoff(this.announcer,this.cc, Direction.Right, coinFlipWinner.Loser, coinFlipWinner.Winner);
            kickoff.Execute();
           
        }

        /// <summary>
        /// 
        /// </summary>
        private void Loop()
        {
            for (int i = 0; i < 80; ++i)
            {
                if (((i % 15) == 1) && (i != 1))
                        scoreboard.MoveToNextQuarter();
                if (!gameOver)
                {
                    
                    //run
                    Team tip = cc.TeamInPossession;
                    Team tnip = cc.TeamNotInPossession;
                    int roll = Dice.Roll("d2");
                    if (roll == 1)//run
                    {

                        Player ballcarrier = tip.TeamOffense.GetPlayerAtPosition("TB");
                        int playerIndex = tip.GetIndexOfPlayer(ballcarrier);
                        CarryStatSheet runStats = (CarryStatSheet)gameStats[tip, playerIndex][StatTypes.Run];
                        RunPlay run = new RunPlay(cc, tip, tnip, ballcarrier, runStats);
                        run.Execute();
                    }
                    else
                    {
                        //pass
                        Player thrower = tip.TeamOffense.GetPlayerAtPosition("QB");

                        PassPlay pass = new PassPlay(gameStats, cc);
                        pass.Execute();
                    }
                    Thread.Sleep(TIMER_SLEEP);
                }
            }
            finalScore=this.announcer.ReportFinalScore();
           
        }

        /// <summary>
        /// 
        /// </summary>
        public string FinalScore
        {
            get { return finalScore; }
        }
       
        /// <summary>
        /// 
        /// </summary>
        public GameAnnouncer GameAnnouncer
        {
            get { return announcer; }
        }

        /// <summary>
        /// 
        /// </summary>
        public GameStats GameStats
        {
            get { return gameStats; }
        }

        /// <summary>
        /// 
        /// </summary>
        public TimeSpan GameTime
        {
            get { return gameTime; }
        }

        /// <summary>
        /// 
        /// </summary>
        public Weather WeatherReport
        {
            get { return weatherReport; }
        }

        /// <summary>
        /// 
        /// </summary>
        public Scoreboard Scoreboard
        {
            get { return scoreboard; }
        }

    }
}
