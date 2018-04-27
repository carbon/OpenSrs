using Xunit;

namespace OpenSrs.Models.Tests
{
	public class ContactTests
	{
		[Fact]
		public void ToDtAssoc()
		{
			var contact = new DomainContact {
				FirstName = "Susan",
				LastName = "Miller",
				Address1 = "518 8th Ave",
				Address2 = "APT 10A",
				City = "San Francisco",
				State = "CA",
				Country = "US",
				PostalCode = "94104",
				Email = "susan@miller.com",
				Phone = "+1.5555555555"
			};

			var expected =
@"<dt_assoc>
  <item key=""first_name"">Susan</item>
  <item key=""last_name"">Miller</item>
  <item key=""phone"">+1.5555555555</item>
  <item key=""email"">susan@miller.com</item>
  <item key=""address1"">518 8th Ave</item>
  <item key=""address2"">APT 10A</item>
  <item key=""city"">San Francisco</item>
  <item key=""state"">CA</item>
  <item key=""country"">US</item>
  <item key=""postal_code"">94104</item>
</dt_assoc>";

			var result = Util.ToDtAssoc(contact.GetParameters());

			Assert.Equal(expected, result.ToString());
		}
	}
}