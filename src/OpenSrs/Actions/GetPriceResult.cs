﻿using System.Globalization;

namespace OpenSrs
{
	public sealed class GetPriceResult
	{
        public GetPriceResult(decimal price)
        {
            Price = price;
        }

		public decimal Price { get; }

		public static GetPriceResult Parse(string text)
		{
			var attributes = ResponseHelper.ParseAttributes(text);

            return new GetPriceResult(
                price: decimal.Parse(attributes["price"], CultureInfo.InvariantCulture)
            );
		}
	}
}

/*

Attributes ------------------------------------------------------------------

*price:			The price of the queried domain. This value includes the OpenSRS price and the ICANN fee.

*/