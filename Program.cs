using System.Globalization;
using System.Text.RegularExpressions;

namespace Parkhaussystem_Software
{
    public class Program
    {
        static void Main(string[] args)
        {
            // UTF8 damit Emojis in der Konsole korrekt angezeigt werden.
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Parkhaus erstesParkhaus = new Parkhaus(100);


            bool laeuft = true;
            do
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("🅿️  Parkhaus Denis Stuttgart  🅿️");
                Console.ResetColor();
                Console.WriteLine("1. Einfahren");
                Console.WriteLine("2. Ausfahren & Bezahlen");
                Console.WriteLine("3. Programm beenden");
                Console.Write("Ihre Auswahl: ");
                string menuAuswahl = Console.ReadLine() ?? "";
                Console.WriteLine();

                switch(menuAuswahl)
                {
                    case "1":
                        Console.Clear();
                        erstesParkhaus.FahrzeugEinfahren();
                        break;
                    case "2":
                        Console.Clear();
                        erstesParkhaus.FahrzeugAusfahren();
                        break;
                    case "3":
                        Console.WriteLine("Auf Wiedersehen!");
                        laeuft = false;
                        break;
                    default:
                        Console.WriteLine("Ungültige Eingabe. Bitte nur 1, 2 oder 3 eingeben.");
                        Console.WriteLine("Bitte 3 Sekunden warten...");
                        // Thread.Sleep damit die Fehlermeldung lesbar bleibt bevor das Menü neu lädt.
                        Thread.Sleep(3000);
                        Console.Clear();
                        break;
                }

            } while (laeuft);


        }
    }
}
