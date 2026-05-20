using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parkhaussystem_Software
{
    public class Parkhaus
    {
        private int _maxPlaetze;

        // Wir erstellen eine "List<string>" da diese im vergleich zum Array dynamisc ist.
        private List<string> _freiePlaetze;

        // Diese Liste speichert Referenzen auf die erstellten Fahrzeug-Objekte,
        // welche sich an verschiedenen Stellen im verwalteten Speicher befinden.
        // Da Motorrad von Fahrzeug erbt, können hier auch Motorrad-Objekte verwaltet werden.
        private List<Fahrzeug> _belegteFahrzeuge;


        public Parkhaus(int maxPlaetze)
        {
            this._maxPlaetze = maxPlaetze;
            _freiePlaetze = new List<string>();
        }

        public void ZeigeFreiePlaetze()
        {
            Random rnd = new Random();
            // Da die angegebene "maxValue" im Parameter nicht inklusive ist, steht am Ende das "+1".
            // Problem = int von rnd.Next kann nicht in List convertiert werden.
            // Lösung = Bemerkt dass eine lokale Variable reicht um die freien Plätze für die for loop zu speichern.
            int rndFreiePlätze = rnd.Next(_maxPlaetze + 1);

            // Problem = Wie füge ich diese Random zahl der maxPlaetze in eine liste sodass
            // ich später alle freien Parkplaetze zur auswahl ausgeben kann.
            // Lösung = mit der .Add funktion in einem loop und implizirter Konvertierung durch dass "P"
            for (int i = 1; i <= rndFreiePlätze; i++)
            {
                _freiePlaetze.Add("P" + i);
            }

            Console.WriteLine($"Anzahl freier Parkplätze: \n{rndFreiePlätze}");

            foreach (var platz in _freiePlaetze)
            {
                Console.WriteLine($"Wähle einen beliebigen Parkplatz: \n{platz}");
            }
        }

        public bool IstPlatzFrei(string platznummer)
        {
            return true; // Platzhalter
        }

        public void FahrzeugEinfahren(string platznummer)
        {

        }

        public void FahrzeugAusfahren(string platznummer)
        {

        }

    }
}
