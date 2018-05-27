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
    public class StatsEntity:FootballEntity
    {
        
        protected StatHolder stats = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">string</param>
        public StatsEntity(string name)
        {
            this.name = name;
            this.stats = new StatHolder(this);
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
        /// <param name="key">StatTypes</param>
        /// <returns>StatSheet</returns>
        public StatSheet this[StatTypes key]
        {
            get { return stats[key]; }
        }

        /// <summary>
        /// 
        /// </summary>
        public StatHolder Stats
        {
            get { return stats; }
        }
    }
}
