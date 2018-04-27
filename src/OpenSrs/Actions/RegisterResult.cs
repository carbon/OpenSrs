using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Linq;
using System.Xml.XPath;

namespace OpenSrs
{
    public class RegisterResult
	{
		public long Id { get; set; }
	
        [DataMember(Name = "admin_email")]
		public string AdminEmail { get; set; }
		
        [DataMember(Name = "async_reason")]
        public string AsyncReason { get; set; }
		/// <summary>
		/// The registration text returned by the registry.
		/// </summary>
        [DataMember(Name = "registration_text")]
		public string RegistrationText { get; set; }

		/// <summary>
		/// The registration code returned by the registry.
		/// </summary>
        [DataMember(Name = "registration_code")]
		public int RegistrationCode { get; set; }
		
        [DataMember(Name = "queue_request_id")]
        public string QueueRequestId { get; set; }
        
        [DataMember(Name = "transfer_id")]
        public string TransferId { get; set; }

        [DataMember(Name = "whois_privacy_state")]
		public string WhoisPrivacyState { get; set; }

		public string Error { get; set; }

		public static RegisterResult Parse(string text)
		{
			var doc = XDocument.Parse(text);

			var itemEl = doc.XPathSelectElement(@"/OPS_envelope/body/data_block/dt_assoc");

			var response = ResponseDetails.FromEl(itemEl);

			if (!response.IsSuccess)
			{
				throw new OpenSrsException(response.ResponseText) {
					ResponseCode = response.ResponseCode
				};
			}

			var attributes = ResponseHelper.GetAttributesAsDictionary(doc);

			var result = new RegisterResult {
				Id					= Int64.Parse(attributes.GetValueOrDefault("id")), // the order id
				AdminEmail			= attributes.GetValueOrDefault("admin_email"),
                AsyncReason         = attributes.GetValueOrDefault("async_reason"),
				RegistrationCode	= attributes.TryGetValue("registration_code", out var registrationCode) ? int.Parse(registrationCode) : 0,
				RegistrationText	= attributes.GetValueOrDefault("registration_text"),
				WhoisPrivacyState	= attributes.GetValueOrDefault("whois_privacy_state"),
				Error				= attributes.GetValueOrDefault("error")
			};

			return result;
		}
	}

	public static class DicExtensions
	{
		public static string GetValueOrDefault(this IDictionary<string, string> dic, string key)
		{
            dic.TryGetValue(key, out string value);

            return value;
		}
	}
}

/*
Attributes ------------------------------------------------------------------

admin_email:			Returns the admin email contact from the order (can be used to send thank you email for .CA based on existing CIRA's registrant profile).

cancelled_orders:		A list of pending orders for this domain that are cancelled by the successful registration of the domain.

error:					Only returned when an order fails.
						A text description of the errors that occurred in a failed transaction.
 
forced_pending:			Only returned if the order has been forced to pending queue.

						Orders that cannot be processed (insufficient funds, domain already taken, and so on) are forced to the pending queue.
*id:					The ID of the order, which can be used in RWI queries.

queue_request_id:		Only returned if the order has been queued.

*registration_code:		The registration code returned by the registry.
*registration_text:		The registration text returned by the registry.

transfer_id:			Only returned if reg_type=transfer
						ID number of the transfer.

*whois_privacy_state:	Returns the state of WHOIS Privacy.
						Allowed values are enabled, disabled, enabling (in process), or disabling (in process).

*/
