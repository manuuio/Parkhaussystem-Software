using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parkhaussystem_Software
{
    public class Fahrzeug
    {
        private string _meinePlatznummer;
        private int _meineParkdauerMinuten = 0;

        // protected damit Motorrad als Kindklasse den Preis im eigenen Konstruktor überschreiben kann.
        // Cent statt Euro um Rundungsfehler bei der Berechnung zu vermeiden.
        protected double _preisProHalbeStunde = 200; // Cent

        public Fahrzeug(string meinePlatznummer)
        {
            this._meinePlatznummer = meinePlatznummer;
        }

        public double GetMeinPreisProHalbeStunde()
        {
            return _preisProHalbeStunde;
        }

        public string GetMeinePlatznummer()
        {
            return _meinePlatznummer;
        }

        public int GetParkdauerMinuten()
        {
            return _meineParkdauerMinuten;
        }

        public void SetParkdauerMinuten()
        {
            Random rndMinuten = new Random();
            // Mindestwert 30 damit kein Fahrzeug 0 Minuten parkt und nichts bezahlt.
            // maxValue bei Next() ist nicht inklusive, deshalb +1.
            _meineParkdauerMinuten = rndMinuten.Next(30, 180 + 1);
        }

        // virtual damit Motorrad die Kostenberechnung mit einem anderen Preis überschreiben kann.
        public virtual double BerechneParkkosten()
        {
            // Cast zu double nötig damit die Division Nachkommastellen behält.
            // Math.Ceiling rundet auf den nächsten vollen 30-Minuten-Block auf (angefangene Blöcke werden voll berechnet).
            double parkkostenBloecke = (double)_meineParkdauerMinuten / 30.0;
            double endpreis = Math.Ceiling(parkkostenBloecke) * _preisProHalbeStunde;
            return endpreis;
        }
    }
}
