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

        public async Task<string> GetWeatherData()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("https://power.larc.nasa.gov/api/temporal/daily/point?parameters=T2M,PS,WS10M&community=AG&longitude=0&latitude=0&start=20170101&end=20170201&format=json");

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                var res = JsonConvert.DeserializeObject<WeatherData>(content);

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                dynamic jsonObject = serializer.Deserialize<dynamic>(content);
                var weatherData = jsonObject["properties"]["parameter"];

                var weatherDataMap = new List<WeatherData>();

                foreach(var weatherType in weatherData)
                {
                    var weather = new WeatherData();
                    weather.WeatherType = weatherType.Key;

                    foreach(var values in weatherType.Value)
                    {
                        weather.Values.Add(values.Key, values.Value);
                    }
                    weatherDataMap.Add(weather);
                }
                
                return content;
            }
            else
            {
                // Handle error, e.g., throw an exception or return null
                return null;
            }
        }
    }
}