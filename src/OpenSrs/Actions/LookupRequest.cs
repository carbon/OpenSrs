using System.Collections.Generic;

namespace OpenSrs
{
    public sealed class LookupRequest : OpenSrsRequest
    {
        public LookupRequest(string domain)
            : base("LOOKUP", "DOMAIN")
        {
            this.Domain = domain;
        }

        public string Domain { get; set; }

        public bool NoCache { get; set; }

        public override Dictionary<string, object> GetParameters()
        {
            return new Dictionary<string, object> {
                { "domain", Domain },
                { "no_cache", NoCache ? "1" : null }
            };
        }
    }
}

/*
action = lookup
object = domain

Attributes ------------------------------------------------------------------

*domain			The domain name to be queried.

no_cache		In order to obtain results quickly, the default for the lookup command is to check the local OpenSRS cache to determine domain name availability. 
				If you specify no_cache = 1, instead of checking the cache, the lookup command queries the applicable registry to determine domain name availability.
				Note: Setting no_cache to 1 increases the amount of time it takes to get results.
*/
