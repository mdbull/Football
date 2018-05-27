using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Football.Engine
{


    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public abstract class StatSheet:FootballEntity
    {
        protected StatsEntity entity = null;


        /// <summary>
        /// 
        /// </summary>
        public StatSheet()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public StatSheet(StatsEntity entity)
        {
            this.entity = entity;
        }

        /// <summary>
        /// 
        /// </summary>
        public void ClearSheets()
        {
            entity.Stats.Clear();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sheets">StatSheet[]</param>
        /// <returns>StatSheet</returns>
        protected abstract StatSheet AggregateStatSheets(params StatSheet[] sheets);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ret">StatSheet</param>
        /// <param name="sheets">StatSheet[]</param>
        /// <returns>StatSheet</returns>
        public static StatSheet Aggregate(StatSheet ret, params StatSheet[] sheets)
        {
            return ret.AggregateStatSheets(sheets);
        }

        /// <summary>
        /// 
        /// </summary>
        public StatsEntity Entity
        {
            get { return entity ; }
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void Init()
        {
           
        }
    }

    
    
}
