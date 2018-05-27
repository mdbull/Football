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
        public class KickoffReturnPlay : Play
        {
           
            protected CarryStatSheet returnStats = null;
            /// <summary>
            /// 
            /// </summary>
            public KickoffReturnPlay() : base(0) { }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="cc">ChainCrew</param>
            /// <param name="offense">Team</param>
            /// <param name="defense">Team</param>
            /// <param name="ballcarrier">Player</param>
            public KickoffReturnPlay(ChainCrew cc, Team offense, Team defense, Player ballcarrier, CarryStatSheet returnStats)
                : base(cc, offense, defense)
            {

                this.principalBallcarrier = ballcarrier;
                this.returnStats = returnStats;
            }


            /// <summary>
            /// 
            /// </summary>
            /// <returns>int</returns>
            public override int Execute()
            {
                string ballcarrierRun = this.principalBallcarrier.PlayerSkills.KickReturn;
                int roll = Dice.Roll("d12");
                //team run bonus here

                //check for natural 12
                if (roll != 12)
                {
                    //apply any bonuses and/or penalties
                    roll += defense.TeamDefense.RunPenalty;
                    roll += offense.TeamOffense.OffensiveLineBonus;
                    if (roll < 0)
                        roll = 1;
                    else if (roll >= 12)
                    {
                        roll = 11;
                    }
                }

                CarryPlayResult res = this.defense.TeamDefense.KickoffReturnDefense(roll);
                bool isTd = false;
                bool isFumble = false;
                switch (res)
                {

                    case CarryPlayResult.Loss:
                        string rollString = String.Format("d{0}", defense.TeamDefense.MaxRunLoss);
                        playLength = -(Dice.Roll(rollString));
                        //check for fumble
                        isFumble = CheckFumble(this.principalBallcarrier);
                        break;
                    case CarryPlayResult.NoGain:
                        playLength = 0;
                        isFumble = CheckFumble(this.principalBallcarrier);
                        break;
                    case CarryPlayResult.NormalGain:
                        playLength = Dice.Roll(ballcarrierRun);
                        break;
                    case CarryPlayResult.Bonus:
                        playLength = Dice.Roll(ballcarrierRun) + this.defense.TeamDefense.BonusRunPenalty;
                        break;
                    case CarryPlayResult.AutoTD:
                        playLength = cc.GetAutoTDDistance();
                        isTd = true;
                        break;
                }
                playLength = cc.MoveBallKickoffReturn(this, isFumble);
                returnStats.AddRun(playLength, isTd, isFumble);
                ((CarryStatSheet)this.principalBallcarrier.Stats[StatTypes.Return]).AddRun(playLength, isTd, isFumble);
                playReport = String.Format("{0} takes the ball to the {1} for a return of {2} yards!", principalBallcarrier.Name,this.cc.CurrentYardLine, playLength);
                       
                return playLength;
            }
           
        }
    
}
