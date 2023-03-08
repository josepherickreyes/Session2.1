using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTFULAPI_Automation.Model
{
    public class PetClass
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("category")]
        public PetCategory PetCategory { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("photoUrls")]
        public string[] PhotoUrls { get; set; }

        [JsonProperty("tags")]
        public PetCategory[] Tags { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
