using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BedrijvenDomein
{
    public class Adres
    {
        public Adres(string woonplaats, string straatnaam, int postcode, string huisnummer)
        {
            List<string> errors = new List<string>();
            try { Postcode = postcode; } catch (BedrijvenDomeinException ex) { errors.Add(ex.Message); }
            try { Woonplaats = woonplaats; } catch (BedrijvenDomeinException ex) { errors.Add(ex.Message); }
            try {Straatnaam = straatnaam; } catch (BedrijvenDomeinException ex) { errors.Add(ex.Message); }
            try {Huisnummer = huisnummer; } catch (BedrijvenDomeinException ex) { errors.Add(ex.Message); }
            if (errors.Count > 0)
            {
                throw new BedrijvenDomeinException(errors);
            }
        }

        private string _woonplaats;
        public string Woonplaats {
            get { return _woonplaats; }
            set { if (string.IsNullOrWhiteSpace(value) || value.Length < 3) throw new BedrijvenDomeinException($"woonplaats fout : {value}");
                _woonplaats = value;
            } }
        private string _straatnaam;
        public string Straatnaam {
            get { return _straatnaam; }
            set { if (string.IsNullOrWhiteSpace(value)) throw new BedrijvenDomeinException($"straatnaam fout : {value}"); _straatnaam = value; } }
        private int _postcode;
        public int Postcode { 
            get { return _postcode; }
            set { if (value < 1000 || value > 9999) 
                    throw new BedrijvenDomeinException($"Postcode fout : {value}"); 
                _postcode = value; } }
        private string _huisnummer;
        public string Huisnummer {
            get { return _huisnummer; }
            set { if (string.IsNullOrWhiteSpace(value) || !char.IsDigit(value[0])) throw new BedrijvenDomeinException($"huisnummer fout : {value}");
                _huisnummer = value; } }
        public override string ToString()
        {
            return $"{Woonplaats},{Postcode},{Straatnaam},{Huisnummer}";
        }
    }
}
