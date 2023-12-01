using CapitalGainsProgram.Models;
using System.Globalization;
using System.Text;
using System.Text.Json;

namespace CapitalGainsProgram
{
    public static class Functions
    {
        /// <summary>
        /// Reads a file and return it's lines as string
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static List<string> ReadFile(string fileName)
        {
            List<string> lines = new();
            try
            {
                using (FileStream fileStream = File.OpenRead(fileName))
                using (StreamReader streamReader = new(fileStream, Encoding.UTF8, true, 256))
                {
                    string? line;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        lines.Add(line);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("File error: {0}", ex.Message));
            }

            return lines;
        }

        /// <summary>
        /// Convert a json string into an Operations object
        /// </summary>
        /// <param name="operations"></param>
        /// <returns></returns>
        public static List<Operations>? ConvertOperationsInput(string operations)
        {
            return !string.IsNullOrEmpty(operations) ?
                JsonSerializer.Deserialize<List<Operations>>(operations) :
                new List<Operations>();
        }

        /// <summary>
        /// Convert list of Taxes object into a serialized string
        /// </summary>
        /// <param name="taxes"></param>
        /// <returns></returns>
        public static string ConvertTaxesOutput(List<Taxes> taxes)
        {
            return JsonSerializer.Serialize(taxes);
        }

        /// <summary>
        /// Add tax to a list of taxes
        /// </summary>
        /// <param name="tax">Tax due</param>
        /// <param name="weightedAverage">Weighted average for the current operation</param>
        /// <param name="shareCount">Share count after operation</param>
        /// <param name="cumulatedLoss">Cumulated loss after operation</param>
        /// <returns></returns>
        public static Taxes AddTax(decimal tax, decimal weightedAverage, int shareCount, decimal cumulatedLoss)
        {
            return new Taxes
            {
                Tax = Math.Round(tax, 2).ToString("0.00", CultureInfo.InvariantCulture),
                CurrentWeightedAverage = weightedAverage,
                CurrentShareCount = shareCount,
                CumulatedLoss = Math.Round(cumulatedLoss, 2)
            };
        }

        /// <summary>
        /// Validates if is the first operation being processed
        /// </summary>
        /// <param name="taxesCount"></param>
        /// <returns></returns>
        public static bool IsFirstOperation(int taxesCount)
        {
            return taxesCount == 0;
        }

        /// <summary>
        /// Validates if it's a buy operation
        /// </summary>
        /// <param name="operation"></param>
        /// <returns></returns>
        public static bool IsBuyOperation(string operation)
        {
            return operation == "buy";
        }

        /// <summary>
        /// Validates if the operation resulted in a loss
        /// </summary>
        /// <param name="unitCost"></param>
        /// <param name="currentWeightedAverage"></param>
        /// <returns></returns>
        public static bool IsLoss(decimal unitCost, decimal currentWeightedAverage)
        {
            return unitCost < currentWeightedAverage;
        }

        /// <summary>
        /// Validates if the operation resulted in a profit
        /// </summary>
        /// <param name="sellAmountCurrentOperation"></param>
        /// <returns></returns>
        public static bool IsProfit(decimal sellAmountCurrentOperation)
        {
            return sellAmountCurrentOperation >= 20000;
        }

        /// <summary>
        /// Validates if the cumulated loss is greater than or equal to the current profit
        /// </summary>
        /// <param name="cumulatedLoss"></param>
        /// <param name="profit"></param>
        /// <returns></returns>
        public static bool IsCumulatedLossGreaterOrEqualToProfit(decimal cumulatedLoss, decimal profit)
        {
            return cumulatedLoss >= profit;
        }

        /// <summary>
        /// Validates if the current operation should deduct loss
        /// </summary>
        /// <param name="cumulatedLoss"></param>
        /// <returns></returns>
        public static bool ShouldDeductLoss(decimal cumulatedLoss)
        {
            return cumulatedLoss > 0;
        }

        /// <summary>
        /// Get the profit to be taxed
        /// </summary>
        /// <param name="weightedAverage">Current weighted average</param>
        /// <param name="quantity">Quantity of shares</param>
        /// <param name="sellAmountCurrentOperation">Sell amount in the current operation</param>
        /// <returns></returns>
        public static decimal GetProfitToBeTaxed(decimal weightedAverage, int quantity, decimal sellAmountCurrentOperation)
        {
            var sellAmountWeightedAverage = weightedAverage * quantity;
            return Math.Round(sellAmountCurrentOperation - sellAmountWeightedAverage, 2);
        }

        /// <summary>
        /// Calculate the weighted average for the current operation
        /// </summary>
        /// <param name="currentShareCount">Current quantity of shares</param>
        /// <param name="currentWeightedAverage">Current weighted average</param>
        /// <param name="quantity">Quantity of shares in the operation</param>
        /// <param name="unitCost">Unit cost of the share in the operation</param>
        /// <returns></returns>
        public static decimal CalculateWeightedAverage(decimal currentShareCount, decimal currentWeightedAverage, int quantity, decimal unitCost)
        {
            return Math.Round(((currentShareCount * currentWeightedAverage) +
                    (quantity * unitCost)) /
                    (currentShareCount + quantity), 2);
        }

        /// <summary>
        /// Calculate tax due based on the profit
        /// </summary>
        /// <param name="valueToCalculateTax"></param>
        /// <returns></returns>
        public static decimal CalculateTax(decimal valueToCalculateTax)
        {
            return Math.Round(((valueToCalculateTax * 20) / 100), 2);
        }
    }
}
