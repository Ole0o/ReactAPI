using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactAPI.Models
{
    public class AQLPostRequest
    {

        public int Lieferlos { get; set; }
        public int Pruefniveau { get; set; }
        public string Kennbuchstabe { get; set; }
        public decimal AQL { get; set; }
    }
}
