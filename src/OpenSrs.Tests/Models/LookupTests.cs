using Xunit;

namespace OpenSrs.Tests
{	
	public class LookupTests
	{
		[Fact]
		public void ToXmlTest()
		{
			var request = new LookupRequest("monsters.com") {
				NoCache = true
			};

			Assert.Equal(
@"<OPS_envelope>
  <header>
    <version>0.9</version>
  </header>
  <body>
    <data_block>
      <dt_assoc>
        <item key=""protocol"">XCP</item>
        <item key=""action"">LOOKUP</item>
        <item key=""object"">DOMAIN</item>
        <item key=""attributes"">
          <dt_assoc>
            <item key=""domain"">monsters.com</item>
            <item key=""no_cache"">1</item>
          </dt_assoc>
        </item>
      </dt_assoc>
    </data_block>
  </body>
</OPS_envelope>", request.ToXml().ToString());
		}

		[Fact]
		public void ParseAvailableResponse()
		{
			var result = LookupResult.Parse(@"<OPS_envelope>
	<header>
		<version>0.9</version>
	</header>
	<body>
		<data_block>
			<dt_assoc>
				<item key=""protocol"">XCP</item>
				<item key=""object"">DOMAIN</item>
				<item key=""response_text"">Domain available</item>
				<item key=""action"">REPLY</item>
				<item key=""attributes"">
					<dt_assoc>
						<item key=""status"">available</item>
						<item key=""match""></item>
					</dt_assoc>
				</item>
				<item key=""response_code"">210</item>
				<item key=""is_success"">1</item>
			</dt_assoc>
		</data_block>
	</body>
</OPS_envelope>");

			Assert.Equal(DomainStatus.Available, result.Status);
		}

		[Fact]
		public void ParseTakenResponse()
		{
			var result = LookupResult.Parse(@"<OPS_envelope>
	<header>
		<version>0.9</version>
	</header>
	<body>
		<data_block>
			<dt_assoc>
				<item key=""protocol"">XCP</item>
				<item key=""object"">DOMAIN</item>
				<item key=""response_text"">Domain available</item>
				<item key=""action"">REPLY</item>
				<item key=""attributes"">
					<dt_assoc>
						<item key=""status"">taken</item>
						<item key=""match""></item>
					</dt_assoc>
				</item>
				<item key=""response_code"">210</item>
				<item key=""is_success"">1</item>
			</dt_assoc>
		</data_block>
	</body>
</OPS_envelope>");

			Assert.Equal(DomainStatus.Taken, result.Status);
		}
	}
}
