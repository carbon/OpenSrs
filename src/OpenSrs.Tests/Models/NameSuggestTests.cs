using Xunit;

namespace OpenSrs.Tests
{
	using System;

	
	public class NameSuggestTests
	{
		[Fact]
		public void RequestToXmlTest()
		{
			var request = new NameSuggestRequest {
				MaxWaitTime = TimeSpan.FromSeconds(5),
				Tlds = new[] { ".com", ".net", ".org" }
			};

			Assert.Equal(@"<OPS_envelope>
  <header>
    <version>0.9</version>
  </header>
  <body>
    <data_block>
      <dt_assoc>
        <item key=""protocol"">XCP</item>
        <item key=""action"">NAME_SUGGEST</item>
        <item key=""object"">DOMAIN</item>
        <item key=""attributes"">
          <dt_assoc>
            <item key=""max_wait_time"">5</item>
            <item key=""tlds"">
              <dt_array>
                <item key=""0"">.com</item>
                <item key=""1"">.net</item>
                <item key=""2"">.org</item>
              </dt_array>
            </item>
          </dt_assoc>
        </item>
      </dt_assoc>
    </data_block>
  </body>
</OPS_envelope>", request.ToXml().ToString());
		}
	}
}
