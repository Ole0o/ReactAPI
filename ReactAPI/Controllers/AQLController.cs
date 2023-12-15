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
    public class AQLController : ControllerBase
    {

        


        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;

        public AQLController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }



        private List<AQL> LoadAQL()
        {

            var  AQLList = new List<AQL>();
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

        [HttpPost]

        public JsonResult Post(AQLPostRequest xpostreqeust)
        {
            var rAQLList = new List<AQL>();
            var AQLList = LoadAQL();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");


            var data = new AQLPostRequest
            {
                Lieferlos = int.Parse((xpostreqeust.Lieferlos).ToString()),
                Pruefniveau = int.Parse((xpostreqeust.Pruefniveau).ToString()),
                Kennbuchstabe = xpostreqeust.Kennbuchstabe,
                AQL = decimal.Parse((xpostreqeust.AQL).ToString())
            };

            var aql = AQLList.FirstOrDefault(xF => xF.aql == data.AQL && xF.Pruefniveau == data.Pruefniveau && xF.Kennbuchstabe == data.Kennbuchstabe && data.Lieferlos >= xF.VonMenge && data.Lieferlos <= xF.BisMenge);
            if (aql == null)
            {
                return new JsonResult(new List<AQL>());
            }

            rAQLList.Add(aql);

         
            return new JsonResult(rAQLList);

        }

       


    }


}
