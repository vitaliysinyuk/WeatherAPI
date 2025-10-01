namespace WeatherAPI.Repository.Models
{
    public class WeatherData
    {
        public string WeatherType { get; set; } = string.Empty;

        public List<Value> Values { get; set; } = new List<Value>();
        public class Value : WeatherBase
        {
                          
        }
    }
}
