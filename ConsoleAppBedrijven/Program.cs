using BedrijvenDatalaag;
using BedrijvenDomein;

namespace ConsoleAppBedrijven
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            string pad = @"c:\tmp\bedrijvenbelgie_18092025.txt";
            BedrijvenBestandsLezer lezer=new BedrijvenBestandsLezer();
            BedrijfBeheerder bedrijfBeheerder = new BedrijfBeheerder(lezer.LeesBedrijvenBestand(pad));
            var res = bedrijfBeheerder.GeefPersoneelBedrijf("VLM Airlines");
            var res2 = bedrijfBeheerder.GeefPersoneelWoonplaats("Gent");
            //try
            //{
            //    Adres a = new Adres("Gent", "Nonnemeers", 900, "14F");
            //    Console.WriteLine(a);
            //}
            //catch (BedrijvenDomeinException ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}
            //List<Personeel> lp = new List<Personeel>();
            //lp.Add(new Personeel());
            //Bedrijf b=new Bedrijf(lp);
            //b.VoegPersoneelToe(new Personeel());
            //b.Personeel().RemoveAt(0);
        }
    }
}
