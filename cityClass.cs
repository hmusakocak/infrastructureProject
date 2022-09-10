using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace infrastructure_inquiry
{
    public class List
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("latitude")]
        public string Latitude { get; set; }

        [JsonProperty("longitude")]
        public string Longitude { get; set; }

        [JsonProperty("population")]
        public int Population { get; set; }

        [JsonProperty("region")]
        public string Region { get; set; }
    }

    public class Root
    {
        [JsonProperty("List")]
        public List<List> List { get; set; }
    }

}
