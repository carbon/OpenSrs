using Xunit;

namespace OpenSrs.Tests
{
	public class CreateDnsZoneTests
	{
		[Fact]
		public void ParseResponse()
		{
			string text = @"<?xml version='1.0' encoding=""UTF-8"" standalone=""no"" ?>
<!DOCTYPE OPS_envelope SYSTEM ""ops.dtd"">
<OPS_envelope>
 <header>
  <version>0.9</version>
  </header>
 <body>
  <data_block>
   <dt_assoc>
    <item key=""protocol"">XCP</item>
    <item key=""object"">DOMAIN</item>
    <item key=""response_text"">Command Successful</item>
    <item key=""action"">REPLY</item>
    <item key=""attributes"">
     <dt_assoc>
      <item key=""records"">
       <dt_assoc>
       </dt_assoc>
      </item>
      <item key=""nameservers_ok"">1</item>
     </dt_assoc>
    </item>
    <item key=""response_code"">200</item>
    <item key=""is_success"">1</item>
   </dt_assoc>
  </data_block>
 </body>
</OPS_envelope>";


		}
	}
}