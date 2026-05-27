using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Parkhaussystem_Software
{
    // Motorrad erbt alle Felder und Methoden von Fahrzeug.
    // Der alleinige Unterschied ist der günstigere Preis pro 30 Minuten.
    public class Motorrad : Fahrzeug
    {

        public Motorrad(string meinePlatznummer) : base(meinePlatznummer)
        {
            // Motorräder zahlen weniger da sie weniger Platz belegen.
            this._preisProHalbeStunde = 100; // Cent
        }

        // override damit bei einem Motorrad-Objekt automatisch der günstigere Preis verwendet wird.
        public override double BerechneParkkosten()
        {
            double parkkostenBloecke = (double)GetParkdauerMinuten() / 30.0;
            double endpreis = Math.Ceiling(parkkostenBloecke) * _preisProHalbeStunde;
            return endpreis;
        }

    }
}
