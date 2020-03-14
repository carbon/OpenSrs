using System.Collections.Generic;
using System.Net;
using System.Xml.Linq;

namespace OpenSrs.Models
{
    public sealed class DnsRecordSet
    {
        private readonly List<ARecord> a = new List<ARecord>();
        private readonly List<AAAARecord> aaaa = new List<AAAARecord>();
        private readonly List<CNAMERecord> cname = new List<CNAMERecord>();
        private readonly List<MXRecord> mx = new List<MXRecord>();
        private readonly List<SRVRecord> srv = new List<SRVRecord>();
        private readonly List<TXTRecord> txt = new List<TXTRecord>();

        public IList<ARecord> A => a;

        public IList<AAAARecord> AAAA => aaaa;

        public IList<CNAMERecord> CNAME => cname;

        public IList<MXRecord> MX => mx;

        public IList<SRVRecord> SRV => srv;

        public IList<TXTRecord> TXT => txt;

        public XElement ToDtAssoc()
        {
            return Util.ToDtAssoc(new
            {
                A = a.Count > 0 ? Util.ToDtArray(a) : null,
                AAAA = aaaa.Count > 0 ? Util.ToDtArray(aaaa) : null,
                CNAME = cname.Count > 0 ? Util.ToDtArray(cname) : null,
                MX = mx.Count > 0 ? Util.ToDtArray(mx) : null,
                SVR = srv.Count > 0 ? Util.ToDtArray(srv) : null,
                TXT = txt.Count > 0 ? Util.ToDtArray(txt) : null,
            });

            /*
			<item key="A">
				<dt_array> 
					<item key="0">
						<dt_assoc>
							<item key="9.36.11.25"></item>
							<item key="subdomain">wwwip_address</item>
						</dt_assoc>
					</item>
				</dt_array>
			</item>
			*/
        }
    }

    public sealed class ARecord : IDtEl
    {
        public IPAddress Address { get; set; }
        public string Subdomain { get; set; }

        public XElement ToDtAssoc()
        {
            return Util.ToDtAssoc(new
            {
                ip_address = this.Address.ToString(),
                subdomain = this.Subdomain
            });
        }
    }

    public sealed class AAAARecord : IDtEl
    {
        public IPAddress Address { get; set; }
        public string Subdomain { get; set; }

        public XElement ToDtAssoc()
        {
            return Util.ToDtAssoc(new
            {
                ipv6_address = this.Address.ToString(),
                subdomain = this.Subdomain
            });
        }
    }

    public sealed class CNAMERecord : IDtEl
    {
        /// <summary>
        /// The FQDN of the domain that you want to access.
        /// </summary>
        public string HostName { get; set; }

        public string Subdomain { get; set; }

        public XElement ToDtAssoc()
        {
            return Util.ToDtAssoc(new
            {
                hostname = this.HostName,
                subdomain = this.Subdomain,
            });
        }
    }

    public sealed class MXRecord : IDtEl
    {
        public int Priority { get; set; }

        public string HostName { get; set; }

        public string Subdomain { get; set; }

        public XElement ToDtAssoc()
        {
            return Util.ToDtAssoc(new
            {
                priority = this.Priority,
                hostname = this.HostName,
                subdomain = this.Subdomain
            });
        }
    }

    public sealed class SRVRecord : IDtEl
    {
        public int Priority { get; set; }

        public int Weight { get; set; }

        public string Subdomain { get; set; }

        /// <summary>
        /// The FQDN of the domain that you want to access.
        /// </summary>
        public string HostName { get; set; }

        public int Port { get; set; }

        public XElement ToDtAssoc()
        {
            return Util.ToDtAssoc(new
            {
                priority = this.Priority,
                weight = this.Weight,
                subdomain = this.Subdomain,
                hostname = this.HostName,
                port = this.Port
            });
        }

        /*
		priority:		The priority of the target host, lower value means more preferred.
		weight:			A relative weight for records with the same priority.
		subdomain:		The third level of the domain name, such as www or ftp.
		hostname:		The FQDN of the domain that you want to access.
		port:			The TCP or UDP port on which the service is to be found.
		*/
    }

    public sealed class TXTRecord : IDtEl
    {
        public string Text { get; set; }
        public string Subdomain { get; set; }

        public XElement ToDtAssoc()
        {
            return Util.ToDtAssoc(new
            {
                text = this.Text,
                subdomain = this.Subdomain
            });
        }
    }
}

/*
<item key="records">
	<dt_assoc> 
		<item key="A">
			<dt_array> 
				<item key="0">
					<dt_assoc>
						<item key="9.36.11.25"></item>
						<item key="subdomain">wwwip_address</item>
					</dt_assoc>
				</item>
			</dt_array>
		</item>
		<item key="MX">
			<dt_array>
				<item key="0">
					<dt_assoc>
						<item key="priority">10</item>
						<item key="hostname">example.org</item>
						<item key="subdomain">www3</item>
					</dt_assoc> 
				</item>
			</dt_array> 
		</item>
		<item key="TXT">
			<dt_array> 
				<item key="0">
					<dt_assoc>
						<item key="text">This is a test</item>
						<item key="subdomain">www5</item>
					</dt_assoc> 
				</item>
			</dt_array>
		</item>
		<item key="SRV">
			<dt_array>
				<item key="0">
					<dt_assoc>
						<item key="priority">10</item>
						<item key="weight">1</item>
						<item key="hostname">example.org</item>
						<item key="subdomain">www4</item>
						<item key="port">443</item>
					</dt_assoc>
				</item>
			</dt_array>
		</item>
		<item key="CNAME">
			<dt_array>
				<item key="0">
					<dt_assoc>
						<item key="hostname">example.org</item>
						<item key="subdomain">www2</item>
					</dt_assoc>
				</item>
			</dt_array>
		</item>
		<item key="AAAA">
			<dt_array>
				<item key="0">
					<dt_assoc>
						<item key="subdomain">www1</item>
						<item key="ipv6_address">2001:00ab:0000:00a1:0001:000b:00cc:00de</item>
					</dt_assoc>
				</item>
			</dt_array>
		</item>
	</dt_assoc>
</item>
*/
