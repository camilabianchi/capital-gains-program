using System.Text.Json.Serialization;

namespace CapitalGainsProgram.Models
{
    [Serializable]
    public class Operations
    {
        public Operations()
        {
            Operation = string.Empty;
            UnitCost = 0;
            Quantity = 0;
        }

        /// <summary>
        /// Operation buy or sell
        /// </summary>
        [JsonPropertyName("operation")]
        public string Operation { get; set; }

        /// <summary>
        /// Price of the share
        /// </summary>
        [JsonPropertyName("unit-cost")]
        public decimal UnitCost { get; set; }

        /// <summary>
        /// Quantity of shares
        /// </summary>
        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }
    }
}
