using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data.Common;
using ReactAPI.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace ReactAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PruefplanController : ControllerBase
    {


        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;

        public PruefplanController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        [HttpGet]

        public JsonResult Get()
        {

            List<Pruefplan> PruefplanList = new List<Pruefplan>();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");


            try
            {
                using (SqlConnection con = new SqlConnection(sqlDataSource))
                {

                        {
                        con.Open();
                        DbCommand cmd = con.CreateCommand();
                        cmd.CommandText = "Select ID, NUMMER, BEZEICHNUNG1, LIEFERANTNUMMER, LIEFERANTENBEZEICHNUNG, PRUEFBEREICH, ZEICHNUNGSNUMMER, ZEICHNUNGSINDEX, " +
                            "CONVERT(VARCHAR(30),DatumVon,121) DatumVon, CONVERT(VARCHAR(30),DatumBis,121) DatumBis, PRUEFART, PRUEFPLATZ, KENNBUCHSTABE, " +
                            "PRUEFNIVEAU, AQL, PHOTOFILENAME, VERSION, STATUSPRUEFPLAN, ERSTELLUNGSBEMERKUNG, PERSONERSTELLUNG, CONVERT(VARCHAR(30),DATUMERSTELLUNG,121) DATUMERSTELLUNG, " +
                            "BEARBEITUNGSBEMERKUNG, PERSONBEARBEITUNG, CONVERT(VARCHAR(30),DATUMBEARBEITUNG,121) DATUMBEARBEITUNG, PRUEFBEMERKUNG, PRUEFPERSON, CONVERT(VARCHAR(30),DATUMPREUFUNG,121) DATUMPREUFUNG, FREIGABEBEMERKUNG," +
                            "FREIGABEPERSON, CONVERT(VARCHAR(30),DATUMFREIGABE,121) DATUMFREIGABE from dbo.Pruefplan";
                        DbDataReader dbDataReader = cmd.ExecuteReader();
                        while (dbDataReader.Read())
                        {
                            var Pruefplan = new Pruefplan();
                            Pruefplan.ID = int.Parse(dbDataReader["ID"].ToString());
                            Pruefplan.Nummer = dbDataReader["NUMMER"].ToString();
                            Pruefplan.Bezeichnung1 = dbDataReader["BEZEICHNUNG1"].ToString();
                            if (int.TryParse(dbDataReader["LIEFERANTNUMMER"].ToString(), out var lieferantennummer))
                            {
                                Pruefplan.LieferantenNummer = int.Parse(dbDataReader["LIEFERANTNUMMER"].ToString());
                            }
                            Pruefplan.LieferantenNummer = lieferantennummer;
                            Pruefplan.LieferantenSuchbegriff = dbDataReader["LIEFERANTENBEZEICHNUNG"].ToString();
                            Pruefplan.PruefBereich = dbDataReader["PRUEFBEREICH"].ToString();
                            Pruefplan.ZeichungsNummer = dbDataReader["ZEICHNUNGSNUMMER"].ToString();
                            Pruefplan.ZeichnungsIndex = dbDataReader["ZEICHNUNGSINDEX"].ToString();
                            Pruefplan.DatumVon = dbDataReader["DatumVon"].ToString();
                            Pruefplan.DatumBis = dbDataReader["DatumBis"].ToString();
                            Pruefplan.Pruefart = dbDataReader["PRUEFART"].ToString();
                            Pruefplan.Pruefplatz = dbDataReader["PRUEFPLATZ"].ToString();
                            Pruefplan.Kennbuchstabe = dbDataReader["KENNBUCHSTABE"].ToString();
                            Pruefplan.Bezeichnung1 = dbDataReader["BEZEICHNUNG1"].ToString();
                            if (int.TryParse(dbDataReader["PRUEFNIVEAU"].ToString(), out var pruefniveau))
                            {
                                Pruefplan.Pruefniveau = int.Parse(dbDataReader["PRUEFNIVEAU"].ToString());
                            }
                            Pruefplan.Pruefniveau = pruefniveau;
                            if (decimal.TryParse(dbDataReader["AQL"].ToString(), out var aqlwert))
                            {
                                Pruefplan.AQL = decimal.Parse(dbDataReader["AQL"].ToString());
                            }
                            Pruefplan.AQL = aqlwert;
                            Pruefplan.Version = dbDataReader["VERSION"].ToString();
                            Pruefplan.Statuspruefplan = dbDataReader["STATUSPRUEFPLAN"].ToString();
                            Pruefplan.Erstellungsbemerkung = dbDataReader["ERSTELLUNGSBEMERKUNG"].ToString();
                            Pruefplan.PersonErstellung = dbDataReader["PERSONERSTELLUNG"].ToString();
                            Pruefplan.DatumErstellung = dbDataReader["DATUMERSTELLUNG"].ToString();
                            Pruefplan.Bearbeitungsbemerkung = dbDataReader["BEARBEITUNGSBEMERKUNG"].ToString();
                            Pruefplan.PersonBearbeitung = dbDataReader["PERSONBEARBEITUNG"].ToString();
                            Pruefplan.DatumBearbeitung = dbDataReader["DATUMBEARBEITUNG"].ToString();
                            Pruefplan.Pruefungsbemerkung = dbDataReader["PRUEFBEMERKUNG"].ToString();
                            Pruefplan.PersonPruefung = dbDataReader["PRUEFPERSON"].ToString();
                            Pruefplan.DatumPruefung = dbDataReader["DATUMPREUFUNG"].ToString();
                            Pruefplan.Freigabebemerkung = dbDataReader["FREIGABEBEMERKUNG"].ToString();
                            Pruefplan.PersonFreigabe = dbDataReader["FREIGABEPERSON"].ToString();
                            Pruefplan.DatumFreigabe = dbDataReader["DATUMFREIGABE"].ToString();
                           
                            PruefplanList.Add(Pruefplan);
                        }



                        dbDataReader.Close();
                        con.Close();
                    }

                }

            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return new JsonResult(PruefplanList);

        }

        [HttpPost]

        public JsonResult Post(Pruefplan xpruefplan)
        {

            List<Pruefplan> PruefplanList = new List<Pruefplan>();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");


            try
            {
                using (SqlConnection con = new SqlConnection(sqlDataSource))
                {

                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("Insert into dbo.Pruefplan (NUMMER, BEZEICHNUNG1, LIEFERANTNUMMER, LIEFERANTENBEZEICHNUNG, PRUEFBEREICH, ZEICHNUNGSNUMMER, ZEICHNUNGSINDEX, " +
                            "DatumVon, DatumBis, PRUEFART, PRUEFPLATZ, KENNBUCHSTABE, " +
                            "PRUEFNIVEAU, AQL, PHOTOFILENAME, VERSION, STATUSPRUEFPLAN, ERSTELLUNGSBEMERKUNG, PERSONERSTELLUNG, DATUMERSTELLUNG, " +
                            "BEARBEITUNGSBEMERKUNG, PERSONBEARBEITUNG, DATUMBEARBEITUNG, PRUEFBEMERKUNG, PRUEFPERSON, DATUMPREUFUNG, FREIGABEBEMERKUNG," +
                            "FREIGABEPERSON, DATUMFREIGABE) values" +
                            "(@NUMMER, @BEZEICHNUNG1, @LIEFERANTNUMMER, @LIEFERANTENBEZEICHNUNG, @PRUEFBEREICH, @ZEICHNUNGSNUMMER, @ZEICHNUNGSINDEX, " +
                            "@DatumVon, @DatumBis, @PRUEFART, @PRUEFPLATZ, @KENNBUCHSTABE, @PRUEFNIVEAU, @AQL, @PHOTOFILENAME, @VERSION, @STATUSPRUEFPLAN, @ERSTELLUNGSBEMERKUNG, @PERSONERSTELLUNG, @DATUMERSTELLUNG, " +
                            "@BEARBEITUNGSBEMERKUNG, @PERSONBEARBEITUNG, @DATUMBEARBEITUNG, @PRUEFBEMERKUNG, @PRUEFPERSON, @DATUMPREUFUNG, @FREIGABEBEMERKUNG," +
                            "@FREIGABEPERSON, @DATUMFREIGABE)");
                        cmd.Parameters.AddWithValue("@NUMMER", xpruefplan.Nummer);
                        cmd.Parameters.AddWithValue("@BEZEICHNUNG1", xpruefplan.Bezeichnung1);
                        if (xpruefplan.LieferantenNummer != 0)
                            cmd.Parameters.AddWithValue("@LIEFERANTNUMMER", xpruefplan.LieferantenNummer);
                        else
                            cmd.Parameters.AddWithValue("@LIEFERANTNUMMER", DBNull.Value);

                        if (xpruefplan.LieferantenSuchbegriff != null)
                            cmd.Parameters.AddWithValue("@LIEFERANTENBEZEICHNUNG", xpruefplan.LieferantenSuchbegriff);
                        else
                            cmd.Parameters.AddWithValue("@LIEFERANTENBEZEICHNUNG", DBNull.Value);

                        if (xpruefplan.PruefBereich != null)
                            cmd.Parameters.AddWithValue("@PRUEFBEREICH", xpruefplan.PruefBereich);
                        else
                            cmd.Parameters.AddWithValue("@PRUEFBEREICH", DBNull.Value);

                        if (xpruefplan.ZeichungsNummer != null)
                            cmd.Parameters.AddWithValue("@ZEICHNUNGSNUMMER", xpruefplan.ZeichungsNummer);
                        else
                            cmd.Parameters.AddWithValue("@ZEICHNUNGSNUMMER", DBNull.Value);

                        if (xpruefplan.ZeichnungsIndex != null)
                            cmd.Parameters.AddWithValue("@ZEICHNUNGSINDEX", xpruefplan.ZeichnungsIndex);
                        else
                            cmd.Parameters.AddWithValue("@ZEICHNUNGSINDEX", DBNull.Value);

                        if (xpruefplan.DatumVon != null)
                            cmd.Parameters.AddWithValue("@DatumVon", xpruefplan.DatumVon);
                        else
                            cmd.Parameters.AddWithValue("@DatumVon", DBNull.Value);

                        if (xpruefplan.DatumBis != null)
                            cmd.Parameters.AddWithValue("@DatumBis", xpruefplan.DatumBis);
                        else
                            cmd.Parameters.AddWithValue("@DatumBis", DBNull.Value);

                        if (xpruefplan.Pruefart != null)
                            cmd.Parameters.AddWithValue("@PRUEFART", xpruefplan.Pruefart);
                        else
                            cmd.Parameters.AddWithValue("@PRUEFART", DBNull.Value);

                        if (xpruefplan.Pruefplatz != null)
                            cmd.Parameters.AddWithValue("@PRUEFPLATZ", xpruefplan.Pruefplatz);
                        else
                            cmd.Parameters.AddWithValue("@PRUEFPLATZ", DBNull.Value);

                        if (xpruefplan.Kennbuchstabe != null)
                            cmd.Parameters.AddWithValue("@KENNBUCHSTABE", xpruefplan.Kennbuchstabe);
                        else
                            cmd.Parameters.AddWithValue("@KENNBUCHSTABE", DBNull.Value);

                        if (xpruefplan.Pruefniveau != 0)
                            cmd.Parameters.AddWithValue("@PRUEFNIVEAU", xpruefplan.Pruefniveau);
                        else
                            cmd.Parameters.AddWithValue("@PRUEFNIVEAU", DBNull.Value);

                        if (xpruefplan.AQL != 0)
                            cmd.Parameters.AddWithValue("@AQL", xpruefplan.AQL);
                        else
                            cmd.Parameters.AddWithValue("@AQL", DBNull.Value);

                        if (xpruefplan.PhtotFileName != null)
                            cmd.Parameters.AddWithValue("@PHOTOFILENAME", xpruefplan.PhtotFileName);
                        else
                            cmd.Parameters.AddWithValue("@PHOTOFILENAME", DBNull.Value);

                        if (xpruefplan.Version != null)
                            cmd.Parameters.AddWithValue("@VERSION", xpruefplan.Version);
                        else
                            cmd.Parameters.AddWithValue("@VERSION", DBNull.Value);

                        if (xpruefplan.Statuspruefplan != null)
                            cmd.Parameters.AddWithValue("@STATUSPRUEFPLAN", xpruefplan.Statuspruefplan);
                        else
                            cmd.Parameters.AddWithValue("@STATUSPRUEFPLAN", DBNull.Value);

                        if (xpruefplan.Erstellungsbemerkung != null)
                            cmd.Parameters.AddWithValue("@ERSTELLUNGSBEMERKUNG", xpruefplan.Erstellungsbemerkung);
                        else
                            cmd.Parameters.AddWithValue("@ERSTELLUNGSBEMERKUNG", DBNull.Value);

                        if (xpruefplan.PersonErstellung != null)
                            cmd.Parameters.AddWithValue("@PERSONERSTELLUNG", xpruefplan.PersonErstellung);
                        else
                            cmd.Parameters.AddWithValue("@PERSONERSTELLUNG", DBNull.Value);

                        if (xpruefplan.DatumErstellung != null)
                            cmd.Parameters.AddWithValue("@DATUMERSTELLUNG", xpruefplan.DatumErstellung);
                        else
                            cmd.Parameters.AddWithValue("@DATUMERSTELLUNG", DBNull.Value);

                        if (xpruefplan.Bearbeitungsbemerkung != null)
                            cmd.Parameters.AddWithValue("@BEARBEITUNGSBEMERKUNG", xpruefplan.Bearbeitungsbemerkung);
                        else
                            cmd.Parameters.AddWithValue("@BEARBEITUNGSBEMERKUNG", DBNull.Value);

                        if (xpruefplan.PersonBearbeitung != null)
                            cmd.Parameters.AddWithValue("@PERSONBEARBEITUNG", xpruefplan.PersonBearbeitung);
                        else
                            cmd.Parameters.AddWithValue("@PERSONBEARBEITUNG", DBNull.Value);

                        if (xpruefplan.DatumBearbeitung != null)
                            cmd.Parameters.AddWithValue("@DATUMBEARBEITUNG", xpruefplan.DatumBearbeitung);
                        else
                            cmd.Parameters.AddWithValue("@DATUMBEARBEITUNG", DBNull.Value);

                        if (xpruefplan.Pruefungsbemerkung != null)
                            cmd.Parameters.AddWithValue("@PRUEFBEMERKUNG", xpruefplan.Pruefungsbemerkung);
                        else
                            cmd.Parameters.AddWithValue("@PRUEFBEMERKUNG", DBNull.Value);

                        if (xpruefplan.PersonPruefung != null)
                            cmd.Parameters.AddWithValue("@PRUEFPERSON", xpruefplan.PersonPruefung);
                        else
                            cmd.Parameters.AddWithValue("@PRUEFPERSON", DBNull.Value);

                        if (xpruefplan.DatumPruefung != null)
                            cmd.Parameters.AddWithValue("@DATUMPREUFUNG", xpruefplan.DatumPruefung);
                        else
                            cmd.Parameters.AddWithValue("@DATUMPREUFUNG", DBNull.Value);

                        if (xpruefplan.Freigabebemerkung != null)
                            cmd.Parameters.AddWithValue("@FREIGABEBEMERKUNG", xpruefplan.Freigabebemerkung);
                        else
                            cmd.Parameters.AddWithValue("@FREIGABEBEMERKUNG", DBNull.Value);

                        if (xpruefplan.PersonFreigabe != null)
                            cmd.Parameters.AddWithValue("@FREIGABEPERSON", xpruefplan.PersonFreigabe);
                        else
                            cmd.Parameters.AddWithValue("@FREIGABEPERSON", DBNull.Value);

                        if (xpruefplan.DatumFreigabe != null)
                            cmd.Parameters.AddWithValue("@DATUMFREIGABE", xpruefplan.DatumFreigabe);
                        else
                            cmd.Parameters.AddWithValue("@DATUMFREIGABE", DBNull.Value);

                        cmd.Connection = con;
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }

                }

            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return new JsonResult("Insert succesfully");

        }

        [HttpPut]

        public JsonResult Put(Pruefplan xpruefplan)
        {

            List<Pruefplan> PruefplanList = new List<Pruefplan>();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");


            try
            {
                using (SqlConnection con = new SqlConnection(sqlDataSource))
                {

                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("Update dbo.Pruefplan set NUMMER=@NUMMER, BEZEICHNUNG1=@BEZEICHNUNG1, LIEFERANTNUMMER=@LIEFERANTNUMMER, LIEFERANTENBEZEICHNUNG=@LIEFERANTENBEZEICHNUNG, PRUEFBEREICH=@PRUEFBEREICH, ZEICHNUNGSNUMMER=@ZEICHNUNGSNUMMER, ZEICHNUNGSINDEX=@ZEICHNUNGSINDEX, DatumVon=@DatumVon, DatumBis=@DatumBis, PRUEFART=@PRUEFART, PRUEFPLATZ=@PRUEFPLATZ, KENNBUCHSTABE=@KENNBUCHSTABE, PRUEFNIVEAU=@PRUEFNIVEAU, AQL=@AQL, PHOTOFILENAME=@PHOTOFILENAME, " +
                            "VERSION=@VERSION, STATUSPRUEFPLAN=@STATUSPRUEFPLAN, ERSTELLUNGSBEMERKUNG=@ERSTELLUNGSBEMERKUNG, PERSONERSTELLUNG=@PERSONERSTELLUNG, DATUMERSTELLUNG=@DATUMERSTELLUNG, " +
                            "BEARBEITUNGSBEMERKUNG=@BEARBEITUNGSBEMERKUNG, PERSONBEARBEITUNG=@PERSONBEARBEITUNG, DATUMBEARBEITUNG=@DATUMBEARBEITUNG, PRUEFBEMERKUNG=@PRUEFBEMERKUNG, PRUEFPERSON=@PRUEFPERSON, DATUMPREUFUNG=@DATUMPREUFUNG, FREIGABEBEMERKUNG=@FREIGABEBEMERKUNG," +
                            "FREIGABEPERSON=@FREIGABEPERSON, DATUMFREIGABE=@DATUMFREIGABE where ID=@ID");

                        if (xpruefplan.ID != 0)
                            cmd.Parameters.AddWithValue("@ID", xpruefplan.ID);
                        else
                            cmd.Parameters.AddWithValue("@ID", DBNull.Value);

                        if (xpruefplan.Nummer != null)
                            cmd.Parameters.AddWithValue("@NUMMER", xpruefplan.Nummer);
                        else
                            cmd.Parameters.AddWithValue("@NUMMER", DBNull.Value);

                        if (xpruefplan.Bezeichnung1 != null)
                            cmd.Parameters.AddWithValue("@BEZEICHNUNG1", xpruefplan.Bezeichnung1);
                        else
                            cmd.Parameters.AddWithValue("@BEZEICHNUNG1", DBNull.Value);

                        if (xpruefplan.LieferantenNummer != 0)
                            cmd.Parameters.AddWithValue("@LIEFERANTNUMMER", xpruefplan.LieferantenNummer);
                        else
                            cmd.Parameters.AddWithValue("@LIEFERANTNUMMER", DBNull.Value);

                        if (xpruefplan.LieferantenSuchbegriff != null)
                            cmd.Parameters.AddWithValue("@LIEFERANTENBEZEICHNUNG", xpruefplan.LieferantenSuchbegriff);
                        else
                            cmd.Parameters.AddWithValue("@LIEFERANTENBEZEICHNUNG", DBNull.Value);

                        if (xpruefplan.PruefBereich != null)
                            cmd.Parameters.AddWithValue("@PRUEFBEREICH", xpruefplan.PruefBereich);
                        else
                            cmd.Parameters.AddWithValue("@PRUEFBEREICH", DBNull.Value);

                        if (xpruefplan.ZeichungsNummer != null)
                            cmd.Parameters.AddWithValue("@ZEICHNUNGSNUMMER", xpruefplan.ZeichungsNummer);
                        else
                            cmd.Parameters.AddWithValue("@ZEICHNUNGSNUMMER", DBNull.Value);

                        if (xpruefplan.ZeichnungsIndex != null)
                            cmd.Parameters.AddWithValue("@ZEICHNUNGSINDEX", xpruefplan.ZeichnungsIndex);
                        else
                            cmd.Parameters.AddWithValue("@ZEICHNUNGSINDEX", DBNull.Value);

                        if (xpruefplan.DatumVon != null)
                            cmd.Parameters.AddWithValue("@DatumVon", xpruefplan.DatumVon);
                        else
                            cmd.Parameters.AddWithValue("@DatumVon", DBNull.Value);

                        if (xpruefplan.DatumBis != null)
                            cmd.Parameters.AddWithValue("@DatumBis", xpruefplan.DatumBis);
                        else
                            cmd.Parameters.AddWithValue("@DatumBis", DBNull.Value);

                        if (xpruefplan.Pruefart != null)
                            cmd.Parameters.AddWithValue("@PRUEFART", xpruefplan.Pruefart);
                        else
                            cmd.Parameters.AddWithValue("@PRUEFART", DBNull.Value);

                        if (xpruefplan.Pruefplatz != null)
                            cmd.Parameters.AddWithValue("@PRUEFPLATZ", xpruefplan.Pruefplatz);
                        else
                            cmd.Parameters.AddWithValue("@PRUEFPLATZ", DBNull.Value);

                        if (xpruefplan.Kennbuchstabe != null)
                            cmd.Parameters.AddWithValue("@KENNBUCHSTABE", xpruefplan.Kennbuchstabe);
                        else
                            cmd.Parameters.AddWithValue("@KENNBUCHSTABE", DBNull.Value);

                        if (xpruefplan.Pruefniveau != 0)
                            cmd.Parameters.AddWithValue("@PRUEFNIVEAU", xpruefplan.Pruefniveau);
                        else
                            cmd.Parameters.AddWithValue("@PRUEFNIVEAU", DBNull.Value);

                        if (xpruefplan.AQL != 0)
                            cmd.Parameters.AddWithValue("@AQL", xpruefplan.AQL);
                        else
                            cmd.Parameters.AddWithValue("@AQL", DBNull.Value);

                        if (xpruefplan.PhtotFileName != null)
                            cmd.Parameters.AddWithValue("@PHOTOFILENAME", xpruefplan.PhtotFileName);
                        else
                            cmd.Parameters.AddWithValue("@PHOTOFILENAME", DBNull.Value);

                        if (xpruefplan.Version != null)
                            cmd.Parameters.AddWithValue("@VERSION", xpruefplan.Version);
                        else
                            cmd.Parameters.AddWithValue("@VERSION", DBNull.Value);

                        if (xpruefplan.Statuspruefplan != null)
                            cmd.Parameters.AddWithValue("@STATUSPRUEFPLAN", xpruefplan.Statuspruefplan);
                        else
                            cmd.Parameters.AddWithValue("@STATUSPRUEFPLAN", DBNull.Value);

                        if (xpruefplan.Erstellungsbemerkung != null)
                            cmd.Parameters.AddWithValue("@ERSTELLUNGSBEMERKUNG", xpruefplan.Erstellungsbemerkung);
                        else
                            cmd.Parameters.AddWithValue("@ERSTELLUNGSBEMERKUNG", DBNull.Value);

                        if (xpruefplan.PersonErstellung != null)
                            cmd.Parameters.AddWithValue("@PERSONERSTELLUNG", xpruefplan.PersonErstellung);
                        else
                            cmd.Parameters.AddWithValue("@PERSONERSTELLUNG", DBNull.Value);

                        if (xpruefplan.DatumErstellung != null)
                            cmd.Parameters.AddWithValue("@DATUMERSTELLUNG", xpruefplan.DatumErstellung);
                        else
                            cmd.Parameters.AddWithValue("@DATUMERSTELLUNG", DBNull.Value);

                        if (xpruefplan.Bearbeitungsbemerkung != null)
                            cmd.Parameters.AddWithValue("@BEARBEITUNGSBEMERKUNG", xpruefplan.Bearbeitungsbemerkung);
                        else
                            cmd.Parameters.AddWithValue("@BEARBEITUNGSBEMERKUNG", DBNull.Value);

                        if (xpruefplan.PersonBearbeitung != null)
                            cmd.Parameters.AddWithValue("@PERSONBEARBEITUNG", xpruefplan.PersonBearbeitung);
                        else
                            cmd.Parameters.AddWithValue("@PERSONBEARBEITUNG", DBNull.Value);

                        if (xpruefplan.DatumBearbeitung != null)
                            cmd.Parameters.AddWithValue("@DATUMBEARBEITUNG", xpruefplan.DatumBearbeitung);
                        else
                            cmd.Parameters.AddWithValue("@DATUMBEARBEITUNG", DBNull.Value);

                        if (xpruefplan.Pruefungsbemerkung != null)
                            cmd.Parameters.AddWithValue("@PRUEFBEMERKUNG", xpruefplan.Pruefungsbemerkung);
                        else
                            cmd.Parameters.AddWithValue("@PRUEFBEMERKUNG", DBNull.Value);

                        if (xpruefplan.PersonPruefung != null)
                            cmd.Parameters.AddWithValue("@PRUEFPERSON", xpruefplan.PersonPruefung);
                        else
                            cmd.Parameters.AddWithValue("@PRUEFPERSON", DBNull.Value);

                        if (xpruefplan.DatumPruefung != null)
                            cmd.Parameters.AddWithValue("@DATUMPREUFUNG", xpruefplan.DatumPruefung);
                        else
                            cmd.Parameters.AddWithValue("@DATUMPREUFUNG", DBNull.Value);

                        if (xpruefplan.Freigabebemerkung != null && xpruefplan.Statuspruefplan != "Gesperrt")
                            cmd.Parameters.AddWithValue("@FREIGABEBEMERKUNG", xpruefplan.Freigabebemerkung);
                        else
                            cmd.Parameters.AddWithValue("@FREIGABEBEMERKUNG", DBNull.Value);

                        if (xpruefplan.PersonFreigabe != null && xpruefplan.Statuspruefplan != "Gesperrt")
                            cmd.Parameters.AddWithValue("@FREIGABEPERSON", xpruefplan.PersonFreigabe);
                        else
                            cmd.Parameters.AddWithValue("@FREIGABEPERSON", DBNull.Value);

                        if (xpruefplan.DatumFreigabe != null && xpruefplan.Statuspruefplan !="Gesperrt")
                            cmd.Parameters.AddWithValue("@DATUMFREIGABE", xpruefplan.DatumFreigabe);
                        else
                            cmd.Parameters.AddWithValue("@DATUMFREIGABE", DBNull.Value);
                  
                        cmd.Connection = con;
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }

                }

            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return new JsonResult("Update succesfully");


        }

        [HttpDelete("{id}")]

        public JsonResult Delete(int id)
        {

            List<Pruefplan> PruefplanList = new List<Pruefplan>();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");


            try
            {
                using (SqlConnection con = new SqlConnection(sqlDataSource))
                {

                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("Delete from dbo.Pruefplan  where ID=@ID");
                        cmd.Parameters.AddWithValue("@ID", id);
                        cmd.Connection = con;
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }

                }

            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return new JsonResult("Delete succesfully");

        }

        [Route("SaveFile")]
        [HttpPost]

        public JsonResult SaveFile()
        {
            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string filename = postedFile.FileName;
                var physicalPath = _env.ContentRootPath + "/Photo/" + filename;

                using( var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }

                return new JsonResult(filename);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                
            }

            return new JsonResult("anonynous.png");
        }
    }
}
