using BedrijvenBL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BedrijvenBL.Domein
{
    public class Bedrijf
    {
        public int? Id { get; set; }
        public string Naam {  get; set; }
        public int JaarOprichting { get; set; }
        public string Sector { get; set; }
        public string Industrie { get; set; }
        public string ExtraInfo { get; set; }
        public string Hoofdkwartier {  get; set; }
        private List<Personeel> _personeel=new List<Personeel>();
        public IReadOnlyList<Personeel> Personeel() {  return _personeel; }

        public Bedrijf(string naam, int jaarOprichting, string sector, string industrie, string extraInfo, string hoofdkwartier, List<Personeel> personeel)
        {
            Naam = naam;
            JaarOprichting = jaarOprichting;
            Sector = sector;
            Industrie = industrie;
            ExtraInfo = extraInfo;
            Hoofdkwartier = hoofdkwartier;
            if (personeel == null) throw new BedrijvenDomeinException("lijst is null");
            if (personeel.Count == 0) throw new BedrijvenDomeinException("lijst is leeg");
            _personeel = personeel;
        }

        public Bedrijf(int id, string naam, int jaarOprichting, string sector, string industrie, string extraInfo, string hoofdkwartier, List<Personeel> personeel) : this(naam,jaarOprichting, sector, industrie, extraInfo, hoofdkwartier,personeel)
        {
            Id = id;
        }

        public void VoegPersoneelToe(Personeel personeel)
        {
            if (personeel == null) throw new BedrijvenDomeinException("personeel is null");
            if (_personeel.Contains(personeel)) throw new BedrijvenDomeinException("personeel bestaat reeds");
            _personeel.Add(personeel);
        }
        public void VerwijderPersoneel(Personeel personeel)
        {
            if (personeel == null) throw new BedrijvenDomeinException("personeel is null");
            if (_personeel.Count==1 || !_personeel.Contains(personeel)) throw new BedrijvenDomeinException("personeel bestaat niet");
            _personeel.Remove(personeel);
        }
    }
}
