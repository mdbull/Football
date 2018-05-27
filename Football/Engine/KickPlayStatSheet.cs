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
    public class KickPlayStatSheet:OffensiveStatSheet
    {
        private int xpAttempted = 0;
        private int xpMade = 0;
        private double xpPercentage =0.0d;

        private int fgAttempted = 0;
        private int fgMade = 0;
        private double fgPercentage =0.0d;

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="kicker">Player</param>
        public KickPlayStatSheet(Player kicker)
            : base(kicker)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="distance">int</param>
        /// <param name="fgResult">FieldGoalResult</param>
        /// <param name="xp">bool</param>
        public void AddKickAttempt(int distance, FieldGoalResult fgResult, bool xp)
        {
            
            if (xp)
            {
                xpAttempted++;
                if (fgResult == FieldGoalResult.Good)
                    xpMade++;
                xpPercentage = CalculatePercentage(xpAttempted, xpMade);
            }
            else
            {
                CheckLongPlay(distance);
                fgAttempted++;
                if (fgResult == FieldGoalResult.Good)
                    fgMade++;
                fgPercentage = CalculatePercentage(fgAttempted, fgMade);
            }
        }

       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sheets">StatSheet[]</param>
        /// <returns>StatSheet</returns>
        protected override StatSheet AggregateStatSheets(params StatSheet[] sheets)
        {
            KickPlayStatSheet ret = new KickPlayStatSheet((Player)this.entity);
            foreach (KickPlayStatSheet sheet in sheets)
            {

                ret.xpAttempted += sheet.xpAttempted;
                ret.xpMade += sheet.xpMade;
                ret.fgAttempted += sheet.fgAttempted;
                ret.fgMade+=sheet.fgMade;
                ret.xpPercentage= CalculatePercentage(ret.xpAttempted, ret.xpMade);
                ret.fgPercentage = CalculatePercentage(ret.fgAttempted, ret.fgMade);
                if (sheet.longPlay > ret.LongPlay)
                    ret.longPlay = sheet.LongPlay;

            }
            return ret;
        }

        /// <summary>
        /// 
        /// </summary>
        public int ExtraPointsAttempted
        {
            get { return xpAttempted; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int ExtraPointsMade
        {
            get { return xpMade; }
        }

        /// <summary>
        /// 
        /// </summary>
        public double ExtraPointPercentage
        {
            get { return xpPercentage; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int FieldGoalsAttempted
        {
            get { return fgAttempted; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int FieldGoalsMade
        {
            get { return fgMade; }
        }

        /// <summary>
        /// 
        /// </summary>
        public double FieldGoalPercentage
        {
            get { return fgPercentage; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            return String.Format("{0} {1}-{2}xp {3}-{4}fg Long: {5}", entity, xpMade, xpAttempted, fgMade, fgAttempted, longPlay);
        }
    }
}
