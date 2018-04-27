using System;

namespace OpenSrs
{
    public class LookupResult
    {
        public DomainStatus Status { get; set; }

        public bool HasClaim { get; set; }

        public string Reason { get; set; }

        public static LookupResult Parse(string text)
        {
            var attributes = ResponseHelper.ParseAttributes(text);

            return new LookupResult {
                Status = (DomainStatus)Enum.Parse(typeof(DomainStatus), attributes["status"], ignoreCase: true),
                HasClaim = attributes.TryGetValue("has_claim", out var claim) && claim == "1",
                Reason = attributes.TryGetValue("reason", out var reason) ? reason : null
            };
        }
    }
}

/*

Attributes ------------------------------------------------------------------

email_available:	Indicates whether a forwarding email is available or not. Applies to .NAME domains only.
					0 = no forwarding email available 
					1 = forwarding email available

noservice:			Required only for .NAME
					Indicates Registry Agent availability. If RA is available, noservice does not appear in response.
					1 = supplier unavailable Note: when noservice = 1, is_success = 0.

price_status:		The pricing status. Undef = non .TV domains Fixed = price is fixed for .TV domains

*status:			Whether the domain is available or taken.

*/
