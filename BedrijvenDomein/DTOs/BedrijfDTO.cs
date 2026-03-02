using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BedrijvenBL.DTOs
{
    public class BedrijfDTO
    {
        public BedrijfDTO(int? id, string naam, int jaarOprichting, string sector, string industrie, string extraInfo, string hoofdkwartier, int aantalPersoneel)
        {
            Id = id;
            Naam = naam;
            JaarOprichting = jaarOprichting;
            Sector = sector;
            Industrie = industrie;
            ExtraInfo = extraInfo;
            Hoofdkwartier = hoofdkwartier;
            AantalPersoneel = aantalPersoneel;
        }

        public int? Id { get; set; }
        public string Naam { get; set; }
        public int JaarOprichting { get; set; }
        public string Sector { get; set; }
        public string Industrie { get; set; }
        public string ExtraInfo { get; set; }
        public string Hoofdkwartier { get; set; }
        public int AantalPersoneel { get; set; }
    }
}
