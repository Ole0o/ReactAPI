using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactAPI.Models
{
    public class AQLResponse
    {
        public int ID { get; set; }
        public int Stichprobenmenge { get; set; }

        public int Annahmefehlermenge { get; set; }

        public int Rueckweisungsfehlermenge { get; set; }

    }
}
