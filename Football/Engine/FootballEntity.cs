using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Football.Engine
{
    [Serializable]
    public abstract class FootballEntity: ICloneable
    {
        protected string name = string.Empty;
        protected string toString = string.Empty;
        public FootballEntity() { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">string</param>
        public FootballEntity(string name)
        {
            this.name = name;
            Init();
        }

        /// <summary>
        /// 
        /// </summary>
        protected abstract void Init();

        /// <summary>
        /// 
        /// </summary>
        protected virtual void SetName() { }

        /// <summary>
        /// 
        /// </summary>
        protected virtual void SetToString()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            get { return name; }
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
        /// <returns>object</returns>
        public object Clone()
        {
            return MemberwiseClone();
        }
    }

}
