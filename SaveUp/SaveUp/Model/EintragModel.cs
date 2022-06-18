using Newtonsoft.Json;

namespace SaveUp.Model
{
    public class EintragModel
    {
        /// <summary>
        /// Name des Ersparnises
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }
        /// <summary>
        /// Gesparter Betrag in CHF
        /// </summary>
        [JsonProperty("betrag")]
        public float Betrag { get; set; }
        /// <summary>
        /// Datum des Ersparnises
        /// </summary>
        [JsonProperty("datum")]
        public string Datum { get; set; }
        /// <summary>
        /// ID des Eintrags (wird für delete Benötigt)
        /// </summary>
        [JsonProperty("id")]
        public int id { get; set; } = 0;
    }
}
