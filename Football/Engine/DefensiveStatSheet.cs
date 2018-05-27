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
    public class DefensiveStatSheet : StatSheet
    {
        protected int tackles = 0;
        protected int sacks = 0;
        protected int fumbleRecoveries = 0;
        protected int interceptions = 0;
        protected int touchdowns = 0;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="player">Player</param>
        public DefensiveStatSheet(Player player) : base(player) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sheets"></param>
        /// <returns></returns>
        protected override StatSheet AggregateStatSheets(params StatSheet[] sheets)
        {
            DefensiveStatSheet ret = new DefensiveStatSheet((Player)this.entity);
            foreach (DefensiveStatSheet sheet in sheets)
            {

                ret.tackles += sheet.tackles;
                ret.sacks +=sheet.sacks;
                ret.interceptions += sheet.interceptions;
                ret.fumbleRecoveries += sheet.fumbleRecoveries;
                ret.touchdowns += sheet.touchdowns;
            }
            return ret;
        }

        /// <summary>
        /// 
        /// </summary>
        public int Tackles
        {
            get { return tackles; }
            set { tackles = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Sacks
        {
            get { return sacks; }
            set { sacks = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Interceptions
        {
            get { return interceptions; }
            set { interceptions = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int FumbleRecoveries
        {
            get { return fumbleRecoveries; }
            set { fumbleRecoveries = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int DefensiveTouchdowns
        {
            get { return touchdowns; }
            set { touchdowns += value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            return String.Format("Tackles {0} Interceptions {1} Sacks {2} TD: {3}", tackles, interceptions, sacks,touchdowns);
        }
    }
}
