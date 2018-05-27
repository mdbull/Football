using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Football.Engine
{
    
    [Serializable]
    public class YardLine : FootballEntity
    {
        public static readonly YardLine ENDZONE_YARDLINE = new YardLine(-1);
        private int index;
        private Direction direction;

        protected YardLine() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index">int</param>
        public YardLine(int index)
        {
            this.index = index;
            Init();
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void Init()
        {
            if (index <= 48)
            {
                this.direction = Direction.Left;
                this.name = String.Format("<{0} YardLine", this.index + 1);
            }
            else if (index == 49)
            {
                this.direction = Direction.Midfield;
                this.name = String.Format("{0} YardLine", this.index + 1);
            }
            else
            {
                this.direction = Direction.Right;
                this.name = String.Format("{0}> YardLine", Math.Abs((this.index + 1) - 100));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Index
        {
            get { return index; }
        }

        /// <summary>
        /// 
        /// </summary>
        public Direction Direction
        {
            get { return direction; }
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
