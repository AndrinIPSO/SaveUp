namespace SaveUp.Model
{
    public class EintragModel
    {
        /// <summary>
        /// Name des Ersparnises
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Gesparter Betrag in CHF
        /// </summary>
        public float Betrag { get; set; }
        /// <summary>
        /// Datum des Ersparnises
        /// </summary>
        public string Datum { get; set; }
        /// <summary>
        /// ID des Eintrags (wird für delete Benötigt)
        /// </summary>
        public int id { get; set; } = 0;
    }
}
