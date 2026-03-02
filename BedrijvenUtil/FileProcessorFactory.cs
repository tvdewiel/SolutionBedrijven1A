using BedrijvenBL.Interfaces;
using BedrijvenDatalaag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BedrijvenUtil
{
    public static class FileProcessorFactory
    {
        public static IBedrijvenFileProcessor GeefFileProcessor(string fileType)
        {
            switch (fileType)
            {
                case "txt": return new BedrijvenBestandsLezer();
                //case "json":
                default: return null;
            }
        }
    }
}
