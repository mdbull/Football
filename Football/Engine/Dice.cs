using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Football.Engine
{
    public static class Dice
    {
        static readonly Random r = new Random();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns>int</returns>
        public static int Roll(int min, int max)
        {
            if (max <= min)
                return min;
            return r.Next(min, max);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        public static int Roll(string format)
        {
            int result = 0;
            int rolls=0;
            //parse string e.g., '3d8'
            try
            {
                string[] parse = format.Split('d');
                if (parse[0].Length == 0)
                    rolls = 1;
                else
                    rolls = Convert.ToInt32( parse[0]);
                int die = Convert.ToInt32(parse[1]);
                int rollTotal = 0;
                for (int i = 0; i < rolls; ++i)
                {
                    rollTotal += r.Next(1, die+1);
                }
                result = rollTotal;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            if (result == 0)
                Console.ReadLine();
            return result;
        }
    }
}
