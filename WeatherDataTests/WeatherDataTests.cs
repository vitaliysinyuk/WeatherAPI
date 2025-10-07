using AutoMapper;
using Moq;
using System.Net.Http;
using System.Reflection;
using WeatherAPI;
using WeatherAPI.BusinessLogic;
using WeatherAPI.BusinessLogic.Interfaces;
using WeatherAPI.Models;
using WeatherAPI.Repository;
using WeatherAPI.Repository.Interfaces;
using WeatherAPI.Repository.Models;

namespace WeatherDataTests
{
    [TestClass]
    public  class WeatherDataTests
    {
        private Mock<IWeatherDataRepository> _weatherDataRepository = new Mock<IWeatherDataRepository>();
        private Mock<IWeatherDataBusinessLogic> _weatherDataBusinessLogic = new Mock<IWeatherDataBusinessLogic>();
        private Mock<IMapper> _autoMapper = new Mock<IMapper>();

        [TestInitialize]
        public void Setup()
        {

            _weatherDataRepository.CallBase = true;
            _weatherDataBusinessLogic.CallBase = true;
            _autoMapper.CallBase = true;

        }
        [TestMethod]
        public void TestGetWeatherData()
        {
            var httpClient = new HttpClient();
            var nasaPowerSettings = new NASAPowerSettings();
            var repo = new WeatherDataRepository(httpClient, nasaPowerSettings);

            var testEmpty = repo.MapWeatherData("");

            CollectionAssert.AreEquivalent(testEmpty, new List<WeatherData>());
        }

        [TestMethod]
        public void TestMapWeatherDataByDay()
        {
            var httpClient = new HttpClient();
            var nasaPowerSettings = new NASAPowerSettings();
            var repo = new WeatherDataRepository(httpClient, nasaPowerSettings);
            var business = new WeatherDataBusinessLogic(repo, _autoMapper.Object);

            //Testing private method
            MethodInfo method = typeof(WeatherDataBusinessLogic).GetMethod("MapWeatherDataByDay", BindingFlags.NonPublic | BindingFlags.Instance);
            List<WeatherAPI.BusinessLogic.Models.WeatherDataByDay> result = (List<WeatherAPI.BusinessLogic.Models.WeatherDataByDay>)method.Invoke(business, new object[] { null });

            CollectionAssert.AreEquivalent(result, new List<WeatherDataByDay>());

        }

        [TestMethod]
        public void TestConvertDate()
        {
            var httpClient = new HttpClient();
            var nasaPowerSettings = new NASAPowerSettings();
            var repo = new WeatherDataRepository(httpClient, nasaPowerSettings);

            //Testing private method
            MethodInfo method = typeof(WeatherDataRepository).GetMethod("ConvertDate", BindingFlags.NonPublic | BindingFlags.Instance);
            string result = (string)method.Invoke(repo, new object[] { new DateTime(2017,1,1)});
            Assert.AreEqual(result, "201711");
        }
    }
}
