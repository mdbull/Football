using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Football.Engine
{
    /// <summary>
    /// 
    /// </summary>
    public class Kickoff : Play
    {
        private Player kicker = null;
        private Player kickReturner = null;
        //private ChainCrew cc = null;
        private GameAnnouncer gameAnnouncer = null;
        //private Weather weatherReport = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameAnnouncer"></param>
        /// <param name="cc"></param>
        /// <param name="direction"></param>
        /// <param name="kickingTeam"></param>
        /// <param name="receivingTeam"></param>
        public Kickoff(GameAnnouncer gameAnnouncer,ChainCrew cc, Direction direction, Team kickingTeam, Team receivingTeam)
            : base(cc, receivingTeam, kickingTeam)
        {
            this.cc = cc;
            this.gameAnnouncer = gameAnnouncer;
            this.kicker = kickingTeam.TeamOffense.GetPlayerAtPosition("K");
            this.kickReturner = receivingTeam.TeamOffense.GetPlayerAtPosition("KR");
            this.cc.SetBall(30, direction, this.defense, this.offense);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>int</returns>
        public override int Execute()
        {
            
            int kickDistance = 0;

            kickDistance = Dice.Roll(30, kicker.PlayerSkills.Kick);

            this.cc.MoveBallKickoff(defense, offense, kickDistance);

            return kickDistance;

        }
    }
}
