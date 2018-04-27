using System.Xml.Linq;

namespace OpenSrs
{
    public class ResponseDetails
    {
        public string Protocol { get; set; }

        public string Object { get; set; }

        public string ResponseText { get; set; }

        public string Action { get; set; }

        public string ResponseCode { get; set; }

        public bool IsSuccess { get; set; }

        public static ResponseDetails FromEl(XElement el)
        {
            var dic = ResponseHelper.ReadAssocAsDic(el);

            return new ResponseDetails
            {
                Protocol = dic["protocol"],
                Object = dic["object"],
                Action = dic["action"],
                ResponseCode = dic["response_code"],
                ResponseText = dic["response_text"],
                IsSuccess = dic["is_success"] == "1"
            };
        }
    }
}

/*
<dt_assoc>
  <item key="protocol">XCP</item>
  <item key="object">DOMAIN</item>
  <item key="response_text">DNS template specified does not exist.  (portfolio)</item>
  <item key="action">REPLY</item>
  <item key="response_code">478</item>
  <item key="is_success">0</item>
</dt_assoc>*/
