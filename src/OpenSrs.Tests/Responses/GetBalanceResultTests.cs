using Xunit;

namespace OpenSrs.Tests
{
    public class GetBalanceResultTests
    {
        [Fact]
        public void A()
        {
            var text = @"<?xml version='1.0' encoding='UTF-8' standalone='no'?>
<!DOCTYPE OPS_envelope SYSTEM 'ops.dtd'>
<OPS_envelope>
    <header>
        <version>0.9</version>
    </header>
    <body>
        <data_block>
            <dt_assoc>
                <item key=""protocol"">XCP</item>
                <item key=""action"">REPLY</item>
                <item key=""object"">BALANCE</item>
                <item key=""is_success"">1</item>
                <item key=""response_code"">200</item>
                <item key=""response_text"">Command successful</item>
                <item key=""attributes"">
                    <dt_assoc>
                        <item key=""balance"">8549.18</item>
                        <item key=""hold_balance"">1676.05</item>
                    </dt_assoc>
                </item>
            </dt_assoc>
        </data_block>
    </body>
</OPS_envelope>";


            var x = GetBalanceResult.Parse(text);

            Assert.Equal(8549.18m, x.Balance);
            Assert.Equal(1676.05m, x.HoldBalance);

        }


        [Fact]
        public void B()
        {
            var result = GetBalanceResult.Parse(
@"<?xml version='1.0' encoding=""UTF-8"" standalone=""no"" ?>
<!DOCTYPE OPS_envelope SYSTEM ""ops.dtd"">
<OPS_envelope>
	<header>
	<version>0.9</version>
	</header>
	<body>
	<data_block>
	<dt_assoc>
	<item key=""protocol"">XCP</item>
	<item key=""object"">BALANCE</item>
	<item key=""response_text"">Command successful</item>
	<item key=""action"">REPLY</item>
	<item key=""attributes"">
		<dt_assoc>
		<item key=""balance"">5000</item>
		<item key=""hold_balance"">0.00</item>
		</dt_assoc>
	</item>
	<item key=""response_code"">200</item>
	<item key=""is_success"">1</item>
	</dt_assoc>
	</data_block>
	</body>
</OPS_envelope>");

            Assert.Equal(5000m, result.Balance);
            Assert.Equal(0m, result.HoldBalance);
        }

    }
}
