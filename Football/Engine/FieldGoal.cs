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
    public enum FieldGoalResult
    {
        Good,
        NotGood,
        Unknown
    }

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class FieldGoal : Play
    {
        private int distance = 0;
        private bool isExtraPoint = false;
        private bool isBlocked = false;
        private FieldGoalResult fieldGoalResult = FieldGoalResult.Unknown;
        private KickPlayStatSheet gameStats = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="kicker">Player</param>
        /// <param name="distance">int</param>
        /// <param name="isExtraPoint">bool</param>
        public FieldGoal(Player kicker, int distance, bool isExtraPoint, KickPlayStatSheet gameStats)
        {
            this.principalBallcarrier = kicker;
            this.distance = distance;
            this.isExtraPoint = isExtraPoint;
            this.gameStats = gameStats;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>int</returns>
        private int CalculateDistancePenalty()
        {
            //later include weather and defense???
            if (distance >= 25)
                return distance;
            return 0;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns>int</returns>
        public override int Execute()
        {
            StringBuilder report = new StringBuilder();
            int ret = -1;
            int roll = Dice.Roll("d100");

            int kick = principalBallcarrier.PlayerSkills.Kick - CalculateDistancePenalty();

            if (roll <= kick)
            {
                fieldGoalResult = FieldGoalResult.Good;

                ret = distance;
            }
            else
            {
                fieldGoalResult = FieldGoalResult.NotGood;
            }

            gameStats.AddKickAttempt(distance, fieldGoalResult, isExtraPoint);
            ((KickPlayStatSheet)principalBallcarrier.Stats[StatTypes.Kicking]).AddKickAttempt(distance, fieldGoalResult, isExtraPoint);

            if (isExtraPoint)
            {
                report.Append(String.Format("Extra Point attempt by {0} ", principalBallcarrier.Name));
            }
            else
            {
                report.Append(String.Format("Field goal attempt by {0} ", principalBallcarrier.Name));
            }
            if (fieldGoalResult == FieldGoalResult.Good)
                report.Append("is good!");
            else
                report.Append("is no good!");
            
            playReport = report.ToString();
            playLength = distance;
            return distance;
        }

        /// <summary>
        /// 
        /// </summary>
        public FieldGoalResult FieldGoalResult
        {
            get { return fieldGoalResult; }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsExtraPoint
        {
            get { return isExtraPoint; }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsBlocked
        {
            get { return isBlocked; }
        }
    }
}
