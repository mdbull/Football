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
    public class Team: StatsEntity,ICollection<Player>
    {
        private string mascot = string.Empty;
        private List<Player> players = new List<Player>();
        private string fieldName = String.Empty;
        private int fieldCapacity = 0;
        private int year = 0;
        private Defense defense = null;
        private Offense offense = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">string</param>
        /// <param name="mascot">string</param>
        public Team(string name, string mascot, int year, string fieldName, int fieldCapacity):base(name)
        {
            this.mascot = mascot;
            this.year = year;
            this.fieldName = fieldName;
            this.fieldCapacity = fieldCapacity;
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void Init()
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        public string Mascot
        {
            get { return mascot; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="player">Player</param>
        public void AddPlayers(params Player [] players)
        {
            this.players.AddRange(players);
        }

        
        /// <summary>
        /// 
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            return String.Format("{0} {1}", name, mascot);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public int GetIndexOfPlayer(Player player)
        {
            return players.IndexOf(player);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item">Player</param>
        public void Add(Player item)
        {
            this.players.Add(item);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Clear()
        {
            this.players.Clear();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item">Player</param>
        /// <returns>bool</returns>
        public bool Contains(Player item)
        {
            return players.Contains(item);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array">Player</param>
        /// <param name="arrayIndex">int</param>
        public void CopyTo(Player[] array, int arrayIndex)
        {
            players.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// 
        /// </summary>
        public int Count
        {
            get { return players.Count; }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsReadOnly
        {
            get { return false; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item">Player</param>
        /// <returns>bool</returns>
        public bool Remove(Player item)
        {
            return players.Remove(item);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>IEnumerator</returns>
        public IEnumerator<Player> GetEnumerator()
        {
            return players.GetEnumerator();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>System.Collections.IEnumerator</returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return players.GetEnumerator();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Player[] ToArray()
        {
            return players.ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        public Defense TeamDefense
        {
            get { return defense; }
            set { defense = value; }
        }

        public Offense TeamOffense
        {
            get { return offense; }
            set { offense = value; }
        }

        public string FieldName
        {
            get { return fieldName; }
        }

        public int FieldCapacity
        {
            get { return fieldCapacity; }
        }

        public int Year
        {
            get { return year; }
        }
    }
}
