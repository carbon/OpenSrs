using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OpenSrs
{
    public class OpenSrsClient
    {
        private static readonly Uri productionEndpoint = new Uri("https://rr-n1-tor.opensrs.net:55443");
        private static readonly Uri testingEndpoint = new Uri("https://horizon.opensrs.net:55443");

        private readonly Uri endpoint;
        private readonly string userName;
        private readonly string key;

        private readonly HttpClient http = new HttpClient();

        public OpenSrsClient(string userName, string key, bool testing)
        {
            this.endpoint = testing ? testingEndpoint : productionEndpoint;
            this.userName = userName ?? throw new ArgumentNullException(nameof(userName));
            this.key      = key ?? throw new ArgumentNullException(nameof(key));
        }

		public async Task<GetBalanceResult> GetBalanceAsync(GetBalanceRequest request) =>
            GetBalanceResult.Parse(await SendAsync(request).ConfigureAwait(false));

        public async Task<GetPriceResult> GetPriceAsync(GetPriceRequest request) => 
            GetPriceResult.Parse(await SendAsync(request));

        public async Task<LookupResult> LookupAsync(LookupRequest request) => 
            LookupResult.Parse(await SendAsync(request).ConfigureAwait(false));
        
        public async Task<NameSuggestResult> NameSuggestAsync(NameSuggestRequest request) => 
            NameSuggestResult.Parse(await SendAsync(request).ConfigureAwait(false));

        public async Task<RegisterResult> RegisterAsync(RegisterRequest request) => 
            RegisterResult.Parse(await SendAsync(request).ConfigureAwait(false));

        #region Execution

        public async Task<string> SendAsync(OpenSrsRequest request)
        {
            var sb = new StringBuilder();

            sb.AppendLine(@"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""no""?>");
            sb.AppendLine(@"<!DOCTYPE OPS_envelope SYSTEM ""ops.dtd"">");
            
            sb.Append(request.ToXml().ToString());

            var requestXml = sb.ToString();

            var webRequest = new HttpRequestMessage(HttpMethod.Post, endpoint) {
                Content = new StringContent(requestXml, Encoding.UTF8, "text/xml"),

                Headers = {
                    { "X-Username", userName },
                    { "X-Signature", ComputeSignature(requestXml) },
                    { "Keep-Alive", "false" }
                }
            };

            using (var response = await http.SendAsync(webRequest).ConfigureAwait(false))
            {
                return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            }

            // X-Username: Username
            // X-Signature: MD5 Signature
            // Content-Length: Length of XML Document
        }

        private static string ComputeSignature(string message)
        {
            // md5_hex(md5_hex($xml, $private_key),$private_key)
            return Util.ComputeMD5Hash(Util.ComputeMD5Hash(message + key) + key);
        }

        #endregion
    }
}

// http://domains.opensrs.guide/docs