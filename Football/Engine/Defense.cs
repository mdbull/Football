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
    public enum PassPlayResult
    {
        Sack,
        Incomplete,
        Interception,
        AutoTD,
        Unknown
    }

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public enum CarryPlayResult
    {
        Loss,
        NoGain,
        NormalGain,
        Bonus,
        AutoTD,
        Unknown
    }

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public enum KickPlayResult
    {
        Block,
        NormalKick,
        Unknown
    }

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public enum DefenseType
    {
        Carry,
        Pass,
        Kick,
        Unknown
    }

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class Defense:FootballEntity
    {
        protected Team team = null;
       
        protected CarryPlayResult[] runDefense = new CarryPlayResult[12];
        protected CarryPlayResult[] kickoffReturnDefense = new CarryPlayResult[12];
        protected PassPlayResult[] passDefense = new PassPlayResult[12];
        protected KickPlayResult[] kickDefense = new KickPlayResult[12];
        protected int runPenalty = 0;
        protected int maxRunLoss = 0;
        protected int maxSackLoss = 0;
        protected int bonusRunPenalty = 0;
        protected int passRushRating = 0;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="team">Team</param>
        /// <param name="runPenalty">int</param>
        /// <param name="maxRunLoss">int</param>
        /// <param name="maxSackLoss">int</param>
        public Defense(Team team,int runPenalty, int maxRunLoss, int maxSackLoss, int passRushRating,int bonusRunPenalty) 
        {
            this.team = team;
            this.runPenalty = runPenalty;
            this.maxRunLoss = maxRunLoss;
            this.maxSackLoss = maxSackLoss;
            this.passRushRating = passRushRating;
            this.bonusRunPenalty = bonusRunPenalty;
        }

        protected override void Init()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="res">CarryPlayResult[]</param>
        public void InitCarryDefense( CarryPlayResult[] res)
        {
            this.runDefense = res;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="res">CarryPlayResult[]</param>
        public void InitKickoffReturnDefense(CarryPlayResult[] res)
        {
            this.kickoffReturnDefense = res;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="res">PassPlayResult[]</param>
        public void InitPassDefense(PassPlayResult[] res)
        {
            this.passDefense= res;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="res"></param>
        public void InitKickDefense(KickPlayResult[] res)
        {
            this.kickDefense = res;
        }


        private int CheckIndex(int index)
        {
            --index;
            if (index <= 0)
                return 0;
            return index;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public CarryPlayResult RunDefense(int index)
        {
            index = CheckIndex(index);
            return runDefense[index];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index">int</param>
        /// <returns>CarryPlayResult</returns>
        public CarryPlayResult KickoffReturnDefense(int index)
        {
            index = CheckIndex(index);
            return kickoffReturnDefense[index];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public PassPlayResult PassDefense(int index)
        {
            index = CheckIndex(index);
            return passDefense[index];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public KickPlayResult KickDefense(int index)
        {
            index = CheckIndex(index);
            return kickDefense[index];
        }

        /// <summary>
        /// 
        /// </summary>
        public int RunPenalty
        {
            get { return runPenalty; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int MaxRunLoss
        {
            get { return maxRunLoss; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int MaxSackLoss
        {
            get { return maxSackLoss; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int BonusRunPenalty
        {
            get { return bonusRunPenalty; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int PassRushRating
        {
            get { return passRushRating; }
        }

        public Team Team
        {
            get { return team; }
        }
    }
}
