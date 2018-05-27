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
    public enum StatTypes
    {
        Run,
        Receive,
        Return,
        Pass,
        Kicking,
        Defense,
        WonLoss,
        Unknown
    }

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class StatHolder:ICloneable
    {
        protected FootballEntity holder = null;
        protected Dictionary<StatTypes, StatSheet> stats = new Dictionary<StatTypes, StatSheet>();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="holder">FootballEntity</param>
        public StatHolder(FootballEntity holder)
        {
            this.holder = holder;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key">StatTypes</param>
        /// <param name="value">StatSheet</param>
        public void AddStatSheet(StatTypes key, StatSheet value)
        {
            if(!this.stats.ContainsKey(key))
                this.stats.Add(key, value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key">StatTypes</param>
        public void RemoveStatSheet(StatTypes key)
        {
            if (this.stats.ContainsKey(key))
                this.stats.Remove(key);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Clear()
        {
            this.stats.Clear();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key">StatTypes</param>
        /// <returns>bool</returns>
        public bool ContainsKey(StatTypes key)
        {
            return this.stats.ContainsKey(key);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>StatSheet[]</returns>
        public StatSheet[] ToArray()
        {
            List<StatSheet> sheets=new List<StatSheet>();
            KeyValuePair<StatTypes,StatSheet> [] pairs= this.stats.ToArray();
            foreach (KeyValuePair<StatTypes, StatSheet> pair in pairs)
                sheets.Add(pair.Value);
            return sheets.ToArray();

        }

        /// <summary>
        /// 
        /// </summary>
        public int Count
        {
            get { return stats.Count; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key">StatTypes</param>
        /// <returns>StatSheet</returns>
        public StatSheet this[StatTypes key]
        {
            get { return this.stats[key]; }
        }

        /// <summary>
        /// 
        /// </summary>
        public FootballEntity Holder
        {
            get { return holder; }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns>object</returns>
        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
