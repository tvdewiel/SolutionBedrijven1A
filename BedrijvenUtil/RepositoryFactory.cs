using BedrijvenBL.Interfaces;
using BedrijvenDLSQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BedrijvenUtil
{
    public static class RepositoryFactory
    {
        public static IBedrijvenRepository GeefRepository(string repoType,string connectionString)
        {
            switch (repoType)
            {
                case "ADO": return new BedrijvenRepositoryADO(connectionString);
                default: return null;
            }
        }
    }
}
