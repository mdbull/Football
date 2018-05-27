using System;
using System.Xml;
using Football.Data;
using Football.Engine;
using System.Collections.Generic;

namespace FootballGUI
{
    public class TeamLoader
    {
        string path = String.Empty;
        Team ret = null;

        public TeamLoader(string filePath)
        {
            this.path = filePath;
            this.ret = LoadTeamFromXML(this.path);
        }

        /// <summary>
        /// Returns the team from the file used.
        /// </summary>
        /// <returns>Team</returns>
        public Team GetTeam()
        {
            if (ret == null)
                throw new Exception(ConfigReader.GetConfigurationValue("NO_TEAM_LOADED_EXCEPTION"));
            return ret;

        }

        /// <summary>
        /// Gets the file path of the team file.
        /// </summary>
        /// <value>string</value>
        public string FilePath
        {
            get { return this.path; }
        }

        /// <summary>
        /// Loads a team from xml.
        /// </summary>
        /// <returns>Team</returns>
        /// <param name="filePath">String</param>
        private Team LoadTeamFromXML(string filePath)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);
            XmlNode root = doc.FirstChild;

            if (root.HasChildNodes)
            {
                foreach (XmlNode xn in root.ChildNodes)
                {

                    if (xn.Name == "Info")
                    {
                        Dictionary<string, string> infoAttributes = new Dictionary<string, string>();
                        foreach (XmlNode cn in xn.ChildNodes)
                        {
                            infoAttributes.Add(cn.Name, cn.InnerText);
                        }
                        ret = new Team(infoAttributes["Name"], infoAttributes["Mascot"], Int32.Parse(infoAttributes["Year"]), infoAttributes["FieldName"], Int32.Parse(infoAttributes["FieldCapacity"]));
                        ret.Stats.AddStatSheet(StatTypes.WonLoss, new WonLossRecord(ret));
                    }
                    if (xn.Name == "TeamOffense")
                    {
                        Dictionary<string, string> offenseAttributes = new Dictionary<string, string>();
                        foreach (XmlNode cn in xn.ChildNodes)
                        {
                            offenseAttributes.Add(cn.Name, cn.InnerXml);
                        }

                        ret.TeamOffense = new Offense(ret, Int32.Parse(offenseAttributes["OffensiveLineBonus"]), Convert.ToBoolean(Int32.Parse(offenseAttributes["HasAllWeatherPenalty"])));
                    }
                    if (xn.Name == "Roster")
                    {
                        foreach (XmlNode cn in xn.ChildNodes)
                        {
                            if (cn.Name == "Offense")
                            {
                                int playerId = 1;
                                foreach (XmlNode ccn in cn.ChildNodes)
                                {
                                    if (ccn.Name == "Player")
                                    {
                                        Player p = null;
                                        PlayerSkills ps = null;
                                        Dictionary<string, string> playerSkillAttributes = new Dictionary<string, string>();
                                        Dictionary<string, string> playerAttributes = new Dictionary<string, string>();
                                        foreach (XmlNode cccn in ccn.ChildNodes)
                                        {
                                            if (cccn.Name == "PlayerSkills")
                                            {
                                                foreach (XmlNode ccccn in cccn.ChildNodes)
                                                {
                                                    playerSkillAttributes.Add(ccccn.Name, ccccn.InnerText);
                                                }
                                            }
                                            else
                                            {

                                                playerAttributes.Add(cccn.Name, cccn.InnerText);

                                            }
                                        }
                                        //Get enums
                                        Race race = Race.Black;
                                        Grade grade = Grade.Senior;
                                        Endurance endurance = Endurance.Superb;
                                        p = new Player(ret, playerId.ToString(), playerAttributes["LastName"], playerAttributes["FirstName"], Int32.Parse(playerAttributes["Age"]), Int32.Parse(playerAttributes["Height"]), Int32.Parse(playerAttributes["Weight"]), race, grade, endurance, playerAttributes["Number"], Int32.Parse(playerAttributes["Fumble"]));

                                        try
                                        {
                                            ps = new PlayerSkills(playerSkillAttributes["Run"], playerSkillAttributes["Receive"], playerSkillAttributes["KickReturn"], Int32.Parse(playerSkillAttributes["Pass"]), Int32.Parse(playerSkillAttributes["Kick"]), playerSkillAttributes["Punt"]);

                                            p.AddPlayerSkills(ps);
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine(ex.Message);
                                            throw ex;
                                        }
                                        ret.Add(p);
                                        playerId++;

                                    }
                                }
                                Player[] players = ret.ToArray();
                                ret.TeamOffense.SetPosition("QB", players[0]);
                                ret.TeamOffense.SetPosition("TB", players[1]);
                                ret.TeamOffense.SetPosition("FB", players[2]);
                                ret.TeamOffense.SetPosition("TE", players[3]);
                                ret.TeamOffense.SetPosition("WR1", players[4]);
                                ret.TeamOffense.SetPosition("WR2", players[5]);
                                ret.TeamOffense.SetPosition("K", players[6]);
                                ret.TeamOffense.SetPosition("P", players[6]);
                                ret.TeamOffense.SetPosition("KR", players[1]);
                                ret.TeamOffense.InitAvailableReceivers();

                            }
                            else
                            {
                                Dictionary<string, int> defenseAttributes = new Dictionary<string, int>();
                                Dictionary<string, List<int>> defenseResults = new Dictionary<string, List<int>>();

                                foreach (XmlNode ccn in cn.ChildNodes)
                                {
                                    if (ccn.Name == "DefenseResults")
                                    {
                                        foreach (XmlNode cccn in ccn.ChildNodes)
                                        {
                                            char[] results = cccn.InnerText.ToCharArray();
                                            List<int> resultArray = new List<int>();
                                            foreach (char c in results)
                                            {
                                                resultArray.Add((int)Char.GetNumericValue(c));
                                            }
                                            defenseResults.Add(cccn.Name, resultArray);
                                        }
                                    }
                                    else
                                    {
                                        defenseAttributes.Add(ccn.Name, Int32.Parse(ccn.InnerText));
                                    }
                                }

                                ret.TeamDefense = new Defense(ret, -defenseAttributes["RunPenalty"], -defenseAttributes["MaxRunLoss"], -defenseAttributes["MaxSackLoss"], -defenseAttributes["PassRushRating"], defenseAttributes["BonusRunPenalty"]);

                                LoadDefenseResults(defenseResults);


                                //ret.TeamDefense.InitCarryDefense(new CarryPlayResult[] { CarryPlayResult.Loss, CarryPlayResult.Loss, CarryPlayResult.Loss, CarryPlayResult.Loss, CarryPlayResult.NoGain, CarryPlayResult.NoGain, CarryPlayResult.NoGain, CarryPlayResult.NoGain, CarryPlayResult.NormalGain, CarryPlayResult.NormalGain, CarryPlayResult.NormalGain, CarryPlayResult.NormalGain });
                                //ret.TeamDefense.InitKickoffReturnDefense(new CarryPlayResult[] { CarryPlayResult.Loss, CarryPlayResult.Loss, CarryPlayResult.NoGain, CarryPlayResult.NoGain, CarryPlayResult.NoGain, CarryPlayResult.NoGain, CarryPlayResult.NoGain, CarryPlayResult.NoGain, CarryPlayResult.NormalGain, CarryPlayResult.NormalGain, CarryPlayResult.NormalGain, CarryPlayResult.NormalGain });
                                //ret.TeamDefense.InitPassDefense(new PassPlayResult[] { PassPlayResult.Sack, PassPlayResult.Sack, PassPlayResult.Sack, PassPlayResult.Incomplete, PassPlayResult.Incomplete, PassPlayResult.Incomplete, PassPlayResult.Incomplete, PassPlayResult.Incomplete, PassPlayResult.Interception, PassPlayResult.Interception, PassPlayResult.Interception, PassPlayResult.Interception });
                            }
                        }
                    }
                }


            }
            return ret;
        }

        /// <summary>
        /// Loads the defense results.
        /// </summary>
        /// <param name="defenseResults">Dictionary</param>
        private void LoadDefenseResults(Dictionary<string,List<int>> defenseResults)
        {

            int[] passResults = defenseResults["PassDefense"].ToArray();
            List<PassPlayResult> pres = new List<PassPlayResult>();
            for (int i = 0; i < passResults.Length; ++i)
            {
                pres.Add((PassPlayResult)passResults[i]);
            }
            ret.TeamDefense.InitPassDefense(pres.ToArray());

            int[] carryResults = defenseResults["CarryDefense"].ToArray();
            List<CarryPlayResult> cres = new List<CarryPlayResult>();
            for (int i = 0; i < carryResults.Length; ++i)
            {
                cres.Add((CarryPlayResult)carryResults[i]);
            }
            ret.TeamDefense.InitCarryDefense(cres.ToArray());

            int[] returnResults = defenseResults["KickoffReturnDefense"].ToArray();
            List<CarryPlayResult> kres = new List<CarryPlayResult>();
            for (int i = 0; i < returnResults.Length; ++i)
            {
                kres.Add((CarryPlayResult)returnResults[i]);
            }
            ret.TeamDefense.InitKickoffReturnDefense(kres.ToArray());

        }
    }
}
