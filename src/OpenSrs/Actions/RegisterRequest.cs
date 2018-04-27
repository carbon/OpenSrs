using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using OpenSrs.Models;

namespace OpenSrs
{
    public class RegisterRequest : OpenSrsRequest
    {
        public RegisterRequest() : base("SW_REGISTER", "DOMAIN")
        {
            Period = 1;
            RegistrationType = RegistrationType.New;
            ProcessImmediately = true;
            LockDomain = true;
        }

        public bool ProcessImmediately { get; set; }

        public string Domain { get; set; }

        /// <summary>
        /// In Years
        /// </summary>
        [Range(1, 10)]
        public int Period { get; set; }

        [DataMember(Name = "reg_type")]
        public RegistrationType RegistrationType { get; set; }

        public bool AutoRenew { get; set; }

        public bool LockDomain { get; set; }

        public string UserName { get; set; }

        // A-Z, a-z, 0-9, ! @\$^,.~|=-+_{}#"
        [MinLength(10), MaxLength(20)]
        public string Password { get; set; }

        public string DnsTemplate { get; set; }

        public ContactSet Contacts { get; set; }

        [DataMember(Name = "custom_nameservers")]
        public bool CustomNameservers { get; set; }

        [DataMember(Name = "f_whois_privacy")]
        public bool WhoisPrivacy { get; set; }

        [DataMember(Name = "custom_tech_contact")]
        public bool CustomTechContact { get; set; }

        // f_lock_domain


        // affiliate_id
        // comments

        public override Dictionary<string, object> GetParameters()
        {
            var data = new Dictionary<string, object> {
                { "auto_renew",          AutoRenew ? "1" : "0" },
                { "contact_set",         Contacts.ToAssocEl() },
                { "custom_nameservers",  CustomNameservers ? "1" : "0" },
                { "custom_tech_contact", CustomTechContact ? "1" : "0" },
                { "f_lock_domain",       LockDomain ? "1" : "0" },
                { "domain",              Domain },
                { "handle",              ProcessImmediately ? "process" : "save" },
                { "period",              Period },
                { "reg_username",        UserName },
                { "reg_password",        Password },
                { "reg_type",            RegistrationType.ToString().ToLower() },
                { "f_whois_privacy",     WhoisPrivacy ? "1" : "0" }
            };

            if (DnsTemplate != null) data["dns_template"] = DnsTemplate;

            return data;
        }
    }
}


