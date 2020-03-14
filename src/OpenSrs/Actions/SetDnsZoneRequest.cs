using System.Collections.Generic;

using OpenSrs.Models;

namespace OpenSrs
{
    public sealed class SetDnsZoneRequest : OpenSrsRequest
    {
        public SetDnsZoneRequest()
            : base("SET_DNS_ZONE", "DOMAIN") { }

        public string Domain { get; set; }

        public DnsRecordSet Records { get; set; }

        public override Dictionary<string, object> GetParameters()
        {
            return new Dictionary<string, object>(2) {
                { "domain",  Domain },
                { "records", Records?.ToDtAssoc() }
            };
        }
    }
}