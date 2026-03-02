using BedrijvenBL.Beheerders;
using BedrijvenUtil;
using Microsoft.Extensions.Configuration;

namespace ConsoleAppBedrijven
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            var config= builder.Build();
            string fileType = config.GetSection("AppSettings")["FileType"];
            string sourceFile= config.GetSection("AppSettings")["SourceFile"];
            string logFile= config.GetSection("AppSettings")["LogFile"];
            string connectionString = config.GetConnectionString("ADOSQLConnection");
            string repoType = config.GetSection("AppSettings")["RepoType"];

            BedrijvenImportBeheerder bedrijvenImportBeheerder = new BedrijvenImportBeheerder(
                FileProcessorFactory.GeefFileProcessor(fileType),
                RepositoryFactory.GeefRepository(repoType,connectionString));

            BedrijfBeheerder bedrijfBeheerder=new BedrijfBeheerder(
                RepositoryFactory.GeefRepository(repoType,connectionString));
            //var bedrijf=bedrijfBeheerder.GeefBedrijf("Bekaert");
            var personeel = bedrijfBeheerder.GeefPersoneelWoonplaats("Gent");

            //var bedrijven=bedrijvenImportBeheerder.LeesBedrijven(sourceFile,logFile);
            //bedrijvenImportBeheerder.ImportBedrijven(bedrijven);
            
        }
    }
}
