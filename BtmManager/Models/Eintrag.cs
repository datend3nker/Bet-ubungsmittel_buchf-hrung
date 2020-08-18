using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BtmManager.Models
{
    public class Eintrag
    {
        [Key]
        public int EintragId { set; get; }
        [Required]
        public byte Einheit { get; set; }
        [Required]
        public string Bezeichnung { get; set; }
        [Required]
        public bool IsFirst { get; set; }
        public string LfdNr { get; set; }
        public DateTime Datum { get; set; }
        public float Anfangsbestand { get; set; }
        public float TheroZugang { get; set; }
        public float PrakZugang { get; set; }
        public float Arbeitsverlust { get; set; }
        public float Abgang { get; set; }
        public float AktuellerBestand { get; set; }
        public string Bemerkung { get; set; }
        public bool WurdeMarkiert { get; set; }
        public string Farbe { get; set; }

        public int StufId { get; set; }
        public Stufe Stufen { get; set; }
    }
}
