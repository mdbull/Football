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
    public class Fumble:Play
    {
        private Player originalBallcarrier = null;//player who fumbled
        /// <summary>
        /// 
        /// </summary>
        /// <param name="originalBallcarrier"></param>
        /// <param name="defense"></param>
        public Fumble(ChainCrew cc,Player originalBallcarrier, Team defense)
        {
            this.cc = cc;
            this.originalBallcarrier = originalBallcarrier;
            this.principalBallcarrier = defense.TeamOffense.GetPlayerAtPosition("TB");
            this.defense = defense;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>int</returns>
        public override int Execute()
        {
            int roll = Dice.Roll("d2");
            bool ballLost = false;
            if (roll == 1)//team loses ball!
                ballLost = true;
            YardLine yl = cc.CurrentYardLine;
            StringBuilder report = new StringBuilder(String.Format("FUMBLE!!!! {0} loses the ball at the {1} yardline after ", this.originalBallcarrier.Name,yl));
            if (playLength > 0)
                report.Append(String.Format("a gain of {0} yards!", playLength));
            else if (playLength < 0)
                report.Append(String.Format("a loss of {0} yards!", playLength));
            else
                report.Append("being stopped at the line of scrimmage!");

            if (ballLost)
            {
                report.Append(String.Format("The ball is recovered by the {0}! Turnover!", this.defense));
                cc.ChangePossession();
                cc.ToggleDirection();
                cc.MoveBallDefensiveReturn(this);
                cc.SetFirstDown();
            }
            else
            {
                report.Append(String.Format("The ball is recovered by {0}! The {1} manage to hold on to the ball!", this.defense, this.defense.Mascot));
            }
            this.playReport = report.ToString();
            return playLength;
        }

        
    }
}
