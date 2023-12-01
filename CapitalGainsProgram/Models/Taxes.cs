using System.Text.Json.Serialization;

namespace CapitalGainsProgram.Models
{
    [Serializable]
    public class Taxes
    {
        public Taxes()
        {
            Tax = string.Empty;
        }

        /// <summary>
        /// Tax due
        /// </summary>
        [JsonPropertyName("tax")]
        public string Tax { get; set; }

        /// <summary>
        /// Total of current share to be used as an auxiliar variable
        /// Not displayed in console output
        /// </summary>
        [JsonIgnore]
        public int CurrentShareCount { get; set; }

        /// <summary>
        /// Current weighted average per share to be used as an auxiliar variable
        /// Not displayed in console output
        /// </summary>
        [JsonIgnore]
        public decimal CurrentWeightedAverage { get; set; }

        /// <summary>
        /// Cumulated loss from previous operations to be used as an auxiliar variable
        /// Not displayed in console output
        /// </summary>
        [JsonIgnore]
        public decimal CumulatedLoss { get; set; }
    }
}
