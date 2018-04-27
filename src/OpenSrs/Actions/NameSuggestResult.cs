using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Xml.XPath;

namespace OpenSrs
{
    public class NameSuggestResult
    {
        public IList<DomainItem> Lookup { get; } = new List<DomainItem>();

        public IList<DomainItem> Suggestions { get; } = new List<DomainItem>();

        public TimeSpan ResponseTime { get; set; }

        public static NameSuggestResult Parse(string text)
        {
            try
            {
                var response = new NameSuggestResult();

                var doc = XDocument.Parse(text);

                var responseTime = doc.XPathSelectElement(@"//item[@key=""request_response_time""]");
                var lookupArray = doc.XPathSelectElement(@"//item[@key=""lookup""]/dt_assoc/item[@key=""items""]/dt_array");
                var suggestionArray = doc.XPathSelectElement(@"//item[@key=""suggestion""]/dt_assoc/item[@key=""items""]/dt_array");

                response.ResponseTime = TimeSpan.FromSeconds((float)responseTime);

                foreach (var item in ResponseHelper.ReadArray(lookupArray))
                {
                    response.Lookup.Add(new DomainItem(item["domain"], item["status"]));
                }

                foreach (var item in ResponseHelper.ReadArray(suggestionArray))
                {
                    response.Suggestions.Add(new DomainItem(item["domain"], item["status"]));
                }

                return response;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message + "/ " + text);
            }
        }
    }

    public class DomainItem
    {
        public DomainItem(string domain, string status)
        {
            Domain = domain;
            Status = status;
        }

        public string Domain { get; }

        public string Status { get; }
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
    <item key="protocol">XCP</item>
    <item key="response_code">200</item>
    <item key="is_success">1</item>
    <item key="request_response_time">0.423</item>
    <item key="response_text">Command completed successfully</item>
    <item key="is_search_completed">1</item>
    <item key="action">REPLY</item>
    <item key="attributes">
     <dt_assoc>
      <item key="lookup">
       <dt_assoc>
        <item key="count">4</item>
        <item key="response_text">Command completed successfully.</item>
        <item key="response_code">200</item>
        <item key="items">
         <dt_array>
          <item key="0">
           <dt_assoc>
            <item key="domain">carbonmade.com</item>
            <item key="status">available</item>
           </dt_assoc>
          </item>
          <item key="1">
           <dt_assoc>
            <item key="domain">carbonmade.net</item>
            <item key="status">available</item>
           </dt_assoc>
          </item>
          <item key="2">
           <dt_assoc>
            <item key="domain">carbonmade.org</item>
            <item key="status">available</item>
           </dt_assoc>
          </item>
          <item key="3">
           <dt_assoc>
            <item key="domain">carbonmade.tv</item>
            <item key="status">available</item>
           </dt_assoc>
          </item>
         </dt_array>
        </item>
        <item key="is_success">1</item>
       </dt_assoc>
      </item>
      <item key="suggestion">
       <dt_assoc>
        <item key="count">50</item>
        <item key="response_text">Command Successful</item>
        <item key="response_code">200</item>
        <item key="is_success">1</item>
        <item key="items">
         <dt_array>
          <item key="0">
           <dt_assoc>
            <item key="domain">carbon-made.org</item>
            <item key="status">available</item>
           </dt_assoc>
          </item>
          <item key="1">
           <dt_assoc>
            <item key="domain">carbon-made.net</item>
            <item key="status">available</item>
           </dt_assoc>
          </item>
          <item key="2">
           <dt_assoc>
            <item key="domain">carbonmanufactured.com</item>
            <item key="status">available</item>
           </dt_assoc>
          </item>
          <item key="3">
           <dt_assoc>
            <item key="domain">carboncreated.com</item>
            <item key="status">available</item>
           </dt_assoc>
          </item>
          <item key="4">
           <dt_assoc>
            <item key="domain">substancemade.com</item>
            <item key="status">available</item>
           </dt_assoc>
          </item>
          <item key="5">
           <dt_assoc>
            <item key="domain">duplicatemade.com</item>
            <item key="status">available</item>
           </dt_assoc>
          </item>
          <item key="6">
           <dt_assoc>
            <item key="domain">elementmade.com</item>
            <item key="status">available</item>
           </dt_assoc>
          </item>
          <item key="7">
           <dt_assoc>
            <item key="domain">materialmanufactured.com</item>
            <item key="status">available</item>
           </dt_assoc>
          </item>
          <item key="8">
           <dt_assoc>
            <item key="domain">carbon-manufactured.com</item>
            <item key="status">available</item>
           </dt_assoc>
          </item>
          <item key="9">
           <dt_assoc>
            <item key="domain">material-made.com</item>
            <item key="status">available</item>
           </dt_assoc>
          </item>
          <item key="10">
           <dt_assoc>
            <item key="domain">materialfabricated.com</item>
            <item key="status">available</item>
           </dt_assoc>
          </item>
          <item key="11">
           <dt_assoc>
            <item key="domain">copymanufactured.com</item>
            <item key="status">available</item>
           </dt_assoc>
          </item>
          <item key="12">
           <dt_assoc>
            <item key="domain">carbonmanufactured.org</item>
            <item key="status">available</item>
           </dt_assoc>
          </item>
          <item key="13">
           <dt_assoc>
            <item key="domain">carbonmanufactured.net</item>
            <item key="status">available</item>
           </dt_assoc>
          </item>
          <item key="14">
           <dt_assoc>
            <item key="domain">materialmade.org</item>
            <item key="status">available</item>
           </dt_assoc>
          </item>
          <item key="15">
           <dt_assoc>
            <item key="domain">materialmade.net</item>
            <item key="status">available</item>
           </dt_assoc>
          </item>
          <item key="16">
           <dt_assoc>
            <item key="domain">carbon-fabricated.com</item>
            <item key="status">available</item>
           </dt_assoc>
          </item>
          <item key="17">
           <dt_assoc>
            <item key="domain">copy-made.com</item>
            <item key="status">available</item>
           </dt_assoc>
          </item>
          <item key="18">
           <dt_assoc>
            <item key="domain">carbonfabricated.org</item>
            <item key="status">available</item>
           </dt_assoc>
          </item>
          <item key="19">
           <dt_assoc>
            <item key="domain">carbonfabricated.net</item>
            <item key="status">available</item>
           </dt_assoc>
          </item>
          <item key="20">
           <dt_assoc>
            <item key="domain">copymade.org</item>
            <item key="status">available</item>
           </dt_assoc>
          </item>
          <item key="21">
           <dt_assoc>
            <item key="domain">copymade.net</item>
            <item key="status">available</item>
           </dt_assoc>
          </item>
          <item key="22">
           <dt_assoc>
            <item key="domain">materialcreated.com</item>
            <item key="status">available</item>
           </dt_assoc>
          </item>
          <item key="23">
           <dt_assoc>
            <item key="domain">copyfabricated.com</item>
            <item key="status">available</item>
           </dt_assoc>
          </item>
          <item key="24">
           <dt_assoc>
            <item key="domain">substancemanufactured.com</item>
            <item key="status">available</item>
           </dt_assoc>
          </item>
          <item key="25">
           <dt_assoc>
            <item key="domain">carbon-created.com</item>
            <item key="status">available</item>
           </dt_assoc>
          </item>
          <item key="26">
           <dt_assoc>
            <item key="domain">substance-made.com</item>
            <item key="status">available</item>
           </dt_assoc>
          </item>
          <item key="27">
           <dt_assoc>
            <item key="domain">carboncreated.org</item>
            <item key="status">available</item>
           </dt_assoc>
          </item>
          <item key="28">
           <dt_assoc>
            <item key="domain">carboncreated.net</item>
            <item key="status">available</item>
           </dt_assoc>
          </item>
          <item key="29">
           <dt_assoc>
            <item key="domain">substancemade.org</item>
            <item key="status">available</item>
           </dt_assoc>
          </item>
          <item key="30">
           <dt_assoc>
            <item key="domain">substancemade.net</item>
            <item key="status">available</item>
           </dt_assoc>
          </item>
          <item key="31">
           <dt_assoc>
            <item key="domain">copycreated.com</item>
            <item key="status">available</item>
           </dt_assoc>
          </item>
          <item key="32">
           <dt_assoc>
            <item key="domain">substancefabricated.com</item>
            <item key="status">available</item>
           </dt_assoc>
          </item>
          <item key="33">
           <dt_assoc>
            <item key="domain">duplicatemanufactured.com</item>
            <item key="status">available</item>
           </dt_assoc>
          </item>
          <item key="34">
           <dt_assoc>
            <item key="domain">duplicate-made.com</item>
            <item key="status">available</item>
           </dt_assoc>
          </item>
          <item key="35">
           <dt_assoc>
            <item key="domain">duplicatemade.org</item>
            <item key="status">available</item>
           </dt_assoc>
          </item>
          <item key="36">
           <dt_assoc>
            <item key="domain">duplicatemade.net</item>
            <item key="status">available</item>
           </dt_assoc>
          </item>
          <item key="37">
           <dt_assoc>
            <item key="domain">substancecreated.com</item>
            <item key="status">available</item>
           </dt_assoc>
          </item>
          <item key="38">
           <dt_assoc>
            <item key="domain">elementmanufactured.com</item>
            <item key="status">available</item>
           </dt_assoc>
          </item>
          <item key="39">
           <dt_assoc>
            <item key="domain">element-made.com</item>
            <item key="status">available</item>
           </dt_assoc>
          </item>
          <item key="40">
           <dt_assoc>
            <item key="domain">duplicatefabricated.com</item>
            <item key="status">available</item>
           </dt_assoc>
          </item>
          <item key="41">
           <dt_assoc>
            <item key="domain">elementmade.org</item>
            <item key="status">available</item>
           </dt_assoc>
          </item>
          <item key="42">
           <dt_assoc>
            <item key="domain">elementmade.net</item>
            <item key="status">available</item>
           </dt_assoc>
          </item>
          <item key="43">
           <dt_assoc>
            <item key="domain">duplicatecreated.com</item>
            <item key="status">available</item>
           </dt_assoc>
          </item>
          <item key="44">
           <dt_assoc>
            <item key="domain">elementfabricated.com</item>
            <item key="status">available</item>
           </dt_assoc>
          </item>
          <item key="45">
           <dt_assoc>
            <item key="domain">material-manufactured.com</item>
            <item key="status">available</item>
           </dt_assoc>
          </item>
          <item key="46">
           <dt_assoc>
            <item key="domain">materialmanufactured.org</item>
            <item key="status">available</item>
           </dt_assoc>
          </item>
          <item key="47">
           <dt_assoc>
            <item key="domain">materialmanufactured.net</item>
            <item key="status">available</item>
           </dt_assoc>
          </item>
          <item key="48">
           <dt_assoc>
            <item key="domain">elementcreated.com</item>
            <item key="status">available</item>
           </dt_assoc>
          </item>
          <item key="49">
           <dt_assoc>
            <item key="domain">material-fabricated.com</item>
            <item key="status">available</item>
           </dt_assoc>
          </item>
         </dt_array>
        </item>
       </dt_assoc>
      </item>
     </dt_assoc>
    </item>
   </dt_assoc>
  </data_block>
 </body>
</OPS_envelope>
*/
