using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Football.Engine;
using Football.Data;
namespace Football
{
    class Program
    {
        /// <summary>
        /// 
        /// </summary>
        public static GameAnnouncer announcer = null;

        public static readonly int NUM_GAMES = 2;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="players">Player[]</param>
        static void InitStatSheets(ref Player [] players)
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
        static void InitReceivers(Offense teamOffense, params Player[] availableReceivers)
        {
            teamOffense.InitAvailableReceivers(/*availableReceivers*/);
        }


   
        /// <summary>
        /// 
        /// </summary>
        /// <param name="players"></param>
        static void ClearStatSheets(Player[] players)
        {
            for (int i = 0; i < players.Length; ++i)
            {
                players[i].Stats.Clear();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args">string[]</param>
        static void Main(string[] args)
        {
            //ConfigReader.Init(".//Data//config.cfg.txt");

            //Weather weatherReport = new Weather(100, Wind.None, Rain.None, Snow.None, Fog.None);
           
            ////Santa Theresa
            //Team stt = new Team("1992 Santa Theresa Temple", "Golden Knights");
           
            //stt.Stats.AddStatSheet(StatTypes.WonLoss, new WonLossRecord(stt));
            //stt.TeamOffense = new Offense(stt, 5, false);

            //Player p1 = new Player(stt, "1", "Barrity", "Trestan", 15, 69, 190, Race.Black, Grade.Sophomore, Endurance.Superb, "21",2);
            //p1.AddPlayerSkills(new PlayerSkills( "2d20", "3d20", "d100", 65, 80, "3d20"));
            //Player p2 = new Player(stt, "2", "Stokes", "Barry", 18, 77, 210, Race.Black, Grade.Junior, Endurance.Superb, "18",4);
            //p2.AddPlayerSkills(new PlayerSkills("2d12", "2d20", "2d20", 75, 30, "d20"));
            //Player p3 = new Player(stt, "3", "Shackleford", "Charles", 18, 76, 195, Race.Black, Grade.Senior, Endurance.Superb, "11",5);
            //p3.AddPlayerSkills(new PlayerSkills("d10", "2d20", "2d20", 25, 25, "d20"));
            //Player p4 = new Player(stt, "4", "Bass", "Marcellus", 17, 73, 195, Race.Black, Grade.Junior, Endurance.Superb, "19",5);
            //p4.AddPlayerSkills(new PlayerSkills("d20", "d20", "d20", 35, 20, "d20"));
            //Player p5 = new Player(stt, "5", "Mills", "Shon", 17, 69, 165, Race.Black, Grade.Junior, Endurance.Superb, "2",10);
            //p5.AddPlayerSkills(new PlayerSkills("d10", "2d20", "2d20", 30, 20, "2d20"));
            //Player p6 = new Player(stt, "6", "Damphousse", "Vincent", 17, 70, 185, Race.Black, Grade.Junior, Endurance.Superb, "27",10);
            //p6.AddPlayerSkills(new PlayerSkills("3d6", "2d20", "3d10", 30, 20, "d20"));
            //Player p6a = new Player(stt, "60", "Andrade", "Voltan", 16, 68, 160, Race.Hispanic, Grade.Sophomore, Endurance.Superb, "3",15);
            //p6a.AddPlayerSkills(new PlayerSkills("3d6", "3d10", "3d10", 50, 80, "4d20"));
            //Player[] sttPlayers = new Player[] { p1, p2, p3, p4, p5, p6,p6a };

            //InitStatSheets(ref sttPlayers);
            

            //stt.AddPlayers(sttPlayers);
            //stt.TeamOffense.SetPosition("TB", p1);
            //stt.TeamOffense.SetPosition("QB", p2);
            //stt.TeamOffense.SetPosition("FB", p4);
            //stt.TeamOffense.SetPosition("SE", p5);
            //stt.TeamOffense.SetPosition("WR1", p3);
            //stt.TeamOffense.SetPosition("WR2", p6);
            //stt.TeamOffense.SetPosition("K", p6a);
            //stt.TeamOffense.SetPosition("P", p6a);
            //stt.TeamOffense.SetPosition("KR", p6);
            //stt.TeamOffense.InitAvailableReceivers();
            //stt.TeamDefense = new Defense(stt,-4, 20, 20, -20, 0);
            //stt.TeamDefense.InitCarryDefense(new CarryPlayResult[] { CarryPlayResult.Loss, CarryPlayResult.Loss, CarryPlayResult.Loss, CarryPlayResult.Loss, CarryPlayResult.NoGain, CarryPlayResult.NoGain, CarryPlayResult.NoGain, CarryPlayResult.NoGain, CarryPlayResult.NormalGain, CarryPlayResult.NormalGain, CarryPlayResult.NormalGain, CarryPlayResult.NormalGain });
            //stt.TeamDefense.InitPassDefense(new PassPlayResult[] { PassPlayResult.Sack, PassPlayResult.Sack, PassPlayResult.Sack, PassPlayResult.Incomplete, PassPlayResult.Incomplete, PassPlayResult.Incomplete, PassPlayResult.Incomplete, PassPlayResult.Incomplete, PassPlayResult.Interception, PassPlayResult.Interception, PassPlayResult.Interception, PassPlayResult.Interception });
            //stt.TeamDefense.InitKickoffReturnDefense(new CarryPlayResult[] { CarryPlayResult.Loss, CarryPlayResult.Loss, CarryPlayResult.NoGain, CarryPlayResult.NoGain, CarryPlayResult.NoGain, CarryPlayResult.NoGain, CarryPlayResult.NoGain, CarryPlayResult.NoGain, CarryPlayResult.NoGain, CarryPlayResult.NoGain, CarryPlayResult.NormalGain, CarryPlayResult.NormalGain });
            ////Southern
            //Team sou = new Team("2000 Southern", "Jaguars");
            
            //sou.Stats.AddStatSheet(StatTypes.WonLoss, new WonLossRecord(sou));
            //sou.TeamOffense = new Offense(sou, 5, false);
            //Player p7 = new Player(sou, "7", "Kidd", "James", 18, 72, 195, Race.Black, Grade.Senior, Endurance.Superb, "33",2);
            //p7.AddPlayerSkills(new PlayerSkills( "2d20", "2d20", "3d20", 35, 30, "d20"));
            //Player p8 = new Player(sou, "8", "Taylor", "Darius", 18, 76, 210, Race.Black, Grade.Senior, Endurance.Superb, "2",2);
            //p8.AddPlayerSkills(new PlayerSkills("2d20", "2d20", "2d20", 75, 30, "d20"));
            //Player p9 = new Player(sou, "9", "Flowers", "Ray", 18, 76, 195, Race.Black, Grade.Senior, Endurance.Superb, "21",5);
            //p9.AddPlayerSkills(new PlayerSkills("d20", "2d20", "3d20", 35, 30, "d20"));
            //Player p10 = new Player(sou, "10", "Mack", "Willie", 18, 72, 210, Race.Black, Grade.Senior, Endurance.Superb, "43",10);
            //p10.AddPlayerSkills(new PlayerSkills("3d6", "d20", "d20", 35, 30, "d20"));
            //Player p11 = new Player(sou, "11", "Simon", "Dee", 18, 69, 170, Race.Black, Grade.Senior, Endurance.Superb, "29",15);
            //p11.AddPlayerSkills(new PlayerSkills("d12", "3d10", "3d10", 35, 30, "d20"));
            //Player p12 = new Player(sou, "12", "Brown", "Len", 18, 76, 240, Race.Black, Grade.Senior, Endurance.Superb, "96",5);
            //p12.AddPlayerSkills(new PlayerSkills("d8", "2d12", "d20", 35, 30, "d20"));
            //Player p12a = new Player(sou, "120", "Dawson", "Bryan", 18, 70, 175, Race.White, Grade.Senior, Endurance.Superb, "12",35);
            //p12a.AddPlayerSkills(new PlayerSkills("d8", "d12", "d20", 35, 80, "d100"));
            //Player[] souPlayers = new Player[] { p7, p8, p9, p10, p11, p12,p12a };

            //InitStatSheets(ref souPlayers);

            
            //sou.AddPlayers(souPlayers);
            //sou.TeamOffense.SetPosition("TB", p7);
            //sou.TeamOffense.SetPosition("QB", p8);
            //sou.TeamOffense.SetPosition("FB", p10);
            //sou.TeamOffense.SetPosition("TE", p12);
            //sou.TeamOffense.SetPosition("WR1", p9);
            //sou.TeamOffense.SetPosition("WR2", p11);
            //sou.TeamOffense.SetPosition("K", p12a);
            //sou.TeamOffense.SetPosition("P", p12a);
            //sou.TeamOffense.SetPosition("KR", p7);
            //sou.TeamOffense.InitAvailableReceivers();
            //sou.TeamDefense = new Defense(sou, -4, 12, 16, -20, 0);
            //sou.TeamDefense.InitCarryDefense(new CarryPlayResult[] { CarryPlayResult.Loss, CarryPlayResult.Loss, CarryPlayResult.Loss, CarryPlayResult.Loss, CarryPlayResult.NoGain, CarryPlayResult.NoGain, CarryPlayResult.NoGain, CarryPlayResult.NoGain, CarryPlayResult.NormalGain, CarryPlayResult.NormalGain, CarryPlayResult.NormalGain, CarryPlayResult.NormalGain});
            //sou.TeamDefense.InitKickoffReturnDefense(new CarryPlayResult[] { CarryPlayResult.Loss, CarryPlayResult.Loss, CarryPlayResult.NoGain, CarryPlayResult.NoGain, CarryPlayResult.NoGain, CarryPlayResult.NoGain, CarryPlayResult.NoGain, CarryPlayResult.NoGain, CarryPlayResult.NoGain, CarryPlayResult.NoGain, CarryPlayResult.NormalGain, CarryPlayResult.NormalGain });
            //sou.TeamDefense.InitPassDefense(new PassPlayResult[] { PassPlayResult.Sack, PassPlayResult.Sack, PassPlayResult.Sack, PassPlayResult.Incomplete, PassPlayResult.Incomplete, PassPlayResult.Incomplete, PassPlayResult.Incomplete, PassPlayResult.Incomplete, PassPlayResult.Interception, PassPlayResult.Interception, PassPlayResult.Interception, PassPlayResult.Interception });

            ////play a few games
            //for (int i = 0; i < NUM_GAMES; ++i)
            //{
            //    Game game1 = new Game(1, "Valhalla Field", 2000, sou, stt, "Bob Tabor",new TimeSpan(17,0,0),weatherReport);
                
            //    game1.GameAnnouncer.ReportNamesInUpperCase = true;

            //    game1.Execute();
            //    Console.WriteLine("Scoring Summary");
            //    Console.WriteLine("--------------------------------");
            //    Console.WriteLine(game1.Scoreboard.GetScoringSummary());
            //    ReportGameStats(game1, stt);
            //    ReportGameStats(game1, sou);
            //    Console.ReadLine();
            //}
            //Console.WriteLine("*************SEASON STATS*******************");
            //Console.WriteLine(p1.Stats[StatTypes.Run].ToString());
            //Console.WriteLine(p2.Stats[StatTypes.Pass].ToString());
            //Console.WriteLine(p3.Stats[StatTypes.Receive].ToString());
            //Console.WriteLine(p7.Stats[StatTypes.Run].ToString());
            //Console.WriteLine(p8.Stats[StatTypes.Pass].ToString());
            //Console.WriteLine(p9.Stats[StatTypes.Receive].ToString());
            //Console.WriteLine(stt.Stats[StatTypes.WonLoss].ToString());
            //Console.WriteLine(sou.Stats[StatTypes.WonLoss].ToString());

            
        
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="game"></param>
        /// <param name="team"></param>
        static void ReportGameStats(Game game, Team team)
        {
            if (game.GameAnnouncer.ReportFrequency != AnnounceReportFrequency.Silent)
            {
                
                Player[] players = team.ToArray();
                Console.WriteLine("---------------------------------------");
                for (int j = 0; j < (int)StatTypes.Unknown - 1; ++j)
                {

                    Console.WriteLine("{0} Stats:", ((StatTypes)j).ToString());
                    Console.WriteLine("-----------------------------------------------");

                    for (int k = 0; k < players.Length; ++k)
                    {
                        StatHolder stats = game.GameStats[team, k];
                        Player player = players[k];
                        Console.WriteLine("{0} {1}",player.Name, stats[(StatTypes)j].ToString());
                    }

                }
            }
        }
        
    }
}
