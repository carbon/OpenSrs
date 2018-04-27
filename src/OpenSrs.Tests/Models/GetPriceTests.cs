using Xunit;

namespace OpenSrs.Tests
{	
	public class GetPriceTests
	{
		[Fact]
		public void ParseResponse()
		{
			var result = GetPriceResult.Parse(@"
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
      <item key=""price"">9.85</item>
     </dt_assoc>
    </item>
    <item key=""response_code"">200</item>
    <item key=""is_success"">1</item>
   </dt_assoc>
  </data_block>
 </body>
</OPS_envelope>");

			Assert.Equal(9.85m, result.Price);
		}
	}
}
