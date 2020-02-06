namespace OpenSrs.Models
{
    public sealed class DomainDetails
    {
        public string Domain { get; set; }

        public DomainStatus Status { get; set; }

        public decimal Price { get; set; }
    }
}