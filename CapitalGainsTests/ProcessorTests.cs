using CapitalGainsProgram;

namespace CapitalGainsTests
{
    public class Processor_Tests
    {

        [Theory]
        [InlineData("", "")]
        [InlineData("[{\"operation\":\"buy\", \"unit-cost\":10.00, \"quantity\": 10000},{\"operation\":\"buy\", \"unit-cost\":25.00, \"quantity\": 5000},{\"operation\":\"sell\", \"unit-cost\":15.00, \"quantity\": 10000}]", "[{\"tax\":\"0.00\"},{\"tax\":\"0.00\"},{\"tax\":\"0.00\"}]")]
        [InlineData("[{\"operation\":\"buy\", \"unit-cost\":10.00, \"quantity\": 10000},{\"operation\":\"sell\", \"unit-cost\":2.00, \"quantity\": 5000},{\"operation\":\"sell\", \"unit-cost\":20.00, \"quantity\": 2000},{\"operation\":\"sell\", \"unit-cost\":20.00, \"quantity\": 2000},{\"operation\":\"sell\", \"unit-cost\":25.00, \"quantity\": 1000},{\"operation\":\"buy\", \"unit-cost\":20.00, \"quantity\": 10000},{\"operation\":\"sell\", \"unit-cost\":15.00, \"quantity\": 5000},{\"operation\":\"sell\", \"unit-cost\":30.00, \"quantity\": 4350},{\"operation\":\"sell\", \"unit-cost\":30.00, \"quantity\": 650}]\r\n", "[{\"tax\":\"0.00\"},{\"tax\":\"0.00\"},{\"tax\":\"0.00\"},{\"tax\":\"0.00\"},{\"tax\":\"3000.00\"},{\"tax\":\"0.00\"},{\"tax\":\"0.00\"},{\"tax\":\"3700.00\"},{\"tax\":\"0.00\"}]")]
        [InlineData("[{\"operation\":\"buy\", \"unit-cost\":10.00, \"quantity\": 10000},{\"operation\":\"sell\", \"unit-cost\":20.00, \"quantity\": 5000}]", "[{\"tax\":\"0.00\"},{\"tax\":\"10000.00\"}]")]
        public void ProcessOperation_CalculateTaxes(string operation, string expectedResult)
        {
            // act
            var result = Processor.ProcessOperation(operation);

            // assert
            Assert.Equal(expectedResult, result);
        }
    }
}