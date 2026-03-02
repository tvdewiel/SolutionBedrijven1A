using BedrijvenBL.Domein;
using BedrijvenBL.DTOs;
using BedrijvenBL.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BedrijvenDLSQL
{
    public class BedrijvenRepositoryADO : IBedrijvenRepository
    {
        string connectionString;

        public BedrijvenRepositoryADO(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public Bedrijf GeefBedrijf(string bedrijfsnaam)
        {
            string sql = "SELECT [naam] ,[sector],[industrie],[extrainfo],[hoofdkwartier],[jaaroprichting],t2.* FROM bedrijf t1 left join personeel t2 on t1.id=t2.bedrijfId WHERE naam=@bedrijfsnaam";
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@bedrijfsnaam", bedrijfsnaam);
                conn.Open();
                SqlDataReader reader=cmd.ExecuteReader();
                Bedrijf bedrijf = null;
                while (reader.Read())
                {
                    Personeel personeel = new Personeel((int)reader["id"], 
                        (string)reader["voornaam"],
                        (string)reader["familienaam"],
                        (string)reader["email"],
                        new Adres((string)reader["woonplaats"], (string)reader["straat"], (int)reader["postcode"], (string)reader["huisnummer"]),
                        (DateTime)reader["geboortedatum"]);                    
                    if (bedrijf == null)
                    {
                        List<Personeel> personeelLijst = new();
                        personeelLijst.Add(personeel);
                        bedrijf = new Bedrijf((int)reader["bedrijfid"],
                            (string)reader["naam"],
                            (int)reader["jaaroprichting"],
                            (string)reader["sector"],
                            (string)reader["industrie"],
                            (string)reader["extrainfo"],
                            (string)reader["hoofdkwartier"], personeelLijst);
                    }
                    else
                    {
                        bedrijf.VoegPersoneelToe(personeel);
                    }
                }
                return bedrijf;
            }            
        }

        public List<BedrijfDTO> GeefBedrijvenDTOs()
        {
            string sql = "SELECT t1.id,t1.naam,t1.sector,t1.industrie,t1.hoofdkwartier,t1.extrainfo,t1.jaaroprichting,count(*) aantalpersoneel FROM [bedrijf] t1 left join [personeel] t2 on t1.id=t2.bedrijfId group by t1.id,t1.naam,t1.sector,t1.industrie,t1.hoofdkwartier,t1.extrainfo,t1.jaaroprichting";
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                List<BedrijfDTO> dtos = new();
                cmd.CommandText = sql;
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                List<Personeel> personeelLijst = new();
                while (reader.Read())
                {
                    BedrijfDTO dto = new BedrijfDTO((int)reader["id"],
                        (string)reader["naam"],
                        (int)reader["jaaroprichting"],
                        (string)reader["sector"],
                        (string)reader["industrie"],
                        (string)reader["extrainfo"],
                        (string)reader["hoofdkwartier"],
                        (int)reader["aantalpersoneel"]);
                    dtos.Add(dto);                   
                }
                return dtos;
            }
        }

        public List<Personeel> GeefPersoneelWoonplaats(string woonplaats)
        {
            string sql = "SELECT * FROM [personeel] where woonplaats=@woonplaats";
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@woonplaats", woonplaats);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                List<Personeel> personeelLijst = new();
                while (reader.Read())
                {
                    Personeel personeel = new Personeel((int)reader["id"],
                        (string)reader["voornaam"],
                        (string)reader["familienaam"],
                        (string)reader["email"],
                        new Adres((string)reader["woonplaats"], (string)reader["straat"], (int)reader["postcode"], (string)reader["huisnummer"]),
                        (DateTime)reader["geboortedatum"]);                                          
                    personeelLijst.Add(personeel);                       
                }
                return personeelLijst;
            }
        }

        public void ImporteerBedrijven(List<Bedrijf> bedrijven)
        {
            string sqlBedrijf = "INSERT INTO bedrijf(naam,sector,industrie,hoofdkwartier,jaaroprichting,extrainfo) output INSERTED.ID VALUES(@naam,@sector,@industrie,@hoofdkwartier,@jaaroprichting,@extrainfo)";
            string sqlPersoneel = "INSERT INTO personeel(voornaam,familienaam,email,woonplaats,straat,huisnummer,postcode,geboortedatum,bedrijfid) VALUES(@voornaam,@familienaam,@email,@woonplaats,@straat,@huisnummer,@postcode,@geboortedatum,@bedrijfid)";
            using(SqlConnection connection = new SqlConnection(connectionString))
            using(SqlCommand cmdBedrijf=connection.CreateCommand())
            using(SqlCommand cmdPersoneel=connection.CreateCommand())
            {
                connection.Open();
                SqlTransaction transaction=connection.BeginTransaction();
                cmdBedrijf.Transaction= transaction;
                cmdPersoneel.Transaction= transaction;

                cmdBedrijf.CommandText = sqlBedrijf;
                cmdBedrijf.Parameters.Add(new SqlParameter("@naam",SqlDbType.NVarChar));
                cmdBedrijf.Parameters.Add(new SqlParameter("@sector", SqlDbType.NVarChar));
                cmdBedrijf.Parameters.Add(new SqlParameter("@industrie", SqlDbType.NVarChar));
                cmdBedrijf.Parameters.Add(new SqlParameter("@hoofdkwartier", SqlDbType.NVarChar));
                cmdBedrijf.Parameters.Add(new SqlParameter("@extrainfo", SqlDbType.NVarChar));
                cmdBedrijf.Parameters.Add(new SqlParameter("@jaaroprichting", SqlDbType.Int));

                cmdPersoneel.CommandText = sqlPersoneel;
                cmdPersoneel.Parameters.Add(new SqlParameter("@voornaam",SqlDbType.NVarChar));
                cmdPersoneel.Parameters.Add(new SqlParameter("@familienaam", SqlDbType.NVarChar));
                cmdPersoneel.Parameters.Add(new SqlParameter("@email", SqlDbType.NVarChar));
                cmdPersoneel.Parameters.Add(new SqlParameter("@woonplaats", SqlDbType.NVarChar));
                cmdPersoneel.Parameters.Add(new SqlParameter("@straat", SqlDbType.NVarChar));
                cmdPersoneel.Parameters.Add(new SqlParameter("@huisnummer", SqlDbType.NVarChar));
                cmdPersoneel.Parameters.Add(new SqlParameter("@postcode", SqlDbType.Int));
                cmdPersoneel.Parameters.Add(new SqlParameter("@geboortedatum", SqlDbType.DateTime2));
                cmdPersoneel.Parameters.Add(new SqlParameter("@bedrijfid", SqlDbType.Int));

                try
                {
                    foreach (Bedrijf b in bedrijven)
                    {
                        cmdBedrijf.Parameters["@naam"].Value = b.Naam;
                        cmdBedrijf.Parameters["@sector"].Value = b.Sector;
                        cmdBedrijf.Parameters["@industrie"].Value = b.Industrie;
                        cmdBedrijf.Parameters["@hoofdkwartier"].Value = b.Hoofdkwartier;
                        cmdBedrijf.Parameters["@jaaroprichting"].Value = b.JaarOprichting;
                        cmdBedrijf.Parameters["@extrainfo"].Value = b.ExtraInfo;
                        int bedrijfid = (int)cmdBedrijf.ExecuteScalar();
                        cmdPersoneel.Parameters["@bedrijfid"].Value = bedrijfid;
                        foreach (Personeel p in b.Personeel())
                        {
                            cmdPersoneel.Parameters["@voornaam"].Value = p.Voornaam;
                            cmdPersoneel.Parameters["@familienaam"].Value = p.Familienaam;
                            cmdPersoneel.Parameters["@email"].Value = p.Email;
                            cmdPersoneel.Parameters["@woonplaats"].Value = p.Adres.Woonplaats;
                            cmdPersoneel.Parameters["@straat"].Value = p.Adres.Straatnaam;
                            cmdPersoneel.Parameters["@huisnummer"].Value = p.Adres.Huisnummer;
                            cmdPersoneel.Parameters["@postcode"].Value = p.Adres.Postcode;
                            cmdPersoneel.Parameters["@geboortedatum"].Value = p.Geboortedatum;
                            cmdPersoneel.ExecuteNonQuery();
                        }
                    }
                    transaction.Commit();
                }
                catch(Exception ex) { transaction.Rollback(); throw ex; }
            }
        }
    }
}