/*
Attributes

affiliate_id:					The unique identifier of an RSP's affiliate, which allows the RSP to track orders coming through different affiliates. Specify a valid affiliate ID (max 100 char).

auto_renew:						Used to set domain to auto-renew. 0 = do not auto-renew 1 = auto-renew

change_contact:					Used to change contact information for .ORG, .INFO, .BIZ, .BE, .CN, .EU, .ME, .MOBI, .UK, and .US domains during a transfer or after transfer completion. Contact changes are always applied for all other TLDs except for .CA, which does not allow contact changes during the transfer.
								0 = do not change contact set
								1 = apply new contact set when transfer completes. Value is always set to 1 for .CC, .TV, .COM, .NET, and .NAME.

* contact_set:					A collection of associative arrays containing contact information for each contact type (owner, admin, billing, tech). See "Contact Set".
								Note: For .CA, .US, and .ES registrations, state is required.

* custom_ nameservers:			An indication of whether to use the RSP's default nameservers, or those provided in the 'sw_register' request.
								0 = use Reseller's default nameservers. If set to 0 and default nameservers are not defined, no nameservers are associated with the domain.
								Note:a .TEL alwys uses the default nameservers, so this value should be 0 for .TEL registrations. Any nameservers submitted for .TEL will be ignored.
								1 = use nameservers provided in request.

custom_transfer_ nameservers:	If not submitted, the nameserver list is ignored
								This flag is only used if reg_type = transfer, and indicates whether to use the nameservers provided in the request or keep the domain's existing nameservers.
								0 = use the domain's existing nameservers and ignore nameservers provided in request.
								1 = use nameservers provided in request. If no nameservers are supplied, domain's nameservers are used.
								Note: If this flag is set to '1', a minimum of two nameservers must be provided.

* custom_tech_contact:			An indication of whether to use the RSP's tech contact info, or the tech contact info provided in the 'sw_register' request.
								0 = use Reseller's tech contact info.
								1 = use tech contact info provided in request.

dns_template:					Specify the name of the DNS template that you want to use to enable DNS and assign initial DNS records, or specify *blank* to enable DNS without defining any records.
								Note: You must enter the word blank preceded and followed by asterisks.
								The template name can be a maximum of 50 characters.
								If this parameter is specified, the nameservers are automatically set to the nameservers for the DNS service:
								ns1.systemdns.com, ns2.systemdns.com, ns3.systemdns.com
 
* domain:						The domain name to be acted upon in the sw_register request.

encoding_type:					The encoding type for the domain.
								If the domain that you're trying to register contains multilingual characters, the domain name must be in Punycode format.
								• If not submitted, Standard ASCII format (English) is used.
								• If left blank, Standard ASCII format (English) is used.
								• If three- character language tag is specified, that language is used.

f_lock_domain:					Allows you to lock the domain so that it cannot be transferred away. To allow a transfer on a locked domain, the domain must first be unlocked. Even if submitted, this setting is not applied to TLDs where locking is not supported (.CA, .DE, .UK, .CH, .NL, .FR, IT, BE, AT).
								0 = do not lock domain 
								1 = lock domain
 
f_parkp:						Allows Resellers to make a customized (logo branded) page for domains in their profile that do not yet have a resolving website. Resellers can then receive revenue from traffic on each Parked Page.
								Parked Pages functionality is available for new and transferred domain registrations for these TLDs: .COM, .NET, .ORG, .INFO, .BIZ, .NAME, .BE, .CA, .CC, .CO, .EU, .ME, .TV, .UK, .US, and .MOBI.
								If Parked Pages is enabled, DNS settings entered at registration are saved for future use and the Parked Pages nameservers are used instead.
								For transfers, the DNS settings that were entered with the transfer request are used. If DNS settings were not submitted at the time of transfer, the pre-transfer DNS settings are used. If the domain was using the Parked Pages DNS at the time of transfer, and DNS settings are not provided, the domain continues to use the Parked Pages DNS.
								Y = enable Parked Pages for the domain 
								N = do not enable Parked Pages for the domain; this is the default.

f_whois_privacy:				Allows you to enable WHOIS Privacy for new .COM, .NET, .ORG, .INFO, .BIZ, .NAME, .ME, .MOBI, .CC, and .TV registrations.
								0 = Disable
								1 = Enable

handle:							Indicates how to process the order. If this parameter is not specified, the default parameter is taken from the RSP's RWI settings.
								save = pend the order for later approval by the RSP.
								process = proceed with the order immediately.
 
link_domains:					Indicates whether to link this domain with others.
								This parameter is only used when ordering multiple domains over multiple calls to sw_register. 
								Also, it is only used if the user does not wish to associate the new registrations with an existing domain/profile in OpenSRS. 
								The first sw_register call to OpenSRS should have link_domains set to 1.
								The return to this call contains an OpenSRS order ID. 
								This value is used as the master_order_id in the subsequent calls to sw_register for the remainder of the domain list.
								0 = do not link domains 
								1 = link domains. If set to 1, the reg_domain field should not be specified.
								Note: Only the first call to sw_register should contain the attribute link_domains.
 
master_order_id:				Required if link_domains = 1
								The order ID returned by the first sw_register call for a group of domains that are to be linked (via the link_domains parameter).

nameserver_list:				A list of nameserver pairs, each of which contain a nameserver's name and sort order. (Minimum two required). For allowed values, see "Nameserver pair".
								If dns_template is specified, the nameservers are automatically set to the nameservers for the DNS service:
								ns1.systemdns.com, ns2.systemdns.com, ns3.systemdns.com

owner_confirm_ address:			Required for .DE, .BE, and .EU transfers
								The email address to which to send messages regarding a .BE, .DE, or .EU transfer. The email address provided is used for the current transfer only, and not for future messages.

* period:						Required for new registrations only
								The length of the registration period. Allowed values are 1 – 10, depending on the TLD, that is, not all registries allow for a 1-year registration. 
								The default is 2, which is valid for all TLDs.

premium_price_to_ verify:		used only if reg_type = premium
								Submits the premium domain price and verifies that it is the same as the list price. If the submitted price does not match the Tucows list price, the command will fail.
								Allowed value is the price for the premium domain, in the format nnn.nn.

reg_domain:						An existing, active domain name in OpenSRS that is owned by the registrant. Providing this parameter links the newly registered or transferred domain to the profile of the existing domain.

* reg_username:					The username of the registrant.

* reg_password:					The registrant's password

* reg_type:						The type of registration being requested: new = a new registration
								premium = register a Premium domain name
								transfer = transfer a domain into OpenSRS
								sunrise = submit a request for a domain during its sunrise period
								whois_privacy = enable WHOIS Privacy for an existing domain. DEPRECATED.
*/
