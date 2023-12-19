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
    public class WareneingangController : ControllerBase
    {

        private readonly IConfiguration _configuration;
       


        public WareneingangController(IConfiguration configuration)
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

        private List<PruefplanPos> LoadPruefplanPositionen()
        {

            var PruefplanPosList = new List<PruefplanPos>();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");


            try
            {
                using (SqlConnection con = new SqlConnection(sqlDataSource))
                {

                    {
                        con.Open();
                        DbCommand cmd = con.CreateCommand();
                        cmd.CommandText = "Select ID, IDPRUEFPLAN, PRUEFMERKMAL, MERKMALSART, POSITIONSNUMMER, KUERZEL , BEZEICHNUNG1, BEZEICHNUNG2, BEZEICHNUNG3, BEZEICHNUNGT, " +
                            " NENNMAß, MAßEINHEIT, OBERETOLERANZ, UNTERETOLERANZ, CONVERT(VARCHAR(30),DATUMEDIT,121) DATUMEDIT,CONVERT(VARCHAR(30),DATUMNEU,121) DATUMNEU, MESSMITTEL from dbo.Pruefplanpositionen";
                        DbDataReader dbDataReader = cmd.ExecuteReader();
                        while (dbDataReader.Read())
                        {
                            var Pruefplanpos = new PruefplanPos();
                            Pruefplanpos.ID = int.Parse(dbDataReader["ID"].ToString());
                            Pruefplanpos.IDPruefplan = int.Parse(dbDataReader["IDPRUEFPLAN"].ToString());
                            Pruefplanpos.Pruefmerkal = dbDataReader["PRUEFMERKMAL"].ToString();
                            Pruefplanpos.Merkmalsart = dbDataReader["MERKMALSART"].ToString();
                            Pruefplanpos.Positionsnummer = int.Parse(dbDataReader["POSITIONSNUMMER"].ToString());
                            Pruefplanpos.Kurzel = dbDataReader["KUERZEL"].ToString();
                            Pruefplanpos.Bezeichnung1 = dbDataReader["BEZEICHNUNG1"].ToString();
                            Pruefplanpos.Bezeichnung2 = dbDataReader["BEZEICHNUNG2"].ToString();
                            Pruefplanpos.Bezeichnung3 = dbDataReader["BEZEICHNUNG3"].ToString();
                            Pruefplanpos.BezeichnungT = dbDataReader["BEZEICHNUNGT"].ToString();
                            Pruefplanpos.Nennmaß = decimal.Parse(dbDataReader["NENNMAß"].ToString());
                            Pruefplanpos.Maßeinheit = dbDataReader["MAßEINHEIT"].ToString();
                            Pruefplanpos.Oberetoleranz = decimal.Parse(dbDataReader["OBERETOLERANZ"].ToString());
                            Pruefplanpos.Unteretoleranz = decimal.Parse(dbDataReader["UNTERETOLERANZ"].ToString());
                            Pruefplanpos.DatumNeu = dbDataReader["DATUMEDIT"].ToString();
                            Pruefplanpos.DatumEdit = dbDataReader["DATUMNEU"].ToString();
                            Pruefplanpos.Messmittel = dbDataReader["MESSMITTEL"].ToString();
                            PruefplanPosList.Add(Pruefplanpos);
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

            return PruefplanPosList;


        }

        private List<ArtikelPruefplanResponse> LoadPruefplanArtikel()
        {

            var PruefplanArtikelList = new List<ArtikelPruefplanResponse>();
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

            return PruefplanArtikelList;


        }

        private List<AQL> LoadAQL()
        {

            var AQLList = new List<AQL>();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");


            try
            {
                using (SqlConnection con = new SqlConnection(sqlDataSource))
                {

                    {
                        con.Open();
                        DbCommand cmd = con.CreateCommand();
                        cmd.CommandText = "Select ID, VONMENGE, BISMENGE, PRUEFNIVEAU, KENNBUCHSTABEN, AQL, STICHPROBENMENGE, ANNAHMEFHLER, RUECKFEHLER from dbo.AQL_2859";
                        DbDataReader dbDataReader = cmd.ExecuteReader();
                        while (dbDataReader.Read())
                        {
                            var AQL = new AQL();
                            AQL.ID = int.Parse(dbDataReader["ID"].ToString());
                            AQL.VonMenge = int.Parse(dbDataReader["VONMENGE"].ToString());
                            AQL.BisMenge = int.Parse(dbDataReader["BISMENGE"].ToString());
                            AQL.Pruefniveau = int.Parse(dbDataReader["PRUEFNIVEAU"].ToString());
                            AQL.Kennbuchstabe = dbDataReader["KENNBUCHSTABEN"].ToString();
                            AQL.aql = decimal.Parse(dbDataReader["AQL"].ToString());
                            AQL.Stichprobenmenge = int.Parse(dbDataReader["STICHPROBENMENGE"].ToString());
                            AQL.Annahmefehlermenge = int.Parse(dbDataReader["ANNAHMEFHLER"].ToString());
                            AQL.Rueckweisungsfehlermenge = int.Parse(dbDataReader["RUECKFEHLER"].ToString());
                            AQLList.Add(AQL);
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

            return AQLList;


        }

        private List<Wareneingangspruefpositionen> LoadWareneingangspositionen()
        {

            var WareneingangsposList = new List<Wareneingangspruefpositionen>();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");


            try
            {
                using (SqlConnection con = new SqlConnection(sqlDataSource))
                {

                    {
                        con.Open();
                        DbCommand cmd = con.CreateCommand();
                        cmd.CommandText = "Select ID, IDWENUMMER, IDPRUEFPLAN, IDPRUEFPLANPOSITION, PRUEFMERKMAL, MERKMALSART, POSITIONSNUMMER, KUERZEL , BEZEICHNUNG1, BEZEICHNUNG2, BEZEICHNUNG3, BEZEICHNUNGT, " +
                            " NENNMAß, MAßEINHEIT, OBERETOLERANZ, UNTERETOLERANZ, CONVERT(VARCHAR(30),DATUMEDIT,121) DATUMEDIT,CONVERT(VARCHAR(30),DATUMNEU,121) DATUMNEU, MESSMITTEL from dbo.Wareneingangspositionen";
                        DbDataReader dbDataReader = cmd.ExecuteReader();
                        while (dbDataReader.Read())
                        {
                            var Wareneingangsposition = new Wareneingangspruefpositionen();
                            Wareneingangsposition.ID = int.Parse(dbDataReader["ID"].ToString());
                            Wareneingangsposition.IDWareneingang = int.Parse(dbDataReader["IDWENUMMER"].ToString());
                            Wareneingangsposition.IDPruefplan = int.Parse(dbDataReader["IDPRUEFPLAN"].ToString());
                            Wareneingangsposition.IDPruefplanpos = int.Parse(dbDataReader["IDPRUEFPLANPOSITION"].ToString());
                            Wareneingangsposition.Pruefmerkal = dbDataReader["PRUEFMERKMAL"].ToString();
                            Wareneingangsposition.Merkmalsart = dbDataReader["MERKMALSART"].ToString();
                            Wareneingangsposition.Positionsnummer = int.Parse(dbDataReader["POSITIONSNUMMER"].ToString());
                            Wareneingangsposition.Kurzel = dbDataReader["KUERZEL"].ToString();
                            Wareneingangsposition.Bezeichnung1 = dbDataReader["BEZEICHNUNG1"].ToString();
                            Wareneingangsposition.Bezeichnung2 = dbDataReader["BEZEICHNUNG2"].ToString();
                            Wareneingangsposition.Bezeichnung3 = dbDataReader["BEZEICHNUNG3"].ToString();
                            Wareneingangsposition.BezeichnungT = dbDataReader["BEZEICHNUNGT"].ToString();
                            Wareneingangsposition.Nennmaß = decimal.Parse(dbDataReader["NENNMAß"].ToString());
                            Wareneingangsposition.Maßeinheit = dbDataReader["MAßEINHEIT"].ToString();
                            Wareneingangsposition.Oberetoleranz = decimal.Parse(dbDataReader["OBERETOLERANZ"].ToString());
                            Wareneingangsposition.Unteretoleranz = decimal.Parse(dbDataReader["UNTERETOLERANZ"].ToString());
                            Wareneingangsposition.DatumNeu = dbDataReader["DATUMEDIT"].ToString();
                            Wareneingangsposition.DatumEdit = dbDataReader["DATUMNEU"].ToString();
                            Wareneingangsposition.Messmittel = dbDataReader["MESSMITTEL"].ToString();
                            WareneingangsposList.Add(Wareneingangsposition);
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

            return WareneingangsposList;

        }

        private List<WareneingangsID> LoadWEID()
        {


            var WEIDList = new List<WareneingangsID>();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");

            try
            {
                using (SqlConnection con = new SqlConnection(sqlDataSource))
                {

                    {
                        con.Open();
                        DbCommand cmd = con.CreateCommand();
                        cmd.CommandText = "Select ID from dbo.Wareneingang WHERE WARENEINGANGSDATUM = (SELECT MAX(WARENEINGANGSDATUM) from dbo.Wareneingang)";
                        DbDataReader dbDataReader = cmd.ExecuteReader();
                        while (dbDataReader.Read())
                        {
                            var WeItem = new WareneingangsID();
                            WeItem.ID = int.Parse(dbDataReader["ID"].ToString());
                            WEIDList.Add(WeItem);
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

            return WEIDList;

        }

        public JsonResult Get()
        {

            List<Wareneingang> WareneingangList = new List<Wareneingang>();
            var responseWareneingangList = new List<WareneingangResponse>();
            var ArtikelList = LoadArtikel();
            var PruefplanList = LoadPruefplan();
            var PrufplanPosList = LoadPruefplanPositionen();
            var PruefplanArtikelList = LoadPruefplanArtikel();
            var AQLList = LoadAQL();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");


            try
            {
                using (SqlConnection con = new SqlConnection(sqlDataSource))
                {

                    {
                        con.Open();
                        DbCommand cmd = con.CreateCommand();
                        cmd.CommandText = "Select ID, WENUMMER, IDARTIKELNUMMER, ARTIKELNUMMER, ARTIKELBEZ1, PRÜFPLANZEICHUNGSNR, PRÜFPLANZEICHNUNGSINDEX, PRÜFPLANLIEFERANTENNUMMER, " +
                            "PRÜFPLANLIEFERANTENSUCHBEGRIFF, ARTIKELLAGERPLATZ, LIEFERMENGESOLL, " +
                            "LIEFERMENGEIST, ARTIKELMENGENEINHEIT, BESTELLNUMMER, CONVERT(VARCHAR(30),BESTELLDATUM,121) BESTELLDATUM, " +
                            "LIEFERSCHEINNUMMER, CONVERT(VARCHAR(30),LIEFERSCHEINDATUM,121) LIEFERSCHEINDATUM, CONVERT(VARCHAR(30),WARENEINGANGSDATUM,121) WARENEINGANGSDATUM, PRUEFSTATUS from dbo.Wareneingang";
                        DbDataReader dbDataReader = cmd.ExecuteReader();
                        while (dbDataReader.Read())
                        {
                            var WeItem = new Wareneingang();
                            WeItem.ID = int.Parse(dbDataReader["ID"].ToString());
                            WeItem.WENummer = dbDataReader["WENUMMER"].ToString();
                            WeItem.IDArtikelNummer = int.Parse(dbDataReader["IDARTIKELNUMMER"].ToString());
                            WeItem.ArtikelNummer = dbDataReader["ARTIKELNUMMER"].ToString();
                            WeItem.ArtikelBez1 = dbDataReader["ARTIKELBEZ1"].ToString();
                            WeItem.PrüfplanZeichnungNummer = dbDataReader["PRÜFPLANZEICHUNGSNR"].ToString();
                            WeItem.PrüfplanZeichnungIndex = dbDataReader["PRÜFPLANZEICHNUNGSINDEX"].ToString();
                            WeItem.PrüfplanZeichnungLieferantenNummer = int.Parse(dbDataReader["PRÜFPLANLIEFERANTENNUMMER"].ToString());
                            WeItem.PrüfplanZeichnungLieferantenSuchbegriff = dbDataReader["PRÜFPLANLIEFERANTENSUCHBEGRIFF"].ToString();
                            WeItem.ArtikelLagerplatz = dbDataReader["ARTIKELLAGERPLATZ"].ToString();
                            WeItem.LiefermengeSoll = decimal.Parse(dbDataReader["LIEFERMENGESOLL"].ToString());
                            WeItem.LiefermengeIst = decimal.Parse(dbDataReader["LIEFERMENGEIST"].ToString());
                            WeItem.ArtikelMengeneinheit = dbDataReader["ARTIKELMENGENEINHEIT"].ToString();
                            WeItem.Bestellnummer = int.Parse(dbDataReader["BESTELLNUMMER"].ToString());
                            WeItem.Bestelldatum = dbDataReader["BESTELLDATUM"].ToString();
                            WeItem.Lieferscheinnummer = int.Parse(dbDataReader["LIEFERSCHEINNUMMER"].ToString());
                            WeItem.Lieferscheindatum = dbDataReader["LIEFERSCHEINDATUM"].ToString();
                            WeItem.Wareneingangsdatum = dbDataReader["WARENEINGANGSDATUM"].ToString();
                            WeItem.Pruefstatus = dbDataReader["PRUEFSTATUS"].ToString();
                            WareneingangList.Add(WeItem);
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

            foreach(var weitem in WareneingangList)
            {
                var artikelitem = ArtikelList.FirstOrDefault(xF => xF.ID == weitem.IDArtikelNummer);
                if (artikelitem == null)
                {
                    return new JsonResult(new List<ArtikelPruefplanPostRequest>());
                }

                var pruefplanartikelitem = PruefplanArtikelList.FirstOrDefault(xF => xF.IDArtikel == weitem.IDArtikelNummer);
                if (pruefplanartikelitem == null)
                {
                    return new JsonResult(new List<ArtikelPruefplanPostRequest>());
                }

                var pruefplanitem = PruefplanList.FirstOrDefault(xF => xF.ID == pruefplanartikelitem.IDPruefplan);
                if (pruefplanitem == null)
                {
                    return new JsonResult(new List<ArtikelPruefplanPostRequest>());

                }

                var aqlitem = AQLList.FirstOrDefault(xF => xF.aql == pruefplanitem.AQL && xF.Pruefniveau == pruefplanitem.Pruefniveau && xF.Kennbuchstabe == pruefplanitem.Kennbuchstabe && weitem.LiefermengeIst >= xF.VonMenge && weitem.LiefermengeIst < xF.BisMenge);
                if (aqlitem == null)
                {
                    return new JsonResult(new List<AQL>());
                }

                var pruefplanpositem = PrufplanPosList.FirstOrDefault(xF => xF.IDPruefplan == pruefplanitem.ID);
                if (pruefplanpositem == null)
                {
                    return new JsonResult(new List<ArtikelPruefplanPostRequest>());
                }

                var responseWEITem = new WareneingangResponse();
                responseWEITem.IDArtikel = artikelitem.ID;
                responseWEITem.ArtikelNummer = artikelitem.Nummer;
                responseWEITem.ArtikelSuchbegriff = artikelitem.Suchbegriff;
                responseWEITem.ArtikelLagerplatz = artikelitem.Lagerplatz;
                responseWEITem.ArtikelMengeneinheit = artikelitem.Mengeneinheit;
                responseWEITem.IDPruefplan = pruefplanitem.ID;
                responseWEITem.PruefplanNummer = pruefplanitem.Nummer;
                responseWEITem.PruefplanSuchbegriff = pruefplanitem.Bezeichnung1;
                responseWEITem.PruefplanLieferantenNummer = pruefplanitem.LieferantenNummer;
                responseWEITem.PruefplanLieferantenSuchbegriff = pruefplanitem.LieferantenSuchbegriff;
                responseWEITem.PruefplanPruefBereich = pruefplanitem.PruefBereich;
                responseWEITem.PruefplanZeichungsNummer = pruefplanitem.ZeichungsNummer;
                responseWEITem.PruefplanZeichnungsIndex = pruefplanitem.ZeichnungsIndex;
                responseWEITem.PruefplanPruefart = pruefplanitem.Pruefart;
                responseWEITem.PruefplanPruefplatz = pruefplanitem.Pruefplatz;
                responseWEITem.IDWareneingang = weitem.ID;
                responseWEITem.WENummer = weitem.WENummer;
                responseWEITem.LiefermengeSoll = weitem.LiefermengeSoll;
                responseWEITem.LiefermengeIst = weitem.LiefermengeIst;
                responseWEITem.Bestellnummer = weitem.Bestellnummer;
                responseWEITem.Bestelldatum = weitem.Bestelldatum;
                responseWEITem.Lieferscheinnummer = weitem.Lieferscheinnummer;
                responseWEITem.Lieferscheindatum = weitem.Lieferscheindatum;
                responseWEITem.Wareneingangsdatum = weitem.Wareneingangsdatum;
                responseWEITem.Pruefstatus = weitem.Pruefstatus;
                responseWEITem.IDAQL = aqlitem.ID;
                responseWEITem.AQLStichprobenmenge = aqlitem.Stichprobenmenge;
                responseWEITem.AQLAnnahmefehlermenge = aqlitem.Annahmefehlermenge;
                responseWEITem.AQLRueckweisungsfehlermenge = aqlitem.Rueckweisungsfehlermenge;
                responseWareneingangList.Add(responseWEITem);
            }

           

            return new JsonResult(responseWareneingangList);

        }


       

        [HttpPost]

        public JsonResult Post(WareneingangPostRequest xpostreqeust)
        {
            var responseWEList = new List<Wareneingang>();
            var ArtikelList = LoadArtikel();
            var PruefplanList = LoadPruefplan();
            var PruefplanArtikelList = LoadPruefplanArtikel();
            var PrueflanposList = LoadPruefplanPositionen();

            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");

            var data = new WareneingangPostRequest
            {
                IDArtikel = int.Parse((xpostreqeust.IDArtikel).ToString()),
                WENummer = xpostreqeust.WENummer.ToString(),
                LiefermengeSoll = decimal.Parse((xpostreqeust.LiefermengeSoll).ToString()),
                LiefermengeIst = decimal.Parse((xpostreqeust.LiefermengeIst).ToString()),
                Bestellnummer = int.Parse((xpostreqeust.Bestellnummer).ToString()),
                Bestelldatum = xpostreqeust.Bestelldatum.ToString(),
                Lieferscheinnummer = int.Parse((xpostreqeust.Lieferscheinnummer).ToString()),
                Lieferscheindatum = xpostreqeust.Lieferscheindatum.ToString(),
                Wareneingangsdatum = xpostreqeust.Wareneingangsdatum.ToString(),
                Pruefstatus = xpostreqeust.Pruefstatus.ToString(),
            };

            var artikelitem = ArtikelList.FirstOrDefault(xF => xF.ID == data.IDArtikel);
            if (artikelitem == null)
            {
                return new JsonResult(new List<ArtikelPruefplanPostRequest>());
            }

            var pruefplanartikelitem = PruefplanArtikelList.FirstOrDefault(xF => xF.IDArtikel == data.IDArtikel);
            if (pruefplanartikelitem == null)
            {
                return new JsonResult(new List<ArtikelPruefplanPostRequest>());
            }

            var pruefplanitem = PruefplanList.FirstOrDefault(xF => xF.ID == pruefplanartikelitem.IDPruefplan);
            if (pruefplanitem == null)
            {
                return new JsonResult(new List<ArtikelPruefplanPostRequest>());

            }
           
            using (SqlConnection con = new SqlConnection(sqlDataSource))
            {

                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Insert into dbo.Wareneingang (WENUMMER, IDARTIKELNUMMER, ARTIKELNUMMER, ARTIKELBEZ1, PRÜFPLANZEICHUNGSNR, PRÜFPLANZEICHNUNGSINDEX, PRÜFPLANLIEFERANTENNUMMER, PRÜFPLANLIEFERANTENSUCHBEGRIFF, ARTIKELLAGERPLATZ, ARTIKELMENGENEINHEIT, LIEFERMENGESOLL, LIEFERMENGEIST, BESTELLNUMMER, BESTELLDATUM, LIEFERSCHEINNUMMER, LIEFERSCHEINDATUM, WARENEINGANGSDATUM, PRUEFSTATUS) values" +
                        "(@WENUMMER, @IDARTIKELNUMMER, @ARTIKELNUMMER, @ARTIKELBEZ1, @PRÜFPLANZEICHUNGSNR, @PRÜFPLANZEICHNUNGSINDEX, @PRÜFPLANLIEFERANTENNUMMER, @PRÜFPLANLIEFERANTENSUCHBEGRIFF, @ARTIKELLAGERPLATZ, @ARTIKELMENGENEINHEIT, @LIEFERMENGESOLL, @LIEFERMENGEIST, @BESTELLNUMMER, @BESTELLDATUM, @LIEFERSCHEINNUMMER, @LIEFERSCHEINDATUM, @WARENEINGANGSDATUM, @PRUEFSTATUS)");

                    if (data.WENummer != null)
                        cmd.Parameters.AddWithValue("@WENUMMER", data.WENummer);
                    else
                        cmd.Parameters.AddWithValue("@WENUMMER", DBNull.Value);

                    if (data.IDArtikel != 0)
                        cmd.Parameters.AddWithValue("@IDARTIKELNUMMER", data.IDArtikel);
                    else
                        cmd.Parameters.AddWithValue("@IDARTIKELNUMMER", DBNull.Value);

                    if (artikelitem.Nummer != null)
                        cmd.Parameters.AddWithValue("@ARTIKELNUMMER", artikelitem.Nummer);
                    else
                        cmd.Parameters.AddWithValue("@ARTIKELNUMMER", DBNull.Value);

                    if (artikelitem.Bezeichnung1 != null)
                        cmd.Parameters.AddWithValue("@ARTIKELBEZ1", artikelitem.Bezeichnung1);
                    else
                        cmd.Parameters.AddWithValue("@ARTIKELBEZ1", DBNull.Value);

                    if (artikelitem.Lagerplatz != null)
                        cmd.Parameters.AddWithValue("@ARTIKELLAGERPLATZ", artikelitem.Lagerplatz);
                    else
                        cmd.Parameters.AddWithValue("@ARTIKELLAGERPLATZ", DBNull.Value);

                    if (artikelitem.Mengeneinheit != null)
                        cmd.Parameters.AddWithValue("@ARTIKELMENGENEINHEIT", artikelitem.Mengeneinheit);
                    else
                        cmd.Parameters.AddWithValue("@ARTIKELMENGENEINHEIT", DBNull.Value);

                    if (pruefplanitem.ZeichungsNummer != null)
                        cmd.Parameters.AddWithValue("@PRÜFPLANZEICHUNGSNR", pruefplanitem.ZeichungsNummer);
                    else
                        cmd.Parameters.AddWithValue("@PRÜFPLANZEICHUNGSNR", DBNull.Value);

                    if (pruefplanitem.ZeichnungsIndex != null)
                        cmd.Parameters.AddWithValue("@PRÜFPLANZEICHNUNGSINDEX", pruefplanitem.ZeichnungsIndex);
                    else
                        cmd.Parameters.AddWithValue("@PRÜFPLANZEICHNUNGSINDEX", DBNull.Value);

                    if (pruefplanitem.LieferantenNummer != 0)
                        cmd.Parameters.AddWithValue("@PRÜFPLANLIEFERANTENNUMMER", pruefplanitem.LieferantenNummer);
                    else
                        cmd.Parameters.AddWithValue("@PRÜFPLANLIEFERANTENNUMMER", DBNull.Value);

                    if (pruefplanitem.LieferantenSuchbegriff != null)
                        cmd.Parameters.AddWithValue("@PRÜFPLANLIEFERANTENSUCHBEGRIFF", pruefplanitem.LieferantenSuchbegriff);
                    else
                        cmd.Parameters.AddWithValue("@PRÜFPLANLIEFERANTENSUCHBEGRIFF", DBNull.Value);

                    if (data.LiefermengeSoll != 0)
                        cmd.Parameters.AddWithValue("@LIEFERMENGESOLL", data.LiefermengeSoll);
                    else
                        cmd.Parameters.AddWithValue("@LIEFERMENGESOLL", DBNull.Value);

                    if (data.LiefermengeIst != 0)
                        cmd.Parameters.AddWithValue("@LIEFERMENGEIST", data.LiefermengeIst);
                    else
                        cmd.Parameters.AddWithValue("@LIEFERMENGEIST", DBNull.Value);

                    if (data.Bestellnummer != 0)
                        cmd.Parameters.AddWithValue("@BESTELLNUMMER", data.Bestellnummer);
                    else
                        cmd.Parameters.AddWithValue("@BESTELLNUMMER", DBNull.Value);

                    if (data.Bestelldatum != null)
                        cmd.Parameters.AddWithValue("@BESTELLDATUM", data.Bestelldatum);
                    else
                        cmd.Parameters.AddWithValue("@BESTELLDATUM", DBNull.Value);

                    if (data.Lieferscheinnummer != 0)
                        cmd.Parameters.AddWithValue("@LIEFERSCHEINNUMMER", data.Lieferscheinnummer);
                    else
                        cmd.Parameters.AddWithValue("@LIEFERSCHEINNUMMER", DBNull.Value);

                    if (data.Lieferscheindatum != null)
                        cmd.Parameters.AddWithValue("@LIEFERSCHEINDATUM", data.Lieferscheindatum);
                    else
                        cmd.Parameters.AddWithValue("@LIEFERSCHEINDATUM", DBNull.Value);

                    if (data.Wareneingangsdatum != null)
                        cmd.Parameters.AddWithValue("@WARENEINGANGSDATUM", data.Wareneingangsdatum);
                    else
                        cmd.Parameters.AddWithValue("@WARENEINGANGSDATUM", DBNull.Value);

                    if (data.Pruefstatus != null)
                        cmd.Parameters.AddWithValue("@PRUEFSTATUS", data.Pruefstatus);
                    else
                        cmd.Parameters.AddWithValue("@PRUEFSTATUS", DBNull.Value);

                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

            }


            using (SqlConnection con = new SqlConnection(sqlDataSource))
            {
                var WEIDList = LoadWEID();
                var wepositem = new Wareneingangspruefpositionen();
                var WEID = WEIDList.First();

                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Insert into dbo.Wareneingangspruefpositionen (IDWARENEINGANG, IDPRUEFPLAN, IDPRUEFPLANPOSITION, PRUEFMERKMAL, MERKMALSART, POSITIONSNUMMER, KUERZEL, BEZEICHNUNG1, BEZEICHNUNG2, BEZEICHNUNG3, BEZEICHNUNGT, NENNMAß, MAßEINHEIT, OBERETOLERANZ, UNTERETOLERANZ, DATUMEDIT, DATUMNEU, MESSMITTEL) values" +
                            "(@IDWENUMMER, @IDPRUEFPLAN, @IDPRUEFPLANPOSITION, @PRUEFMERKMAL, @MERKMALSART, @POSITIONSNUMMER, @KUERZEL, @BEZEICHNUNG1, @BEZEICHNUNG2, @BEZEICHNUNG3, @BEZEICHNUNGT, @NENNMAß, @MAßEINHEIT, @OBERETOLERANZ, @UNTERETOLERANZ, @DATUMEDIT, @DATUMNEU, @MESSMITTEL)");

                    foreach (var positem in PrueflanposList.FindAll(xF => xF.IDPruefplan == pruefplanitem.ID))
                    {
                        wepositem.IDWareneingang = WEID.ID;
                        if (wepositem.IDWareneingang != 0)
                            cmd.Parameters.AddWithValue("@IDWENUMMER", wepositem.IDWareneingang);
                        else
                            cmd.Parameters.AddWithValue("@IDWENUMMER", DBNull.Value);

                        wepositem.IDPruefplan = pruefplanitem.ID;
                        if(wepositem.IDPruefplan != 0)
                            cmd.Parameters.AddWithValue("@IDPRUEFPLAN", wepositem.IDPruefplan);
                        else
                            cmd.Parameters.AddWithValue("@IDPRUEFPLAN", DBNull.Value);

                        wepositem.IDPruefplanpos = positem.ID;
                        if (wepositem.IDPruefplanpos != 0)
                            cmd.Parameters.AddWithValue("@IDPRUEFPLANPOSITION", wepositem.IDPruefplanpos);
                        else
                            cmd.Parameters.AddWithValue("@IDPRUEFPLANPOSITION", DBNull.Value);

                        wepositem.Pruefmerkal = positem.Pruefmerkal;
                        if (wepositem.Pruefmerkal != null)
                            cmd.Parameters.AddWithValue("@PRUEFMERKMAL", wepositem.Pruefmerkal);
                        else
                            cmd.Parameters.AddWithValue("@PRUEFMERKMAL", DBNull.Value);

                        wepositem.Merkmalsart = positem.Merkmalsart;
                        if (wepositem.Merkmalsart != null)
                            cmd.Parameters.AddWithValue("@MERKMALSART", wepositem.Merkmalsart);
                        else
                            cmd.Parameters.AddWithValue("@MERKMALSART", DBNull.Value);

                        wepositem.Positionsnummer = positem.Positionsnummer;
                        if (wepositem.Positionsnummer != 0)
                            cmd.Parameters.AddWithValue("@POSITIONSNUMMER", wepositem.Positionsnummer);
                        else
                            cmd.Parameters.AddWithValue("@POSITIONSNUMMER", DBNull.Value);

                        wepositem.Kurzel = positem.Kurzel;
                        if (wepositem.Kurzel != null)
                            cmd.Parameters.AddWithValue("@KUERZEL", wepositem.Kurzel);
                        else
                            cmd.Parameters.AddWithValue("@KUERZEL", DBNull.Value);

                        wepositem.Bezeichnung1 = positem.Bezeichnung1;
                        if (wepositem.Bezeichnung1 != null)
                            cmd.Parameters.AddWithValue("@BEZEICHNUNG1", wepositem.Bezeichnung1);
                        else
                            cmd.Parameters.AddWithValue("@BEZEICHNUNG1", DBNull.Value);

                        wepositem.Bezeichnung2 = positem.Bezeichnung2;
                        if (wepositem.Bezeichnung2 != null)
                            cmd.Parameters.AddWithValue("@BEZEICHNUNG2", wepositem.Bezeichnung2);
                        else
                            cmd.Parameters.AddWithValue("@BEZEICHNUNG2", DBNull.Value);

                        wepositem.Bezeichnung3 = positem.Bezeichnung3;
                        if (wepositem.Bezeichnung3 != null)
                            cmd.Parameters.AddWithValue("@BEZEICHNUNG3", wepositem.Bezeichnung3);
                        else
                            cmd.Parameters.AddWithValue("@BEZEICHNUNG3", DBNull.Value);

                        wepositem.BezeichnungT = positem.BezeichnungT;
                        if (wepositem.BezeichnungT != null)
                            cmd.Parameters.AddWithValue("@BEZEICHNUNGT", wepositem.BezeichnungT);
                        else
                            cmd.Parameters.AddWithValue("@BEZEICHNUNGT", DBNull.Value);

                        wepositem.Nennmaß = positem.Nennmaß;
                        if (wepositem.Nennmaß != 0)
                            cmd.Parameters.AddWithValue("@NENNMAß", wepositem.Nennmaß);
                        else
                            cmd.Parameters.AddWithValue("@NENNMAß", DBNull.Value);

                        wepositem.Maßeinheit = positem.Maßeinheit;
                        if (wepositem.Maßeinheit != null)
                            cmd.Parameters.AddWithValue("@MAßEINHEIT", wepositem.Maßeinheit);
                        else
                            cmd.Parameters.AddWithValue("@MAßEINHEIT", DBNull.Value);

                        wepositem.Oberetoleranz = positem.Oberetoleranz;
                        if (wepositem.Oberetoleranz != 0)
                            cmd.Parameters.AddWithValue("@OBERETOLERANZ", wepositem.Oberetoleranz);
                        else
                            cmd.Parameters.AddWithValue("@OBERETOLERANZ", DBNull.Value);

                        wepositem.Unteretoleranz = positem.Unteretoleranz;
                        if (wepositem.Unteretoleranz != 0)
                            cmd.Parameters.AddWithValue("@UNTERETOLERANZ", wepositem.Unteretoleranz);
                        else
                            cmd.Parameters.AddWithValue("@UNTERETOLERANZ", DBNull.Value);

                        wepositem.DatumEdit = System.DateTime.Now.ToShortDateString();
                        if (wepositem.DatumEdit != null)
                            cmd.Parameters.AddWithValue("@DATUMEDIT", wepositem.DatumEdit);
                        else
                            cmd.Parameters.AddWithValue("@DATUMEDIT", DBNull.Value);

                        wepositem.DatumNeu = System.DateTime.Now.ToShortDateString();
                        if (wepositem.DatumNeu != null)
                            cmd.Parameters.AddWithValue("@DATUMNEU", wepositem.DatumNeu);
                        else
                            cmd.Parameters.AddWithValue("@DATUMNEU", DBNull.Value);

                        wepositem.Messmittel = positem.Messmittel;
                        if (wepositem.Messmittel != null)
                            cmd.Parameters.AddWithValue("@MESSMITTEL", wepositem.Messmittel);
                        else
                            cmd.Parameters.AddWithValue("@MESSMITTEL", DBNull.Value);

                        cmd.Connection = con;
                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();

                    }
                    con.Close();
                }

            }

            return new JsonResult("Anlage erfolgreich");


        }

    }            
    
}
