using BedrijvenDomein;

namespace BedrijvenDatalaag
{
    public class BedrijvenBestandsLezer
    {
        public List<Bedrijf> LeesBedrijvenBestand(string fileName,string logFileName)
        {
            Dictionary<string,Bedrijf> bedrijven = new(); //sleutel is bedrijfsnaam
            using(StreamWriter sw = new StreamWriter(logFileName)) 
            using (StreamReader sr = new StreamReader(fileName))
            {
                string lijn;
                while ((lijn = sr.ReadLine()) != null)
                {
                    string[] ss = lijn.Split('|');
                    string bedrijfsnaam = ss[0];                  string industrie = ss[1];
                    string sector = ss[2];                        string hoofdkwartier = ss[3];
                    int jaarOprichting = int.Parse(ss[4]);        string extraInfo = ss[5];
                    int id = int.Parse(ss[6]);                    string voornaam = ss[7];
                    string familienaam = ss[8];                   DateTime geboortedatum = DateTime.Parse(ss[9]);
                    string woonplaats = ss[10];                   int postcode = int.Parse(ss[11]);
                    string straatnaam = ss[12];                   string huisnummer = ss[13];
                    string email = ss[14];
                    try
                    {
                        Adres adres = new Adres(woonplaats, straatnaam, postcode, huisnummer);
                        Personeel personeel = new Personeel(id, voornaam, familienaam, email, adres, geboortedatum);
                        if (!bedrijven.ContainsKey(bedrijfsnaam))
                        {
                            Bedrijf bedrijf = new Bedrijf(bedrijfsnaam, jaarOprichting, sector, industrie, extraInfo, hoofdkwartier, new List<Personeel>() { personeel });
                            bedrijven.Add(bedrijfsnaam, bedrijf);
                        }
                        else
                        {
                            bedrijven[bedrijfsnaam].VoegPersoneelToe(personeel);
                        }
                    }
                    catch (BedrijvenDomeinException ex) { sw.WriteLine(string.Join('|',ex.Errors)); }
                }
            }
            return bedrijven.Values.ToList();
        }
    }
}
