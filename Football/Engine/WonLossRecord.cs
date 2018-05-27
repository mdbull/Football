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
    public enum GameResultType
    {
        Win,
        Loss,
        Tie,
        Unknown
    }

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class GameResult:FootballEntity
    {
        private Team opponent = null;
        private bool isHomeGame = true;
        private int oppPF = 0;
        private int teamPF = 0;
        private GameResultType gameResultType=GameResultType.Unknown;
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="team">Team</param>
        /// <param name="opponent">Team</param>
        /// <param name="teamPF">int</param>
        /// <param name="oppPF">int</param>
        /// <param name="isHomeGame">bool</param>
        public GameResult(Team opponent, int teamPF, int oppPF, bool isHomeGame)
        {
           
            this.opponent = opponent;
            this.teamPF = teamPF;
            this.oppPF = oppPF;
            this.isHomeGame = isHomeGame;
            if(teamPF>oppPF)
                gameResultType=GameResultType.Win;
            else if(oppPF>teamPF)
                gameResultType=GameResultType.Loss;
            else
                gameResultType=GameResultType.Tie;
            SetName();
        }

        protected override void SetName()
        {
            StringBuilder ret = new StringBuilder();
            string oppName = opponent.Name;
            if (!isHomeGame)
                ret.Append("@");
            if (isHomeGame)
                oppName = oppName.ToUpper();
            ret.Append(oppName);
            switch (gameResultType)
            {
                case GameResultType.Win:
                    ret.Append(String.Format(" W, {0}-{1}", teamPF, oppPF));
                    break;
                case GameResultType.Loss:
                    ret.Append(String.Format(" L, {0}-{1}", oppPF, teamPF));
                    break;
                case GameResultType.Tie:
                    ret.Append(String.Format(" T, {0}-{0}", teamPF));
                    break;
            }
            name = ret.ToString();
        }

       
        /// <summary>
        /// 
        /// </summary>
        public Team Opponent
        {
            get { return opponent; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int TeamPF
        {
            get { return teamPF; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int OpponentPF
        {
            get { return oppPF; }
        }

        /// <summary>
        /// 
        /// </summary>
        public GameResultType GameResultType
        {
            get { return gameResultType; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            return name;

        }

        /// <summary>
        /// 
        /// </summary>
        protected override void Init()
        {
            
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class WonLossRecord:StatSheet
    {
        private List<GameResult> gameResults = new List<GameResult>();
        private int wins = 0;
        private int losses = 0;
        private int ties = 0;
        private int teamPF = 0;
        private int oppPF = 0;
        private double winPercentage = 0.0d;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity">StatsEntity</param>
        public WonLossRecord(StatsEntity entity):base(entity)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sheets">StatSheet[]</param>
        /// <returns>StatSheet</returns>
        protected override StatSheet AggregateStatSheets(params StatSheet[] sheets)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>double</returns>
        private double CalculateWinPercentage()
        {
            int totalGames = wins + losses + ties;
            return (double)wins / (double)totalGames;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameResult">GameResult</param>
        public void AddGameResult(GameResult gameResult)
        {
            switch (gameResult.GameResultType)
            {
                case GameResultType.Win:
                    wins++;
                    break;
                case GameResultType.Loss: 
                    losses++;
                    break;
                case GameResultType.Tie:
                    ties++;
                    break;
            }
            teamPF += gameResult.TeamPF;
            oppPF += gameResult.OpponentPF;
            gameResults.Add(gameResult);
            winPercentage = CalculateWinPercentage();
            SetName();
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void SetName()
        {
            name = String.Format("{0} {1}-{2}-{3} {4} PF: {5} PA: {6}",entity.Name, wins, losses, ties, winPercentage,teamPF,oppPF);
        }

        /// <summary>
        /// 
        /// </summary>
        public int Wins
        {
            get { return wins; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Losses
        {
            get { return losses; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Ties
        {
            get { return ties; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int TeamPF
        {
            get { return teamPF; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int OpponentPF
        {
            get { return oppPF; }
        }

        /// <summary>
        /// 
        /// </summary>
        public double WinPercentage
        {
            get { return winPercentage; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index">int</param>
        /// <returns>GameResult</returns>
        public GameResult this[int index]
        {
            get { return gameResults[index]; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            return name;
        }
    }
}
