using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactAPI.Models
{
    public class AQL    
    {
        public int ID { get; set; }

        public int VonMenge { get; set; }
        public int BisMenge { get; set; }

        public int Pruefniveau { get; set; }

        public string Kennbuchstabe { get; set; }

        public decimal aql { get; set; }

        public int Stichprobenmenge { get; set; }

        public int Annahmefehlermenge { get; set; }

        public int Rueckweisungsfehlermenge { get; set; }

    }
}
