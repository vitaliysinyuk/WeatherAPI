using Newtonsoft.Json.Serialization;

namespace WeatherAPI.Repository.Models
{
    public class WeatherData
    {
        public string WeatherType { get; set; } = string.Empty;

        public List<Value> Values { get; set; } = new List<Value>();
        public class Value
        {
            public string Day { get; set; } = string.Empty;

            public DateTime? Date
            {
                get
                {
                    if (!string.IsNullOrEmpty(Day))
                    {
                        var year = int.Parse(Day.Substring(0, 4));
                        var month = int.Parse(Day.Substring(4, 2));
                        var day = int.Parse(Day.Substring(6, 2));

                        return new DateTime(year, month, day);
                    }
                    return null;
                
                }
                set
                {
   
                }
            }
            public double Val { get; set; }
            public int? DayOfYear
            {
                get
                {
                    if(Date != null)
                    {
                        return Date.Value.DayOfYear;
                    }
                    return null;
                }
                set
                {

                }
            }
        }
    }
}
