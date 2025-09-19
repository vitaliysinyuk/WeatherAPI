using Nancy;
using Nancy.Extensions;
using Nancy.Json;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.JavaScript;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using WeatherAPI.Repository.Interfaces;
using WeatherAPI.Repository.Models;

namespace WeatherAPI.Repository
{
   
    public class WeatherDataRepository : IWeatherDataRepository
    {
        private readonly HttpClient _httpClient;

        public WeatherDataRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<WeatherData>?> GetWeatherData()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("https://power.larc.nasa.gov/api/temporal/daily/point?parameters=T2M,T2M_MAX,T2M_MIN&community=AG&latitude=41.505493&longitude=-81.681290&start=20170101&end=20171231&units=imperial&format=json");

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                //var res = JsonConvert.DeserializeObject<WeatherData>(content);

                //var weatherDatamap = MapWeatherData(content);
                var weatherDatamap = MapWeatherData(content);

                return weatherDatamap;
            }
            else
            {
                // Handle error, e.g., throw an exception or return null
                return null;
            }
        }



        public List<WeatherData> MapWeatherData(string content)
        {
            var weatherDataMap = new List<WeatherData>();

            //return empty list of no content
            if(string.IsNullOrEmpty(content)) return weatherDataMap;

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