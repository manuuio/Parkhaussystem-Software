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

        // Liste mit festgelegten Cent Werten welche das Terminal akzeptiert.
        private List<double> _ertlaubteMuenzen;
        

        public Parkhaus(int maxPlaetze)
        {
            this._maxPlaetze = maxPlaetze;
            // Die Listen müssen initialisiert werden (existieren) bevor jemand einfährt.
            _freiePlaetze = new List<string>();
            _belegteFahrzeuge = new List<Fahrzeug>();
            _ertlaubteMuenzen = new List<double> { 100, 50, 20, 10 };

            // ==== Ich habe diesen gesammten Block von "ZeigeFreiePlaetze()" in den Konstruktor verschoben. ==== //

            Random rnd = new Random();
            // Da die angegebene "maxValue" im Parameter nicht inklusive ist, steht am Ende das "+1".
            // Problem = int von rnd.Next kann nicht in List convertiert werden.
            // Lösung = Bemerkt dass eine lokale Variable reicht um die freien Plätze für die for loop zu speichern.

            int rndFreiePlaetze = rnd.Next(_maxPlaetze + 1);

            // rnd.Next auf 0 setzen um zu testen ob die Liste "_freiePlaetze;" dann wirklich keine Elemente hat.
            //int rndFreiePlaetze = rnd.Next(0); // Funktioniert, gibt: "Es gibt im moment keine freien Parkplätze." aus.

            // Problem = Wie füge ich diese Random zahl der maxPlaetze in eine liste sodass
            // ich später alle freien Parkplaetze zur auswahl ausgeben kann.
            // Lösung = mit der .Add funktion in einem loop und implizirter Konvertierung durch dass "P"
            for (int i = 1; i <= rndFreiePlaetze; i++)
            {
                _freiePlaetze.Add("P" + i);
            }


            // ==== Ich habe diesen gesammten Block von "ZeigeFreiePlaetze()" in den Konstruktor verschoben. ==== //


        }

        // Prüft ob es freie Parkplätze gibt und gibt diese dann aus.
        public void ZeigeFreiePlaetze()
        {
            if (_freiePlaetze.Count > 0) 
            { 
                Console.WriteLine($"Anzahl freier Parkplätze: \n{_freiePlaetze.Count}.");
                Console.WriteLine($"Verfügbare Parkplatznummern: \n");
                foreach (var platz in _freiePlaetze)
                {
                    Console.WriteLine($"{platz}");
                }
            }
            else
            {
                Console.WriteLine($"Es gibt im moment keine freien Parkplätze.");
            }
        }

        // Prüft ob die eingegebene Platznummer in der Liste der freien Plätze vorhanden ist.
        public bool IstPlatzFrei(string platznummer)
        {
            if (_freiePlaetze.Contains(platznummer))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Liest die Fahrereingabe, prüft sie, erstellt ein Fahrzeug-Objekt und blockiert den Platz.
        // Dokumentation: Beim Implementieren festgestellt dass platznummer kein Parameter sein muss da die Eingabe innerhalb der Methode über Console.ReadLine() eingelesen wird.
        // Problem: Herausfinden welche schleife am besten für die Kontrollstruktur ist.
        public void FahrzeugEinfahren()
        {
            Console.WriteLine($"Wähle einen beliebigen Parkplatz: \n");
            string gewaehlteNummer = Console.ReadLine() ?? "";

            while (gewaehlteNummer == "" || !IstPlatzFrei(gewaehlteNummer))
            {
                Console.WriteLine("Parkplatz nicht frei oder vorhanden, \n" +
                    "bitte einen der oben aufgelisteten Parkplatznummern eingeben. \n" +
                    "Zum Beispiel: 'P34' etc.");
                gewaehlteNummer = Console.ReadLine() ?? "";
            }


            Console.WriteLine($"Fahren sie ein Motorrad? (j/n):");
            string istMotorrad = Console.ReadLine() ?? "";

            // Wir fangen alle ungültige eingaben in einer Schleife ab.
            while (istMotorrad != "j" && istMotorrad != "n")
            {
                Console.WriteLine("Ungültige Eingabe!");
                Console.WriteLine($"Fahren sie ein Motorrad? Bitte antworten sie mit 'j' oder 'n'");
                istMotorrad = Console.ReadLine() ?? "";
            }

            if(istMotorrad == "j")
            {
                Motorrad eingefahrenesMotorrad = new Motorrad(gewaehlteNummer);
                _belegteFahrzeuge.Add(eingefahrenesMotorrad);
                _freiePlaetze.Remove(gewaehlteNummer);
            }
            else if (istMotorrad == "n")
            {
                // Problem: Wie bekomme ich die gewaehlteNummer in "_belegteFahrzeuge"
                // Lösung: Ein neues Fahrzeug Objekt erstellen und dies in der "_belegteFahrzeuge" List<Fahrzeug> speichern.
                Fahrzeug eingefahrenesFahrzeug = new Fahrzeug(gewaehlteNummer);
                _belegteFahrzeuge.Add(eingefahrenesFahrzeug);
                _freiePlaetze.Remove(gewaehlteNummer);
            }

        }

        // Tag 5 - Problem: Die Konsole muss aktualisiert werden um nach dem Münzeinwurf den verbleibende Betrag anzuzeigen.
        // Tag 5 - Problemlösung: Eine neue Methode welche die RechnungAusgeben() Methode den neuen Wert übergibt und callt.
        public void RechnungAusgeben(double endpreis, double verbleibenderBetrag, int parkdauerMinuten, string parkplatzNummer, double preisProHalbeStunde)
        {
            const double MwSt = 0.19;
            double nurMwSt = endpreis * MwSt;

            Console.WriteLine(new string('-', 39));
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("🅿️  Parkhaus Denis Stuttgart  🅿️");
            Console.ResetColor();
            Console.WriteLine(new string('-', 39));
            Console.WriteLine("Ihre genutzte Parkplatznummer:        " + $"{parkplatzNummer,10}");
            Console.WriteLine("Ihre Parkdauer in Minuten:        " + $"{parkdauerMinuten,10} Stück");
            Console.WriteLine("Kosten pro 30 Minuten:       " + $"{preisProHalbeStunde,20:c}");
            Console.WriteLine(new string('-', 39));
            Console.WriteLine("\u001b[1mEndpreis:          " + $"{endpreis,20:c}\u001b[0m");
            Console.WriteLine($"inkl. {MwSt * 100:f0} % MwSt.   " + $"{nurMwSt,20:c}");
            Console.WriteLine(new string('-', 39));
            Console.WriteLine("\n");
            Console.WriteLine("\n");
            Console.WriteLine("Bitte werfen sie genug Münzen ein um den Endpreis zu begleichen.");
            Console.WriteLine($"Ihr zu bezahlender Betrag: {verbleibenderBetrag,20:c}\u001b[0m");
            Console.WriteLine("Wir akzeptieren: 1 Euro, 50 Cent, 20 Cent und 10 Cent Münzen.");
            Console.WriteLine("Bitte eingeben: 1 Euro = 1, 50 Cent = 50, 20 Cent = 20 und 10 Cent = 10.");
            Console.WriteLine("Ihre Eingabe:");

        }

        public void RechnungBezahlen(double endpreis, int parkdauerMinuten, string parkplatzNummer, double preisProHalbeStunde)
        {
            double eingegebenerBetrag;
            double verbleibenderBetrag = endpreis;
            bool vollstaendigBezahlt = false;

            RechnungAusgeben(endpreis, verbleibenderBetrag, parkdauerMinuten, parkplatzNummer, preisProHalbeStunde);
            bool korrekteEingabe = double.TryParse(Console.ReadLine(), out eingegebenerBetrag);

            // Tag 5 - Problem: Wie prüfe ich am effektivsten ob nur einer der 4 erlaubten Werte eingegeben wurden?
            while (!korrekteEingabe && !_ertlaubteMuenzen.Contains(eingegebenerBetrag))
            {
                Console.WriteLine("Falsche Eingabe. Bitte erneut versuchen:");
                korrekteEingabe = double.TryParse(Console.ReadLine(), out eingegebenerBetrag);
            }


            // Tag 5 - Änderung: if muss zu while geändert werden da der switch laufen muss BIS der verbleibende Betrag = 0 ist.
            //if (verbleibenderBetrag > 0)
            // Tag 5 - Problem: Wie gehe ich sicher dass der Fahrer nicht zu viel einwirft.
            // Tag 5 - Problem: Sollte ich lieber mit Cent Beträgen rechnen? Wie kann das Terminal sonst zwischen 1 Euro und 1 Cent unterscheiden falls wir diese Option anbieten wollen.
            while (verbleibenderBetrag > 0)
            {
                switch(eingegebenerBetrag)
                {
                    case 100:
                        verbleibenderBetrag -= 100;
                        Console.Clear();
                        RechnungAusgeben(endpreis, verbleibenderBetrag, parkdauerMinuten, parkplatzNummer, preisProHalbeStunde);
                        break;
                    case 50:
                        verbleibenderBetrag -= 50;
                        Console.Clear();
                        RechnungAusgeben(endpreis, verbleibenderBetrag, parkdauerMinuten, parkplatzNummer, preisProHalbeStunde);
                        break;
                    case 20:
                        verbleibenderBetrag -= 20;
                        Console.Clear();
                        RechnungAusgeben(endpreis, verbleibenderBetrag, parkdauerMinuten, parkplatzNummer, preisProHalbeStunde);
                        break;
                    case 10:
                        verbleibenderBetrag -= 10;
                        Console.Clear();
                        RechnungAusgeben(endpreis, verbleibenderBetrag, parkdauerMinuten, parkplatzNummer, preisProHalbeStunde);
                        break;
                }
            }
            
            if (verbleibenderBetrag == 0)
            {
                Console.WriteLine("Sie haben ihre Kosten erfolgreich ohne einen Restbetrag beglichen.");
                Console.WriteLine("Schöne Weiterfahrt!");
            }
            // Math.Abs() um die negative Zahl in eine positive Zahl umzuwandeln
            else if (verbleibenderBetrag < 0)
            {
                Console.WriteLine("Sie haben ihre Kosten erfolgreich beglichen.");
                Console.WriteLine($"Den zu viel bezahlten Betrag von: {Math.Abs(verbleibenderBetrag)} Cent bekommen sie jetzt ausgezahlt.");
                Console.WriteLine("Schöne Weiterfahrt!");
            }

        }


        // Berechnet Kosten, erstellt Rechnung, speichter Rechnung als JSON und gibt den Platz wieder frei.
        // Dokumentation: Beim Implementieren festgestellt dass "platznummer" kein Parameter sein muss da die Eingabe innerhalb der Methode über Console.ReadLine() eingelesen wird.
        public void FahrzeugAusfahren()
        {
            Console.WriteLine("Wilkommen zurück! Bitte geben sie ihre Parkplatznummer ein:");
            string gesuchteParkplatznummer = Console.ReadLine() ?? "";

            while (gesuchteParkplatznummer == "")
            {
                Console.WriteLine("Parkplatzeingabe fehlerhaft, \n" +
                    "bitte geben sie ihre bei der Einfahrt ausgewählte Parknummer ein,\n" +
                    "zum Beispiel: 'P34' etc.");
                gesuchteParkplatznummer = Console.ReadLine() ?? "";
            }

            bool fahrzeugGefunden = false;
            while (!fahrzeugGefunden)
            {
                foreach (var fahrzeug in _belegteFahrzeuge)
                {
                    var tempPlatznummer = fahrzeug.GetMeinePlatznummer();

                    if (gesuchteParkplatznummer == tempPlatznummer)
                    {
                        fahrzeugGefunden = true;
                        fahrzeug.SetParkdauerMinuten();
                        int parkdauerMinuten = fahrzeug.GetParkdauerMinuten();
                        double endpreis = fahrzeug.BerechneParkkosten();
                        double preisProHalbeStunde = fahrzeug.GetMeinPreisProHalbeStunde();
                        string parkplatzNummer = fahrzeug.GetMeinePlatznummer();

                        RechnungBezahlen(endpreis, parkdauerMinuten, parkplatzNummer, preisProHalbeStunde);

                        _belegteFahrzeuge.Remove(fahrzeug);
                        _freiePlaetze.Add(fahrzeug.GetMeinePlatznummer());
                        break;
                    }
                }
                if (!fahrzeugGefunden)
                {

                    Console.WriteLine($"Die eingegebene Parkplatznummer: {gesuchteParkplatznummer} existiert nicht. \n" +
                        $"bitte geben sie ihre bei der Einfahrt ausgewählte Parknummer ein,\n" +
                        $"zum Beispiel: 'P34' etc.");
                    gesuchteParkplatznummer = Console.ReadLine() ?? "";

                }
            }
            
        }

    }
}
