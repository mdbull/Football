using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Football.Engine
{

    [Serializable]
    public class Field:FootballEntity
    {
        private YardLine[] yardlines = null;
        private int capacity = 0;
        private Scoreboard scoreboard = null;

        protected Field()
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">string</param>
        /// <param name="capacity">int</param>
        /// <param name="scoreboard">Scoreboard</param>
        public Field(string name, int capacity, Scoreboard scoreboard):base(name)
        {
            this.capacity = capacity;
            this.scoreboard = scoreboard;
            
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void Init()
        {
            yardlines = new YardLine[99];
            for (int i = 0; i < 99; ++i)
            {

                yardlines[i] = new YardLine(i);
                Debug.WriteLine(String.Format ("{0} - {1}", i, yardlines[i]));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index">int</param>
        /// <returns></returns>
        public YardLine this[int index]
        {
            get 
            { 
                if(index >=0 && index <=98)
                    return yardlines[index];
                else
                    return YardLine.ENDZONE_YARDLINE;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Scoreboard Scoreboard
        {
            get { return scoreboard; }
        }
    }
}
