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
    public class RunPlay: Play
    {
        protected CarryStatSheet runStats = null;
        /// <summary>
        /// 
        /// </summary>
        public RunPlay():base(0) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cc">ChainCrew</param>
        /// <param name="offense">Team</param>
        /// <param name="defense">Team</param>
        /// <param name="ballcarrier">Player</param>
        public RunPlay(ChainCrew cc,Team offense, Team defense, Player ballcarrier, CarryStatSheet runStats):base(cc,offense,defense)
        {
            
            this.principalBallcarrier = ballcarrier;
            this.runStats = runStats;
        }

       
       /// <summary>
       /// 
       /// </summary>
       /// <returns>int</returns>
        public override int Execute()
        {
            string ballcarrierRun = principalBallcarrier.PlayerSkills.Run;
            int roll=Dice.Roll("d12");
            
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

            CarryPlayResult res = this.defense.TeamDefense.RunDefense(roll);
            bool isTd=false;
            bool isFumble = false;
            switch (res)
            {

                case CarryPlayResult.Loss:
                    string rollString = String.Format("d{0}", defense.TeamDefense.MaxRunLoss);
                    playLength= -(Dice.Roll(rollString));
                    //check for fumble
                    isFumble = CheckFumble(principalBallcarrier);
                    break;
                case CarryPlayResult.NoGain:
                    playLength = 0;
                    isFumble = CheckFumble(principalBallcarrier);
                    break;
                case CarryPlayResult.NormalGain:
                    playLength = Dice.Roll(ballcarrierRun);
                    break;
                case CarryPlayResult.Bonus:
                    playLength=Dice.Roll(ballcarrierRun) + this.defense.TeamDefense.BonusRunPenalty;
                    break;
                case CarryPlayResult.AutoTD:
                    playLength = cc.GetAutoTDDistance();
                    isTd = true;
                    break;
            }
            playLength = cc.MoveBallRun(this, isFumble);
            //playLength = cc.MoveBall(PlayType.Run, null, principalBallcarrier, playLength, false, isFumble);
           
            runStats.AddRun(playLength, isTd, isFumble);
            ((CarryStatSheet)this.principalBallcarrier.Stats[StatTypes.Run]).AddRun(playLength, isTd, isFumble);
            return playLength;
        }

        
    }

    
}
