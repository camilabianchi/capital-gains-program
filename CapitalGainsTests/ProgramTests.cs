using CapitalGainsProgram;

namespace CapitalGainsTests
{
    public class Program_Tests
    {

        [Theory]
        [InlineData("..\\..\\..\\Files\\EmptyFile.txt", "")]
        [InlineData("..\\..\\..\\Files\\OperationProfit.txt", "[{\"tax\":\"0.00\"},{\"tax\":\"10000.00\"}]\r\n")]
        [InlineData("..\\..\\..\\Files\\OperationProfitAndLoss.txt", "[{\"tax\":\"0.00\"},{\"tax\":\"0.00\"},{\"tax\":\"1000.00\"}]\r\n")]
        public void Main_CalculateTaxes(string fileName, string expectedResult)
        {
            // arrange
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            // act
            string[] args = { fileName };
            Program.Main(args);

            // assert
            var output = stringWriter.ToString();

            Assert.Equal(expectedResult, output);
        }
    }
}