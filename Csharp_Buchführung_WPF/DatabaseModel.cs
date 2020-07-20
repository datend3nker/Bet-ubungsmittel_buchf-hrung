using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Csharp_Buchführung_WPF
{
    public class Projekt
    {
        [Key]
        public int ProjektId { get; set; }
        public int BtmBestandsbuchNr { get; set; }
        public int Stufenanzahl { get; set; }
        public string Produktbezeichnung { get; set; }
        public int ProduktNr { get; set; }
        public DateTime Zeitraum { get; set; }
        public List<Stufe> Stufe { get; } = new List<Stufe>();
    }

    public class Stufe
    {
        [Key]
        public int StufenId { get; set; }
        public int BtmBestandsbuchNr { get; set; }
        public int StufenNummer { get; set; }
        public string MaterialName { get; set; }
        public int Anzahleinträge { get; set; }
        public List<Eintrag> Einträge { get; } = new List<Eintrag>();
    }

    public class Eintrag
    {
        [Key]
        public int EintragId { get; set; }
        public int BtmBestandsbuchNr { get; set; }
        public int StufenNummer { get; set; }
        public char Einheit { get; set; }
        public string LfdNr { get; set; }
        public DateTime Datum { get; set; }
        public float Anfangsbestand { get; set; }
        public float TheroZugang { get; set; }
        public float PrakZugang { get; set; }
        public float Arbeitsverlust { get; set; }
        public float Abgang { get; set; }
        public float AktuellerBestand { get; set; }
        public string Bemerkung { get; set; }
    }
}
