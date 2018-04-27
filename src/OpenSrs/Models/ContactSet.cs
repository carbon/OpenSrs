namespace OpenSrs.Models
{
	using System.Xml.Linq;

	public class ContactSet
	{
		public DomainContact Owner { get; set; }
		public DomainContact Admin { get; set; }
		public DomainContact Tech { get; set; }
		public DomainContact Billing { get; set; }

		public XElement ToAssocEl()
		{
			return Util.ToDtAssoc(new {
				owner =		Owner != null ?		Util.ToDtAssoc(Owner.GetParameters())	: null,
				admin =		Admin != null ?		Util.ToDtAssoc(Admin.GetParameters())	: null,
				tech =		Tech != null ?		Util.ToDtAssoc(Tech.GetParameters())	: null,
				billing =	Billing != null ?	Util.ToDtAssoc(Billing.GetParameters())	: null
			});
		}
	}
}

/*
<item key="contact_set">
	<dt_assoc>
		<item key="owner">
			<dt_assoc> 
				<item key="org_name">Setter Sanctuary</item>
			</dt_assoc>
		</item> 
		<item key="admin">
			<dt_assoc> 
				<item key="country">CA</item>
				<item key="org_name">Setter Sanctuary</item>
				<item key="phone">+14165551212</item> 
				<item key="state">ON</item>
				<item key="last_name">Wilk</item> 
				<item key="address2"></item>
				<item key="email">settersanctuary@hotmail.com</item>
				<item key="lang_pref">EN</item>
				<item key="city">Toronto</item>
				<item key="postal_code">M1M1M1</item>
				<item key="fax"></item>
				<item key="address1"> 123 Oak St. </item>
				<item key="nationality"></item>
				<item key="first_name">Robson</item>
			</dt_assoc>
	</item> 
</item>
*/
