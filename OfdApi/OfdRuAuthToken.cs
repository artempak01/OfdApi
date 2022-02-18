using Newtonsoft.Json;
using System;

namespace OfdRuApi
{
    public class OfdRuAuthToken
    {

        [JsonProperty("AuthToken")]
        public string Token { get; set; }

        [JsonProperty("ExpirationDateUtc")]
        public DateTime ExpDateTime { get; set; }
    }
}
