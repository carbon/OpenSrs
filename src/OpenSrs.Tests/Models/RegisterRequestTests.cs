using Xunit;

using OpenSrs.Models;

namespace OpenSrs.Tests
{
	public class RegisterRequestTests
	{
		[Fact]
		public void CanSerialize()
		{
			var contact = new DomainContact
			{
				FirstName = "John",
				LastName = "Doe",
				OrganizationName = "Tucows",
				Address1 = "141 Lafayette St.",
				Address2 = "Floor 99",
				City = "New York",
				State = "NY",
				Country = "US",
				PostalCode = "10013",
				Email = "test@test.com",
				Phone = "+1.8661111111"
			};

			var request = new RegisterRequest
			{
				Domain = "test.com",
				Contacts = new ContactSet
				{
					Admin = contact,
					Billing = contact,
					Tech = contact,
					Owner = contact
				},
				AutoRenew = false,
				LockDomain = true,
				RegistrationType = RegistrationType.New,
				Period = 1,
                WhoisPrivacy = true,
				UserName = "test",
				Password = "password"
			};

			var text = request.ToXml().ToString();

			Assert.Equal(
@"<OPS_envelope>
  <header>
    <version>0.9</version>
  </header>
  <body>
    <data_block>
      <dt_assoc>
        <item key=""protocol"">XCP</item>
        <item key=""action"">SW_REGISTER</item>
        <item key=""object"">DOMAIN</item>
        <item key=""attributes"">
          <dt_assoc>
            <item key=""auto_renew"">0</item>
            <item key=""contact_set"">
              <dt_assoc>
                <item key=""owner"">
                  <dt_assoc>
                    <item key=""first_name"">John</item>
                    <item key=""last_name"">Doe</item>
                    <item key=""phone"">+1.8661111111</item>
                    <item key=""email"">test@test.com</item>
                    <item key=""org_name"">Tucows</item>
                    <item key=""address1"">141 Lafayette St.</item>
                    <item key=""address2"">Floor 99</item>
                    <item key=""city"">New York</item>
                    <item key=""state"">NY</item>
                    <item key=""country"">US</item>
                    <item key=""postal_code"">10013</item>
                  </dt_assoc>
                </item>
                <item key=""admin"">
                  <dt_assoc>
                    <item key=""first_name"">John</item>
                    <item key=""last_name"">Doe</item>
                    <item key=""phone"">+1.8661111111</item>
                    <item key=""email"">test@test.com</item>
                    <item key=""org_name"">Tucows</item>
                    <item key=""address1"">141 Lafayette St.</item>
                    <item key=""address2"">Floor 99</item>
                    <item key=""city"">New York</item>
                    <item key=""state"">NY</item>
                    <item key=""country"">US</item>
                    <item key=""postal_code"">10013</item>
                  </dt_assoc>
                </item>
                <item key=""tech"">
                  <dt_assoc>
                    <item key=""first_name"">John</item>
                    <item key=""last_name"">Doe</item>
                    <item key=""phone"">+1.8661111111</item>
                    <item key=""email"">test@test.com</item>
                    <item key=""org_name"">Tucows</item>
                    <item key=""address1"">141 Lafayette St.</item>
                    <item key=""address2"">Floor 99</item>
                    <item key=""city"">New York</item>
                    <item key=""state"">NY</item>
                    <item key=""country"">US</item>
                    <item key=""postal_code"">10013</item>
                  </dt_assoc>
                </item>
                <item key=""billing"">
                  <dt_assoc>
                    <item key=""first_name"">John</item>
                    <item key=""last_name"">Doe</item>
                    <item key=""phone"">+1.8661111111</item>
                    <item key=""email"">test@test.com</item>
                    <item key=""org_name"">Tucows</item>
                    <item key=""address1"">141 Lafayette St.</item>
                    <item key=""address2"">Floor 99</item>
                    <item key=""city"">New York</item>
                    <item key=""state"">NY</item>
                    <item key=""country"">US</item>
                    <item key=""postal_code"">10013</item>
                  </dt_assoc>
                </item>
              </dt_assoc>
            </item>
            <item key=""custom_nameservers"">0</item>
            <item key=""custom_tech_contact"">0</item>
            <item key=""f_lock_domain"">1</item>
            <item key=""domain"">test.com</item>
            <item key=""handle"">process</item>
            <item key=""period"">1</item>
            <item key=""reg_username"">test</item>
            <item key=""reg_password"">password</item>
            <item key=""reg_type"">new</item>
            <item key=""f_whois_privacy"">1</item>
          </dt_assoc>
        </item>
      </dt_assoc>
    </data_block>
  </body>
</OPS_envelope>", text);
		}

		[Fact]
		public void ParseResponse()
		{
			string text =

@"<?xml version='1.0' encoding=""UTF-8"" standalone=""no"" ?>
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
    <item key=""response_code"">200</item>
    <item key=""is_success"">1</item>
    <item key=""response_text"">Domain registration successfully completed
Domain successfully locked.
Whois Privacy successfully enabled.</item>
    <item key=""action"">REPLY</item>
    <item key=""attributes"">
     <dt_assoc>
      <item key=""registration_text"">Domain registration successfully completed
Domain successfully locked.
Whois Privacy successfully enabled.</item>
      <item key=""admin_email"">test@test.com</item>
      <item key=""registration_code"">200</item>
      <item key=""id"">1580054</item>
     </dt_assoc>
    </item>
   </dt_assoc>
  </data_block>
 </body>
</OPS_envelope>";

			var result = RegisterResult.Parse(text);

			Assert.Equal(1580054L, result.Id);
			Assert.Equal("test@test.com", result.AdminEmail);
			Assert.Equal(200, result.RegistrationCode);
			Assert.Equal(@"Domain registration successfully completed
Domain successfully locked.
Whois Privacy successfully enabled.".Replace("\r", ""), result.RegistrationText);

			Assert.Null(result.Error);
		}

		[Fact]
		public void ParseResponse2()
		{
			string text = @"<OPS_envelope>
  <header>
	<version>0.9</version>
  </header>
  <body>
    <data_block>
	  <dt_assoc>
        <item key=""protocol"">XCP</item>
        <item key=""action"">REPLY</item>
        <item key=""object"">DOMAIN</item>
        <item key=""is_success"">1</item>
        <item key=""response_code"">200</item>
        <item key=""response_text"">Domain registration successfully completed. WHOIS Privacy successfully enabled. Domain successfully locked.</item>
        <item key=""attributes"">
          <dt_assoc>
            <item key=""admin_email"">jsmith@example.com</item>
            <item key=""whois_privacy_state"">enabled</item>
            <item key=""registration_text"">Domain registration successfully completed. WHOIS Privacy successfully enabled. Domain successfully locked.</item>
            <item key=""registration_code"">200</item>
            <item key=""id"">3735281</item>
            <item key=""cancelled_orders"">
              <dt_array>
                <item key=""0"">1</item>
                <item key=""1"">2</item>
              </dt_array>
            </item>
          </dt_assoc>
        </item>
	  </dt_assoc>
    </data_block>
  </body>
</OPS_envelope>";

			var result = RegisterResult.Parse(text);

			Assert.Equal(3735281, result.Id);
			Assert.Equal("jsmith@example.com", result.AdminEmail);
			Assert.Equal("enabled", result.WhoisPrivacyState);
			Assert.Equal(200, result.RegistrationCode);
		}

		[Fact]
		public void ParseInvalid()
		{
			var text = @"<?xml version='1.0' encoding=""UTF-8"" standalone=""no"" ?>
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
    <item key=""response_text"">DNS template specified does not exist.  (portfolio)
</item>
    <item key=""action"">REPLY</item>
    <item key=""response_code"">478</item>
    <item key=""is_success"">0</item>
   </dt_assoc>
  </data_block>
 </body>
</OPS_envelope>";

            Assert.Throws<OpenSrsException>(() => {
				var result = RegisterResult.Parse(text);
			});
			
		}
	}
}
