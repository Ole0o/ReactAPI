using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data.Common;
using ReactAPI.Models;


namespace ReactAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WareneingangspruefpositionenController : ControllerBase
    {

        private readonly IConfiguration _configuration;
      

        public WareneingangspruefpositionenController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]

        public JsonResult Get()
        {

            List<Wareneingangspruefpositionen> WareneingangsposList = new List<Wareneingangspruefpositionen>();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");


            try
            {
                using (SqlConnection con = new SqlConnection(sqlDataSource))
                {

                    {
                        con.Open();
                        DbCommand cmd = con.CreateCommand();
                        cmd.CommandText = "Select ID, IDWARENEINGANG, IDPRUEFPLAN, IDPRUEFPLANPOSITION, PRUEFMERKMAL, MERKMALSART, POSITIONSNUMMER, KUERZEL , BEZEICHNUNG1, BEZEICHNUNG2, BEZEICHNUNG3, BEZEICHNUNGT, " +
                            " NENNMAß, MAßEINHEIT, OBERETOLERANZ, UNTERETOLERANZ, CONVERT(VARCHAR(30),DATUMEDIT,121) DATUMEDIT,CONVERT(VARCHAR(30),DATUMNEU,121) DATUMNEU, MESSMITTEL from dbo.Wareneingangspruefpositionen";
                        DbDataReader dbDataReader = cmd.ExecuteReader();
                        while (dbDataReader.Read())
                        {
                            var Wareneingangsposition = new Wareneingangspruefpositionen();
                            Wareneingangsposition.ID = int.Parse(dbDataReader["ID"].ToString());
                            Wareneingangsposition.IDWareneingang = int.Parse(dbDataReader["IDWARENEINGANG"].ToString());
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

            return new JsonResult(WareneingangsposList);

        }


    }
}
