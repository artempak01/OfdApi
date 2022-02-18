using Newtonsoft.Json;
using System;

namespace OfdRuApi
{
    public class OfdReportInfo
    {
        [JsonProperty("Id")]
        public string Id { get; set; }

        [JsonProperty("CDateUtc")]
        public DateTime CDateUtc { get; set; }

        [JsonProperty("ReportTypeName")]
        public string ReportTypeName { get; set; }

        [JsonProperty("ReportType")]
        public string ReportType { get; set; }

        [JsonProperty("DateFrom")]
        public DateTime DateFrom { get; set; }

        [JsonProperty("DateTo")]
        public DateTime DateTo { get; set; }

        [JsonProperty("ExpiredDate")]
        public DateTime ExpiredDate { get; set; }

        [JsonProperty("KktCount")]
        public int KktCount { get; set; }

        [JsonProperty("Status")]
        public string Status { get; set; }

        [JsonProperty("AccountEmail")]
        public string AccountEmail { get; set; }

        [JsonProperty("AccountName")]
        public string AccountName { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("EmailsToNotify")]
        public string[] EmailsToNotify { get; set; }

        [JsonProperty("FileFormat")]
        public string FileFormat { get; set; }

        [JsonProperty("IsAutoReport")]
        public bool IsAutoReport { get; set; }

        [JsonProperty("AssumptiveCompleteTime")]
        public DateTime AssumptiveCompleteTime { get; set; }

        [JsonProperty("DownloadUrl")]
        public Uri DownloadUrl { get; set; }

        [JsonProperty("DeleteUrl")]
        public Uri DeleteUrl { get; set; }

        [JsonProperty("CancelUrl")]
        public Uri CancelUrl { get; set; }
    }
}
