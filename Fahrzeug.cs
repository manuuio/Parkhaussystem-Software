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

        // Ich setze hier auf "0" da ich davon ausgehe dass der Fahrer gerade erst eingefahren ist,
        // bis dieser wert von SetParkdauerMinuten() überschrieben wird.
        private int _meineParkdauerMinuten = 0;

        // "_preisProHalbeStunde" wird von der Motorrad (Kind-Klasse) überschrieben, deshalb "protected".
        protected double _preisProHalbeStunde = 2; // Euro

        public Fahrzeug(string meinePlatznummer)
        {
            this._meinePlatznummer = meinePlatznummer;
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
            //Da die angegebene 180 (Minuten) im Parameter nicht inklusive ist, steht am Ende das "+1".
            _meineParkdauerMinuten = rndMinuten.Next(30, 180 + 1);
        }

        // Diese Methode ist "virutal" damit Motorrad sie mit "override" überschreiben kann.
        public virtual double BerechneParkkosten()
        {
            return 1.1; // Platzhalter!
        }
    }
}
