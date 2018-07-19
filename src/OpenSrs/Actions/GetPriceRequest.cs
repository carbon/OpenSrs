using System;
using System.Collections.Generic;

using OpenSrs.Models;

namespace OpenSrs
{
    public sealed class GetPriceRequest : OpenSrsRequest
    {
        public GetPriceRequest(string domain, int period = 1)
            : base("GET_PRICE", "DOMAIN")
        {
            this.Domain = domain ?? throw new ArgumentNullException(nameof(domain));
            this.Period = period;
            this.RegistrationType = RegistrationType.New;
        }

        public string Domain { get; set; }

        public int Period { get; set; }

        /// <summary>
        /// reg_type (NEW, Renewal)
        /// </summary>
        public RegistrationType RegistrationType { get; set; }

        public override Dictionary<string, object> GetParameters()
        {
            return new Dictionary<string, object> {
                { "domain", Domain },
                { "period", Period },
                { "reg_type", RegistrationType.ToString().ToLower() }
            };
        }
    }
}

/*

action = get_price
object = domain

Attributes ------------------------------------------------------------------

*domain:		The domain to be queried.

period:			The desired registration period. The default is 1.

reg_type:		The type of registration.
				Allowed values are new and renewal. The default is new.

*/
