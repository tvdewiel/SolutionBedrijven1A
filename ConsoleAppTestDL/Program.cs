using BedrijvenDatalaag;

namespace ConsoleAppTestDL
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            string pad = @"c:\tmp\bedrijven_test_fouten.txt";
            string padLog = @"c:\tmp\log_bedrijven_test_fouten.txt";
            BedrijvenBestandsLezer bl=new BedrijvenBestandsLezer();
            var res=bl.LeesBedrijvenBestand(pad,padLog);
        }
    }
}
