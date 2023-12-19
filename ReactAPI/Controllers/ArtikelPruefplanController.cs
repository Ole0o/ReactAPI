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
using System.Net.Http;
using Newtonsoft.Json;

namespace ReactAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtikelPruefplanController : ControllerBase
    {

        private readonly IConfiguration _configuration;

        public ArtikelPruefplanController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
        
        }



        private List<Artikel> LoadArtikel()
        {
           
            var ArtikelList = new List<Artikel>();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");


            try
            {
                using (SqlConnection con = new SqlConnection(sqlDataSource))
                {

                    {
                        con.Open();
                        DbCommand cmd = con.CreateCommand();
                        cmd.CommandText = "Select ID, NUMMER, SUCHBEGRIFF, BEZEICHNUNG1, BEZEICHNUNG2, BEZEICHNUNG3, BEZEICHNUNGT, LAGERPLATZ, MAßEINHEIT, " +
                            "CONVERT(VARCHAR(30),DATUMNEU,121) DATUMNEU, CONVERT(VARCHAR(30),DATUMEDIT,121) DATUMEDIT, REVISIONSSTAND, CONVERT(VARCHAR(30),REVISIONSDATUM,121) REVISIONSDATUM from dbo.Artikel";
                        DbDataReader dbDataReader = cmd.ExecuteReader();
                        while (dbDataReader.Read())
                        {
                            var Artikel = new Artikel();
                            Artikel.ID = int.Parse(dbDataReader["ID"].ToString());
                            Artikel.Nummer = dbDataReader["NUMMER"].ToString();
                            Artikel.Suchbegriff = dbDataReader["SUCHBEGRIFF"].ToString();
                            Artikel.Bezeichnung1 = dbDataReader["BEZEICHNUNG1"].ToString();
                            Artikel.Bezeichnung2 = dbDataReader["BEZEICHNUNG2"].ToString();
                            Artikel.Bezeichnung3 = dbDataReader["BEZEICHNUNG3"].ToString();
                            Artikel.BezeichnungT = dbDataReader["BEZEICHNUNGT"].ToString();
                            Artikel.Lagerplatz = dbDataReader["LAGERPLATZ"].ToString();
                            Artikel.Mengeneinheit = dbDataReader["MAßEINHEIT"].ToString();
                            Artikel.DatumNeu = dbDataReader["DATUMNEU"].ToString();
                            Artikel.DatumEdit = dbDataReader["DATUMEDIT"].ToString();
                            Artikel.Revisionsstand = dbDataReader["REVISIONSSTAND"].ToString();
                            Artikel.Revisionsdatum = dbDataReader["REVISIONSDATUM"].ToString();
                            ArtikelList.Add(Artikel);
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

            return ArtikelList;


        }

        private List<Pruefplan> LoadPruefplan()
        {

            var PruefplanList = new List<Pruefplan>();
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
                            Pruefplan.Pruefniveau = int.Parse(dbDataReader["PRUEFNIVEAU"].ToString());
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

            return PruefplanList;


        }

        [HttpGet]
        public JsonResult Get()
        {

            List<ArtikelPruefplanResponse> PruefplanArtikelList = new List<ArtikelPruefplanResponse>();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");


            try
            {
                using (SqlConnection con = new SqlConnection(sqlDataSource))
                {

                    {
                        con.Open();
                        DbCommand cmd = con.CreateCommand();
                        cmd.CommandText = "Select ID, IDPRUEFPLAN, IDARTIKEL, CONVERT(VARCHAR(30),DATUMNEU,121) DATUMNEU, CONVERT(VARCHAR(30),DATUMEDIT,121) DATUMEDIT, ARTIKELNUMMER, ARTIKELSUCHBEGRIFF, PRUEFPLANNUMMER, PRUEFPLANSUCHBEGRIFF from dbo.ArtikelPruefplan";
                        DbDataReader dbDataReader = cmd.ExecuteReader();
                        while (dbDataReader.Read())
                        {
                            var PruefplanArtikelItem = new ArtikelPruefplanResponse();
                            PruefplanArtikelItem.ID = int.Parse(dbDataReader["ID"].ToString());
                            PruefplanArtikelItem.IDPruefplan = int.Parse(dbDataReader["IDPRUEFPLAN"].ToString());
                            PruefplanArtikelItem.IDArtikel = int.Parse(dbDataReader["IDARTIKEL"].ToString());
                            PruefplanArtikelItem.DatumNeu = dbDataReader["DATUMNEU"].ToString();
                            PruefplanArtikelItem.DatumEdit = dbDataReader["DATUMEDIT"].ToString();
                            PruefplanArtikelItem.ArtikelNummer = dbDataReader["ARTIKELNUMMER"].ToString();
                            PruefplanArtikelItem.ArtikelSuchbegriff = dbDataReader["ARTIKELSUCHBEGRIFF"].ToString();
                            PruefplanArtikelItem.PruefplanNummer = dbDataReader["PRUEFPLANNUMMER"].ToString();
                            PruefplanArtikelItem.PruefplanSuchbegriff = dbDataReader["PRUEFPLANSUCHBEGRIFF"].ToString();
                            PruefplanArtikelList.Add(PruefplanArtikelItem);
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

            return new JsonResult(PruefplanArtikelList);

        }

        [HttpPost]

        public JsonResult Post(ArtikelPruefplanPostRequest xpostreqeust)
        {
            //var responseArtikelPrueflanList = new List<ArtikelPruefplanResponse>();
            var ArtikelList = LoadArtikel();
            var PruefplanList = LoadPruefplan();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");

            var data = new ArtikelPruefplanPostRequest();
            data.IDPruefplan = int.Parse((xpostreqeust.IDPruefplan).ToString());
            data.IDArtikel = int.Parse((xpostreqeust.IDArtikel).ToString());
            
            var artikelitem = ArtikelList.FirstOrDefault(xF => xF.ID == data.IDArtikel);
            if (artikelitem == null)
            {
                return new JsonResult(new List<ArtikelPruefplanPostRequest>());
            }

            var pruefplanitem = PruefplanList.FirstOrDefault(xF => xF.ID == data.IDPruefplan);
            if (pruefplanitem == null)
            {
                return new JsonResult(new List<ArtikelPruefplanPostRequest>());
            }

            var responseArtikelPruefplanItem = new ArtikelPruefplanResponse();
            responseArtikelPruefplanItem.IDArtikel = artikelitem.ID;
            responseArtikelPruefplanItem.ArtikelNummer = artikelitem.Nummer;
            responseArtikelPruefplanItem.ArtikelSuchbegriff = artikelitem.Suchbegriff;
            responseArtikelPruefplanItem.IDPruefplan = pruefplanitem.ID;
            responseArtikelPruefplanItem.PruefplanNummer = pruefplanitem.Nummer;
            responseArtikelPruefplanItem.PruefplanSuchbegriff = pruefplanitem.Bezeichnung1;
            responseArtikelPruefplanItem.DatumNeu = DateTime.Now.ToString();
            responseArtikelPruefplanItem.DatumEdit = DateTime.Now.ToString();
            //responseArtikelPrueflanList.Add(responseArtikelPruefplanItem);
                        

            using (SqlConnection con = new SqlConnection(sqlDataSource))
            {

                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Insert into dbo.ArtikelPruefplan (IDPRUEFPLAN, IDARTIKEL, DATUMNEU, DATUMEDIT, ARTIKELNUMMER, ARTIKELSUCHBEGRIFF, PRUEFPLANNUMMER, PRUEFPLANSUCHBEGRIFF) values" +
                        "(@IDPRUEFPLAN, @IDARTIKEL, @DATUMNEU, @DATUMEDIT, @ARTIKELNUMMER, @ARTIKELSUCHBEGRIFF, @PRUEFPLANNUMMER, @PRUEFPLANSUCHBEGRIFF)");
     
                    if (responseArtikelPruefplanItem.IDArtikel != 0)
                        cmd.Parameters.AddWithValue("@IDARTIKEL", responseArtikelPruefplanItem.IDArtikel);
                    else
                        cmd.Parameters.AddWithValue("@IDARTIKEL", DBNull.Value);

                    if (responseArtikelPruefplanItem.IDPruefplan != 0)
                        cmd.Parameters.AddWithValue("@IDPRUEFPLAN", responseArtikelPruefplanItem.IDPruefplan);
                    else
                        cmd.Parameters.AddWithValue("@IDPRUEFPLAN", DBNull.Value);

                    cmd.Parameters.AddWithValue("@DATUMNEU", responseArtikelPruefplanItem.DatumNeu);
                    cmd.Parameters.AddWithValue("@DATUMEDIT", responseArtikelPruefplanItem.DatumEdit);

                    if (responseArtikelPruefplanItem.ArtikelNummer != null)
                        cmd.Parameters.AddWithValue("@ARTIKELNUMMER", responseArtikelPruefplanItem.ArtikelNummer);
                    else
                        cmd.Parameters.AddWithValue("@ARTIKELNUMMER", DBNull.Value);

                    if (responseArtikelPruefplanItem.ArtikelSuchbegriff != null)
                        cmd.Parameters.AddWithValue("@ARTIKELSUCHBEGRIFF", responseArtikelPruefplanItem.ArtikelSuchbegriff);
                    else
                        cmd.Parameters.AddWithValue("@ARTIKELSUCHBEGRIFF", DBNull.Value);

                    if (responseArtikelPruefplanItem.PruefplanNummer != null)
                        cmd.Parameters.AddWithValue("@PRUEFPLANNUMMER", responseArtikelPruefplanItem.PruefplanNummer);
                    else
                        cmd.Parameters.AddWithValue("@PRUEFPLANNUMMER", DBNull.Value);

                    if (responseArtikelPruefplanItem.PruefplanSuchbegriff != null)
                        cmd.Parameters.AddWithValue("@PRUEFPLANSUCHBEGRIFF", responseArtikelPruefplanItem.PruefplanSuchbegriff);
                    else
                        cmd.Parameters.AddWithValue("@PRUEFPLANSUCHBEGRIFF", DBNull.Value);

                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

            }
            return new JsonResult("Insert succesfully");

        }




    }
}

