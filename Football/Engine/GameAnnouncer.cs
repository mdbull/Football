using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Football.Data;

namespace Football.Engine
{
    /// <summary>
    /// 
    /// </summary>
    public class AnnounceReportEventArgs : EventArgs
    {
        /// <summary>
        /// 
        /// </summary>
        private string report;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="report">string</param>
        public AnnounceReportEventArgs(string report)
        {
            this.report = report;
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
    /// <param name="e">AnnounceReportEventArgs</param>
    public delegate void AnnounceReport(AnnounceReportEventArgs e);

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public enum AnnounceReportFrequency
    {
        Standard,
        Verbose,
        Silent,
        Unknown
    }

    /// <summary>
    /// 
    /// </summary>
    public class GameAnnouncer
    {
        /// <summary>
        /// 
        /// </summary>
        public event AnnounceReport reportAnnounced;

        /// <summary>
        /// 
        /// </summary>
        private string name=string.Empty;
        private bool reportNamesUpperCase = false;

        private ChainCrew cc = null;
        private AnnounceReportFrequency reportFrequency = (AnnounceReportFrequency)Utilities.ConvertStringToEnum(Type.GetType("Football.Engine.AnnounceReportFrequency"), ConfigReader.GetConfigurationValue("ANNOUNCER_REPORT_FREQUENCY"));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cc">ChainCrew</param>
        /// <param name="name">string</param>
        public GameAnnouncer(ChainCrew cc,string name)
        {
            this.cc = cc;
            this.cc.touchdownScored += new TouchdownScoredEventHandler(cc_onTouchdownScored);
            this.cc.playCompleted += new PlayEventHandler(cc_onPlayCompleted);
            this.cc.turnoverCompleted += new TurnoverEventHandler(cc_onTurnoverCompleted);
            this.cc.fieldGoalAttempted += new FieldGoalAttemptedEventHandler(cc_fieldGoalAttempted);
            this.cc.fumbleOccurred += new FumbleOccurredEventHandler(cc_fumbleOccurred);
            this.cc.safetyOccurred += new SafetyEventHandler(cc_safetyOccurred);
            this.name = name;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e">SafetyEventArgs</param>
        void cc_safetyOccurred(SafetyEventArgs e)
        {
            if (reportFrequency != AnnounceReportFrequency.Silent)
            {
                ReportGameEvent(e.Report);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e">FumbleOccurredEventArgs</param>
        void cc_fumbleOccurred(FumbleOccurredEventArgs e)
        {
            if (reportFrequency != AnnounceReportFrequency.Silent)
            {
                ReportGameEvent(e.Report);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e">FieldGoalAttemptedEventArgs</param>
        void cc_fieldGoalAttempted(FieldGoalAttempedEventArgs e)
        {
            if (reportFrequency != AnnounceReportFrequency.Silent)
            {
                ReportGameEvent(e.Report);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e">TouchdownScoredEventArgs</param>
        void cc_onTouchdownScored(TouchdownScoredEventArgs e)
        {
            if (reportFrequency != AnnounceReportFrequency.Silent)
            {
                ReportGameEvent(e.Report);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="report">string</param>
        void cc_onTurnoverCompleted(string report)
        {
            if (reportFrequency != AnnounceReportFrequency.Silent)
            {
                ReportGameEvent(report);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="thrower"></param>
        /// <param name="ballcarrier"></param>
        /// <param name="length"></param>
        /// <param name="isTd"></param>
        void cc_onPlayCompleted(Player thrower, Player ballcarrier, int length, bool isTd, bool isSack, bool isInterception, bool isFumble)
        {
            if (reportFrequency != AnnounceReportFrequency.Silent)
            {
                ReportPlay(thrower, ballcarrier, length, isTd, isSack, isInterception, isFumble);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>string</returns>
        public string ReportGameOpening(Game game, Field field)
        {
            StringBuilder report = new StringBuilder(String.Format("Hello everybody! This is {0}. Welcome to {1} for ", name, field.Name));
            if (game.GameTime.Hours >= 18)
                report.Append(" tonight's ");
            else
                report.Append(" today's ");
            report.Append(String.Format(" game between the {0} and the {1}.",field.Scoreboard.RoadTeam.ToString(),field.Scoreboard.HomeTeam.ToString()));
            report.Append(String.Format(" Weather conditions: {0}", game.WeatherReport.ToString()));
            string ret = report.ToString();
            reportAnnounced(new AnnounceReportEventArgs(ret));
            return ret;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>string</returns>
        public string ReportFinalScore()
        {
            string report=String.Format("And the final score from {0} is the {1}. Thank you for tuning in. This is {2} signing out. See you next time.",this.cc.Field.Name,this.cc.Field.Scoreboard,this.name);
            reportAnnounced(new AnnounceReportEventArgs(report));
            return report;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="report">string</param>
        public void ReportGameEvent(string report)
        {
            if (reportFrequency != AnnounceReportFrequency.Silent)
            {
                if (reportAnnounced != null)
                {
                    StringBuilder builder = new StringBuilder(String.Format("{0}: ", name));
                    builder.AppendLine(report);
                    reportAnnounced(new AnnounceReportEventArgs(builder.ToString()));
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">string</param>
        /// <returns>string</returns>
        private string ConvertNameToUpper(string name)
        {
            if (reportNamesUpperCase)
                return name.ToUpper();
            return name;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>string</returns>
        public string ReportBallLocationAndDown()
        {
            return String.Format("{0}: Ball at the {1} yard line. {2} down and {3}.", name, cc.CurrentYardLine, cc.CurrentDown, cc.YardsToGo);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="thrower"></param>
        /// <param name="ballcarrier"></param>
        /// <param name="yards"></param>
        /// <param name="isTd"></param>
        /// <param name="isSack"></param>
        /// <param name="isInterception"></param>
        /// <param name="isFumble"></param>
        public void ReportPlay(Player thrower, Player ballcarrier, int yards, bool isTd, bool isSack, bool isInterception,bool isFumble)
        {
            if (reportFrequency != AnnounceReportFrequency.Silent)
            {
                if (reportAnnounced != null)
                {
                    StringBuilder reportString = new StringBuilder();

                    if (ballcarrier == null)//incomplete pass - no sack, no interception
                    {
                        reportString.AppendLine(String.Format("{0}: 'PASS INCOMPLETE!'", name));
                        reportString.AppendLine(ReportBallLocationAndDown());

                    }
                    else if (thrower == null)//run
                    {
                        if (isTd)
                        {
                            reportString.AppendLine(String.Format("{0}: '{1} scores from {2} yards out. TOUCHDOWN {3}!'", name, ConvertNameToUpper(ballcarrier.Name), yards, ballcarrier.Team));
                        }
                        else
                        {

                            if (yards < 0)
                            {
                                if (isSack)
                                    reportString.AppendLine(String.Format("{0}: '{1} is SACKED for a loss of {2} yards!'", name, ConvertNameToUpper(ballcarrier.Name), Math.Abs(yards)));
                                else
                                    reportString.AppendLine(String.Format("{0}: '{1} is STOPPED behind the line of scrimage for a loss of {2} yards!'", name, ConvertNameToUpper(ballcarrier.Name), Math.Abs(yards)));
                            }
                            else if (yards == 0)
                            {
                                reportString.AppendLine(String.Format("{0}: '{1} is STOPPED at the line of scrimage! No gain on the play.'", name, ConvertNameToUpper(ballcarrier.Name)));
                            }
                            else if ((yards > 0) && (yards < 20))
                            {
                                reportString.AppendLine(String.Format("{0}: '{1} runs for a gain of {2} yards.'", name, ConvertNameToUpper(ballcarrier.Name), yards));
                            }
                            else if ((yards >= 20) && (yards <= 50))
                            {
                                reportString.AppendLine(String.Format("{0}: '{1} breaks a big one and is loose in the secondary for a run of {2} yards!'", name, ConvertNameToUpper(ballcarrier.Name), yards));
                            }
                            else if ((yards >= 50) && !isTd)
                            {
                                reportString.AppendLine(String.Format("{0}: '{1} runs for a long gain of {2} yards!'", name, ConvertNameToUpper(ballcarrier.Name), yards));
                            }
                            reportString.AppendLine(ReportBallLocationAndDown());
                        }


                    }
                    else if (thrower != null)// completed pass
                    {
                        if (isTd)
                        {
                            reportString.AppendLine(String.Format("{0}: '{1} scores from {2} yards out on a pass from {3}. TOUCHDOWN {4}!!!", name, ConvertNameToUpper(ballcarrier.Name), yards, ConvertNameToUpper(thrower.Name), ballcarrier.Team));
                        }
                        else
                        {
                            if (yards == 0)//incomplete pass
                                reportString.AppendLine(String.Format("{0}: 'PASS INCOMPLETE! Intended for {1} by {2}.'", name, ConvertNameToUpper(ballcarrier.Name), ConvertNameToUpper(thrower.Name)));
                            else
                                reportString.AppendLine(String.Format("{0}: 'PASS COMPLETED by {1} to {2} for {3} yards!", name, ConvertNameToUpper(thrower.Name), ConvertNameToUpper(ballcarrier.Name), yards));
                            reportString.AppendLine(ReportBallLocationAndDown());

                        }
                    }
                    if (isTd)
                        reportString.AppendLine(String.Format("{0}: 'And the score is {1}.'", name, cc.Field.Scoreboard.GetScore()));

                    reportAnnounced(new AnnounceReportEventArgs(reportString.ToString()));
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool ReportNamesInUpperCase
        {
            get { return reportNamesUpperCase; }
            set { reportNamesUpperCase = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            get { return this.name; }
        }

        /// <summary>
        /// 
        /// </summary>
        public AnnounceReportFrequency ReportFrequency
        {
            get { return reportFrequency; }
            set { reportFrequency = value; }
        }
    }
}
