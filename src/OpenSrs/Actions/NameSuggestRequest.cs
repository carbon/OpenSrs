using System;
using System.Collections.Generic;

namespace OpenSrs
{
    public sealed class NameSuggestRequest : OpenSrsRequest
    {
        public NameSuggestRequest()
            : base("NAME_SUGGEST", "DOMAIN")
        {
            MaxWaitTime = TimeSpan.FromSeconds(5);
        }

        public string Query { get; set; }

        public TimeSpan MaxWaitTime { get; set; }

        public bool? NoCacheTlds { get; set; }

        /// <summary>
        /// The TLDs you want to check for domain name availability and suggestions.
        /// Lookups are available for all gTLDs and ccTLDs.
        /// [.com,.net,.org,.info,.biz,.us,.mobi]
        /// </summary>
        public string[] Tlds { get; set; }

        public override Dictionary<string, object> GetParameters()
        {
            var dic = new Dictionary<string, object>(4) {
                { "max_wait_time",  MaxWaitTime.TotalSeconds },
                { "searchstring",   Query },
                { "tlds",           Util.ToDtArray(Tlds) }
            };

            if (NoCacheTlds == true)
            {
                dic["no_cache_tlds"] = "1";
            }

            return dic;
        }
    }
}