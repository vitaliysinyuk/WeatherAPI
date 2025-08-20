using Moq;
using WeatherAPI;
using WeatherAPI.Repository;
using WeatherAPI.Repository.Interfaces;
using WeatherAPI.Repository.Models;

namespace WeatherDataTests
{
    [TestClass]
    public  class WeatherDataTests
    {
        private Mock<IWeatherDataRepository> _weatherDataRepository = new Mock<IWeatherDataRepository>();

        [TestInitialize]
        public void Setup()
        {

            _weatherDataRepository.CallBase = true;

        }
        [TestMethod]
        public void TestGetWeatherData()
        {
            var httpClient = new HttpClient();
            var repo = new WeatherDataRepository(httpClient);
            //var test = repo.MapWeatherData("{\r\n  \"type\": \"Feature\",\r\n  \"geometry\": {\r\n    \"type\": \"Point\",\r\n    \"coordinates\": [0, 0, 0]\r\n  },\r\n  \"properties\": {\r\n    \"parameter\": {\r\n      \"T2M\": {\r\n        \"20170101\": 26.45,\r\n        \"20170102\": 27.01,\r\n        \"20170103\": 26.69,\r\n        \"20170104\": 26.47,\r\n        \"20170105\": 26.07,\r\n        \"20170106\": 25.79,\r\n        \"20170107\": 26.79,\r\n        \"20170108\": 27.18,\r\n        \"20170109\": 26.61,\r\n        \"20170110\": -1,\r\n        \"20170111\": 26.59,\r\n        \"20170112\": 27.2,\r\n        \"20170113\": 27.52,\r\n        \"20170114\": 27.6,\r\n        \"20170115\": 27.33,\r\n        \"20170116\": 27.58,\r\n        \"20170117\": 27.14,\r\n        \"20170118\": 27.1,\r\n        \"20170119\": 27.23,\r\n        \"20170120\": 27.54,\r\n        \"20170121\": 27.81,\r\n        \"20170122\": 26.98,\r\n        \"20170123\": 26.84,\r\n        \"20170124\": 26.25,\r\n        \"20170125\": 27.13,\r\n        \"20170126\": 26.87,\r\n        \"20170127\": 26.78,\r\n        \"20170128\": 27.24,\r\n        \"20170129\": 27.18,\r\n        \"20170130\": 27.19,\r\n        \"20170131\": 26.74,\r\n        \"20170201\": 26.45\r\n      },\r\n      \"PS\": {\r\n        \"20170101\": 101.07,\r\n        \"20170102\": 101.12,\r\n        \"20170103\": 101.14,\r\n        \"20170104\": 101.12,\r\n        \"20170105\": 101.14,\r\n        \"20170106\": 101.12,\r\n        \"20170107\": 101.12,\r\n        \"20170108\": 101.13,\r\n        \"20170109\": 101,\r\n        \"20170110\": 100.92,\r\n        \"20170111\": 100.97,\r\n        \"20170112\": 101.09,\r\n        \"20170113\": 101,\r\n        \"20170114\": 100.89,\r\n        \"20170115\": 100.93,\r\n        \"20170116\": 100.91,\r\n        \"20170117\": 100.93,\r\n        \"20170118\": 101.03,\r\n        \"20170119\": 101.01,\r\n        \"20170120\": 101,\r\n        \"20170121\": 101,\r\n        \"20170122\": 101.1,\r\n        \"20170123\": 101.26,\r\n        \"20170124\": 101.35,\r\n        \"20170125\": 101.21,\r\n        \"20170126\": 101.28,\r\n        \"20170127\": 101.39,\r\n        \"20170128\": 101.32,\r\n        \"20170129\": 101.18,\r\n        \"20170130\": 101.12,\r\n        \"20170131\": 101.16,\r\n        \"20170201\": 101.24\r\n      },\r\n      \"WS10M\": {\r\n        \"20170101\": 1.6,\r\n        \"20170102\": 3.93,\r\n        \"20170103\": 2.1,\r\n        \"20170104\": 3.56,\r\n        \"20170105\": 4.69,\r\n        \"20170106\": 2.94,\r\n        \"20170107\": 4.63,\r\n        \"20170108\": 3.86,\r\n        \"20170109\": 3.36,\r\n        \"20170110\": 4.71,\r\n        \"20170111\": 4.51,\r\n        \"20170112\": 4.33,\r\n        \"20170113\": 4.14,\r\n        \"20170114\": 3.46,\r\n        \"20170115\": 4.07,\r\n        \"20170116\": 4.44,\r\n        \"20170117\": 4.76,\r\n        \"20170118\": 4.52,\r\n        \"20170119\": 3.4,\r\n        \"20170120\": 3.71,\r\n        \"20170121\": 4.46,\r\n        \"20170122\": 5.1,\r\n        \"20170123\": 4.53,\r\n        \"20170124\": 2.89,\r\n        \"20170125\": 3.08,\r\n        \"20170126\": 3.62,\r\n        \"20170127\": 3.57,\r\n        \"20170128\": 4.5,\r\n        \"20170129\": 3.62,\r\n        \"20170130\": 3.68,\r\n        \"20170131\": 3.74,\r\n        \"20170201\": 4.02\r\n      }\r\n    }\r\n  },\r\n  \"header\": {\r\n    \"title\": \"NASA/POWER Source Native Resolution Daily Data\",\r\n    \"api\": {\r\n      \"version\": \"v2.7.4\",\r\n      \"name\": \"POWER Daily API\"\r\n    },\r\n    \"sources\": [\r\n      \"MERRA2\",\r\n      \"POWER\"\r\n    ],\r\n    \"fill_value\": -999,\r\n    \"time_standard\": \"LST\",\r\n    \"start\": \"20170101\",\r\n    \"end\": \"20170201\"\r\n  },\r\n  \"messages\": [],\r\n  \"parameters\": {\r\n    \"T2M\": {\r\n      \"units\": \"C\",\r\n      \"longname\": \"Temperature at 2 Meters\"\r\n    },\r\n    \"PS\": {\r\n      \"units\": \"kPa\",\r\n      \"longname\": \"Surface Pressure\"\r\n    },\r\n    \"WS10M\": {\r\n      \"units\": \"m/s\",\r\n      \"longname\": \"Wind Speed at 10 Meters\"\r\n    }\r\n  },\r\n  \"times\": {\r\n    \"data\": 1.36,\r\n    \"process\": 0.05\r\n  }\r\n}");

            var testEmpty = repo.MapWeatherData("");

            //Assert.AreEqual(testEmpty, new List<WeatherData>());
            CollectionAssert.AreEquivalent(testEmpty, new List<WeatherData>());
        }
    }
}
