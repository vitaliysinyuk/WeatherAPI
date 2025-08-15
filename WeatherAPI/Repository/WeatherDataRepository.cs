using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using WeatherAPI.Repository.Interfaces;

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
                //var res = JsonConvert.DeserializeObject<string>(content);
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