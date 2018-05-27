using System;
using System.Collections.Generic;
using System.Xml;
using System.Text;
using System.IO;
using Football;
using Football.Data;
using Football.Engine;

namespace FootballGUI
{
    /// <summary>
    /// 
    /// </summary>
	public class TeamReader
    {
   

		private TeamReader()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configFile">string</param>
		public static Team GetTeam(string teamFile)
        {
			Team team=null;
			Dictionary<string,object> teamInfo=new Dictionary<string, object>();
			Dictionary<string, Player> players=new Dictionary<string, Player>();
			Dictionary<string,int[]>teamDefenseInfo=new Dictionary<string, int[]>();

			StreamReader fs = new StreamReader(teamFile);
            while (fs.Peek() != -1)
            {
                
				string [] teamLine = fs.ReadLine().Split('=');
				if(teamLine.Length==2)//team values
				{
					if(teamLine[1].Length=1)//we have team info
					{
						teamInfo.Add(teamLine[0],teamLine[1]);
				
					}
					else if(teamLine[1].Length= 16)//we have a player
					{

						object [] playerInfo=teamLine[1].Split('|');
						Player player=new Player(team,teamLine[0],playerInfo[0],playerInfo[1],playerInfo[2],playerInfo[3],playerInfo[4],(Race)playerInfo[5],(Grade)playerInfo[6],(Endurance)playerInfo[7],playerInfo[8],playerInfo[9]);
						PlayerSkills playerSkills=new PlayerSkills(player,playerInfo[10],playerInfo[11],playerInfo[12],playerInfo[13],playerInfo[14],playerInfo[15]);
						player.AddPlayerSkills(playerSkills);
						players.Add(teamLine[0],player);
					
					}
					else if(teamLine[1].Length=12)//defensive information
					{
						teamDefenseInfo.Add(teamLine[0],teamLine[1].Split('|'));
					}
				}
            }
			team=new Team(teamInfo["Name"],teamInfo["Mascot"]);
			team.TeamOffense=new Offense(team,teamInfo["OFFLINEBONUS"],teamInfo["WEATHERPENALTY"]);

			Player [] teamPlayers=players.Values;
			InitStatSheets(players);
			team.AddPlayers(teamPlayers);
			team.TeamOffense.InitAvailableReceivers();

			team.TeamDefense=new Defense(team,teamInfo["RUNPENALTY"],teamInfo["MAXRUNLOSS"],teamInfo["MAXSACKLOSS"],teamInfo["PASSRUSHRATING"],teamInfo["BONUSRUNPENALTY"]);
			team.TeamDefense.InitCarryDefense(teamDefenseInfo["CARRYDEFENSE"]);
			team.TeamDefense.InitPassDefense(teamDefenseInfo["PASSDEFENSE"]);
			team.TeamDefense.InitKickoffReturnDefense(teamDefenseInfo["KICKOFFRETURNDEFENSE"]);
			return team;
		
		}

		private static void InitStatSheets(ref Player[] players)
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


    }
}
