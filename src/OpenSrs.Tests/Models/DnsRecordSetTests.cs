using Xunit;

namespace OpenSrs.Models.Tests
{
	using System;
	using System.Net;
	
	public class DnsRecordTests
	{
		[Fact]
		public void GoogleAppMXRecords()
		{
			var records = new DnsRecordSet {
				MX = { 
					new MXRecord {
						HostName = "ASPMX.L.GOOGLE.COM",
						Priority = 1
					},
					new MXRecord {
						HostName = "ALT1.ASPMX.L.GOOGLE.COM",
						Priority = 5
					},
					new MXRecord {
						HostName = "ALT2.ASPMX.L.GOOGLE.COM",
						Priority = 5
					},
					new MXRecord {
						HostName = "ASPMX2.GOOGLEMAIL.COM",
						Priority = 10
					},
					new MXRecord {
						HostName = "ASPMX3.GOOGLEMAIL.COM",
						Priority = 10
					}
				}
			};

			Assert.Equal(@"<dt_assoc>
  <item key=""MX"">
    <dt_array>
      <item key=""0"">
        <dt_assoc>
          <item key=""priority"">1</item>
          <item key=""hostname"">ASPMX.L.GOOGLE.COM</item>
        </dt_assoc>
      </item>
      <item key=""1"">
        <dt_assoc>
          <item key=""priority"">5</item>
          <item key=""hostname"">ALT1.ASPMX.L.GOOGLE.COM</item>
        </dt_assoc>
      </item>
      <item key=""2"">
        <dt_assoc>
          <item key=""priority"">5</item>
          <item key=""hostname"">ALT2.ASPMX.L.GOOGLE.COM</item>
        </dt_assoc>
      </item>
      <item key=""3"">
        <dt_assoc>
          <item key=""priority"">10</item>
          <item key=""hostname"">ASPMX2.GOOGLEMAIL.COM</item>
        </dt_assoc>
      </item>
      <item key=""4"">
        <dt_assoc>
          <item key=""priority"">10</item>
          <item key=""hostname"">ASPMX3.GOOGLEMAIL.COM</item>
        </dt_assoc>
      </item>
    </dt_array>
  </item>
</dt_assoc>", records.ToDtAssoc().ToString());
		}

		[Fact]
		public void ToDtElement()
		{
			var records = new DnsRecordSet {
				A = {
					new ARecord { 
						Address = IPAddress.Parse("192.168.1.1") 
					},
					new ARecord { 
						Address = IPAddress.Parse("192.168.1.1"),
						Subdomain = "www" 
					}
				},
				MX = { 
					new MXRecord {
						HostName = "google.com",
						Subdomain = "www",
						Priority = 1
					}
				}
			};

			Assert.Equal(@"<dt_assoc>
  <item key=""A"">
    <dt_array>
      <item key=""0"">
        <dt_assoc>
          <item key=""ip_address"">192.168.1.1</item>
        </dt_assoc>
      </item>
      <item key=""1"">
        <dt_assoc>
          <item key=""ip_address"">192.168.1.1</item>
          <item key=""subdomain"">www</item>
        </dt_assoc>
      </item>
    </dt_array>
  </item>
  <item key=""MX"">
    <dt_array>
      <item key=""0"">
        <dt_assoc>
          <item key=""priority"">1</item>
          <item key=""hostname"">google.com</item>
          <item key=""subdomain"">www</item>
        </dt_assoc>
      </item>
    </dt_array>
  </item>
</dt_assoc>", records.ToDtAssoc().ToString());
		}
	}
}