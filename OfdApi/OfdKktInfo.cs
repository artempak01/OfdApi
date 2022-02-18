using Newtonsoft.Json;
using System;

namespace OfdRuApi
{
    public class OfdKktInfo
    {
        [JsonProperty("Id")]
        public string OfdKktId { get; set; }

        [JsonProperty("KktName")]
        public string OfdKktName { get; set; }

        [JsonProperty("SerialNumber")]
        public string OfdKktSerialNumber { get; set; }

        [JsonProperty("ActivationDate")]
        public DateTime OfdKktActivationDate { get; set; }

        [JsonProperty("ContractStartDate")]
        public DateTime OfdKktContractStartDate { get; set; }

        [JsonProperty("ContractEndDate")]
        public DateTime OfdKktContractEndDate { get; set; }

        [JsonProperty("CreateDate")]
        public DateTime OfdKktCreateDate { get; set; }

        [JsonProperty("FnNumber")]
        public string OfdKktFnNumber { get; set; }

        [JsonProperty("SignDate")]
        public DateTime OfdKktSignDate { get; set; }

        [JsonProperty("PaymentDate")]
        public DateTime OfdKktPaymentDate { get; set; }

        [JsonProperty("CheckDate")]
        public DateTime OfdKktCheckDate { get; set; }

        [JsonProperty("LastDocOnKktDateTime")]
        public DateTime OfdLastDocOnKktDateTime { get; set; }

        [JsonProperty("LastDocOnOfdDateTimeUtc")]
        public DateTime OfdKktLastDocOnOfdDateTimeUtc { get; set; }

        [JsonProperty("FirstDocumentDate")]
        public DateTime OfdKktFirstDocumentDate { get; set; }

        [JsonProperty("FiscalAddress")]
        public string OfdKktFiscalAddress { get; set; }

        [JsonProperty("FiscalPlace")]
        public string OfdKktFiscalPlace { get; set; }

        [JsonProperty("Path")]
        public string OfdKktPath { get; set; }

        [JsonProperty("KktModel")]
        public string OfdKktModel { get; set; }

        [JsonProperty("FnEndDate")]
        public DateTime OfdFnEndDate { get; set; }

    }
}
