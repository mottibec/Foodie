using Newtonsoft.Json;
using System;

namespace Foodie.Core.Api
{
    public class RecallReport
    {
        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("address_1")]
        public string Address1 { get; set; }

        [JsonProperty("reason_for_recall")]
        public string ReasonForRecall { get; set; }

        [JsonProperty("address_2")]
        public string Address2 { get; set; }

        [JsonProperty("product_quantity")]
        public string ProductQuantity { get; set; }

        [JsonProperty("code_info")]
        public string CodeInfo { get; set; }

        [JsonProperty("center_classification_date")]
        public long CenterClassificationDate { get; set; }

        [JsonProperty("distribution_pattern")]
        public string DistributionPattern { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("product_description")]
        public string ProductDescription { get; set; }

        [JsonProperty("report_date"), JsonConverter(typeof(DateFormatConverter), "yyyyMMdd")]
        public DateTime ReportDate { get; set; }

        [JsonProperty("classification")]
        public string Classification { get; set; }

        [JsonProperty("openfda")]
        public Openfda Openfda { get; set; }

        [JsonProperty("recalling_firm")]
        public string RecallingFirm { get; set; }

        [JsonProperty("recall_number")]
        public string RecallNumber { get; set; }

        [JsonProperty("initial_firm_notification")]
        public string InitialFirmNotification { get; set; }

        [JsonProperty("product_type")]
        public string ProductType { get; set; }

        [JsonProperty("event_id")]
        public long EventId { get; set; }

        [JsonProperty("termination_date")]
        public long TerminationDate { get; set; }

        [JsonProperty("more_code_info")]
        public object MoreCodeInfo { get; set; }

        [JsonProperty("recall_initiation_date")]
        public long RecallInitiationDate { get; set; }

        [JsonProperty("postal_code")]
        public string PostalCode { get; set; }

        [JsonProperty("voluntary_mandated")]
        public string VoluntaryMandated { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }

    public partial class Openfda
    {
    }
}
