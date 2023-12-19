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
    public class ArtikelController : ControllerBase
    {


        private readonly IConfiguration _configuration;
     
        public ArtikelController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]

        public JsonResult Get()
        {

            List<Artikel> ArtikelList = new List<Artikel>();
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

            return new JsonResult(ArtikelList);

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
                            "PRUEFNIVEAU, AQL, PHOTOFILENAME) values" +
                            "(@NUMMER, @BEZEICHNUNG1, @LIEFERANTNUMMER, @LIEFERANTENBEZEICHNUNG, @PRUEFBEREICH, @ZEICHNUNGSNUMMER, @ZEICHNUNGSINDEX, " +
                            "@DatumVon, @DatumBis, @PRUEFART, @PRUEFPLATZ, @KENNBUCHSTABE, @PRUEFNIVEAU, @AQL, @PHOTOFILENAME)");
                        cmd.Parameters.AddWithValue("@NUMMER", xpruefplan.Nummer);
                        cmd.Parameters.AddWithValue("@BEZEICHNUNG1", xpruefplan.Bezeichnung1);
                        if (xpruefplan.LieferantenNummer == 0)
                        {
                            var lieferantennummer = PruefplanList.FirstOrDefault(xF => xF.ID == xpruefplan.ID && xpruefplan.LieferantenNummer != 0);
                            cmd.Parameters.AddWithValue("@LIEFERANTNUMMER", lieferantennummer);
                        }

                        else
                        {
                            cmd.Parameters.AddWithValue("@LIEFERANTNUMMER", xpruefplan.LieferantenNummer);
                        }
                        cmd.Parameters.AddWithValue("@LIEFERANTENBEZEICHNUNG", xpruefplan.LieferantenSuchbegriff);
                        cmd.Parameters.AddWithValue("@PRUEFBEREICH", xpruefplan.PruefBereich);
                        cmd.Parameters.AddWithValue("@ZEICHNUNGSNUMMER", xpruefplan.ZeichungsNummer);
                        cmd.Parameters.AddWithValue("@ZEICHNUNGSINDEX", xpruefplan.ZeichnungsIndex);
                        cmd.Parameters.AddWithValue("@DatumVon", xpruefplan.DatumVon);
                        cmd.Parameters.AddWithValue("@DatumBis", xpruefplan.DatumBis);
                        cmd.Parameters.AddWithValue("@PRUEFART", xpruefplan.Pruefart);
                        cmd.Parameters.AddWithValue("@PRUEFPLATZ", xpruefplan.Pruefplatz);
                        cmd.Parameters.AddWithValue("@KENNBUCHSTABE", xpruefplan.Kennbuchstabe);
                        cmd.Parameters.AddWithValue("@PRUEFNIVEAU", xpruefplan.Pruefniveau);
                        cmd.Parameters.AddWithValue("@AQL", xpruefplan.AQL);
                        cmd.Parameters.AddWithValue("@PHOTOFILENAME", xpruefplan.PhtotFileName);
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
                        SqlCommand cmd = new SqlCommand("Update dbo.Pruefplan set NUMMER=@NUMMER, BEZEICHNUNG1=@BEZEICHNUNG1, LIEFERANTNUMMER=@LIEFERANTNUMMER, LIEFERANTENBEZEICHNUNG=@LIEFERANTENBEZEICHNUNG, PRUEFBEREICH=@PRUEFBEREICH, ZEICHNUNGSNUMMER=@ZEICHNUNGSNUMMER, ZEICHNUNGSINDEX=@ZEICHNUNGSINDEX, DatumVon=@DatumVon, DatumBis=@DatumBis, PRUEFART=@PRUEFART, PRUEFPLATZ=@PRUEFPLATZ, KENNBUCHSTABE=@KENNBUCHSTABE, PRUEFNIVEAU=@PRUEFNIVEAU, AQL=@AQL, PHOTOFILENAME=@PHOTOFILENAME where ID=@ID");
                        cmd.Parameters.AddWithValue("@ID", xpruefplan.ID);
                        cmd.Parameters.AddWithValue("@NUMMER", xpruefplan.Nummer);
                        cmd.Parameters.AddWithValue("@BEZEICHNUNG1", xpruefplan.Bezeichnung1);
                        if (xpruefplan.LieferantenNummer == 0)
                        {
                            var lieferantennummer = PruefplanList.FirstOrDefault(xF => xF.ID == xpruefplan.ID && xpruefplan.LieferantenNummer != 0);
                            cmd.Parameters.AddWithValue("@LIEFERANTNUMMER", lieferantennummer);
                        }

                        else
                        {
                            cmd.Parameters.AddWithValue("@LIEFERANTNUMMER", xpruefplan.LieferantenNummer);
                        }
                        cmd.Parameters.AddWithValue("@LIEFERANTENBEZEICHNUNG", xpruefplan.LieferantenSuchbegriff);
                        cmd.Parameters.AddWithValue("@PRUEFBEREICH", xpruefplan.PruefBereich);
                        cmd.Parameters.AddWithValue("@ZEICHNUNGSNUMMER", xpruefplan.ZeichungsNummer);
                        cmd.Parameters.AddWithValue("@ZEICHNUNGSINDEX", xpruefplan.ZeichnungsIndex);
                        cmd.Parameters.AddWithValue("@DatumVon", xpruefplan.DatumVon);
                        cmd.Parameters.AddWithValue("@DatumBis", xpruefplan.DatumBis);
                        cmd.Parameters.AddWithValue("@PRUEFART", xpruefplan.Pruefart);
                        cmd.Parameters.AddWithValue("@PRUEFPLATZ", xpruefplan.Pruefplatz);
                        cmd.Parameters.AddWithValue("@KENNBUCHSTABE", xpruefplan.Kennbuchstabe);
                        cmd.Parameters.AddWithValue("@PRUEFNIVEAU", xpruefplan.Pruefniveau);
                        cmd.Parameters.AddWithValue("@AQL", xpruefplan.AQL);
                        cmd.Parameters.AddWithValue("@PHOTOFILENAME", xpruefplan.PhtotFileName);
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
    }
}
