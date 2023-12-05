using CapitalGainsProgram.Models;

namespace CapitalGainsProgram
{
    public static class Processor
    {
        public static string ProcessOperation(string line)
        {
            if (string.IsNullOrEmpty(line)) return string.Empty;

            var operations = Functions.ConvertOperationsInput(line);

            var taxes = new List<Taxes>();

            for (var count = 0; count < operations?.Count; count++)
            {
                var currentOperation = operations[count];

                if (Functions.IsFirstOperation(taxes.Count))
                {
                    taxes.Add(Functions.AddTax(0, currentOperation.UnitCost, currentOperation.Quantity, 0));
                    
                    continue;
                }

                var previousOperationResult = taxes[count - 1];
                Taxes tax;

                if (Functions.IsBuyOperation(currentOperation.Operation))
                {
                    tax = ProcessBuyOperation(currentOperation, previousOperationResult);

                    taxes.Add(tax);
                        
                    continue;
                }
                 
                tax = ProcessSellOperation(currentOperation, previousOperationResult);

                taxes.Add(tax);
            }

            return Functions.ConvertTaxesOutput(taxes);
        }

        private static Taxes ProcessBuyOperation(Operations currentOperation, Taxes previousOperationResult)
        {
            var weightedAverage = currentOperation.UnitCost;
            if (previousOperationResult.CurrentShareCount > 0)
            {
                weightedAverage = Functions.CalculateWeightedAverage(
                    previousOperationResult.CurrentShareCount,
                    previousOperationResult.CurrentWeightedAverage,
                    currentOperation.Quantity,
                    currentOperation.UnitCost);
            }

            return Functions.AddTax(0, 
                weightedAverage, 
                previousOperationResult.CurrentShareCount + currentOperation.Quantity, 
                previousOperationResult.CumulatedLoss);
        }

        private static Taxes ProcessSellOperation(Operations currentOperation, Taxes previousOperationResult) 
        {
            var sellAmountCurrentOperation = currentOperation.UnitCost * currentOperation.Quantity;

            if (Functions.IsLoss(currentOperation.UnitCost, previousOperationResult.CurrentWeightedAverage))
            {
                var sellAmountWeightedAverage = previousOperationResult.CurrentWeightedAverage * currentOperation.Quantity;
                var loss = sellAmountWeightedAverage - sellAmountCurrentOperation;

                return Functions.AddTax(0, 
                    previousOperationResult.CurrentWeightedAverage, 
                    previousOperationResult.CurrentShareCount - currentOperation.Quantity, 
                    previousOperationResult.CumulatedLoss + loss);
            }

            decimal tax = 0;
            var cumulatedLoss = Math.Round(previousOperationResult.CumulatedLoss, 2);

            if (Functions.IsProfit(sellAmountCurrentOperation))
            {
                var profit = Functions.GetProfitToBeTaxed(
                    previousOperationResult.CurrentWeightedAverage,
                    currentOperation.Quantity,
                    sellAmountCurrentOperation);

                if (Functions.ShouldDeductLoss(cumulatedLoss))
                {
                    if (Functions.IsCumulatedLossGreaterOrEqualToProfit(cumulatedLoss, profit))
                    {
                        cumulatedLoss -= profit;
                    }
                    else
                    {
                        profit -= cumulatedLoss;
                        tax = Functions.CalculateTax(profit);
                    }
                }
                else
                {
                    tax = Functions.CalculateTax(profit);
                }
            }

            return Functions.AddTax(tax, 
                previousOperationResult.CurrentWeightedAverage, 
                previousOperationResult.CurrentShareCount - currentOperation.Quantity, 
                cumulatedLoss);
        }
    }
}
