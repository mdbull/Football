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
    public class Safety:Play
    {
        
        private Player recoveringDefender = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cc">ChainCrew</param>
        /// <param name="offense">Team</param>
        /// <param name="defense">Team</param>
        public Safety(ChainCrew cc, Player ballcarrier, Team defense, int playLength)
            : base(playLength)
        {
            this.cc = cc;
            this.defense = defense;
            principalBallcarrier = ballcarrier;
            recoveringDefender = new Player(defense, "11", "Defender", "RM", 18, 72, 200, Race.Black, Grade.Senior, Endurance.Good, "99", 25);
            recoveringDefender.Positions.Add("DL");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>int</returns>
        public override int Execute()
        {
            cc.Field.Scoreboard.AddScore(this);
           
            playReport = String.Format("SAFETY!!! {0} is stopped in the endzone for a loss of {1} yards. The ball is recovered by {2}'s {3}!", principalBallcarrier.Name, playLength, this.defense.Name, recoveringDefender.Name);
            
            return playLength;
        }

        /// <summary>
        /// 
        /// </summary>
        public Player RecoveringDefender
        {
            get { return recoveringDefender; }
        }
    }
}
