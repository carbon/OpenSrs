using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Xml.XPath;

namespace OpenSrs
{
    public class ResponseHelper
	{
		public static Dictionary<string, string> ParseAttributes(string text)
		{
			var doc = XDocument.Parse(text);

			var itemEl = doc.XPathSelectElement(@"//item[@key=""attributes""]/dt_assoc");

			return ReadAssoc(itemEl);
		}

		public static Dictionary<string, string> GetAttributesAsDictionary(XDocument doc)
		{
			var itemEl = doc.XPathSelectElement(@"//item[@key=""attributes""]/dt_assoc");

			return ReadAssocAsDic(itemEl);
		}

		public static Dictionary<string, string> ReadAssocAsDic(XElement dtAssocEl)
		{
			if (dtAssocEl == null) throw new ArgumentNullException("dtAsscEl");

			var expando = new Dictionary<string, string>();

			foreach (var el in dtAssocEl.Elements("item"))
			{
				expando.Add(el.Attribute("key").Value, el.Value);
			}

			return expando;
		}


		public static Dictionary<string, string> ReadAssoc(XElement dtAssocEl)
		{
			var expando = new Dictionary<string, string>();

			foreach (var el in dtAssocEl.Elements("item"))
			{
				expando.Add(el.Attribute("key").Value, el.Value);
			}

			return expando;
		}



		public static IEnumerable<Dictionary<string, string>> ReadArray(XElement dtArrayEl)
		{
			foreach (var item in dtArrayEl.Elements("item"))
			{
				yield return ReadAssoc(item.Element("dt_assoc"));
			}
		}

		/*
		 <dt_array>
			<item key="0">
			<dt_assoc>
			<item key="domain">carbonmade.com</item>
			<item key="status">available</item>
			</dt_assoc>
			</item>
		 </dt_array>
		 */
	}
}

/*
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
</OPS_envelope>
*/