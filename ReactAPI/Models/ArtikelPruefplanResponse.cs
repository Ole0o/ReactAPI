using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactAPI.Models
{
    public class ArtikelPruefplanResponse
    {

        public int ID { get; set; }
        public int IDArtikel { get; set; }
        public string ArtikelNummer { get; set; }
        public string ArtikelSuchbegriff { get; set; }
        public int IDPruefplan { get; set; }
        public string PruefplanNummer { get; set; }
        public string PruefplanSuchbegriff { get; set; }

        public string DatumNeu { get; set; }

        public string DatumEdit { get; set; }

    }
}
