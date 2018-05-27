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
    public abstract class OffensiveStatSheet : StatSheet
    {
        protected int touches = 0;
        protected int yards = 0;
        protected int longPlay = 0;
        protected int fumbles = 0;
        protected List<int> touchdowns = new List<int>();

        public OffensiveStatSheet(StatsEntity player) : base(player) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="yards">int</param>
        public void AddTouchdown(int yards)
        {
            this.touchdowns.Add(yards);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="attempts">int</param>
        /// <param name="completions">int</param>
        /// <returns>double</returns>
        protected double CalculatePercentage(int attempts, int completions)
        {
            return (double)completions / (double)attempts;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="yards">int</param>
        /// <returns>int</returns>
        protected void CheckLongPlay(int yards)
        {
            if (yards > longPlay)
                longPlay = yards;
        }

        /// <summary>
        /// 
        /// </summary>
        public int Fumbles
        {
            get { return fumbles; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Touches
        {
            get { return touches; }
            
        }

        /// <summary>
        /// 
        /// </summary>
        public int Yards
        {
            get { return yards; }
           
        }

        /// <summary>
        /// 
        /// </summary>
        public float Average
        {
            get { return (float)yards / (float)touches; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Touchdowns
        {
            get { return touchdowns.Count; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int LongPlay
        {
            get { return longPlay; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index">int</param>
        /// <returns>int</returns>
        public int this[int index]
        {
            get { return touchdowns[index]; }
        }
    }
}
