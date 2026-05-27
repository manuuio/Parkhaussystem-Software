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

        // List statt Array damit Plätze dynamisch hinzugefügt und entfernt werden können.
        private List<string> _freiePlaetze;

        // List<Fahrzeug> statt List<string> damit wir beim Ausfahren direkt auf alle
        // Fahrzeugdaten zugreifen können (Parkdauer, Kosten, Platznummer).
        // Da Motorrad von Fahrzeug erbt können beide Typen in dieser Liste gespeichert werden.
        private List<Fahrzeug> _belegteFahrzeuge;

        // Nur diese Münzwerte werden am Terminal akzeptiert (in Cent).
        private List<double> _ertlaubteMuenzen;
        

        public Parkhaus(int maxPlaetze)
        {
            this._maxPlaetze = maxPlaetze;
            _freiePlaetze = new List<string>();
            _belegteFahrzeuge = new List<Fahrzeug>();
            _ertlaubteMuenzen = new List<double> { 100, 50, 20, 10, 1 };


            // Zufällige Startbelegung im Konstruktor damit beim Programmstart
            // realistische Parkhaus-Verhältnisse simuliert werden.
            Random rnd = new Random();

            // maxValue bei Next() ist nicht inklusive, deshalb +1.
            int rndFreiePlaetze = rnd.Next(_maxPlaetze + 1);


            for (int i = 1; i <= rndFreiePlaetze; i++)
            {
                _freiePlaetze.Add("P" + i);
            }

        }

        // Zeigt freie Plätze in Spalten an um Scrollen bei vielen Plätzen zu vermeiden.
        public void ZeigeFreiePlaetze()
        {
            if (_freiePlaetze.Count > 0) 
            { 
                Console.WriteLine($"Anzahl freier Parkplätze: \n{_freiePlaetze.Count}.");
                Console.WriteLine();
                Console.WriteLine($"Verfügbare Parkplatznummern: \n");
                int zaehler = 0;
                int ebene = 1;
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"🅿 Parkhausebene {ebene}: 🅿️");
                Console.ResetColor();
                foreach (var platz in _freiePlaetze)
                {
                    // -6 damit alle Spalten gleich breit sind (linksbündig).
                    Console.Write($"{platz, -6}");
                    zaehler++;

                    // % 20 zuerst prüfen da jede durch 20 teilbare Zahl auch durch 10 teilbar is
                    if (zaehler % 20 == 0)
                    {
                        // Neue Ebene anzeigen um Ausgabe übersichtlicher zu gestalten.
                        ebene++;
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine($"🅿️ Parkhausebene {ebene}: 🅿️");
                        Console.ResetColor();
                        
                    }
                    else if (zaehler % 10 == 0)
                    {
                        Console.WriteLine();
                    }
                }
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine($"Es gibt im moment keine freien Parkplätze.");
                Console.WriteLine($"Bitte kommen Sie später wieder.");
            }
        }

        // Prüft ob die eingegebene Platznummer noch in der Liste der freien Plätze ist.
        public bool IstPlatzFrei(string platznummer)
        {
            return _freiePlaetze.Contains(platznummer);
        }

        // Simuliert das Terminal an der Einfahrt.
        public void FahrzeugEinfahren()
        {
            string gewaehlteNummer;
            do
            {
                ZeigeFreiePlaetze();
                Console.WriteLine();
                Console.Write($"Wählen Sie einen beliebigen Parkplatz: ");
                gewaehlteNummer = Console.ReadLine() ?? "";

                if(gewaehlteNummer == "" || !IstPlatzFrei(gewaehlteNummer))
                {
                    Console.Write("Parkplatz nicht frei oder vorhanden, " +
                    "bitte einen der oben aufgelisteten Parkplatznummern eingeben. \n" +
                    "Zum Beispiel: 'P34' etc. Erneute Eingabe wird vorbereitet...");
                    // Thread.Sleep damit der Nutzer die Fehlermeldung lesen kann bevor die Konsole neu lädt.
                    Thread.Sleep(4000);
                    Console.Clear();
                }

            } while (gewaehlteNummer == "" || !IstPlatzFrei(gewaehlteNummer));

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("🅿️  Parkhaus Denis Stuttgart  🅿️");
            Console.ResetColor();
            Console.WriteLine($"Sie haben den Parkplatz: '{gewaehlteNummer}' erfolgreich gewählt.");
            Console.Write($"Fahren Sie ein Motorrad? (j/n): ");
            string istMotorrad = Console.ReadLine() ?? "";
 
            while (istMotorrad != "j" && istMotorrad != "n")
            {
                Console.WriteLine("");
                Console.WriteLine("Ungültige Eingabe!");
                Console.WriteLine($"Fahren sie ein Motorrad?");
                Console.Write($"Bitte antworten sie mit 'j' oder 'n': ");
                istMotorrad = Console.ReadLine() ?? "";
            }

            if(istMotorrad == "j")
            {
                Motorrad eingefahrenesMotorrad = new Motorrad(gewaehlteNummer);
                _belegteFahrzeuge.Add(eingefahrenesMotorrad);
                _freiePlaetze.Remove(gewaehlteNummer);
                Console.WriteLine($"Wir haben ihr Motorrad mit dem Parkplatz '{gewaehlteNummer}' gespeichert.");
                Console.WriteLine($"Bitte notieren sie sich diese Nummer da Sie diese später bei der Ausfahrt angeben müssen.");
                Console.WriteLine("Das Programm startet in 10 Sekunden neu...");
                // Thread.Sleep damit der Nutzer die Parkplatznummer notieren kann.
                Thread.Sleep(10000);
                Console.Clear();
            }
            else if (istMotorrad == "n")
            {
                Fahrzeug eingefahrenesFahrzeug = new Fahrzeug(gewaehlteNummer);
                _belegteFahrzeuge.Add(eingefahrenesFahrzeug);
                _freiePlaetze.Remove(gewaehlteNummer);
                Console.WriteLine($"Wir haben ihr Fahrzeug mit dem Parkplatz '{gewaehlteNummer}' gespeichert.");
                Console.WriteLine($"Bitte notieren sie sich diese Nummer da Sie diese später bei der Ausfahrt angeben müssen.");
                Console.WriteLine("Das Programm startet in 10 Sekunden neu...");
                // Thread.Sleep damit der Nutzer die Parkplatznummer notieren kann.
                Thread.Sleep(10000);
                Console.Clear();
            }

        }

        // Gibt die Rechnung formatiert aus. Alle Beträge intern in Cent, Ausgabe in Euro.
        public void RechnungAusgeben(double endpreis, double verbleibenderBetrag, int parkdauerMinuten, string parkplatzNummer, double preisProHalbeStunde)
        {
            string linie = new string('-', 50);
            string titel = "🅿️  Parkhaus Denis Stuttgart  🅿️";
            int breiteDerRechnung = 50;

            // Titel zentrieren: linkes Padding = (Gesamtbreite - Titellänge) / 2
            int padding = (breiteDerRechnung - titel.Length) / 2;

            const double MwSt = 0.19;
            double nurMwSt = endpreis * MwSt;

            Console.WriteLine(linie);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(titel.PadLeft(padding + titel.Length));
            Console.ResetColor();
            Console.WriteLine(linie);
            // PadRight(30) für linken Text + PadLeft(20) für rechten Wert = 50 Zeichen Gesamtbreite.
            Console.WriteLine("Ihre genutzte Parkplatznummer:".PadRight(30) + $"{parkplatzNummer.PadLeft(20)}");
            Console.WriteLine("Ihre Parkdauer in Minuten:".PadRight(30) + $"{(parkdauerMinuten + " Minuten").ToString().PadLeft(20)}");
            Console.WriteLine("Kosten pro 30 Minuten:".PadRight(30) + $"{(preisProHalbeStunde / 100.0 + " €").ToString().PadLeft(20)}");
            Console.WriteLine(linie);
            Console.WriteLine("Endpreis:".PadRight(30) + $"{(endpreis / 100.0 + " €").ToString().PadLeft(20)}");
            Console.WriteLine($"inkl. {MwSt * 100:f0} % MwSt.".PadRight(30) + $"{(nurMwSt / 100.0 + " €").ToString().PadLeft(20)}");
            Console.WriteLine(linie);
            Console.WriteLine("\n");
            Console.WriteLine("Bitte werfen sie genug Münzen ein um den Endpreis zu begleichen.");
            Console.WriteLine($"Ihr offener Betrag:".PadRight(30) + $"{(verbleibenderBetrag / 100.0 + " €").ToString().PadLeft(20)}");
            Console.WriteLine("");
        }

        // Zeigt akzeptierte Münzwerte an und fordert zur Eingabe auf.
        public void ZahlungsMoeglichkeiten()
        {
            Console.WriteLine("Wir akzeptieren nur: 1 Euro, 50 Cent, 20 Cent und 10 Cent Münzen.");
            Console.WriteLine("Bitte eingeben: 1 Euro = 1, 50 Cent = 50, 20 Cent = 20 und 10 Cent = 10.");
            Console.Write("Ihre Eingabe: ");
        }

        public void RechnungBezahlen(double endpreis, int parkdauerMinuten, string parkplatzNummer, double preisProHalbeStunde)
        {
            double eingegebenerBetrag;
            double verbleibenderBetrag = endpreis;
            bool vollstaendigBezahlt = false;

            RechnungAusgeben(endpreis, verbleibenderBetrag, parkdauerMinuten, parkplatzNummer, preisProHalbeStunde);
            ZahlungsMoeglichkeiten();
            bool korrekteEingabe = double.TryParse(Console.ReadLine(), out eingegebenerBetrag);

            // || statt && weil wiederholt werden soll wenn die Eingabe KEIN gültiger double ist
            // ODER nicht in der erlaubten Münzliste ist — nicht nur wenn beides gleichzeitig falsch ist.
            while (!korrekteEingabe || !_ertlaubteMuenzen.Contains(eingegebenerBetrag))
            {
                Console.Write("Falsche Eingabe. Bitte erneut versuchen: ");
                korrekteEingabe = double.TryParse(Console.ReadLine(), out eingegebenerBetrag);
            }

            while (verbleibenderBetrag > 0)
            {
                // Intern wird in Cent gerechnet, deshalb 1 Euro Eingabe in 100 Cent umwandeln.
                if (eingegebenerBetrag == 1)
                    eingegebenerBetrag = 100;

                switch (eingegebenerBetrag)
                {
                    case 100:
                        verbleibenderBetrag -= 100;
                        break;
                    case 50:
                        verbleibenderBetrag -= 50;
                        break;
                    case 20:
                        verbleibenderBetrag -= 20;
                        break;
                    case 10:
                        verbleibenderBetrag -= 10;
                        break;
                    default:
                        Console.WriteLine("Ungültige Münzeneingabe! Neue Eingabe in 3 Sekunden...");
                        Thread.Sleep(3000);
                        break;
                }

                Console.Clear();
                RechnungAusgeben(endpreis, verbleibenderBetrag, parkdauerMinuten, parkplatzNummer, preisProHalbeStunde);
                ZahlungsMoeglichkeiten();

                // Keine weitere Eingabe wenn Betrag bereits beglichen oder überzahlt wurde.
                if (verbleibenderBetrag > 0)
                {
                    korrekteEingabe = double.TryParse(Console.ReadLine(), out eingegebenerBetrag);
                }
            }

            Console.Clear();
            RechnungAusgeben(endpreis, verbleibenderBetrag, parkdauerMinuten, parkplatzNummer, preisProHalbeStunde);
            Console.WriteLine();

            if (verbleibenderBetrag == 0)
            {
                Console.WriteLine("Sie haben ihre Kosten vollständig beglichen.");
            }
            else if (verbleibenderBetrag < 0)
            {
                Console.WriteLine("Sie haben ihre Kosten erfolgreich beglichen.");
                // Math.Abs() um den negativen Restbetrag als positive Zahl auszugeben.
                Console.WriteLine($"Den zu viel bezahlten Betrag von: {Math.Abs(verbleibenderBetrag)} Cent bekommen sie jetzt ausgezahlt.");
            }

            Console.WriteLine("Schöne Weiterfahrt!");
            Thread.Sleep(10000);

        }

        public void FahrzeugAusfahren()
        {
            Console.WriteLine("Wilkommen zurück! Bitte geben sie ihre Parkplatznummer ein:");
            string gesuchteParkplatznummer = Console.ReadLine() ?? "";

            while (gesuchteParkplatznummer == "")
            {
                Console.WriteLine($"Die eingegebene Parkplatznummer: '{gesuchteParkplatznummer}' existiert nicht oder gehört nicht zu Ihnen. \n" +
                        $"Bitte geben sie ihre bei der Einfahrt ausgewählte Parkplatznummer ein. Zum Beispiel: 'P34' etc.");
                Console.Write($"Ihre Parknummer: ");
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
                        Console.Clear();
                        fahrzeugGefunden = true;
                        fahrzeug.SetParkdauerMinuten();
                        int parkdauerMinuten = fahrzeug.GetParkdauerMinuten();
                        double endpreis = fahrzeug.BerechneParkkosten();
                        double preisProHalbeStunde = fahrzeug.GetMeinPreisProHalbeStunde();
                        string parkplatzNummer = fahrzeug.GetMeinePlatznummer();

                        RechnungBezahlen(endpreis, parkdauerMinuten, parkplatzNummer, preisProHalbeStunde);

                        _belegteFahrzeuge.Remove(fahrzeug);
                        _freiePlaetze.Add(tempPlatznummer);

                        // Numerisch sortieren damit z.B. P10 nicht vor P2 erscheint (alphabetische Sortierung wäre falsch).
                        _freiePlaetze.Sort((a, b) => int.Parse(a.Substring(1)).CompareTo(int.Parse(b.Substring(1))));

                        break;
                    }
                }
                if (!fahrzeugGefunden)
                {

                    Console.WriteLine($"Die eingegebene Parkplatznummer: '{gesuchteParkplatznummer}' existiert nicht oder gehört nicht zu Ihnen. \n" +
                        $"Bitte geben sie ihre bei der Einfahrt ausgewählte Parkplatznummer ein. Zum Beispiel: 'P34' etc.");
                    Console.Write($"Ihre Parknummer: ");
                    gesuchteParkplatznummer = Console.ReadLine() ?? "";

                }
            }
            
        }

    }
}
