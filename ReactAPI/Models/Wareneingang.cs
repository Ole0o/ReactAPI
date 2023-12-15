using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactAPI.Models
{
    public class Wareneingang
    {

        public int ID { get; set; }
        public string WENummer   { get; set; }

        public int IDArtikelNummer { get; set; }

        public string ArtikelNummer { get; set; }

        public string ArtikelBez1 { get; set; }

        public string PrüfplanZeichnungNummer { get; set; }

        public string PrüfplanZeichnungIndex { get; set; }

        public int PrüfplanZeichnungLieferantenNummer { get; set; }

        public string PrüfplanZeichnungLieferantenSuchbegriff { get; set; }

        public string ArtikelLagerplatz { get; set; }

        public decimal LiefermengeSoll { get; set; }

        public decimal LiefermengeIst { get; set; }

        public string ArtikelMengeneinheit { get; set; }

        public int Bestellnummer { get; set; }

        public string Bestelldatum { get; set; }

        public int Lieferscheinnummer { get; set; }

        public string Lieferscheindatum { get; set; }

        public string Wareneingangsdatum { get; set; }

        public string Pruefstatus { get; set; }

    }
}
