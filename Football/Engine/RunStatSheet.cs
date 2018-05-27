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
    public class CarryStatSheet : OffensiveStatSheet
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="player">Player</param>
        public CarryStatSheet(Player player):base(player)
        {
           
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sheets">StatSheet[]</param>
        /// <returns>StatSheet</returns>
        protected override StatSheet AggregateStatSheets(params StatSheet[] sheets)
        {
            CarryStatSheet ret = new CarryStatSheet((Player)this.entity);
            foreach (CarryStatSheet sheet in sheets)
            {
                ret.touches += sheet.touches;
                ret.yards += sheet.yards;
                ret.touchdowns.AddRange(sheet.touchdowns.ToArray());
                ret.fumbles += sheet.fumbles;
                if (sheet.longPlay > ret.LongPlay)
                    ret.longPlay = sheet.LongPlay;

            }
            return ret;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="yards">int</param>
        /// <param name="td">bool</param>
        /// <param name="isFumble">bool</param>
        public void AddRun(int yards, bool isTd, bool isFumble)
        {
            this.touches++;
            this.yards += yards;
            CheckLongPlay(yards);
            if (isTd)
            {
                AddTouchdown(yards);
            }
            if (isFumble)
                this.fumbles++;
        }
       

        /// <summary>
        /// 
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            StringBuilder ret = new StringBuilder();
            ret.Append(String.Format("{0} {1}-",entity.ToString(),touches));
            if(yards < 0)
                ret.Append(String.Format("({0})",yards));
            else
                ret.Append(yards);
            ret.Append(String.Format(" {0}avg {1}TD Long {2}", Average.ToString(), touchdowns.Count, longPlay));
            return ret.ToString();
        }
    }
}
