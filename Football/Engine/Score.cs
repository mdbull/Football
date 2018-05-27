using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Football.Engine
{
    [Serializable]
    public class Score
    {
        private Scoreboard scoreboard = null;
        private Team teamInPossession = null;
        private Player scoringPlayer = null;
        private int playLength = 0;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="teamInPossession"></param>
        /// <param name="scoringPlayer"></param>
        /// <param name="playLength"></param>
        public Score(Scoreboard scoreboard,Team teamInPossession, Player scoringPlayer, int playLength)
        {
            this.scoreboard = scoreboard;
            this.teamInPossession = teamInPossession;
            this.scoringPlayer = scoringPlayer;
            this.playLength = playLength;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scoreType">ScoreType</param>
        public void RecordScore(ScoreType scoreType)
        {
            //this.scoreboard.UpdateScoreboard(this.teamInPossession, scoreType);
        }
        /// <summary>
        /// 
        /// </summary>
        public Team ScoringTeam
        {
            get { return teamInPossession; }
        }

        /// <summary>
        /// 
        /// </summary>
        public Player ScoringPlayer
        {
            get { return scoringPlayer; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int PlayLength
        {
            get { return playLength; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            return String.Format("TOUCHDOWN {0}!!! {1} scores from {2} yards out!!! {1} {2}", this.teamInPossession.Name, this.scoringPlayer.Name, this.playLength);
        }
    }
}
