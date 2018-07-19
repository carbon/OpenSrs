namespace OpenSrs
{
    public sealed class GetBalanceRequest : OpenSrsRequest
    {
        public GetBalanceRequest() : base("GET_BALANCE", "BALANCE") { }
    }
}