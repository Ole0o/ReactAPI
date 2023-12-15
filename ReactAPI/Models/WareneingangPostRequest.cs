using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactAPI.Models
{
    public class WareneingangPostRequest
    {
        public int IDArtikel { get; set; }

        public string WENummer { get; set; }        

        public decimal LiefermengeSoll { get; set; }

        public decimal LiefermengeIst { get; set; }

        public int Bestellnummer { get; set; }

        public string Bestelldatum { get; set; }

        public int Lieferscheinnummer { get; set; }

        public string Lieferscheindatum { get; set; }

        public string Wareneingangsdatum { get; set; }

        public string Pruefstatus { get; set; }
    }
}
