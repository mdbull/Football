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
    public enum Wind
    {
        None,
        Breezy,
        Windy,
        Blustery,
        Unknown
    }

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public enum Rain
    {
        None,
        Drizzle,
        Normal,
        Pouring,
        Torrential,
        Unknown
    }

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public enum Snow
    {
        None,
        Flurry,
        Normal,
        Blizzard,
        Unknown
    }

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public enum Fog
    {
        None,
        Light,
        Dense,
        Unknown
    }

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class Weather
    {
        private int tempF = 75;
        private double tempC = 0;
        private Wind wind = Wind.None;
        private Rain rain = Rain.None;
        private Snow snow = Snow.None;
        private Fog fog = Fog.None;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tempF">int</param>
        /// <param name="wind">Wind</param>
        /// <param name="rain">Rain</param>
        /// <param name="snow">Snow</param>
        /// <param name="fog">Fog</param>
        public Weather(int tempF, Wind wind, Rain rain, Snow snow, Fog fog)
        {
            this.tempF = tempF;
            this.tempC=ConvertFToC();
            this.wind = wind;
            this.rain = rain;
            this.snow = snow;
            this.fog = fog;
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="degrees">int</param>
        public void ChangeTemperature(int degreesF)
        {
            this.tempF += degreesF;
            this.tempC=ConvertFToC();
        }

        /// <summary>
        /// 
        /// </summary>
        private double ConvertFToC()
        {
            return ((double)tempF - 32.0d) * 0.556d;
        }

        /// <summary>
        /// 
        /// </summary>
        public int TempF
        {
            get { return tempF; }
        }

        /// <summary>
        /// 
        /// </summary>
        public double TempC
        {
            get { return tempC; }
        }

        /// <summary>
        /// 
        /// </summary>
        public Wind Wind
        {
            get { return wind; }
        }

        /// <summary>
        /// 
        /// </summary>
        public Rain Rain
        {
            get { return rain; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            StringBuilder weatherReport = new StringBuilder();
            weatherReport.Append(String.Format("{0}°F ({1}°C)", tempF, tempC.ToString("0.0")));
            if(rain > Rain.None)
                weatherReport.Append(String.Format(" Rain={0}",rain));
            if(wind > Wind.None)
                weatherReport.Append(String.Format(" Wind={0}",wind));
            if (snow > Snow.None)
                weatherReport.Append(String.Format(" Snow={0}", snow));
            if (fog > Fog.None)
                weatherReport.Append(String.Format(" Fog={0}", fog));
            return weatherReport.ToString();
        }
    }
}
