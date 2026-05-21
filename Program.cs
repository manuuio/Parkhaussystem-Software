namespace Parkhaussystem_Software
{
    public class Program
    {
        static void Main(string[] args)
        {
            //Testen der Funktionalität:
            Parkhaus erstesParkhaus = new Parkhaus(200);
            erstesParkhaus.ZeigeFreiePlaetze();
        }
    }
}
