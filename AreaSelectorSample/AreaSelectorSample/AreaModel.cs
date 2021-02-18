using System.Collections.Generic;
using Newtonsoft.Json;

namespace AreaSelectorSample
{
    public class AreaModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("fullname")]
        public string Fullname { get; set; }

        [JsonProperty("pinyin")]
        public List<string> Pinyin { get; set; }

        [JsonProperty("location")]
        public Location Location { get; set; }

        [JsonProperty("cidx")]
        public List<int> Cidx { get; set; }

        public override string ToString()
        {
            return Fullname;
        }
    }

    public class Location
    {
        [JsonProperty("lat")]
        public double Lat { get; set; }

        [JsonProperty("lng")]
        public double Lng { get; set; }
    }
}