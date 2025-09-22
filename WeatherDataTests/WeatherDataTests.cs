using Moq;
using System.Net.Http;
using WeatherAPI;
using WeatherAPI.BusinessLogic;
using WeatherAPI.BusinessLogic.Interfaces;
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

        [TestInitialize]
        public void Setup()
        {

            _weatherDataRepository.CallBase = true;
            _weatherDataBusinessLogic.CallBase = true;

        }
        [TestMethod]
        public void TestGetWeatherData()
        {
            var httpClient = new HttpClient();
            var repo = new WeatherDataRepository(httpClient);

            var testEmpty = repo.MapWeatherData("");

            //Assert.AreEqual(testEmpty, new List<WeatherData>());
            CollectionAssert.AreEquivalent(testEmpty, new List<WeatherData>());
        }

        [TestMethod]
        public void TestMapWeatherDataByDay()
        {
            var httpClient = new HttpClient();
            var repo = new WeatherDataRepository(httpClient);
            var business = new WeatherDataBusinessLogic(repo);
            var testEmpty = business.MapWeatherDataByDay(null);

            CollectionAssert.AreEquivalent(testEmpty, new List<WeatherDataByDay>());

        }
    }
}
