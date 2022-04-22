using System;
using System.Collections.Generic;
using System.Text;

namespace SaveUp.Model
{
    public class EintragModel
    {
        public string Name { get; set; }
        public float Betrag { get; set; }
        public string Beschreibung { get; set; }
        public string Datum { get; set; }
        public int id { get; set; } = 0;
    }
}
