using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Football.Engine
{
    /// <summary>
    /// 
    /// </summary>
    public enum CoinFlipResult
    {
        Heads = 1,
        Tails
    }

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public struct CoinFlipWinner
    {
        public Team Winner;
        public Team Loser;
    }

    /// <summary>
    /// 
    /// </summary>
    public class CoinFlip
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns>CoinFlipResult</returns>
        public CoinFlipResult Flip()
        {
            return (CoinFlipResult) Dice.Roll("d2");

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="callingTeam"></param>
        /// <param name="nonCallingTeam"></param>
        /// <param name="call"></param>
        /// <returns></returns>
        public CoinFlipWinner DeterminePossession(GameAnnouncer announcer,Team callingTeam, Team nonCallingTeam, CoinFlipResult call)
        {
            
            CoinFlipWinner winner = new CoinFlipWinner() { Winner = callingTeam, Loser = nonCallingTeam };
            if (Flip() != call)
            {
                winner.Winner = nonCallingTeam;
                winner.Loser = callingTeam;
            }
            announcer.ReportGameEvent(String.Format("{0}: '{1} have won the coin flip.'",announcer.Name, winner.Winner));
            return winner;
                
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e">AnnounceReportEventArgs</param>
        private void announcer_reportAnnounced(AnnounceReportEventArgs e)
        {
            Console.WriteLine(e.Report);
        }
    }
}
