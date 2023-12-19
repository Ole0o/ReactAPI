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
    public class PruefplanPosController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        

        public PruefplanPosController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]

        public JsonResult Get()
        {

            List<PruefplanPos> PruefplanPosList = new List<PruefplanPos>();
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

            return new JsonResult(PruefplanPosList);

        }

        public JsonResult Post(PruefplanPos xpruefplanpos)
        {

            List<PruefplanPos> PruefplanList = new List<PruefplanPos>();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");


            try
            {
                using (SqlConnection con = new SqlConnection(sqlDataSource))
                {

                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("Insert into dbo.Pruefplanpositionen" +
                            "(IDPRUEFPLAN, PRUEFMERKMAL, MERKMALSART, POSITIONSNUMMER, KUERZEL, BEZEICHNUNG1,BEZEICHNUNG2, BEZEICHNUNG3, BEZEICHNUNGT, NENNMAß, MAßEINHEIT, OBERETOLERANZ, UNTERETOLERANZ, DATUMEDIT, DATUMNEU, MESSMITTEL) values" +
                            "(@IDPRUEFPLAN, @PRUEFMERKMAL, @MERKMALSART, @POSITIONSNUMMER, @KUERZEL, @BEZEICHNUNG1, @BEZEICHNUNG2, @BEZEICHNUNG3, @BEZEICHNUNGT, @NENNMAß, @MAßEINHEIT, @OBERETOLERANZ, @UNTERETOLERANZ, @DATUMEDIT, @DATUMNEU, @MESSMITTEL)");

                        if (xpruefplanpos.IDPruefplan != 0)
                            cmd.Parameters.AddWithValue("@IDPRUEFPLAN", xpruefplanpos.IDPruefplan);
                        else
                            cmd.Parameters.AddWithValue("@IDPRUEFPLAN", DBNull.Value);

                        if (xpruefplanpos.Pruefmerkal != null)

                            cmd.Parameters.AddWithValue("@PRUEFMERKMAL", xpruefplanpos.Pruefmerkal);
                        else
                            cmd.Parameters.AddWithValue("@PRUEFMERKMAL", DBNull.Value);

                        if (xpruefplanpos.Merkmalsart != null)

                            cmd.Parameters.AddWithValue("@MERKMALSART", xpruefplanpos.Merkmalsart);
                        else
                            cmd.Parameters.AddWithValue("@MERKMALSART", DBNull.Value);

                        if(xpruefplanpos.Positionsnummer != 0)

                            cmd.Parameters.AddWithValue("@POSITIONSNUMMER", xpruefplanpos.Positionsnummer);
                        else
                            cmd.Parameters.AddWithValue("@POSITIONSNUMMER", DBNull.Value);

                        if (xpruefplanpos.Kurzel != null)

                            cmd.Parameters.AddWithValue("@KUERZEL", xpruefplanpos.Kurzel);
                        else
                            cmd.Parameters.AddWithValue("@KUERZEL", DBNull.Value);

                        if (xpruefplanpos.Bezeichnung1 != null)

                            cmd.Parameters.AddWithValue("@BEZEICHNUNG1", xpruefplanpos.Bezeichnung1);
                        else
                            cmd.Parameters.AddWithValue("@BEZEICHNUNG1", DBNull.Value);

                        if (xpruefplanpos.Bezeichnung2 != null)

                            cmd.Parameters.AddWithValue("@Bezeichnung2", xpruefplanpos.Bezeichnung2);
                        else
                            cmd.Parameters.AddWithValue("@Bezeichnung2", DBNull.Value);

                        if (xpruefplanpos.Bezeichnung3 != null)

                            cmd.Parameters.AddWithValue("@BEZEICHNUNG3", xpruefplanpos.Bezeichnung3);
                        else
                            cmd.Parameters.AddWithValue("@BEZEICHNUNG3", DBNull.Value);

                        if (xpruefplanpos.BezeichnungT != null)

                            cmd.Parameters.AddWithValue("@BEZEICHNUNGT", xpruefplanpos.BezeichnungT);
                        else
                            cmd.Parameters.AddWithValue("@BEZEICHNUNGT", DBNull.Value);

                        if (xpruefplanpos.Nennmaß != 0)

                            cmd.Parameters.AddWithValue("@NENNMAß", xpruefplanpos.Nennmaß);
                        else
                            cmd.Parameters.AddWithValue("@NENNMAß", DBNull.Value);

                        if (xpruefplanpos.Maßeinheit != null)

                            cmd.Parameters.AddWithValue("@MAßEINHEIT", xpruefplanpos.Maßeinheit);
                        else
                            cmd.Parameters.AddWithValue("@MAßEINHEIT", DBNull.Value);

                        if (xpruefplanpos.Oberetoleranz != 0)

                            cmd.Parameters.AddWithValue("@OBERETOLERANZ", xpruefplanpos.Oberetoleranz);
                        else
                            cmd.Parameters.AddWithValue("@OBERETOLERANZ", DBNull.Value);

                        if (xpruefplanpos.Unteretoleranz != 0)

                            cmd.Parameters.AddWithValue("@UNTERETOLERANZ", xpruefplanpos.Unteretoleranz);
                        else
                            cmd.Parameters.AddWithValue("@UNTERETOLERANZ", DBNull.Value);

                        if (xpruefplanpos.DatumNeu != null)

                            cmd.Parameters.AddWithValue("@DATUMEDIT", xpruefplanpos.DatumNeu);
                        else
                            cmd.Parameters.AddWithValue("@DATUMEDIT", DBNull.Value);

                        if (xpruefplanpos.DatumEdit != null)

                            cmd.Parameters.AddWithValue("@DATUMNEU", xpruefplanpos.DatumEdit);
                        else
                            cmd.Parameters.AddWithValue("@DATUMNEU", DBNull.Value);

                        if (xpruefplanpos.Messmittel != null)

                            cmd.Parameters.AddWithValue("@MESSMITTEL", xpruefplanpos.Messmittel);
                        else
                            cmd.Parameters.AddWithValue("@MESSMITTEL", DBNull.Value);

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

        [HttpDelete("{id}")]

        public JsonResult Delete(int id)
        {

            List<PruefplanPos> PruefplanList = new List<PruefplanPos>();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");


            try
            {
                using (SqlConnection con = new SqlConnection(sqlDataSource))
                {

                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("Delete from dbo.Pruefplanpositionen  where ID=@ID");
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
