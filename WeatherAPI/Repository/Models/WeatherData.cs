using Newtonsoft.Json.Serialization;

namespace WeatherAPI.Repository.Models
{
    public class WeatherData
    {
        /* public class Properties
         {
             public List<Parameter> parameter { get; set; } = new List<Parameter>();
         }

         public class Parameter
         {
             public List<T2M> T2M { get; set; } = new List<T2M>();
             public List<PS> PS { get; set; } = new List<PS>();
             public List<WS10M> WS10M { get; set; } = new List<WS10M>();
         }

         public class T2M
         {
             public Dictionary<string, double> Values = new Dictionary<string, double>();
         }

         public class PS
         {
             public Dictionary<string, double> Values = new Dictionary<string, double>();
         }

         public class WS10M
         {
             public Dictionary<string, double> Values = new Dictionary<string, double>();
         }*/

        public string Type { get; set; } = string.Empty;
        public Geometry geometry { get; set; } = new Geometry();

        public class Geometry
        {
            //JsonProperty["type"]
            string Type { get; set; } = string.Empty;
            double[] Coordinates { get; set; } 
        }
    }
}
