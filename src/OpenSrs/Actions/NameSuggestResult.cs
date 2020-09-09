using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Xml.XPath;

namespace OpenSrs
{
    public sealed class NameSuggestResult
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

                XElement responseTime = doc.XPathSelectElement(@"//item[@key=""request_response_time""]");
                XElement lookupArray = doc.XPathSelectElement(@"//item[@key=""lookup""]/dt_assoc/item[@key=""items""]/dt_array");
                XElement suggestionArray = doc.XPathSelectElement(@"//item[@key=""suggestion""]/dt_assoc/item[@key=""items""]/dt_array");

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
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "/ " + text);
            }
        }
    }

    public sealed class DomainItem
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