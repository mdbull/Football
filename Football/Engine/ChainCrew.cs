using System;
using System.Collections.Generic;
using System.Text;
using Football.Data;
namespace Football.Engine
{
    /// <summary>
    /// 
    /// </summary>
    public struct MoveChainsVariables
    {
        public int playLength;
        public bool isFumble;
        public bool isSafety;
        public bool isTd;
    }

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public enum Down
    {

        First,
        Second,
        Third,
        Fourth,
        Unknown
    }

    /// <summary>
    /// 
    /// </summary>
    public class PlayEventArgs : EventArgs
    {
        private Player thrower = null;
        private Player ballcarrier = null;
        private int length = 0;
        private bool isTd = false;
        private bool isSack = false;
        private bool isInterception = false;
        private bool isFumble = false;
       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="scoringPlayer">Player</param>
        /// <param name="length">int</param>
        public PlayEventArgs(Player thrower,Player ballcarrier, int length, bool isTd, bool isSack, bool isInterception, bool isFumble)
        {
            this.thrower = thrower;
            this.ballcarrier = ballcarrier;
            this.length = length;
            this.isTd = isTd;
            this.isSack = isSack;
            this.isInterception = isInterception;
            this.isFumble = isFumble;
            
        }

        /// <summary>
        /// 
        /// </summary>
        public Player Thrower
        {
            get { return thrower; }
        }

