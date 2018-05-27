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
    public class PlayerSkills
    {
        private string run = string.Empty;
        private string receive = string.Empty;
        private string kickReturn = string.Empty;
        private int pass;
        private int kick;
        private string punt=string.Empty;

        private Player player = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="run"></param>
        /// <param name="receive"></param>
        /// <param name="kickReturn"></param>
        /// <param name="pass"></param>
        /// <param name="kick"></param>
        /// <param name="punt"></param>
        public PlayerSkills(string run, string receive, string kickReturn, int pass, int kick, string punt)
        {
            this.run = run;
            this.receive = receive;
            this.kickReturn = kickReturn;
            this.pass = pass;
            this.kick = kick;
            this.punt = punt;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Run
        {
            get { return run; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Receive
        {
            get { return receive; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string KickReturn
        {
            get { return kickReturn; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Pass
        {
            get { return pass; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Kick
        {
            get { return kick; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Punt
        {
            get { return punt; }
        }
    }
}
