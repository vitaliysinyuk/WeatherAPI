using Nancy.Json;
using WeatherAPI.Models;
using WeatherAPI.Repository.Interfaces;
using WeatherAPI.Repository.Models;

namespace WeatherAPI.Repository
{
   
    public class WeatherDataRepository : IWeatherDataRepository
    {
        private readonly HttpClient _httpClient;
        NASAPowerSettings _nasaPowerSettings { get; set; }

        public WeatherDataRepository(HttpClient httpClient, NASAPowerSettings nasaPowerSettings)
        {
            _httpClient = httpClient;
            _nasaPowerSettings = nasaPowerSettings;
        }

        public async Task<List<WeatherData>?> GetWeatherData(DateTime fromDate, DateTime toDate, float lat, float lon)
        {
            var start = ConvertDate(fromDate);
            var end = ConvertDate(toDate);
            var url = FormatNasaUrl(_nasaPowerSettings.DailyWeather.Community, _nasaPowerSettings.DailyWeather.Parameters, _nasaPowerSettings.DailyWeather.Format, _nasaPowerSettings.DailyWeather.TimeStandard);

            HttpResponseMessage response = await _httpClient.GetAsync($"{_nasaPowerSettings.DailyWeather.NASAPowerBaseURL}latitude={lat}&longitude={lon}&start={start}&end={end}&{url}");

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();

                var weatherDatamap = MapWeatherData(content);

                return weatherDatamap;
            }
            else
            {
                // Handle error, e.g., throw an exception or return null
                return null;
            }
        }

        private string FormatNasaUrl(string community, string parameters, string timeStandard, string format)
        {
            return $"{parameters}&{community}&{format}&{timeStandard}";
        }

        /// <summary>
        /// Converts dates to the string format that the nasa power API requests
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        private string ConvertDate(DateTime date)
        {
            var year = date.Year.ToString();
            var month = date.Month.ToString();
            var day = date.Day.ToString();

            var dateString = new string(year + month + day);
            return dateString;
        }

        public List<WeatherData> MapWeatherData(string content)
        {
            var weatherDataMap = new List<WeatherData>();

            //return empty list of no content
            if(string.IsNullOrEmpty(content)) return weatherDataMap;


            //Data mapping is difficult because of how the data comes back from API, we need to pull out what we specifically want
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            dynamic jsonObject = serializer.Deserialize<dynamic>(content);
            var weatherData = jsonObject["properties"]["parameter"];

            foreach (var weatherType in weatherData)
            {
                var weather = new WeatherData();
                weather.WeatherType = weatherType.Key;

                foreach (var values in weatherType.Value)
                {
                    //skip if no data exists for day
                    if (values.Value == -999) continue;

                    var val = new WeatherData.Value();
                    val.Day = values.Key;
                    val.Val = values.Value;
                    weather.Values.Add(val);
                }
                weatherDataMap.Add(weather);
            }

            return weatherDataMap;
        }
       
    }
}