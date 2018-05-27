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
    public enum PlayType
    {
        Run,
        Pass,
        Kickoff,
        KickoffReturn,
        Punt,
        PuntReturn,
        ExtraPoint,
        FieldGoal,
        InterceptionReturn,
        FumbleReturn,
        SafetyRecovery,
        Unknown
    }

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public abstract class Play: IExecutable
    {
        /// <summary>
        /// 
        /// </summary>
        protected Team offense = null;
        protected Team defense = null;
        protected int playLength=0;
        protected ChainCrew cc = null;
        protected string playReport = string.Empty;
        protected Player principalBallcarrier = null;

        /// <summary>
        /// 
        /// </summary>
        public Play() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cc">ChainCrew</param>
        /// <param name="offense">Team</param>
        /// <param name="defense">Team</param>
        public Play(ChainCrew cc,Team offense, Team defense)
        {
            this.cc = cc;
            this.cc.playCompleted += new PlayEventHandler(cc_onPlayCompleted);
            this.offense = offense;
            this.defense = defense;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cc">ChainCrew</param>
        /// <param name="offense">Team</param>
        /// <param name="defense">Team</param>
        /// <param name="principalBallcarrier">Player</param>
        /// <param name="playLength">int</param>
        public Play(ChainCrew cc, Team offense, Team defense,Player principalBallcarrier, int playLength)
        {
            this.cc = cc;
            this.cc.playCompleted += new PlayEventHandler(cc_onPlayCompleted);
            this.offense = offense;
            this.defense = defense;
            this.principalBallcarrier = principalBallcarrier;
            this.playLength = playLength;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="thrower"></param>
        /// <param name="ballcarrier"></param>
        /// <param name="length"></param>
        /// <param name="isTd"></param>
        /// <param name="isSack"></param>
        /// <param name="isInterception"></param>
        /// <param name="isFumble"></param>
        void cc_onPlayCompleted(Player thrower, Player ballcarrier, int length, bool isTd,bool isSack, bool isInterception, bool isFumble)
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="player">Player</param>
        /// <returns>bool</returns>
        public static bool CheckFumble(Player player)
        {
            int roll=Dice.Roll("d100");
            if (roll <= player.Fumble)
                return true;
            return false;
        }
        
        /// <summary>
        /// 
        /// </summary>
        public abstract int Execute();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="length">int</param>
        public Play(int length)
        {
            this.playLength = length;
        }

        /// <summary>
        /// 
        /// </summary>
        public Team Offense
        {
            get { return offense; }
        }

        /// <summary>
        /// 
        /// </summary>
        public Team Defense
        {
            get { return defense; }
        }

        /// <summary>
        /// 
        /// </summary>
        public Player PrincipalBallcarrier
        {
            get { return principalBallcarrier; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int PlayLength
        {
            get { return playLength; }
            set { playLength = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string PlayReport
        {
            get { return playReport; }
        }
    }
}
