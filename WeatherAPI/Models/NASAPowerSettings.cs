namespace WeatherAPI.Models
{
    public class NASAPowerSettings
    {
        public Daily DailyWeather = new Daily();
        public class Daily
        {
            public string NASAPowerBaseURL { get; set; }
            public string Community { get; set; }
            public string Parameters { get; set; }
            public string TimeStandard { get; set; }
            public string Format { get; set; }
        }
       
    }
}
