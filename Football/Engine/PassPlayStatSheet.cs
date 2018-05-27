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
    public class PassPlayStatSheet : OffensiveStatSheet
    {


        protected int attempts = 0;
        protected int completions = 0;
        protected int interceptions = 0;
        protected int timesSacked = 0;
        protected double passerRating = 0.0d;
        protected double completionPct = 0.0d;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="player">Player</param>
        public PassPlayStatSheet(Player player)
            : base(player)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sheets">StatSheet[]</param>
        /// <returns>StatSheet</returns>
        protected override StatSheet AggregateStatSheets(params StatSheet[] sheets)
        {
            PassPlayStatSheet ret = new PassPlayStatSheet((Player)this.entity);
            foreach (PassPlayStatSheet sheet in sheets)
            {
               
                ret.yards += sheet.yards;
                ret.touchdowns.AddRange(sheet.touchdowns.ToArray());
                ret.completions += sheet.completions;
                ret.attempts += sheet.attempts;
                ret.completionPct = CalculatePercentage(ret.attempts, ret.completions);
                ret.passerRating = PassPlayStatSheet.CalculatePasserRating(ret.attempts, ret.yards, ret.touchdowns.Count, ret.completions, ret.interceptions);
                if (sheet.longPlay > ret.LongPlay)
                    ret.longPlay = sheet.LongPlay;

            }
            return ret;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="attempts"></param>
        /// <param name="yards"></param>
        /// <param name="td"></param>
        /// <param name="completions"></param>
        /// <param name="interceptions"></param>
        /// <returns></returns>
        public static double CalculatePasserRating(int attempts, int yards, int td, int completions, int interceptions)
        {
            //NCAA formula
            return ((8.4 * yards) + (330 * td) + (100 * completions) - (200 * interceptions)) / attempts;
        }

        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="thrower">Player</param>
        /// <param name="receiver">Player</param>
        /// <param name="yards">int</param>
        /// <param name="isTd">int</param>
        public void CompletePass(Player thrower, Player receiver, int yards, bool isTd)
        {
            this.attempts++;
            this.completions++;
            this.yards+=yards;
            this.completionPct = CalculatePercentage(attempts, completions);
            this.passerRating = PassPlayStatSheet.CalculatePasserRating(this.attempts,this.yards, this.touchdowns.Count, this.completions, this.interceptions);
            CheckLongPlay(yards);
            //update receiver stats here:
            //receiverGameStats.AddRun(yards, isTd, false);
            //((CarryStatSheet)receiver.Stats[StatTypes.Receive]).AddRun(yards, isTd,false);

            if (isTd)
            {
                AddTouchdown(yards);
            }
        
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="thrower">Player</param>
        /// <param name="result">result</param>
        public void IncompletePass(Player thrower, PassPlayResult result)
        {
            this.attempts++;
            this.completionPct = CalculatePercentage(this.attempts, this.completions);
            switch (result)
            {
                case PassPlayResult.Sack:
                    this.timesSacked++;
                    break;
                case PassPlayResult.Incomplete:
                    break;
                case PassPlayResult.Interception:
                    this.interceptions++;
                    break;

            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Passes
        {
            get { return attempts; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Completions
        {
            get { return completions; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Interceptions
        {
            get { return interceptions; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int TimesSacked
        {
            get { return timesSacked; }
        }

        /// <summary>
        /// 
        /// </summary>
        public double PasserRating
        {
            get { return passerRating; }
        }

        /// <summary>
        /// 
        /// </summary>
        public double CompletionPct
        {
            get { return completionPct; }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            return String.Format("{0} {1}-{2}-{3} {4}pct {5}TD {6}INT Long {7} [Rating: {8}]", entity.ToString(), completions, attempts, yards, completionPct.ToString("P"), touchdowns.Count, interceptions,longPlay,this.passerRating.ToString("0.00"));
        }
    }
}
