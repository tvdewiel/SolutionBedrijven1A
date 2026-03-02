using BedrijvenBL.Domein;
using BedrijvenBL.DTOs;
using BedrijvenBL.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BedrijvenBL.Beheerders
{
    public class BedrijfBeheerder
    {
        private IBedrijvenRepository repository;

        public BedrijfBeheerder(IBedrijvenRepository repository)
        {
            this.repository = repository;
        }

        public List<Personeel> GeefPersoneelWoonplaats(string woonplaats)
        {
            return repository.GeefPersoneelWoonplaats(woonplaats);
        }
        public Bedrijf GeefBedrijf(string bedrijfsnaam) 
        {
            return repository.GeefBedrijf(bedrijfsnaam);
        }

        public List<BedrijfDTO> GeefBedrijvenDTOs()
        {
            return repository.GeefBedrijvenDTOs();
        }
    }
}
