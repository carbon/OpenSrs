using System.Collections.Generic;

namespace OpenSrs
{
    public sealed class DeleteDnsZoneRequest : OpenSrsRequest
    {
        public DeleteDnsZoneRequest()
            : base("DELETE_DNS_ZONE", "DOMAIN") { }

        public string Domain { get; set; }

        public override Dictionary<string, object> GetParameters()
        {
            return new Dictionary<string, object> {
                { "domain", Domain }
            };
        }
    }
}
