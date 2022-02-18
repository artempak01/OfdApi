using Newtonsoft.Json;

namespace OfdRuApi
{
    public class OfdAgreement
    {
        [JsonProperty("OfdAgreementId")]
        public string OfdAgreementId { get; set; }

        [JsonProperty("OfdAgreementLimit")]
        public int OfdAgreementLimit { get; set; }

        [JsonProperty("ConsumedLimit")]
        public int ConsumedLimit { get; set; }

        [JsonProperty("List")]
        public OfdReportInfo[] OfdReports { get; set; }
    }
}
