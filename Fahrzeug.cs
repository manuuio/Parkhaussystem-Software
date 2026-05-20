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
        private int _meineParkdauerminuten;
        // "_preisProHalbeStunde" wird von der Motorrad (Kind-Klasse) überschrieben, deshalb "protected".
        protected double _preisProHalbeStunde = 2; // Euro

        public Fahrzeug(string meinePlatznummer, int meineParkdauerminuten)
        {
            this._meinePlatznummer = meinePlatznummer;
            this._meineParkdauerminuten = meineParkdauerminuten;
        }

        public string GetMeinePlatznummer()
        {
            return _meinePlatznummer;
        }

        public int GetParkdauerMinuten()
        {
            return _meineParkdauerminuten;
        }

        // Diese Methode ist "virutal" damit Motorrad sie mit "override" überschreiben kann.
        public virtual double BerechneParkkosten()
        {
            return 1.1; // Platzhalter!
        }
    }
}
