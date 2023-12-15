using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactAPI.Models
{
    public class Pruefplan  
    {
        public int ID { get; set;  }

        public string Nummer { get; set; }
        public string Bezeichnung1 { get; set; }

        public int LieferantenNummer { get; set; }

        public string LieferantenSuchbegriff { get; set; }

        public string PruefBereich { get; set; }

        public string ZeichungsNummer { get; set; }
               
        public string ZeichnungsIndex { get; set; }

        public string DatumVon { get; set; }

        public string DatumBis { get; set; }

        public string Pruefart { get; set; }

        public string Pruefplatz { get; set; }

        public string Kennbuchstabe { get; set; }

        public int Pruefniveau { get; set; }

        public decimal AQL { get; set; }

        public string PhtotFileName { get; set; }

        public string Version { get; set; }

        public string Statuspruefplan { get; set; }

        public string Erstellungsbemerkung { get; set; }

        public string PersonErstellung { get; set; }

        public string DatumErstellung { get; set; }

        public string Bearbeitungsbemerkung { get; set; }

        public string PersonBearbeitung { get; set; }

        public string DatumBearbeitung { get; set; }

        public string Pruefungsbemerkung { get; set; }

        public string PersonPruefung { get; set; }

        public string DatumPruefung { get; set; }

        public string Freigabebemerkung { get; set; }

        public string PersonFreigabe { get; set; }

        public string DatumFreigabe { get; set; }

    }
}
