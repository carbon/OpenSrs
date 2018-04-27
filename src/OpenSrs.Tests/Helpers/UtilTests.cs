using Xunit;

using System.Collections.Generic;

namespace OpenSrs.Tests
{


	public class UtilTests
	{
		[Fact]
		public void ComputeMD5HashTest()
		{
			Assert.Equal(
				expected: "e787cc1d1951dfec4827cede7b1a0933",
				actual: Util.ComputeMD5Hash("ConnecttoOpenSRSviaSSL")
			);
		}

		[Fact]
		public void ToDtAssoc()
		{
			var parameters = new Dictionary<string, object> { 
				{ "protocol", "XPC" },
				{ "object", "DOMAIN" },
				{ "response_text", "Command Successful" },
				{ "action", "REPLY" },
			};

			var expected =
@"<dt_assoc>
  <item key=""protocol"">XPC</item>
  <item key=""object"">DOMAIN</item>
  <item key=""response_text"">Command Successful</item>
  <item key=""action"">REPLY</item>
</dt_assoc>";

			Assert.Equal(expected, Util.ToDtAssoc(parameters).ToString());
		}
	}
}
