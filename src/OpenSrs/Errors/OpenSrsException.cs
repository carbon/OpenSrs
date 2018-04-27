using System;
namespace OpenSrs
{
    public class OpenSrsException : Exception
    {
        public OpenSrsException(string message)
            : base(message) { }


        public string ResponseCode { get; set; }
    }

    // https://opensrs.com/docs/integration/Reseller_Agent_Return_Codes.htm
}
