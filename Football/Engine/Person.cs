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
    public enum Race
    {
        Black,
        White,
        Hispanic,
        Polynesian,
        Other,
        Unknown
    }

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public enum Hand
    {
        Right,
        Left,
        Ambidextrous,
        Unknown
    }

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public abstract class Person:StatsEntity, IComparable<Person>
    {
        protected string id = string.Empty;
        protected string lastName = string.Empty;
        protected string firstName = string.Empty;
        protected int age = 0;
        protected Race race = Race.Unknown;
        protected int height=70;
        protected int weight =170;
        protected Hand hand = Hand.Right;
        protected Team team = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="team"></param>
        /// <param name="id"></param>
        /// <param name="ln"></param>
        /// <param name="fn"></param>
        /// <param name="age"></param>
        /// <param name="height"></param>
        /// <param name="weight"></param>
        /// <param name="race"></param>
        public Person(Team team,string id, string ln, string fn, int age, int height, int weight, Race race):base(String.Format("{0} {1}",fn,ln))
        {
            this.team = team;
            this.id = id;
            this.lastName = ln;
            this.firstName = fn;
            this.age = age;
            this.race = race;
            this.height = height;
            this.weight = weight;
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
        public string Id
        {
            get { return id; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string LastName
        {
            get { return lastName; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string FirstName
        {
            get { return firstName; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Age
        {
            get { return age; }
        }

        /// <summary>
        /// 
        /// </summary>
        public Race Race
        {
            get { return race; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Height
        {
            get 
            {
                int feet = height / 12;
                int inches = height % 12;

                return String.Format("{0}-{1}", feet, inches);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Hand Hand
        {
            get { return hand; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Weight
        {
            get { return weight; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            return String.Format("{0} {1}", firstName, lastName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other">Person</param>
        /// <returns>int</returns>
        public int CompareTo(Person other)
        {
            return this.lastName.CompareTo(other.lastName);
        }
    }
}
