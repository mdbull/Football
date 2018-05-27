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
    public class Touchdown:Play
    {
        Play scoringPlay = null;
        Game game = null;
        ChainCrew cc = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="game">Game</param>
        /// <param name="cc">ChainCrew</param>
        /// <param name="scoringPlay">Play</param>
        public Touchdown(Game game,ChainCrew cc,Play scoringPlay)
        {
            this.game = game;
            this.cc = cc;
            this.scoringPlay = scoringPlay;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns>int</returns>
        public override int Execute()
        {
            this.cc.Field.Scoreboard.AddScore(scoringPlay);
            Player ballcarrier = scoringPlay.PrincipalBallcarrier;
            int indexOfBallcarrier = ballcarrier.Team.GetIndexOfPlayer(ballcarrier);
            if (scoringPlay is RunPlay)
            {
                CarryStatSheet gameStats = (CarryStatSheet)game.GameStats[ballcarrier.Team, indexOfBallcarrier][StatTypes.Run];
                gameStats.AddTouchdown(scoringPlay.PlayLength);
                ((CarryStatSheet)ballcarrier.Stats[StatTypes.Run]).AddTouchdown(scoringPlay.PlayLength);
            }
            else if (scoringPlay is PassPlay)
            {
                
                Player thrower = ((PassPlay)scoringPlay).Thrower;
                int indexOfPasser = thrower.Team.GetIndexOfPlayer(thrower);
               
                PassPlayStatSheet gamePassingStats = (PassPlayStatSheet)game.GameStats[ballcarrier.Team, indexOfPasser][StatTypes.Pass];
                gamePassingStats.AddTouchdown(scoringPlay.PlayLength);
                ((PassPlayStatSheet)thrower.Stats[StatTypes.Pass]).AddTouchdown(scoringPlay.PlayLength);
                CarryStatSheet gameReceivingStats = (CarryStatSheet)game.GameStats[ballcarrier.Team, indexOfBallcarrier][StatTypes.Receive];
                gameReceivingStats.AddTouchdown(scoringPlay.PlayLength);
                ((CarryStatSheet)ballcarrier.Stats[StatTypes.Receive]).AddTouchdown(scoringPlay.PlayLength);
            }
            else if ((scoringPlay is Interception) || (scoringPlay is Fumble))
            {
                DefensiveStatSheet gameDefensiveStats = (DefensiveStatSheet)game.GameStats[ballcarrier.Team, indexOfBallcarrier][StatTypes.Defense];
                gameDefensiveStats.DefensiveTouchdowns++;
                ((DefensiveStatSheet)ballcarrier.Stats[StatTypes.Defense]).DefensiveTouchdowns++;
            }
            else if (scoringPlay is KickoffReturnPlay)
            {
                CarryStatSheet gameReturnStats = (CarryStatSheet)game.GameStats[ballcarrier.Team, indexOfBallcarrier][StatTypes.Return];
                gameReturnStats.AddTouchdown(scoringPlay.PlayLength);
                ((CarryStatSheet)ballcarrier.Stats[StatTypes.Return]).AddTouchdown(scoringPlay.PlayLength);
            }

            StringBuilder report = new StringBuilder();
            if (scoringPlay is RunPlay)
                report.Append((String.Format("TOUCHDOWN!! {0}! {1} scores from {2} yards out.", ballcarrier.Team.Name, ballcarrier.Name, scoringPlay.PlayLength)));
            else if (scoringPlay is PassPlay)
                report.Append((String.Format("TOUCHDOWN!! {0}! {1} scores from {2} yards out on a pass from {3}!", ballcarrier.Team.Name, ballcarrier.Name, scoringPlay.PlayLength, ((PassPlay)scoringPlay).Thrower.Name)));
            else if (scoringPlay is KickoffReturnPlay)
                report.Append((String.Format("TOUCHDOWN!! {0}! {1} scores on a {2} yard kickoff return!", ballcarrier.Team.Name, ballcarrier.Name, scoringPlay.PlayLength)));
            else if (scoringPlay is Interception)
                report.Append((String.Format("TOUCHDOWN!! {0}! {1} scores on an interception return of {2} yards!", ballcarrier.Team.Name, ballcarrier.Name, scoringPlay.PlayLength)));
            else if (scoringPlay is Fumble)
                report.Append((String.Format("TOUCHDOWN!! {0}! {1} rumbles into the endzone on a fumble return of {2} yards!", ballcarrier.Team.Name, ballcarrier.Name, scoringPlay.PlayLength)));
            report.AppendLine(String.Format(" And the score is {0}", game.Scoreboard));

            playReport = report.ToString();

            return scoringPlay.PlayLength;
        }

        /// <summary>
        /// 
        /// </summary>
        Play ScoringPlay
        {
            get { return scoringPlay; }
        }
    }
}
