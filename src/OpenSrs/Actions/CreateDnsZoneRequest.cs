using System.Collections.Generic;

using OpenSrs.Models;

namespace OpenSrs
{
    public sealed class CreateDnsZoneRequest : OpenSrsRequest
    {
        public CreateDnsZoneRequest()
            : base("CREATE_DNS_ZONE", "DOMAIN") { }

        public string Domain { get; set; }

        public string DnsTemplate { get; set; }

        public DnsRecordSet Records { get; set; }

        public override Dictionary<string, object> GetParameters()
        {
            return new Dictionary<string, object> {
                { "domain",         Domain },
                { "dns_template",   DnsTemplate },
                { "records",        Records?.ToDtAssoc() }
            };
        }
    }
}