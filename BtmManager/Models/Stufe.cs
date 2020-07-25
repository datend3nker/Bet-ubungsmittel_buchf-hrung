using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BtmManager.Models
{
    public class Stufe
    { 
        [Key]
        public int StufId { set; get; }
        [Required]
        public int StufenNummer { get; set; }
        public string MaterialName { get; set; }
        [Required]
        public int AnzahlEinträge { get; set; }

        public int ProjektId { get; set; }
        public Projekt Projekte { get; set; }

        public IList<Eintrag> Einträge { get; set; }
    }
}
