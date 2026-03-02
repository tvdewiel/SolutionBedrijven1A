using BedrijvenBL.Domein;
using BedrijvenBL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BedrijvenBL.Beheerders
{
    public class BedrijvenImportBeheerder
    {
        IBedrijvenFileProcessor fileProcessor;
        IBedrijvenRepository repository;

        public BedrijvenImportBeheerder(IBedrijvenFileProcessor fileProcessor, IBedrijvenRepository repository)
        {
            this.fileProcessor = fileProcessor;
            this.repository = repository;
        }

        public void ImportBedrijven(List<Bedrijf> bedrijven)
        {
           repository.ImporteerBedrijven(bedrijven);
        }

        public List<Bedrijf> LeesBedrijven(string fileName,string logFileName)
        {
            return fileProcessor.LeesBedrijvenBestand(fileName,logFileName);
        }
    }
}
