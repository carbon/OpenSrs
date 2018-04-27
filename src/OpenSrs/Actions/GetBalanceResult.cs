namespace OpenSrs
{
	public class GetBalanceResult
	{
		public decimal Balance { get; set; }

		public decimal HoldBalance { get; set; }

		public static GetBalanceResult Parse(string responseText)
		{
			var attributes = ResponseHelper.ParseAttributes(responseText);

			return new GetBalanceResult {
				Balance     = decimal.Parse(attributes["balance"]),
				HoldBalance = decimal.Parse(attributes["hold_balance"])
			};
		}
	}
}

/*

Attributes ------------------------------------------------------------------

*balance:			The total amount of money in the requester's account, including the amount that is allocated to pending transactions.

*hold_balance:		The amount of money in the requester's account that is allocated to pending transactions.

*/