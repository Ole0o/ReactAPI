using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactAPI.Models
{
    public class Artikel
    {
        public int ID { get; set; }

        public string Nummer { get; set; }
        public string Suchbegriff { get; set; }

        public string Bezeichnung1 { get; set; }

        public string Bezeichnung2 { get; set; }

        public string Bezeichnung3 { get; set; }

        public string BezeichnungT { get; set; }

        public string Lagerplatz { get; set; }

        public string Mengeneinheit { get; set; }

        public string Revisionsstand { get; set; }

        public string Revisionsdatum { get; set; }

        public string DatumNeu { get; set; }

        public string DatumEdit { get; set; }

    }
}
