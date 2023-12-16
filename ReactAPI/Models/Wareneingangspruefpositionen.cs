using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactAPI.Models
{
    public class Wareneingangspruefpositionen
    {

        public int ID { get; set; }

        public int IDWareneingang { get; set; }

        public int IDPruefplan { get; set; }

        public int IDPruefplanpos { get; set; }

        public string Pruefmerkal { get; set; }

        public string Merkmalsart { get; set; }

        public int Positionsnummer { get; set; }

        public string Kurzel { get; set; }

        public string Bezeichnung1 { get; set; }

        public string Bezeichnung2 { get; set; }

        public string Bezeichnung3 { get; set; }

        public string BezeichnungT { get; set; }

        public decimal Nennmaß { get; set; }

        public string Maßeinheit { get; set; }

        public decimal Oberetoleranz { get; set; }

        public decimal Unteretoleranz { get; set; }

        public string DatumNeu { get; set; }

        public string DatumEdit { get; set; }

        public string Messmittel { get; set; }
    }
}
