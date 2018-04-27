namespace OpenSrs
{
    public class GetOrderInfoResult
    {
        public string AffiliateId { get; set; }

        public string ApplicationId { get; set; }

        public string ApplicationStatus { get; set; }

        public string Comments { get; set; }

        public string CompletedDate { get; set; }

        public decimal Cost { get; set; }

        public string Doamin { get; set; }

        public string EncodingType { get; set; }

        public int ExpiryYear { get; set; }

        public bool AutoRenew { get; set; }

        public bool LockDomain { get; set; }
    }
}
