namespace WeatherAPI.BusinessLogic.Models
{
    public class WeatherDataByDay : WeatherBase
    {
        public double DailyTemp { get; set; }
        public double DailyTempMax { get; set; }
        public double DailyTempMin { get; set; }
    }
}
