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
    public enum ScoreType
    {
        ExtraPoint=1,
        Safety=2,
        FieldGoal=3,
        Touchdown=6,
        Unknown
    }

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public struct ScoreStatus
    {
        public Team LeadingTeam;
        public Team TrailingTeam;
        public int LeadingTeamScore;
        public int TrailingTeamScore;

        /// <summary>
        /// 
        /// </summary>
        /// <returns>bool</returns>
        public bool CheckForTie()
        {
            return ((TrailingTeamScore - LeadingTeamScore) == 0);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public delegate void GameOverEventHandler();

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class ScoreInformation:FootballEntity
    {
        /// <summary>
        /// 
        /// </summary>

        private int currentQuarter = -1;
        private ScoreType scoreType = ScoreType.Unknown;
        private int distance = 0;
        private Player scoringPlayer = null;
        private Player thrower = null;
        private PlayType playType = PlayType.Unknown;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="play">Play</param>
        /// <param name="playType">PlayType</param>
        /// <param name="scoreType">ScoreType</param>
        public ScoreInformation(int currentQuarter,Play play, PlayType playType, ScoreType scoreType)
        {
            this.currentQuarter = currentQuarter;
            if (play is PassPlay)
                this.thrower = ((PassPlay)play).Thrower;
            if (play is Safety)
                this.scoringPlayer = ((Safety)play).RecoveringDefender;
            else
                this.scoringPlayer = play.PrincipalBallcarrier;
            this.scoreType = scoreType;
            this.playType = playType;
            this.distance = play.PlayLength;
            SetToString();
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void SetToString()
        {
            StringBuilder ret = new StringBuilder();
            //ret.Append(String.Format("Q{0} - {1} ",currentQuarter+1,scoringPlayer.Team.Name));
            ret.Append(String.Format("{0} ", scoringPlayer.Team.Name));
            switch (playType)
            {
                case PlayType.Run:
                    ret.Append(String.Format("{0} {1} run", scoringPlayer.Name, distance));
                    break;
                case PlayType.Pass:
                    ret.Append(String.Format("{0} {1} reception from {2}", scoringPlayer.Name, distance, thrower.Name));
                    break;
                case PlayType.KickoffReturn:
                    ret.Append(String.Format("{0} {1} kick return", scoringPlayer.Name, distance));
                    break;
                case PlayType.PuntReturn:
                    ret.Append(String.Format("{0} {1} punt run", scoringPlayer.Name, distance));
                    break;
                case PlayType.FieldGoal:
                    ret.Append(String.Format("{0} {1} fg", scoringPlayer.Name, distance));
                    break;
                case PlayType.ExtraPoint:
                    ret.Append(String.Format("{0} xp", scoringPlayer.Name));
                    break;
                case PlayType.SafetyRecovery:
                    ret.Append(String.Format("{0} {1} safety", scoringPlayer.Name, distance));
                    break;
            }
            toString = ret.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        public int CurrentQuarter
        {
            get { return currentQuarter; }
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
        public Player ThrowingPlayer
        {
            get { return thrower; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Distance
        {
            get { return distance; }
        }

        /// <summary>
        /// 
        /// </summary>
        public ScoreType ScoreType
        {
            get { return scoreType; }
        }

        /// <summary>
        /// 
        /// </summary>
        public PlayType PlayType
        {
            get { return playType; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            return toString;
        }


        protected override void Init()
        {
            
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="scoreboard"></param>
    public delegate void ScoreboardUpdatedEventHandler(Scoreboard scoreboard);

    /// <summary>
    /// 
    /// </summary>
    public delegate void OvertimeEventHandler(int currentQuarter);

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class Scoreboard:FootballEntity
    {
        private Team roadTeam = null;
        private Team homeTeam = null;
        private int currentQuarter = -1;
        private List<int> roadTeamQuarterScores = new List<int>();
        private List<int> homeTeamQuarterScores = new List<int>();
        private List<ScoreInformation> scoringSummary = new List<ScoreInformation>();

        public event GameOverEventHandler gameOver;

        public event ScoreboardUpdatedEventHandler scoreboardUpdated;

        public event OvertimeEventHandler overtimeQuarterIncremented;



        /// <summary>
        /// 
        /// </summary>
        /// <param name="roadTeam">Team</param>
        /// <param name="homeTeam">Team</param>
        public Scoreboard(Team roadTeam, Team homeTeam)
        {
            this.roadTeam = roadTeam;
            this.homeTeam = homeTeam;
            for (int i = 0; i < 4; ++i)
            {
                roadTeamQuarterScores.Add(0);
                homeTeamQuarterScores.Add(0);
            }
            MoveToNextQuarter();
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void Init()
        {
            throw new NotImplementedException();
        }

        private void OnGameOver()
        {
            if (gameOver != null)
                gameOver();
        }

        /// <summary>
        /// 
        /// </summary>
        public Team RoadTeam
        {
            get { return roadTeam; }
        }

        /// <summary>
        /// 
        /// </summary>
        public Team HomeTeam
        {
            get { return homeTeam; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int CurrentQuarter
        {
            get { return currentQuarter; }
        }

        /// <summary>
        /// 
        /// </summary>
        public void MoveToNextQuarter()
        {
            currentQuarter++;
            if (currentQuarter > 3)
            {
                if (!GoToOvertime())
                {
                    OnGameOver();
                }
                else
                {
                    roadTeamQuarterScores.Add(0);
                    homeTeamQuarterScores.Add(0);
                    onOvertimeQuarterIncremented(currentQuarter);
                }

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="currentQuarter">int</param>
        private void onOvertimeQuarterIncremented(int currentQuarter)
        {
            if(overtimeQuarterIncremented!=null)
                overtimeQuarterIncremented(currentQuarter);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>bool</returns>
        private bool GoToOvertime()
        {
            return GetLeadingTeam().CheckForTie();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scoringTeam">Team</param>
        /// <param name="scoreType">ScoreType</param>
        private void UpdateScoreboard(Team scoringTeam, ScoreType scoreType)
        {
            if (scoringTeam == roadTeam)
            {
                roadTeamQuarterScores[currentQuarter] += (int)scoreType;
            }
            else
            {
                homeTeamQuarterScores[currentQuarter] += (int)scoreType;
            }

            OnScoreboardUpdated();
        }

        /// <summary>
        /// 
        /// </summary>
        private void OnScoreboardUpdated()
        {
            if (scoreboardUpdated != null)
                scoreboardUpdated(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="play">Play</param>
        /// <param name="scoreType">ScoreType</param>
        private void AddScore(Play play, ScoreType scoreType)
        {
            ScoreInformation scoreInfo = null;
            PlayType playType=PlayType.Unknown;

            if (play is RunPlay)
                playType = PlayType.Run;
            else if (play is PassPlay)
                playType = PlayType.Pass;
            else if (play is KickoffReturnPlay)
                playType = PlayType.KickoffReturn;
            else if (play is Safety)
                playType = PlayType.SafetyRecovery;
            else if (play is Fumble)
                playType = PlayType.FumbleReturn;
            else if (play is Interception)
                playType = PlayType.InterceptionReturn;
            else if (play is FieldGoal)
            {
                if (((FieldGoal)play).IsExtraPoint)
                    playType = PlayType.ExtraPoint;
                else
                    playType = PlayType.FieldGoal;
            }
            scoreInfo = new ScoreInformation(currentQuarter,play,playType,scoreType);
            scoringSummary.Add(scoreInfo);
            UpdateScoreboard(play.PrincipalBallcarrier.Team, scoreType);
        }

       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="play">Play</param>
        public void AddScore(Play play)
        {
            ScoreType scoreType = ScoreType.Touchdown;
           
            if(play is Safety)
            {
                scoreType = ScoreType.Safety;
            }
            else if (play is FieldGoal)
            {
                if (((FieldGoal)play).IsExtraPoint)
                    scoreType = ScoreType.ExtraPoint;
                else
                    scoreType = ScoreType.FieldGoal;
            }
            AddScore(play, scoreType);
          
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index">int</param>
        /// <param name="homeTeam">homeTeam</param>
        /// <returns>int</returns>
        public int GetTeamScoreByQuarter(int index, bool homeTeam)
        {
            if (!homeTeam)
                return roadTeamQuarterScores[index];
            else
                return homeTeamQuarterScores[index];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="team">Team</param>
        /// <returns>int</returns>
        public int GetTeamScore(Team team)
        {
            if (team==roadTeam)
                return roadTeamQuarterScores.Sum();
            else
                return homeTeamQuarterScores.Sum();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>LeadingTeam</returns>
        public ScoreStatus GetLeadingTeam()
        {
            //assume home team is winning
           
            int roadScore=GetTeamScore(roadTeam);
            int homeScore=GetTeamScore(homeTeam); 
            ScoreStatus leadingTeam = new ScoreStatus(){LeadingTeam=homeTeam,LeadingTeamScore=homeScore,TrailingTeam=roadTeam,TrailingTeamScore=roadScore};
            if (roadScore > homeScore)//is road team winning?
            {
                leadingTeam.LeadingTeam = roadTeam;
                leadingTeam.LeadingTeamScore=roadScore;
                leadingTeam.TrailingTeam = homeTeam;
                leadingTeam.TrailingTeamScore = homeScore;
            }
            return leadingTeam;
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>string</returns>
        public string GetScore()
        {
            ScoreStatus leadingTeam = GetLeadingTeam();
            return String.Format("{0} {1}, {2} {3}", leadingTeam.LeadingTeam,leadingTeam.LeadingTeamScore,  leadingTeam.TrailingTeam,leadingTeam.TrailingTeamScore);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index">int</param>
        /// <returns>ScoreInformation</returns>
        public ScoreInformation this[int index]
        {
            get { return scoringSummary[index]; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            return GetScore();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>string</returns>
        public string GetScoringSummary()
        {
            StringBuilder ret=new StringBuilder();
            for (int i = 0; i < scoringSummary.Count; ++i)
                ret.AppendLine(scoringSummary[i].ToString());
            return ret.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>string</returns>
        public string GetScoringSummaryByQuarters()
        {
            Dictionary<int, List<ScoreInformation>> map = new Dictionary<int, List<ScoreInformation>>();
            StringBuilder ret = new StringBuilder();
            for (int i = 0; i < scoringSummary.Count; ++i)
            {
                int currQuarter=scoringSummary[i].CurrentQuarter;
                if (!map.ContainsKey(currQuarter))
                    map.Add(currQuarter, new List<ScoreInformation>());
                map[currQuarter].Add(scoringSummary[i]);
                

            }

            Dictionary<int, List<ScoreInformation>>.Enumerator cursor = map.GetEnumerator();
            while (cursor.MoveNext())
            {
                int quarter = cursor.Current.Key + 1;
                if (quarter > 4)
                    ret.AppendFormat("**OT{0}***\n", quarter - 4);
                else
                    ret.AppendLine(String.Format("***Q{0}***", quarter));
                ScoreInformation [] scores=cursor.Current.Value.ToArray();
                for (int j = 0; j < scores.Length; ++j)
                {
                    ret.AppendLine(scores[j].ToString());
                }
            }

            return ret.ToString();
        }
    }
}