        /// <summary>
        /// 
        /// </summary>
        public Player BallCarrier
        {
            get { return ballcarrier; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Length
        {
            get { return length; }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsTd
        {
            get { return IsTd; }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsSack
        {
            get { return IsSack; }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsInterception
        {
            get { return IsInterception; }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsFumble
        {
            get { return IsFumble; }
        }
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
    public delegate void PlayEventHandler(Player thrower,Player ballcarrier, int length, bool isTd, bool isSack, bool isInterception, bool isFumble);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="report">string</param>
    public delegate void TurnoverEventHandler(string report);
 
    /// <summary>
    /// 
    /// </summary>
    /// <param name="e">TouchdownScoredEventArgs</param>
    public delegate void TouchdownScoredEventHandler(TouchdownScoredEventArgs e);

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class TouchdownScoredEventArgs: EventArgs
    {

        private Player ballcarrier=null;
        private int playLength=0;
        private string report=string.Empty;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ballcarrier">Player</param>
        /// <param name="playLength">int</param>
        /// <param name="report">string</param>
        public TouchdownScoredEventArgs(Player ballcarrier, int playLength, string report)
        {
            this.ballcarrier=ballcarrier;
            this.playLength=playLength;
            this.report=report;
        }

        /// <summary>
        /// 
        /// </summary>
        public Player Player
        {
            get { return ballcarrier; }
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
        public string Report
        {
            get { return report; }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="e"></param>
    public delegate void FumbleOccurredEventHandler(FumbleOccurredEventArgs e);

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class FumbleOccurredEventArgs : EventArgs
    {

        private Player ballcarrier = null;
        private int playLength = 0;
        private Team teamInPossession=null;
        private string report = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ballcarrier"></param>
        /// <param name="newTeamInPossession"></param>
        /// <param name="playLength"></param>
        /// <param name="report"></param>
        public FumbleOccurredEventArgs(Player ballcarrier, Team teamInPossession, int playLength, string report)
        {
            this.ballcarrier = ballcarrier;
            this.teamInPossession = teamInPossession;
            this.playLength = playLength;
            this.report = report;
        }

        /// <summary>
        /// 
        /// </summary>
        public Player Player
        {
            get { return ballcarrier; }
        }

        /// <summary>
        /// 
        /// </summary>
        public Team TeamInPossession
        {
            get { return teamInPossession; }
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
        public string Report
        {
            get { return report; }
        }
    }



    /// <summary>
    /// 
    /// </summary>
    /// <param name="e">FieldGoalAttempedEventArgs</param>
    public delegate void FieldGoalAttemptedEventHandler(FieldGoalAttempedEventArgs e);

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class FieldGoalAttempedEventArgs : EventArgs
    {
        private Player kicker = null;
        private FieldGoalResult fgResult = FieldGoalResult.Unknown;
        private int distance = 0;
        private bool isXp = false;
        private bool isBlocked = false;
        private string report = string.Empty;
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fgAttempt">FieldGoal</param>
        public FieldGoalAttempedEventArgs(FieldGoal fgAttempt)
        {
            this.kicker = fgAttempt.PrincipalBallcarrier;
            this.fgResult = fgAttempt.FieldGoalResult;
            this.distance = fgAttempt.PlayLength;
            this.report = fgAttempt.PlayReport;
            this.isXp = fgAttempt.IsExtraPoint;
            this.isBlocked = fgAttempt.IsBlocked;
        }

        /// <summary>
        /// 
        /// </summary>
        public Player Kicker
        {
            get { return kicker; }
        }

        /// <summary>
        /// 
        /// </summary>
        public FieldGoalResult FieldGoalResult
        {
            get { return fgResult; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Report
        {
            get { return report; }
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
        public bool IsXp
        {
            get { return isXp; }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsBlocked
        {
            get { return isBlocked; }
        }
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="e">SafetyEventArgs</param>
    public delegate void SafetyEventHandler(SafetyEventArgs e);

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class SafetyEventArgs : EventArgs
    {

        private Player ballcarrier = null;
        private int playLength = 0;
        private string report = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ballcarrier">Player</param>
        /// <param name="playLength">int</param>
        /// <param name="report">string</param>
        public SafetyEventArgs(Player ballcarrier, int playLength, string report)
        {
            this.ballcarrier = ballcarrier;
            this.playLength = playLength;
            this.report = report;
        }

        /// <summary>
        /// 
        /// </summary>
        public Player Player
        {
            get { return ballcarrier; }
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
        public string Report
        {
            get { return report; }
        }
    }
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class ChainCrew
    {
        public static readonly int DEFAULT_STARTING_YARDLINE = Convert.ToInt32(ConfigReader.GetConfigurationValue("DEFAULT_STARTING_YARDLINE"));
        public static readonly int DEFAULT_STARTING_YARDLINE_SAFETY = Convert.ToInt32(ConfigReader.GetConfigurationValue("DEFAULT_STARTING_YARDLINE_SAFETY"));
        private int currentIndex=-1;
        private Direction currentDirection;
        private Down currentDown;
        private int nextFirstDown = 0;
        private int yardsToGo = 0;
        private Field field;
        private Team teamInPossession = null;
        private Team teamNotInPossession = null;
        private Game game = null;
        
        /// <summary>
        /// 
        /// </summary>
        public event PlayEventHandler playCompleted;

        /// <summary>
        /// 
        /// </summary>
        public event TurnoverEventHandler turnoverCompleted;

        /// <summary>
        /// 
        /// </summary>
        public event TouchdownScoredEventHandler touchdownScored;

        /// <summary>
        /// 
        /// </summary>
        public event FieldGoalAttemptedEventHandler fieldGoalAttempted;

        /// <summary>
        /// 
        /// </summary>
        public event FumbleOccurredEventHandler fumbleOccurred;

        /// <summary>
        /// 
        /// </summary>
        public event SafetyEventHandler safetyOccurred;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="f">Field</param>
        public ChainCrew(Field f,Game game)
        {
            this.field = f;
            this.game = game;    
        }

        /// <summary>
        /// 
        /// </summary>
        private void ToggleTeamInPossession()
        {
            Team temp=teamInPossession;
            this.teamInPossession = teamNotInPossession;
            this.teamNotInPossession = temp;
            
        }

        /// <summary>
        /// 
        /// </summary>
        public void ToggleDirection()
        {
            
            if (this.currentDirection == Direction.Left)
                this.currentDirection = Direction.Right;
            else if(this.currentDirection==Direction.Right)
                this.currentDirection = Direction.Left;
        }

        /// <summary>
        /// 
        /// </summary>
        public void ChangePossession()
        {
            ToggleTeamInPossession();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="yardline">YardLine</param>
        /// <param name="direction">Direction</param>
        /// <returns>YardLine</returns>
        public YardLine SetBall(int yardline, Direction direction,Team tip,Team teamNotInPossession)
        {
            currentDirection = direction;
            this.teamInPossession = tip;
            this.currentDown = Down.First;
            this.teamNotInPossession = teamNotInPossession;
            if (currentDirection == Direction.Left)
            {
                currentIndex = 100-(yardline+1);
                nextFirstDown = currentIndex - 10;
            }
            else if (currentDirection == Direction.Right)
            {
                currentIndex = yardline - 1;
                nextFirstDown = currentIndex + 10;
            }
            return field[currentIndex];

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Down</returns>
        private Down CheckDown()
        {
            bool gotFirstDown = false;
            if (currentDirection == Direction.Left)
            {
                if (currentIndex <= nextFirstDown)
                {
                    gotFirstDown = true;
                    if ((currentIndex - 10) <= 0)
                    {
                        nextFirstDown = 0;
                    }
                    else
                    {
                        nextFirstDown = (currentIndex+1) - 10;
                    }
                    
                }
                else
                {
                    yardsToGo = currentIndex - nextFirstDown;
                }
                
            }
            else if (currentDirection == Direction.Right)
            {
                
                if (currentIndex >= nextFirstDown)
                {
                    gotFirstDown = true;
                    if ((currentIndex + 10) >= 99)
                    {
                        nextFirstDown = 99;
                    }
                    else
                    {
                        nextFirstDown = currentIndex + 10;
                    }
                }
                else
                {
                   
                    yardsToGo = nextFirstDown - currentIndex;
                    
                }
            }
            
            if (gotFirstDown)
            {
                if (currentDirection == Direction.Left)
                {
                    yardsToGo = (currentIndex+1) - nextFirstDown;
                }
                else
                {
                    yardsToGo = nextFirstDown - currentIndex;
                }
               
                currentDown=Down.First;
               
            }
            else
            {
                currentDown++;
                
            }
            return currentDown;
        }

        /// <summary>
        /// 
        /// </summary>
        private void TurnoverOnDowns()
        {
            SetFirstDown();
            ToggleTeamInPossession();
            ToggleDirection();
            OnTurnoverCompleted(String.Format("Turnover on downs! The {0} take over at the {1} yardline.",this.teamInPossession,CurrentYardLine));
        
        }

        /// <summary>
        /// 
        /// </summary>
        public void SetFirstDown()
        {
            this.currentDown=Down.First;
            this.yardsToGo = 10;
        }
        /// <summary>
        /// 
        /// </summary>
        private void OnTurnoverCompleted(string report)
        {
            if (turnoverCompleted != null)
                turnoverCompleted(report);
        }

        /// <summary>
        /// Return ball in case of fumble or interception return.
        /// </summary>
        /// <param name="defender">Player</param>
        /// <param name="yards">int</param>
        /// <returns>int</returns>
        public int MoveBallDefensiveReturn(Play play)
        {
            MoveChainsVariables var = MoveChains(play, Play.CheckFumble(play.PrincipalBallcarrier));
            play.PlayLength = var.playLength;
            if (!var.isTd)
            {
                if (var.isFumble)
                {
                    OnFumbleOccurred(play.PrincipalBallcarrier, var.playLength);
                }
                else
                {
                    SetFirstDown();
                    this.game.GameAnnouncer.ReportGameEvent(play.PlayReport.ToString());
                }
            }
            else
            {
                OnTouchdownScored(play);
            }
            return var.playLength;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="kickingTeam">Team</param>
        /// <param name="receivingTeam">Team</param>
        /// <returns>int</returns>
        public int MoveBallKickoff(Team kickingTeam, Team receivingTeam, int kickDistance)
        {
            
            if (currentDirection == Direction.Left)
            {
                if ((currentIndex - kickDistance) <= 0)//touchback
                {
                    kickDistance = currentIndex;
                    currentIndex = 19;
                }
                else
                {
                    currentIndex -= kickDistance;
                    
                }
            }
            else if (currentDirection == Direction.Right)
            {
                if ((currentIndex + kickDistance) >=99)//touchback
                {
                    kickDistance = currentIndex;
                    currentIndex = 79;
                }
                else
                {
                    currentIndex += kickDistance;

                }
            }
            
            
            SetBall(currentIndex, currentDirection, receivingTeam, kickingTeam);
            ToggleDirection();
            Player kicker=kickingTeam.TeamOffense.GetPlayerAtPosition("K");
            Player returner=receivingTeam.TeamOffense.GetPlayerAtPosition("KR");
            int playerIndex=receivingTeam.GetIndexOfPlayer(returner);
            string reportPlay = String.Format("And the kickoff by {0} is in the air! It is fielded by {1} at the {2} yardline", kicker.Name, returner.Name, CurrentYardLine);
            this.game.GameAnnouncer.ReportGameEvent(reportPlay);
            
            //Return the kickoff
            CarryStatSheet returnStats = (CarryStatSheet)game.GameStats[receivingTeam, playerIndex][StatTypes.Return];
            KickoffReturnPlay kickoffReturn = new KickoffReturnPlay(this, receivingTeam, kickingTeam, returner, returnStats);
            int returnDistance=kickoffReturn.Execute();
            MoveBallKickoffReturn(kickoffReturn, false);
           
            return kickDistance;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="play">KickoffReturnPlay</param>
        /// <param name="isFumble">bool</param>
        /// <returns>int</returns>
        public int MoveBallKickoffReturn(KickoffReturnPlay play, bool isFumble)
        {
            MoveChainsVariables var = MoveChains(play, isFumble);
            play.PlayLength = var.playLength;
            if (!var.isTd)
            {
                if (var.isSafety)
                {
                    OnSafetyOccurred(play);
                    if (isFumble)
                        OnFumbleOccurred(play.PrincipalBallcarrier, var.playLength);
                }
                else
                {
                    if (isFumble)
                    {

                        OnFumbleOccurred(play.PrincipalBallcarrier, var.playLength);
                    }
                    else
                    {
                        SetFirstDown();
                        this.game.GameAnnouncer.ReportGameEvent(play.PlayReport);
                    }
                }
            }
            else
            {
                OnTouchdownScored(play);
            }
            return var.playLength;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="play">Play</param>
        /// <param name="isFumble">bool</param>
        /// <returns>MoveChainsVariables</returns>
        private MoveChainsVariables MoveChains(Play play,bool isFumble)
        {
            MoveChainsVariables var = new MoveChainsVariables();
            var.playLength = play.PlayLength;
            var.isTd = false;
            var.isFumble = isFumble;
            var.isSafety = false;

            if (currentDirection == Direction.Left)
            {
                if ((currentIndex - var.playLength) <= 0)
                {
                    //td here! 
                    var.playLength = (currentIndex + 1);
                    currentIndex = -1;
                    var.isTd = true;
                }
                else if ((currentIndex - var.playLength) > 99)
                {
                    //safety!
                    var.isSafety = true;

                    var.playLength = 99 - currentIndex;
                    currentIndex = -1;
                }
                else
                {
                    currentIndex -= var.playLength;
                }
            }
            else if (currentDirection == Direction.Right)
            {
                if ((currentIndex + var.playLength) > 99)
                {
                    var.playLength = (100 - currentIndex) - 1;
                    currentIndex = -1;
                    var.isTd = true;
                }
                else if ((currentIndex + var.playLength) <= 0)
                {
                    //safety!
                    var.isSafety = true;

                    var.playLength = currentIndex + 1;
                    currentIndex = -1;
                }
                else
                {
                    currentIndex += var.playLength;
                   
                }
            }

            return var;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="play">Play</param>
        /// <param name="isSack">bool</param>
        /// <param name="isInterception">bool</param>
        /// <param name="isFumble">bool</param>
        /// <returns>int</returns>
        public int MoveBallPass(PassPlay play,bool isSack,bool isInterception, bool isFumble)
        {
            MoveChainsVariables var = MoveChains(play, isFumble);
            play.PlayLength = var.playLength;
            if (!var.isTd)
            {
               
                if (var.isSafety)
                {
                    OnSafetyOccurred(play);
                    if (isFumble)
                        OnFumbleOccurred(play.Thrower, var.playLength);
                    
                }
                else
                {
                    if (isFumble)
                    {
                        OnFumbleOccurred(play.Thrower, var.playLength);
                    }
                    else
                    {
                        if (CheckDown() > Down.Fourth)
                        {
                            TurnoverOnDowns();
                        } 
                        ReportPassPlay(play,var.isTd,isSack,isInterception,var.isFumble);
                        
                    }
                }
            }
            else
            {
                OnTouchdownScored(play);
            }
            return var.playLength;  
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="play">Play</param>
        /// <param name="isFumble">bool</param>
        /// <returns>int</returns>
        public int MoveBallRun(Play play, bool isFumble)
        {
            
            MoveChainsVariables var = MoveChains(play, isFumble);
            play.PlayLength = var.playLength;
            if (!var.isTd)
            {
                Player tempBallcarrier = play.PrincipalBallcarrier;
                if (var.isSafety)
                {
                    OnSafetyOccurred(play);
                    if (isFumble)
                        OnFumbleOccurred(tempBallcarrier, var.playLength);
                }
                else
                {
                    if (isFumble)
                    {
                        OnFumbleOccurred(tempBallcarrier, var.playLength);
                    }
                    else
                    {
                        if (CheckDown() > Down.Fourth)
                        {
                            TurnoverOnDowns();
                        }
                        ReportPlay(play, var.isTd, var.isFumble);
                    }
                }
            }
            else
            {
                OnTouchdownScored(play);
            }
            return var.playLength;  
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isPostSafety">bool</param>
        private void DoKickoff(bool isPostSafety)
        {
            int startYardline = DEFAULT_STARTING_YARDLINE;
            if (isPostSafety)
            {
                startYardline = DEFAULT_STARTING_YARDLINE_SAFETY;
            }
            
            ChangePossession();
            ToggleDirection();
            
            SetBall(startYardline, this.currentDirection, this.teamInPossession, this.teamNotInPossession);

            Kickoff kickoff = new Kickoff(this.game.GameAnnouncer, this, Direction.Right, this.teamNotInPossession, this.TeamInPossession);
            kickoff.Execute();
        }

        /// <summary>
        /// 
        /// </summary>
        private void ExecuteExtraPointAttempt()
        {
           
            Player kicker = this.teamInPossession.TeamOffense.GetPlayerAtPosition("K");
            int indexOfKicker = this.teamInPossession.GetIndexOfPlayer(kicker);
            KickPlayStatSheet kickerGameStats = (KickPlayStatSheet)game.GameStats[this.teamInPossession, indexOfKicker][StatTypes.Kicking];
            FieldGoal fieldGoal = new FieldGoal(kicker, 20, true, kickerGameStats);
            fieldGoal.Execute();
            FieldGoalResult fgResult = fieldGoal.FieldGoalResult;
            if (fgResult == FieldGoalResult.Good)
                this.field.Scoreboard.AddScore(fieldGoal);
            OnFieldGoalAttempted(fieldGoal);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="play">Play</param>
        private void OnSafetyOccurred(Play play)
        {
            Safety safety = new Safety(this, play.PrincipalBallcarrier, play.Defense, play.PlayLength);
            safety.Execute();
            DoKickoff(true);
            if (safetyOccurred != null)
                safetyOccurred(new SafetyEventArgs(safety.RecoveringDefender, Math.Abs(safety.PlayLength), safety.PlayReport));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="play">Play</param>
        private void OnTouchdownScored(Play play)
        {
            Touchdown td = new Touchdown(this.game,this, play);
            td.Execute();
            if (touchdownScored != null)
                touchdownScored(new TouchdownScoredEventArgs(td.PrincipalBallcarrier, td.PlayLength, td.PlayReport));
            ExecuteExtraPointAttempt();
            DoKickoff(false);
        }
     

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fgAttempt">FieldGoal</param>
        private void OnFieldGoalAttempted(FieldGoal fgAttempt)
        {
            if (fieldGoalAttempted != null)
                fieldGoalAttempted(new FieldGoalAttempedEventArgs(fgAttempt));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="player">Player</param>
        private void OnFumbleOccurred(Player player, int playLength)
        {
            Fumble fumble = new Fumble(this, player, teamNotInPossession);
            fumble.Execute();
            if(fumbleOccurred!=null)
                fumbleOccurred(new FumbleOccurredEventArgs(player, teamInPossession, playLength, fumble.PlayReport.ToString()));
                 
                
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="play"></param>
        /// <param name="isTd"></param>
        /// <param name="isFumble"></param>
        private void ReportPlay(Play play, bool isTd, bool isFumble)
        {
            if (playCompleted != null)
                playCompleted(null, play.PrincipalBallcarrier, play.PlayLength, isTd, false,false, isFumble);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="play"></param>
        /// <param name="isTd"></param>
        /// <param name="isSack"></param>
        /// <param name="isInterception"></param>
        /// <param name="isFumble"></param>
        private void ReportPassPlay(PassPlay play,bool isTd,bool isSack,bool isInterception,bool isFumble)
        {
            if (isTd)
            {
                
            }
            if (playCompleted != null)
                playCompleted(play.Thrower, play.PrincipalBallcarrier, play.PlayLength, isTd, isSack, isInterception, isFumble);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>int</returns>
        public int GetAutoTDDistance()
        {
            if (currentDirection == Direction.Midfield)
                return 50;
            else if (currentDirection == Direction.Left)
            {
                return currentIndex;
            }
            else
            {
                return 100 - currentIndex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Field Field
        {
            get { return field; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="f">Field</param>
        /// <returns>YardLine</returns>
        public YardLine CurrentYardLine
        {
            get { return field[currentIndex]; }
        }

        /// <summary>
        /// 
        /// </summary>
        public Direction CurrentDirection
        {
            get { return currentDirection; }
        }

        /// <summary>
        /// 
        /// </summary>
        public Down CurrentDown
        {
            get { return currentDown; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int YardsToGo
        {
            get { return yardsToGo; }
        }

        /// <summary>
        /// 
        /// </summary>
        public Team TeamInPossession
        {
            get { return teamInPossession; }
        }

        /// <summary>
        /// 
        /// </summary>
        public Team TeamNotInPossession
        {
            get { return teamNotInPossession; }
        }

       
    }
}
