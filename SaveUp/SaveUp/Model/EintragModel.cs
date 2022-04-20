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
        public DateTime Datum { get; set; }
    }
}
