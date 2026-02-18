using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BedrijvenDomein
{
    public class BedrijfBeheerder
    {
        public BedrijfBeheerder(List<Bedrijf> bedrijven)
        {
            foreach (Bedrijf bedrijf in bedrijven) {
                bedrijfPersoneel.Add(bedrijf.Naam, bedrijf.Personeel().ToList());
                foreach(Personeel personeel in bedrijf.Personeel())
                {
                    if (!woonplaatsPersoneel.ContainsKey(personeel.Adres.Woonplaats))
                        woonplaatsPersoneel.Add(personeel.Adres.Woonplaats, new List<Personeel>());
                    woonplaatsPersoneel[personeel.Adres.Woonplaats].Add(personeel);
                }
            }
        }
        Dictionary<string, List<Personeel>> woonplaatsPersoneel=new();//string is woonplaats
        Dictionary<string,List<Personeel>> bedrijfPersoneel=new();//string bedrijfsnaam
        public List<Personeel> GeefPersoneelWoonplaats(string woonplaats)
        {
            return woonplaatsPersoneel[woonplaats];
        }
        public List<Personeel> GeefPersoneelBedrijf(string bedrijfsnaam) 
        {
            return bedrijfPersoneel[bedrijfsnaam];
        }
    }
}
