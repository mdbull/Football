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
    public enum Grade
    {
        Freshman = 9,
        Sophomore,
        Junior,
        Senior,
        Unknown
    }

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public enum Endurance
    {
        Superb,
        Excellent,
        Good,
        Fair,
        Poor,
        Unknown
    }

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class Player : Person, IComparable<Player>
    {

        private string number = string.Empty;
        private Grade grade = Grade.Unknown;
        private Endurance endurance=Endurance.Unknown;
        private int health = 100;
        private Team team = null;
        private List<string> positions = new List<string>();
        private int fumble = 20;

        private PlayerSkills playerSkills = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="lastName"></param>
        /// <param name="firstName"></param>
        /// <param name="age"></param>
        /// <param name="height"></param>
        /// <param name="weight"></param>
        /// <param name="race"></param>
        /// <param name="grade"></param>
        /// <param name="endurance"></param>
        /// <param name="number"></param>
        public Player(Team team,string id, string lastName, string firstName, int age, int height, int weight, Race race, Grade grade, Endurance endurance, string number, int fumble)
            : base(team,id, lastName, firstName, age, height, weight, race)
        {

            this.number = number;
            this.grade = grade;
            this.endurance = endurance;
            this.fumble = fumble;
           
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="playerSkills">PlayerSkills</param>
        public void AddPlayerSkills(PlayerSkills playerSkills)
        {
            this.playerSkills = playerSkills;
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
        public Grade Grade
        {
            get { return grade; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Number
        {
            get { return number; }
        }

        /// <summary>
        /// 
        /// </summary>
        public List<string> Positions
        {
            get { return positions; }
        }

        /// <summary>
        /// 
        /// </summary>
        public PlayerSkills PlayerSkills
        {
            get { return playerSkills; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            get { return string.Format("{0} {1} {2} {3}",positions[0], number, firstName, lastName); }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Fumble
        {
            get { return fumble; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return String.Format("{0} {1} {2} {3}, {4}lbs {5}", number, firstName, lastName, Height, weight, grade);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other">Player</param>
        /// <returns>int</returns>
        public int CompareTo(Player other)
        {
            return this.number.CompareTo(other.number);
        }

        
    }
}
