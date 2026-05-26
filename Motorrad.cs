using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parkhaussystem_Software
{
    public class Motorrad : Fahrzeug
    {
        // Keine extra festgelegten Variablen da alles was wir brauchen von Fahrzeug geerbt wird.


        public Motorrad(string meinePlatznummer) : base(meinePlatznummer)
        {
            // Tag 5 - Änderung: Ich habe den Preis aufgrund von Sicherheit vor Rundungsfehlern etc. von einem Euro auf einen Cent Wert verändert 
            this._preisProHalbeStunde = 100; // Cent
        }

        // Wir überschreiben die "virtual" Methode aus der Basisklasse "Fahrzeug".
        public override double BerechneParkkosten()
        {
            double parkkostenBloecke = (double)GetParkdauerMinuten() / 30.0;
            double endpreis = Math.Ceiling(parkkostenBloecke) * _preisProHalbeStunde;
            return endpreis;
        }

    }
}
