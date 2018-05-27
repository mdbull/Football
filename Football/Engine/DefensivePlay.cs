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
    public abstract class DefensivePlay:Play
    {
        protected Player originalBallcarrier = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="originalBallcarrier">Player</param>
        /// <param name="defender">Player</param>
        public DefensivePlay(Player originalBallcarrier, Team defense)
        {
            this.originalBallcarrier = originalBallcarrier;
            this.principalBallcarrier = defense.TeamOffense.GetPlayerAtPosition("TB");
        }

       
        /// <summary>
        /// 
        /// </summary>
        public Player OriginalBallcarrier
        {
            get { return originalBallcarrier; }
        }

        /// <summary>
        /// 
        /// </summary>
        public Player Defender
        {
            get { return principalBallcarrier; }
        }
    }
}
