using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactAPI.Models
{
    public class WareneingangResponse
    {
        public int IDArtikel { get; set; }
        public string ArtikelNummer { get; set; }
        public string ArtikelSuchbegriff { get; set; }

        public string ArtikelLagerplatz { get; set; }

        public string ArtikelMengeneinheit { get; set; }

        public int IDPruefplan { get; set; }

        public string PruefplanNummer { get; set; }
        public string PruefplanSuchbegriff { get; set; }

        public int PruefplanLieferantenNummer { get; set; }

        public string PruefplanLieferantenSuchbegriff { get; set; }

        public string PruefplanPruefBereich { get; set; }

        public string PruefplanZeichungsNummer { get; set; }

        public string PruefplanZeichnungsIndex { get; set; }

        public string PruefplanPruefart { get; set; }

        public string PruefplanPruefplatz { get; set; }

        public int IDWareneingang { get; set; }

        public string WENummer { get; set; }

        public decimal LiefermengeSoll { get; set; }

        public decimal LiefermengeIst { get; set; }

        public int Bestellnummer { get; set; }

        public string Bestelldatum { get; set; }

        public int Lieferscheinnummer { get; set; }

        public string Lieferscheindatum { get; set; }

        public string Wareneingangsdatum { get; set; }

        public string Pruefstatus { get; set; }

        public int IDAQL { get; set; }
        public int AQLStichprobenmenge { get; set; }

        public int AQLAnnahmefehlermenge { get; set; }

        public int AQLRueckweisungsfehlermenge { get; set; }
    }
}
