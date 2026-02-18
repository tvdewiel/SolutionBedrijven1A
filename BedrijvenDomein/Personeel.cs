using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BedrijvenDomein
{
    public class Personeel
    {
        public Personeel(int id, string voornaam, string familienaam, string email, Adres adres, DateTime geboortedatum)
        {
            Id = id;
            Voornaam = voornaam;
            Familienaam = familienaam;
            Email = email;
            Adres = adres;
            Geboortedatum = geboortedatum;
        }

        public int Id { get; set; }
        public string Voornaam { get; set; }
        public string Familienaam { get; set; }
        public string Email { get; set; }
        public Adres Adres { get; set; }
        public DateTime Geboortedatum { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is Personeel personeel &&
                   Id == personeel.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
    }
}
