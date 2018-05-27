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
    public class Interception:DefensivePlay
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cc">ChainCrew</param>
        /// <param name="thrower">Player</param>
        /// <param name="defender">Player</param>
        public Interception(ChainCrew cc, Player thrower, Team defense):base(thrower,defense)
        {
            this.cc = cc;
            this.originalBallcarrier= thrower;
            this.principalBallcarrier = defense.TeamOffense.GetPlayerAtPosition("TB");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>int</returns>
        public override int Execute()
        {
            StringBuilder report = new StringBuilder();
                
            DefensiveStatSheet defensiveStats = (DefensiveStatSheet)this.principalBallcarrier.Stats[StatTypes.Defense];
            defensiveStats.Interceptions++;

            this.cc.ChangePossession();
            this.cc.ToggleDirection();

            int returnDistance = Dice.Roll(this.principalBallcarrier.PlayerSkills.Run);

            report.Append("INTERCEPTION!!!! ");


            report.Append(String.Format("{0} intercepts the ball and takes it to the {1} for a return of {2} yards! First and 10.", this.principalBallcarrier.Name, this.cc.CurrentYardLine, playLength));

            playReport = report.ToString();
            playLength = returnDistance;
            return playLength;
        }

      
    }
}
