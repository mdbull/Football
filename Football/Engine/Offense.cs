using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Football.Engine
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class Offense: FootballEntity
    {
        //public static const int MAX_AVAILABLE_RECEIVERS = 5;
        private Team team = null;
        private int offensiveLineBonus = 0;
        private bool hasWeatherPenalty = true;
        private Dictionary<string, Player> positions = new Dictionary<string, Player>(6);
        private Player[] availableReceivers = null;
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="team">Team</param>
        /// <param name="offensiveLineBonus">int</param>
        /// <param name="hasWeatherPenalty">bool</param>
        public Offense(Team team, int offensiveLineBonus, bool hasWeatherPenalty)
        {
            this.team = team;
            this.offensiveLineBonus = offensiveLineBonus;
            this.hasWeatherPenalty = hasWeatherPenalty;
            this.availableReceivers = new Player[5];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"></param>
        /// <param name="player"></param>
        public void SetPosition(string position, Player player)
        {
            if (!positions.ContainsKey(position))
            {
                positions.Add(position, player);
            }
            else
            {
                positions[position] = player;
            }
            player.Positions.Add(position);
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void Init()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        public Team Team
        {
            get { return team; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int OffensiveLineBonus
        {
            get { return offensiveLineBonus; }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool HasWeatherPenalty
        {
            get { return hasWeatherPenalty; }
        }

        /// <summary>
        /// 
        /// </summary>
        public Player[] AvailableReceivers
        {
            get { return availableReceivers; }
            set { availableReceivers = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="player">Player</param>
        /// <returns>int</returns>
        public int GetAvailableReceiverAtIndex(Player player)
        {
            for (int i = 0; i < availableReceivers.Length; ++i)
            {
                if (availableReceivers[i].Id == player.Id)
                    return i;
            }
            return -1;
        }
        
        /// <summary>
        /// 
        /// </summary>
        public void InitAvailableReceivers()
        {
            this.availableReceivers = new Player[5];
            Dictionary<string, Player>.Enumerator cursor = this.positions.GetEnumerator();

            int i = 0;
            int j = 0;
            while (cursor.MoveNext())
            {
                if (cursor.Current.Key != "QB" && cursor.Current.Key != "K" && cursor.Current.Key != "P")
                {
                    if (j < availableReceivers.Length)
                    {
                        this.availableReceivers[j] = cursor.Current.Value;
                        ++j;
                    }
                }
                ++i;
            }
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Player GetPlayerAtPosition(string key)
        {
            return positions[key];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Player[]</returns>
        public Player[] GetPlayers()
        {
            List<Player> players = new List<Player>();
            Dictionary<string, Player>.Enumerator cursor = positions.GetEnumerator();

            while (cursor.MoveNext())
            {
                players.Add(cursor.Current.Value);
            }
            return players.ToArray();
        }

        
    }
}
