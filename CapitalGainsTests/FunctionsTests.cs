using CapitalGainsProgram;
using CapitalGainsProgram.Models;

namespace CapitalGainsTests
{
    public class Functions_Tests
    {
        [Fact]
        public void ReadFile_ReturnEmptyList_WhenFileDoesNotExists()
        {
            // arrange
            string fileName = ".\\Invalid.txt";

            // act
            var lines = Functions.ReadFile(fileName);

            // assert
            Assert.Empty(lines);
        }


        [Fact]
        public void ReadFile_ReturnEmptyList_WhenFileIsEmpty()
        {
            // arrange
            string fileName = "..\\..\\..\\Files\\EmptyFile.txt";

            // act
            var lines = Functions.ReadFile(fileName);

            // assert
            Assert.Empty(lines);
        }


        [Fact]
        public void ReadFile_ReturnListWithOperations_WhenFileHasLines()
        {
            // arrange
            string fileName = "..\\..\\..\\Files\\Operations.txt";

            // act
            var lines = Functions.ReadFile(fileName);

            // assert
            Assert.NotEmpty(lines);
            Assert.Equal(9, lines.Count);
        }

        [Fact]
        public void ConvertOperationsInput_ReturnsEmptyList_WhenOperationsNull()
        {
            // act
            var operations = Functions.ConvertOperationsInput("");

            // assert
            Assert.Empty(operations);
        }

        [Fact]
        public void ConvertOperationsInput_ReturnListWithOperations_WhenOperationsNotNull()
        {
            // act
            var operations = Functions.ConvertOperationsInput("[{\"operation\":\"buy\", \"unit-cost\":20.00, \"quantity\": 10000},{\"operation\":\"buy\", \"unit-cost\":10, \"quantity\": 5000},{\"operation\":\"sell\", \"unit-cost\":16.68, \"quantity\": 15000}]");

            // assert
            Assert.NotEmpty(operations);
        }

        [Fact]
        public void ConvertTaxesOutput_ReturnsEmptyString_WhenListIsEmpty()
        {
            // act
            var serializedTaxes = Functions.ConvertTaxesOutput(new List<Taxes>());

            // assert
            Assert.Equal("[]", serializedTaxes);
        }

        [Fact]
        public void ConvertTaxesOutput_ReturnsSerializedTaxes_WhenListNotEmpty()
        {
            // act
            var taxes = new List<Taxes>
            {
                new Taxes { Tax = "5.00", CumulatedLoss = 0, CurrentShareCount = 20, CurrentWeightedAverage = 5 },
                new Taxes { Tax = "10.00", CumulatedLoss = 0, CurrentShareCount = 5, CurrentWeightedAverage = 5 },
            };

            var serializedTaxes = Functions.ConvertTaxesOutput(taxes);

            // assert
            Assert.Equal("[{\"tax\":\"5.00\"},{\"tax\":\"10.00\"}]", serializedTaxes);
        }

        [Fact]
        public void AddTax_ReturnsTax_WhenAddingTax()
        {
            // act
            var expectedValue = new Taxes
            {
                Tax = "500.00",
                CurrentShareCount = 200,
                CurrentWeightedAverage = 10,
                CumulatedLoss = 0
            };

            var tax = Functions.AddTax(500, 10, 200, 0);

            // assert
            Assert.Equivalent(expectedValue, tax);
        }

        [Theory]
        [InlineData(0, true)]
        [InlineData(1, false)]
        public void IsFirstOperation_ReturnsTrueOrFalse_WhileValidation(int listCount, bool expectedValue)
        {
            // act
            var result = Functions.IsFirstOperation(listCount);

            // assert
            Assert.Equal(expectedValue, result);
        }

        [Theory]
        [InlineData("buy", true)]
        [InlineData("sell", false)]
        public void IsBuyOperation_ReturnsTrueOrFalse_WhileValidation(string operation, bool expectedValue)
        {
            // act
            var result = Functions.IsBuyOperation(operation);

            // assert
            Assert.Equal(expectedValue, result);
        }

        [Theory]
        [InlineData(5, 9, true)]
        [InlineData(5, 4, false)]
        public void IsLoss_ReturnsTrueOrFalse_WhileValidation(
            decimal unitPrice, decimal weightedAverage, bool expectedValue)
        {
            // act
            var result = Functions.IsLoss(unitPrice, weightedAverage);

            // assert
            Assert.Equal(expectedValue, result);
        }

        [Theory]
        [InlineData(25000, true)]
        [InlineData(10000, false)]
        public void IsProfit_ReturnsTrueOrFalse_WhileValidation(decimal sellAmount, bool expectedValue)
        {
            // act
            var result = Functions.IsProfit(sellAmount);

            // assert
            Assert.Equal(expectedValue, result);
        }

        [Theory]
        [InlineData(20000, 10000, true)]
        [InlineData(20000, 40000, false)]
        public void IsCumulatedLossGreaterOrEqualToProfit_ReturnsTrueOrFalse_WhileValidation(
            decimal cumulatedLoss, decimal profit, bool expectedValue)
        {
            // act
            var result = Functions.IsCumulatedLossGreaterOrEqualToProfit(cumulatedLoss, profit);

            // assert
            Assert.Equal(expectedValue, result);
        }

        [Theory]
        [InlineData(20000, true)]
        [InlineData(0, false)]
        public void ShouldDeductLoss_ReturnsTrueOrFalse_WhileValidation(decimal cumulatedLoss, bool expectedValue)
        {
            // act
            var result = Functions.ShouldDeductLoss(cumulatedLoss);

            // assert
            Assert.Equal(expectedValue, result);
        }

        [Theory]
        [InlineData(10, 5000, 100000, 50000)]
        [InlineData(10, 5000, 150000, 100000)]
        public void GetProfitToBeTaxed_ReturnsValueToBeTaxed_WhenProfitExists(
            decimal weightedAverage, int quantity, decimal sellAmountCurrentOperation, decimal expectedValue)
        {
            // act
            var result = Functions.GetProfitToBeTaxed(weightedAverage, quantity, sellAmountCurrentOperation);

            // assert
            Assert.Equal(expectedValue, result);
        }

        [Theory]
        [InlineData(10000, 20, 5000, 10, 16.67)]
        [InlineData(5, 20, 5, 10, 15)]
        public void CalculateWeightedAverage_ReturnsValueToBeTaxed_WhenProfitExists(
            decimal currentShareCount, decimal currentWeightedAverage, int quantity, decimal unitCost, decimal expectedValue)
        {
            // act
            var result = Functions.CalculateWeightedAverage(currentShareCount, currentWeightedAverage, quantity, unitCost);

            // assert
            Assert.Equal(expectedValue, result);
        }

        [Theory]
        [InlineData(1000, 200)]
        [InlineData(5265, 1053)]
        public void CalculateTax_ReturnsTaxValue_WhenProfitExists(decimal valueToCalculateTax, decimal expectedValue)
        {
            // act
            var result = Functions.CalculateTax(valueToCalculateTax);

            // assert
            Assert.Equal(expectedValue, result);
        }
    }
}