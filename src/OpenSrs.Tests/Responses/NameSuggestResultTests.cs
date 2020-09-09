using Xunit;

namespace OpenSrs.Tests
{
    public class GetSuggestionsResponseTests
	{
		[Fact]
		public void ParseResponse2()
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
    <item key=""response_code"">200</item>
    <item key=""is_success"">1</item>
    <item key=""request_response_time"">0.423</item>
    <item key=""response_text"">Command completed successfully</item>
    <item key=""is_search_completed"">1</item>
    <item key=""action"">REPLY</item>
    <item key=""attributes"">
     <dt_assoc>
      <item key=""lookup"">
       <dt_assoc>
        <item key=""count"">4</item>
        <item key=""response_text"">Command completed successfully.</item>
        <item key=""response_code"">200</item>
        <item key=""items"">
         <dt_array>
          <item key=""0"">
           <dt_assoc>
            <item key=""domain"">a.com</item>
            <item key=""status"">available</item>
           </dt_assoc>
          </item>
          <item key=""1"">
           <dt_assoc>
            <item key=""domain"">b.net</item>
            <item key=""status"">available</item>
           </dt_assoc>
          </item>
          <item key=""2"">
           <dt_assoc>
            <item key=""domain"">c.org</item>
            <item key=""status"">available</item>
           </dt_assoc>
          </item>
          <item key=""3"">
           <dt_assoc>
            <item key=""domain"">d.tv</item>
            <item key=""status"">available</item>
           </dt_assoc>
          </item>
         </dt_array>
        </item>
        <item key=""is_success"">1</item>
       </dt_assoc>
      </item>
      <item key=""suggestion"">
       <dt_assoc>
        <item key=""count"">50</item>
        <item key=""response_text"">Command Successful</item>
        <item key=""response_code"">200</item>
        <item key=""is_success"">1</item>
        <item key=""items"">
         <dt_array>
          <item key=""0"">
           <dt_assoc>
            <item key=""domain"">a.org</item>
            <item key=""status"">available</item>
           </dt_assoc>
          </item>
          <item key=""1"">
           <dt_assoc>
            <item key=""domain"">b.net</item>
            <item key=""status"">available</item>
           </dt_assoc>
          </item>
          <item key=""2"">
           <dt_assoc>
            <item key=""domain"">c.com</item>
            <item key=""status"">available</item>
           </dt_assoc>
          </item>
          <item key=""3"">
           <dt_assoc>
            <item key=""domain"">d.com</item>
            <item key=""status"">available</item>
           </dt_assoc>
          </item>
          <item key=""4"">
           <dt_assoc>
            <item key=""domain"">e.com</item>
            <item key=""status"">available</item>
           </dt_assoc>
          </item>
          <item key=""5"">
           <dt_assoc>
            <item key=""domain"">f.com</item>
            <item key=""status"">available</item>
           </dt_assoc>
          </item>
          <item key=""6"">
           <dt_assoc>
            <item key=""domain"">g.com</item>
            <item key=""status"">available</item>
           </dt_assoc>
          </item>
          <item key=""7"">
           <dt_assoc>
            <item key=""domain"">h.com</item>
            <item key=""status"">available</item>
           </dt_assoc>
          </item>
          <item key=""8"">
           <dt_assoc>
            <item key=""domain"">i.com</item>
            <item key=""status"">available</item>
           </dt_assoc>
          </item>
          <item key=""9"">
           <dt_assoc>
            <item key=""domain"">material-made.com</item>
            <item key=""status"">available</item>
           </dt_assoc>
          </item>
          <item key=""10"">
           <dt_assoc>
            <item key=""domain"">materialfabricated.com</item>
            <item key=""status"">available</item>
           </dt_assoc>
          </item>
          <item key=""11"">
           <dt_assoc>
            <item key=""domain"">copymanufactured.com</item>
            <item key=""status"">available</item>
           </dt_assoc>
          </item>
          <item key=""12"">
           <dt_assoc>
            <item key=""domain"">carbonmanufactured.org</item>
            <item key=""status"">available</item>
           </dt_assoc>
          </item>
          <item key=""13"">
           <dt_assoc>
            <item key=""domain"">carbonmanufactured.net</item>
            <item key=""status"">available</item>
           </dt_assoc>
          </item>
          <item key=""14"">
           <dt_assoc>
            <item key=""domain"">materialmade.org</item>
            <item key=""status"">available</item>
           </dt_assoc>
          </item>
          <item key=""15"">
           <dt_assoc>
            <item key=""domain"">materialmade.net</item>
            <item key=""status"">available</item>
           </dt_assoc>
          </item>
          <item key=""16"">
           <dt_assoc>
            <item key=""domain"">carbon-fabricated.com</item>
            <item key=""status"">available</item>
           </dt_assoc>
          </item>
          <item key=""17"">
           <dt_assoc>
            <item key=""domain"">copy-made.com</item>
            <item key=""status"">available</item>
           </dt_assoc>
          </item>
          <item key=""18"">
           <dt_assoc>
            <item key=""domain"">carbonfabricated.org</item>
            <item key=""status"">available</item>
           </dt_assoc>
          </item>
          <item key=""19"">
           <dt_assoc>
            <item key=""domain"">carbonfabricated.net</item>
            <item key=""status"">available</item>
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
</OPS_envelope>";


			var x = NameSuggestResult.Parse(text);

			Assert.Equal(4, x.Lookup.Count);
			Assert.Equal("a.com", x.Lookup[0].Domain);
			Assert.Equal("available", x.Lookup[0].Status);

			Assert.Equal(20, x.Suggestions.Count);

			Assert.Equal("a.org", x.Suggestions[0].Domain);
			Assert.Equal("available", x.Suggestions[0].Status);
		}
	}
}